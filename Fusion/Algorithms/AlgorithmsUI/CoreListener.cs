using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CommonTypes;
using ConnectionProvider;
using Converter;
using HeatCharge;
using Implements;

namespace AlgorithmsUI
{
    public class CoreListener : IEventListener
    {
        public CoreListener()
        {
            
        }
        public void Init()
        {
            var fex = new FlexHelper("OPC.Read-OPC.HM-Chemistry.Event.");
            fex.Fire(Program.MainGate);
        }
        public void OnEvent(BaseEvent evt)
        {
            using (Logger l = new Logger("OnEvent"))
            {
                if (evt is FlexEvent)
                {
                    var fex = new FlexHelper(evt as FlexEvent);
                }
            }
        }

    }
}