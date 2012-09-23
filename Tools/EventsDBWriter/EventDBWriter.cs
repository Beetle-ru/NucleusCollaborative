using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Core;
using System.ServiceModel;
using System.Globalization;
using System.Threading;
using CommonTypes;
using ConnectionProvider.MainGate;

namespace Tools.DB
{
    class EventDBWriter
    {
        private Type[] m_EventTypes;
        private int m_UnitNumber;
        private EventsListener m_Events;
        private ConnectionProvider.Client m_MainGate;
        private bool m_WorkingFlag;
        private string _channelName;

        private void ProccesAssync()
        {
            while (m_WorkingFlag)
            {
                while (m_Events.Queue.Count > 0)
                {
                    BaseEvent _event;
                    lock (m_Events.Queue)
                    {
                        _event = m_Events.Queue.Peek();
                    }

                    //проверяем событие на необходимость записи в базу.
                    Type eventType = _event.GetType();
                    var data = eventType.GetCustomAttributes(false).FirstOrDefault(x => x.GetType().Name == "DBGroup");
                    if (data == null)
                    {
                        lock (m_Events.Queue)
                        {
                            m_Events.Queue.Dequeue();
                        }
                        Console.WriteLine("{0}:  Don't need to insert {1} Queue.Lenght={2}", DateTime.Now, _event.GetType().Name, m_Events.Queue.Count);
                        continue;
                    }

                    if (DBWorker.Instance.Insert(_event, m_UnitNumber))
                    {
                        lock (m_Events.Queue)
                        {
                            m_Events.Queue.Dequeue();
                        }
                        Console.WriteLine("{0}: {1} inserted Queue.Lenght={2}",DateTime.Now, _event.GetType().Name, m_Events.Queue.Count);
                    }
                    else
                    {
                        Console.WriteLine("{0}: Can't insert {1} Queue.Lenght={2}", DateTime.Now, _event.GetType().Name, m_Events.Queue.Count);
                        System.Threading.Thread.Sleep(3000);
                    }
                }
                System.Threading.Thread.Sleep(300);
            }
        }

        public void Start()
        {
            if (m_Events == null)
            {
                m_Events = new EventsListener();
            }

            if (m_MainGate == null)
            {
                m_MainGate = new ConnectionProvider.Client(_channelName + m_UnitNumber.ToString(),m_Events);
                m_MainGate.Subscribe();
            }


            m_WorkingFlag = true;
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(ProccesAssync));
            thread.Start();
        }

        public EventDBWriter(string moduleName, int unitNumber, string channelName)
        {
            Assembly a = null;
            //try
            {
                m_UnitNumber = unitNumber;
                _channelName = channelName;
                a = Assembly.LoadFrom(moduleName);
                m_EventTypes = BaseEvent.GetEvents();
                DBWorker.Instance.CheckTables(m_EventTypes,m_UnitNumber);
                CultureInfo newCInfo = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
                newCInfo.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = newCInfo;
            }
            //catch
            {
               // throw new Exception(string.Format("Не могу загрузить сборку \"{0}\"", moduleName));
            }
        }



        

        
    }
}
