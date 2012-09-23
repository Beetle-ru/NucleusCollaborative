using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using ConnectionProvider.MainGate;
using CommonTypes;
using System.ServiceModel;
using System.Threading;
using Implements;

namespace ConnectionProvider
{
    [CallbackBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        UseSynchronizationContext = false)]
    internal class PrimaryListener : IMainGateCallback
    {
        private IEventListener m_EventListener = null;
        public Queue<BaseEvent> EventQueue = new Queue<BaseEvent>();
        public Thread EventThread = new Thread(EventProc);

        public PrimaryListener(IEventListener eventListener)
        {
            m_EventListener = eventListener;
            if (m_EventListener != null)
            {
                //EventThread.SetApartmentState(ApartmentState.MTA);
                EventThread.Priority = ThreadPriority.BelowNormal;
                EventThread.IsBackground = true;
                EventThread.Start(this);
            }
        }

        #region IEventCallback Members

        public void OnEvent(BaseEvent newEvent)
        {
            lock (EventQueue)
            {
                EventQueue.Enqueue(newEvent);
            }
        }

        public static void EventProc(object o)
        {
            var _this = (PrimaryListener) o;
            BaseEvent baseEvent;
            while (true)
            {
                lock (_this.EventQueue)
                {
                    baseEvent = _this.EventQueue.Count > 0 ? _this.EventQueue.Dequeue() : null;
                }
                if (baseEvent != null)
                {
                    _this.CustomOnEvent(baseEvent);
                }
                else
                {
                    Thread.Sleep(Client.THREAD_SLEEP);
                }
            }
        }

        public void CustomOnEvent(BaseEvent newEvent)
        {
            using (var l = new Logger("ConnectionProvider.PrimaryListener.EventProc"))
            {
                try
                {
                    m_EventListener.OnEvent(newEvent);
                }
                catch (Exception e)
                {
                    l.err("Event {0} exception {1}::{2} returned:\n{3} ", newEvent.ToString(), e.Source,
                          e.TargetSite, e.Message);
                }
            }
        }

        #endregion
    }
}