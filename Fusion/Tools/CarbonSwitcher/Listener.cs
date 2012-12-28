using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;

namespace CarbonSwitcher
{
    class Listener : IEventListener
    {
        public long HeatNumber;
        public Listener()
        {
            InstantLogger.log("Listener", "Started\n", InstantLogger.TypeMessage.important);
        }
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("SublanceGenerator Listener"))
            {
                if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    if (HeatNumber != hce.HeatNumber)
                    {
                        l.msg("Heat Changed. New Heat ID: {0}\n", hce.HeatNumber);
                        HeatNumber = hce.HeatNumber;
                        Program.Reset();
                    }
                    else
                    {
                        l.msg("Heat No Changed. Heat ID: {0}\n", hce.HeatNumber);
                    }

                }

                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;

                    CarbonEventHandler(fxe, 0, "OffGasDecarbonater");
                    CarbonEventHandler(fxe, 1, "SMFCarbon");
                    CarbonEventHandler(fxe, 2, "CPlusProcessor");

                }
            }
        }

        public void CarbonEventHandler(FlexEvent felexE, int id, string prefix)
        {
            var evtName = String.Format("{0}.Result", prefix);
            if (felexE.Operation.StartsWith(evtName))
            {
                var key = "C";
                try
                {
                    Program.ModelList[id].C = (double)felexE.Arguments[key];
                    Program.Iterate();
                }
                catch (Exception e)
                {
                    InstantLogger.err("{2} - {1} : \n{0}", e.ToString(), key, evtName);
                }
            }

            evtName = String.Format("{0}.ModelIsStarted", prefix);
            if (felexE.Operation.StartsWith(evtName))
            {
                Program.ModelList[id].IsStarted = true;
                Program.Iterate();
            }

            evtName = String.Format("{0}.DataFix", prefix);
            if (felexE.Operation.StartsWith(evtName))
            {
                Program.ModelList[id].IsFixed = true;
                Program.Iterate();
            }
        }
    }
}
