using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DirectOPCClient.MainGate;
using Core;

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
