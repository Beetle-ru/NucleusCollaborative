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
//using System.ServiceModel;
//using System.Windows.Forms;

namespace NeuralProcessorC
{
    class Listener : IEventListener
    {
        private static double m_carbonMonoxideVolumePercentPrevious;
        private static double m_carbonOxideVolumePercentPrevious;
        public Listener()
        {
            m_carbonMonoxideVolumePercentPrevious = 0.0;
            m_carbonOxideVolumePercentPrevious = 0.0;
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        
        public Int64 HeatNumberRemooveAxcess(Int64 heatNSpectroluks)
        {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNSpectroluks, 10000, out reminder);
            return res * 1000 + reminder;
        }

        public void OnEvent(BaseEvent newEvent)
        {
           // InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.error);
            using (var l = new Logger("Listener"))
            {
                if (newEvent is HeatChangeEvent)
                {
                    var heatChangeEvent = newEvent as HeatChangeEvent;

                    if (CIterator.FirstHeating)
                    {
                        CIterator.StartHeating();
                        l.msg("Start First Heating");
                    }
                    else
                    {
                        //CIterator.ResetHeating(CIterator.CurrentHeatResult); // y
                        CIterator.StartHeating();
                        l.msg("Reset Heating");
                    }

                    CIterator.CurrentHeatResult.NumberHeat = heatChangeEvent.HeatNumber;
                    l.msg("Number Heat: {0}", CIterator.CurrentHeatResult.NumberHeat);
                }
                //if (newEvent is SublanceCEvent) // на будущее из другого места брать углерод
                //{
                //    var sublanceCEvent = newEvent as SublanceCEvent;
                //    CIterator.CurrentHeatResult.SteelCarbonPercent = sublanceCEvent.C;
                //    l.msg("Steel carbone percent: {0}", CIterator.CurrentHeatResult.SteelCarbonPercent);
                //}
                if (newEvent is visSpectrluksEvent) // углерод со спектролюкса
                {
                    var vse = newEvent as visSpectrluksEvent;
                    CIterator.AddCarbonToQueue(HeatNumberRemooveAxcess(vse.HeatNumber), vse.C);
                }
                if (newEvent is ScrapEvent)
                {
                    var scrapEvent = newEvent as ScrapEvent;
                    if (scrapEvent.ConverterNumber == Program.ConverterNumber)
                    {
                        CIterator.DataCurrentHeat.ScrapMass = scrapEvent.TotalWeight;
                        l.msg("Scrap mass: {0}", CIterator.DataCurrentHeat.ScrapMass);
                    }
                }
                if (newEvent is MixerAnalysisEvent)
                {
                    var mixerAnalysisEvent = newEvent as MixerAnalysisEvent;
                    CIterator.DataCurrentHeat.IronCarbonPercent = mixerAnalysisEvent.C;
                    l.msg("Iron carbon Percent: {0}", CIterator.DataCurrentHeat.IronCarbonPercent);
                    if (CIterator.DataCurrentHeat.IronCarbonPercent <= 0)
                    {
                        CIterator.DataCurrentHeat.IronCarbonPercent = 5.1133;
                        l.err("Iron carbon Percent is bad, default value: {0}", CIterator.DataCurrentHeat.IronCarbonPercent);
                    }
                }
                if (newEvent is HotMetalLadleEvent)
                {
                    var hotMetalLadleEvent = newEvent as HotMetalLadleEvent;
                    if (hotMetalLadleEvent.ConverterNumber == Program.ConverterNumber)
                    {
                        CIterator.DataCurrentHeat.IronMass = hotMetalLadleEvent.HotMetalTotalWeight;
                        l.msg("Iron mass: {0}", CIterator.DataCurrentHeat.IronMass);
                        if (CIterator.DataCurrentHeat.IronMass <= 0)
                        {
                            CIterator.DataCurrentHeat.IronMass = 300000.1133;
                            l.err("Iron mass is bad, default value: {0}", CIterator.DataCurrentHeat.IronMass);
                        }
                    }
                }
                if (newEvent is LanceEvent)
                {
                    var lanceEvent = newEvent as LanceEvent;
                    //CIterator.DataCurrentHeat.OxygenVolumeRate = lanceEvent.O2Flow;
                    CIterator.DataSmoothCurrent.OxygenVolumeRate.Add(lanceEvent.O2Flow);
                    l.msg("Oxygen volume rate: {0}", lanceEvent.O2Flow);
                    //CIterator.DataCurrentHeat.OxygenVolumeCurrent = lanceEvent.O2TotalVol;
                    CIterator.DataSmoothCurrent.OxygenVolumeCurrent.Add(lanceEvent.O2TotalVol);
                    l.msg("Oxygen volume current: {0}", lanceEvent.O2TotalVol);
                    //CIterator.DataCurrentHeat.HeightLanceCentimeters = lanceEvent.LanceHeight;
                    CIterator.DataSmoothCurrent.HeightLanceCentimeters.Add((double)lanceEvent.LanceHeight);
                    //CIterator.SetMaxDownLancePosition(lanceEvent.LanceHeight); // самое низкое положение за плавку для старта многофакторной модели
                    //CIterator.SmoothSecondLancePosition.Add((double)lanceEvent.LanceHeight);
                    CIterator.CalculateLanceSpeed(lanceEvent.LanceHeight); // фиксация данных по скорости и положению фурмы для старта многофакторной модели
                    l.msg("Height lance: {0}", lanceEvent.LanceHeight);
                    //CIterator.MomentFixDataForMFactorModel();
                }
                if (newEvent is OffGasAnalysisEvent)
                {
                    var offGasAnalysisEvent = newEvent as OffGasAnalysisEvent;
                    //CIterator.DataCurrentHeat.CarbonMonoxideVolumePercentPrevious =
                    //    CIterator.DataCurrentHeat.CarbonMonoxideVolumePercent;
                    CIterator.DataSmoothCurrent.CarbonMonoxideVolumePercentPrevious.Add(m_carbonMonoxideVolumePercentPrevious);
                    m_carbonMonoxideVolumePercentPrevious = offGasAnalysisEvent.CO;
                    //CIterator.DataCurrentHeat.CarbonOxideVolumePercentPrevious =
                    //    CIterator.DataCurrentHeat.CarbonOxideVolumePercent;
                    CIterator.DataSmoothCurrent.CarbonOxideVolumePercentPrevious.Add(m_carbonOxideVolumePercentPrevious);
                    m_carbonOxideVolumePercentPrevious = offGasAnalysisEvent.CO2;
                    //CIterator.DataCurrentHeat.CarbonMonoxideVolumePercent = offGasAnalysisEvent.CO;
                    CIterator.DataSmoothCurrent.CarbonMonoxideVolumePercent.Add(offGasAnalysisEvent.CO);
                    //CIterator.DataCurrentHeat.CarbonOxideVolumePercent = offGasAnalysisEvent.CO2;
                    CIterator.DataSmoothCurrent.CarbonOxideVolumePercent.Add(offGasAnalysisEvent.CO2);
                }
                if (newEvent is OffGasEvent)
                {
                    var offGasEvent = newEvent as OffGasEvent;
                    //CIterator.DataCurrentHeat.OffgasVolumeRate = offGasEvent.OffGasFlow;
                    CIterator.DataSmoothCurrent.OffgasVolumeRate.Add(offGasEvent.OffGasFlow);
                    CIterator.DataCurrentHeat = CIterator.DataSmoothCurrent.GetHeatData(CIterator.DataCurrentHeat, CIterator.PeriodSec);
                    CIterator.Iterate(CIterator.DataCurrentHeat);
                    l.msg("Iterate");
                    l.msg("[Heat number: {0}][Carbone calculation percent: {1}][Carbone calculation mass: {2}]",
                        CIterator.CurrentHeatResult.NumberHeat,
                        CIterator.RemainCarbonPercent,
                        CIterator.RemainCarbonMass
                        );
                }
                if (newEvent is BlowingEvent)
                {
                    //var blowingEvent = newEvent as BlowingEvent;
                    //CIterator.Iterate(CIterator.DataCurrentHeat);
                    //l.msg("Iterate");
                    //l.msg("[Heat number: {0}][Carbone calculation percent: {1}][Carbone calculation mass: {2}]",
                    //    CIterator.CurrentHeatResult.NumberHeat, 
                    //    CIterator.RemainCarbonPercent,
                    //    CIterator.RemainCarbonMass
                    //    );
                }

            }
        }
    }
}
