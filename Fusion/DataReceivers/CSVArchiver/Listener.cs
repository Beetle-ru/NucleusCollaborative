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
            if (newEvent is LanceEvent)
            {
                var lanceEvent = newEvent as LanceEvent;
                Program.SDS.LanceHeigth.Add(lanceEvent.LanceHeight);
                Program.SDS.OxygenRate.Add(lanceEvent.O2Flow);
                Program.OxygenRate = lanceEvent.O2Flow;
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
            if (newEvent is HeatChangeEvent)
            {
                var heatChangeEvent = newEvent as HeatChangeEvent;
                Program.SaverData(Program.SDList, m_lasIdHeat);
                m_lasIdHeat = heatChangeEvent.HeatNumber;
                Program.Init();
            }
        }
    }
}
