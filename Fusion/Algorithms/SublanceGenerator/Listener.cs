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

namespace SublanceGenerator
{
    class Listener : IEventListener
    {

        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("FlexEventSaver"))
            {
                if (evt is LanceEvent)
                {
                    var le = evt as LanceEvent;
                    Iterator.Oxigen.Add(le.O2TotalVol);
                    Iterator.Iterate();
                }
                if (evt is OffGasAnalysisEvent)
                {
                    var ogae = evt as OffGasAnalysisEvent;
                    Iterator.CarbonMonoxide.Add(ogae.CO);
                    Iterator.Iterate();
                }
                if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    l.msg("Heat Changed. New Heat ID: {0}", hce.HeatNumber);
                    Iterator.Renit();
                    Int64 rem;
                    Int64 res = Math.DivRem(hce.HeatNumber, 10000, out rem);
                    Iterator.HeatNumber = res*100000 + rem;
                    
                }
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1"))
                    {
                        if ((string) fxe.Arguments["SHEATNO"] == Convert.ToString(Iterator.HeatNumber))
                        {
                            l.msg("Iron Correction from Pipe: {0}\n", fxe.Arguments["NWGH_NETTO"]);
                            Iterator.HotMetallMass = Convert.ToDouble(fxe.Arguments["NWGH_NETTO"]);
                        }
                        else
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                Iterator.HeatNumber, fxe.Arguments["SHEATNO"]
                                );
                    }
                }
            }
        }
    }
}
