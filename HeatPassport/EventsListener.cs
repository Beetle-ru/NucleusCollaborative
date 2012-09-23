using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using CommonTypes;
using ConnectionProvider.MainGate;
using ConnectionProvider;

namespace HeatPassport
{
    class EventsListener : IEventListener
    {
        public Queue<BaseEvent> Queue { get; set; }  

        public EventsListener()
        {
            Queue = new Queue<BaseEvent>();
        }

        public void OnEvent(BaseEvent newEvent)
        {
            Console.WriteLine("Event come {0}", newEvent.GetType().Name);
            lock (Queue)
            {
                Queue.Enqueue(newEvent);
            }
        }

    }
}
