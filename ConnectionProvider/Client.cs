using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider.MainGate;
using System.ServiceModel;
using Core;
using System.Threading;
using CommonTypes;
using Implements;

namespace ConnectionProvider
{
    public class Client : IMainGate
    {
        public const int THREAD_SLEEP = 50;
        private MainGateClient m_MainGateClient;
        private string m_ConnectionName = null;
        private Queue<BaseEvent> eventsQueue = new Queue<BaseEvent>();
        private bool m_SendingThreadWork = false;
        private IMainGateCallback m_CallBack;
        private SynchronizationContext m_SyncContext = null;
        private IEventListener m_EventListener = null;
        // для залочивания создания потока отсылки сообщений
        private object m_PushEventThreadLocker = new object();
        private bool m_IsSubscribed = false;

        private volatile bool m_Initialized = false;


        private void InnerDuplexChannel_Faulted(object obj1, object obj2)
        {
            //Console.WriteLine("Connection faulted");
            //Thread.Sleep(500);
            m_MainGateClient = m_ConnectionName == null
                                   ? new MainGateClient(new InstanceContext(new PrimaryListener(m_EventListener)))
                                   : new MainGateClient(new InstanceContext(new PrimaryListener(m_EventListener)),
                                                        m_ConnectionName);
            m_MainGateClient.InnerDuplexChannel.Faulted += new EventHandler(InnerDuplexChannel_Faulted);
            m_MainGateClient.InnerDuplexChannel.Closed += new EventHandler(InnerDuplexChannel_Faulted);
            if (m_IsSubscribed &&
                (m_MainGateClient.State == CommunicationState.Opened ||
                 m_MainGateClient.State == CommunicationState.Created))
            {
                m_MainGateClient.Subscribe();
            }
        }

        public Client()
        {
            m_Start();
        }

        public Client(string ConnectionName)
        {
            m_ConnectionName = ConnectionName;
            m_Start();
        }

        public Client(IEventListener listener)
        {
            m_EventListener = listener;
            m_Start();
        }

        public Client(string ConnectionName, IEventListener listener)
        {
            m_ConnectionName = ConnectionName;
            m_EventListener = listener;
            m_Start();
        }

        private void m_Start()
        {
            m_SyncContext = SynchronizationContext.Current;

            Thread thread = new Thread(new System.Threading.ThreadStart(ReceivingThread));
            thread.IsBackground = true;
            thread.Start();

            while (!m_Initialized)
            {
                Thread.Sleep(100);
            }
        }

        private void ReceivingThread()
        {
            m_MainGateClient = m_ConnectionName == null
                                   ? new MainGateClient(new InstanceContext(new PrimaryListener(m_EventListener)))
                                   : new MainGateClient(new InstanceContext(new PrimaryListener(m_EventListener)),
                                                        m_ConnectionName);
            m_MainGateClient.InnerDuplexChannel.Faulted += new EventHandler(InnerDuplexChannel_Faulted);
            m_MainGateClient.InnerDuplexChannel.Closed += new EventHandler(InnerDuplexChannel_Faulted);
            if (m_IsSubscribed &&
                (m_MainGateClient.State == CommunicationState.Opened ||
                 m_MainGateClient.State == CommunicationState.Created))
            {
                m_MainGateClient.Subscribe();
            }

            m_Initialized = true;
            do
            {
                Thread.Sleep(100);
            } while (true);
        }

        private void SendingThread()
        {
            using (var l = new Logger("ConnectionProvider.SendingThread"))
            {
                BaseEvent evt = null;
                var prevState = m_MainGateClient.State;
                var currState = prevState;
                while (true)
                {
                    bool doSleep = true;
                    if (prevState != currState)
                    {
                        l.msg("Connection state is \"{0}\"", currState);
                        prevState = currState;
                        doSleep = false;
                    }

                    if (currState <= CommunicationState.Opened)
                    {
                        try
                        {
                            lock (eventsQueue)
                            {
                                evt = eventsQueue.Count > 0 ? eventsQueue.Dequeue() : null;
                            }
                            if (evt != null)
                            {
                                m_MainGateClient.PushEvent(evt);
                                doSleep = false;
                            }
                        }
                        catch (Exception e)
                        {
                            l.err("proc {2} event fail\n{0}\n while processing event\n{1}", e.Message, evt, e.TargetSite.Name);
                        }
                    }
                    if (doSleep) Thread.Sleep(THREAD_SLEEP);
                    currState = m_MainGateClient.State;
                }
            }
        }

        #region IMainGate Members

        public
            bool Autentificate(string login, string password)
        {
            return
                m_MainGateClient.Autentificate(login, password);
        }

        public
            void PushEvent(BaseEvent baseEvent)
        {
            lock (eventsQueue)
            {
                eventsQueue.Enqueue(baseEvent);
                if (!m_SendingThreadWork)
                {
                    lock (m_PushEventThreadLocker)
                    {
                        Thread thread = new Thread(SendingThread);
                        thread.IsBackground = true;
                        thread.Start();
                        m_SendingThreadWork = true;
                    }
                }
            }
        }

        public bool Subscribe()
        {
            lock (m_PushEventThreadLocker)
            {
                m_IsSubscribed = true;
                return m_MainGateClient.Subscribe();
            }
        }

        public bool Unsubscribe()
        {
            m_IsSubscribed = false;
            return m_MainGateClient.Unsubscribe();
        }

        #endregion
    }
}