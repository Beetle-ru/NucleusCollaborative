using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using ConnectionProvider.MainGate;
using CommonTypes;
using ConnectionProvider;

namespace Client
{
    class DummyListener : IEventListener
    {
        #region IEventCallback Members

        public void OnEvent(BaseEvent newEvent)
        {
            Console.WriteLine(newEvent.ToString());
        }

        #endregion
    }
}
