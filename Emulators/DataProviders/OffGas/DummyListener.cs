using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Client.MainGate;
using CommonTypes;

namespace Client
{
    class DummyListener : IMainGateCallback
    {
        #region IEventCallback Members

        public void OnEvent(BaseEvent newEvent)
        {
            Console.WriteLine(newEvent.ToString());
        }

        #endregion
    }
}
