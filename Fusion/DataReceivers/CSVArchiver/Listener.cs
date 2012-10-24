using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConnectionProvider;
using Converter;
using CommonTypes;
using Implements;

namespace CSVArchiver
{
    class Listener : IEventListener
    {
        private long m_lasIdHeat;
        public Listener()
        {
            m_lasIdHeat = 0;
            // InstantLogger.log("Listener started");
        }
        public void OnEvent(BaseEvent newEvent)
        {
            using (var l = new Logger("FlexEventSaver"))
            {
                if (newEvent is LanceEvent)
                {
                    var lanceEvent = newEvent as LanceEvent;
                    Program.SDS.LanceHeigth.Add(lanceEvent.LanceHeight);
                    Program.SDS.OxygenRate.Add(lanceEvent.O2Flow);
                    Program.OxygenRate.Add(lanceEvent.O2Flow);
                }
                if (newEvent is OffGasAnalysisEvent)
                {
                    var offGasAnalysisEvent = newEvent as OffGasAnalysisEvent;
                    Program.SDS.H2Perc.Add(offGasAnalysisEvent.H2);
                    Program.SDS.O2Perc.Add(offGasAnalysisEvent.O2);
                    Program.SDS.COPerc.Add(offGasAnalysisEvent.CO);
                    Program.SDS.CO2Perc.Add(offGasAnalysisEvent.CO2);
                    Program.SDS.N2Perc.Add(offGasAnalysisEvent.N2);
                    Program.SDS.ArPerc.Add(offGasAnalysisEvent.Ar);
                }
                if (newEvent is OffGasEvent)
                {
                    var offGasEvent = newEvent as OffGasEvent;
                    Program.SDS.VGas.Add(offGasEvent.OffGasFlow);
                    Program.SDS.TGas.Add(offGasEvent.OffGasTemp);
                }
                if (newEvent is CalculatedCarboneEvent)
                {
                    var cCarbon = newEvent as CalculatedCarboneEvent;
                    Program.SDS.CCalc.Add(cCarbon.CarbonePercent);
                }
                if (newEvent is SublanceCEvent)
                {
                    var sublanceCEvent = newEvent as SublanceCEvent;
                    Program.SDS.CSubLance = sublanceCEvent.C;
                }
                if (newEvent is IgnitionEvent)
                {
                    var ign = newEvent as IgnitionEvent;
                    Program.SDS.Ignition = ign.FusionIgnition;
                }
                if (newEvent is DecompressionOffGasEvent)
                {
                    var doge = newEvent as DecompressionOffGasEvent;
                    Program.SDS.Decompression.Add(doge.Decompression);
                }
                if (newEvent is O2Event)
                {
                    var o2e = newEvent as O2Event;
                    if (o2e.RightLanceIsSelected && !o2e.LeftLanceIsSelected)
                    {
                        Program.SDS.QOxygenCL.Add(o2e.QOxygenRight);
                        Program.SDS.POxygenCL.Add(o2e.POxygenRight);
                        Program.SDS.TOxygenCL.Add(o2e.TOxygenRight);
                        Program.SDS.DPOxygenCL.Add(o2e.DPOxygenRight);
                    }
                    else
                    {
                        Program.SDS.QOxygenCL.Add(o2e.QOxygenLeft);
                        Program.SDS.POxygenCL.Add(o2e.POxygenLeft);
                        Program.SDS.TOxygenCL.Add(o2e.TOxygenLeft);
                        Program.SDS.DPOxygenCL.Add(o2e.DPOxygenLeft);
                    }
                }
                if (newEvent is N2Event)
                {
                    var n2e = newEvent as N2Event;
                    Program.SDS.QNitrogenLanceWindow.Add(n2e.QNitrogenLanceWindow);
                    Program.SDS.QNitrogenBoiler.Add(n2e.QNitrogenBoiler);
                }
                if (newEvent is visAdditionTotalEvent)
                {
                    var vate = newEvent as visAdditionTotalEvent;
                    Program.SDS.RB5 = vate.RB5TotalWeight;
                    Program.SDS.RB6 = vate.RB6TotalWeight;
                    Program.SDS.RB7 = vate.RB7TotalWeight;
                    Program.SDS.RB8 = vate.RB8TotalWeight;
                    Program.SDS.RB9 = vate.RB9TotalWeight;
                    Program.SDS.RB10 = vate.RB10TotalWeight;
                    Program.SDS.RB11 = vate.RB11TotalWeight;
                    Program.SDS.RB12 = vate.RB12TotalWeight;
                }
                if (newEvent is HeatChangeEvent)
                {
                    var heatChangeEvent = newEvent as HeatChangeEvent;
                    Program.SaverData(Program.SDList, m_lasIdHeat);
                    m_lasIdHeat = heatChangeEvent.HeatNumber;
                    Program.Init();
                }
                if (newEvent is FlexEvent)
                {
                    var fxe = newEvent as FlexEvent;
                    if (fxe.Operation.StartsWith("NeuralProcessorC.Calc"))
                    {
                        var key = "C";
                        //l.msg(fxe.ToString());
                        if (fxe.Arguments.ContainsKey(key))
                        {
                            try
                            {
                                Program.SDS.NeuralC.Add((double) fxe.Arguments[key]);
                            }
                            catch (Exception e)
                            {
                                l.err("NeuralProcessorC.Calc - {1} : \n{0}", e.ToString(), key);
                            }
                        }
                    }
                }
            }
        }
    }
}
