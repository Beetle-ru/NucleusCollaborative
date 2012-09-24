using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using CommonTypes;

namespace ConverterUI.Util
{
    /// <summary>
    /// Хранит очередь событий и постоянно проверяет ее на наличие какого-либо события
    /// Найдя событие, запускает реакцию
    /// </summary>
    public class EventsStack
    {
        private readonly Queue<BaseEvent> _eventsQueue;

        public bool ListeningEnabled;

        public EventsStack()
        {
            _eventsQueue = new Queue<BaseEvent>();
            ListeningEnabled = true;
            var t = new Thread(StartProcessingStack);
            t.Start();
        }

        public void Add(BaseEvent e)
        {
            lock (_eventsQueue)
            {
                _eventsQueue.Enqueue(e);
                Debug.WriteLine(DateTime.Now.TimeOfDay + " Added to queue " + e.ToString());
            }
        }

        private void StartProcessingStack(object state)
        {
            while (ListeningEnabled)
            {
                lock (_eventsQueue)
                {
                    if (_eventsQueue.Count > 0)
                    {
                        Debug.WriteLine(DateTime.Now.TimeOfDay + " Removed from Queue");
                        React(this, _eventsQueue.Dequeue());
                    }
                    Thread.Sleep(0);
                }
            }
        }

        public delegate void ReactEventHandler(object sender, object data);

        public event ReactEventHandler React;
    }
}
