using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Core;

using CommonTypes;
using ConnectionProvider;

namespace Tools.DB
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
            
            lock (Queue)
            {
                Queue.Enqueue(newEvent);
            }
        }

    }
}
