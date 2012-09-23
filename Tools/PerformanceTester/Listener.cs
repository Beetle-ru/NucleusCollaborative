using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace PerformanceTester
{
    class Listener : IEventListener
    {
        public Thread EventThread = new Thread(EventProc);
        public static void EventProc(object o)
        {
            var _this = (Listener)o;
            while (true)
            {
                BaseEvent baseEvent = null;
                lock (_this.EventQueue)
                {
                    if (_this.EventQueue.Count > 0)
                    {
                        baseEvent = _this.EventQueue.Dequeue();
                    }
                }
                if (baseEvent != null)
                {
                    _this.CustomOnEvent(baseEvent);
                }
                else
                {
                    Thread.Sleep(0);
                }


            }
        }
        public Queue<BaseEvent> EventQueue = new Queue<BaseEvent>();
        public Listener()
        {
            //MessageBox.Show("qqq", "ww");
            EventThread.SetApartmentState(ApartmentState.MTA);
            EventThread.Priority = ThreadPriority.Normal;
            EventThread.Start(this);
        }
        public void OnEvent(BaseEvent newEvent)
        {
            EventQueue.Enqueue(newEvent);
        }
        public void CustomOnEvent(BaseEvent newEvent)
        {
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Event processing done - {0}", EventQueue.Count);
        }
    }
}
