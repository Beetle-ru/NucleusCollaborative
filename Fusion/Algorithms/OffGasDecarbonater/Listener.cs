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

namespace OffGasDecarbonater {
    internal class Listener : IEventListener {
        private static double m_carbonMonoxideVolumePercentPrevious;
        private static double m_carbonOxideVolumePercentPrevious;
        private static double m_lastScrapMass;
        private static double m_lastHotIronMass;
        private static double m_lastHotIronCarbon;
        private static int m_lanceHeigth;

        public Listener() {
            Reset();
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }

        public void Reset() {
            m_carbonMonoxideVolumePercentPrevious = 0.0;
            m_carbonOxideVolumePercentPrevious = 0.0;
            m_lastHotIronMass = CIterator.DefHotIronMass;
            m_lastScrapMass = CIterator.DefScrapMass;
            m_lastHotIronCarbon = CIterator.DefHotIronCarbonPercent;
            m_lanceHeigth = Int32.MaxValue;
        }

        public double VerifyHICP(double carbon) {
            const double minCarbonTreshold = 3.5;
            carbon = carbon < minCarbonTreshold ? CIterator.DefHotIronCarbonPercent : carbon;
            return carbon;
        }

        public Int64 HeatNumberToShort(Int64 heatNLong) {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNLong, 10000, out reminder);
            return res*1000 + reminder;
        }

        public Int64 HeatNumberToLong(Int64 heatNShort) {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNShort, 10000, out reminder);
            return res*100000 + reminder;
        }

        public void OnEvent(BaseEvent newEvent) {
            // InstantLogger.log(newEvent.ToString(), "Received", InstantLogger.TypeMessage.error);
            using (var l = new Logger("Listener")) {
                if (newEvent is FlexEvent) {
                    var fxe = newEvent as FlexEvent;
                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1")) {
                        if ((string) fxe.Arguments["SHEATNO"] ==
                            Convert.ToString(HeatNumberToLong(CIterator.HeatNumber))) {
                            l.msg("Iron Correction from Pipe: {0}\n", fxe.Arguments["NWGH_NETTO"]);
                            m_lastHotIronMass = Convert.ToDouble(fxe.Arguments["NWGH_NETTO"])*1000;
                            CIterator.DataCurrentHeat.IronMass = m_lastHotIronMass;
                        }
                        else {
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                CIterator.HeatNumber, fxe.Arguments["SHEATNO"]
                                );
                        }
                    }

                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_XIMIRON")) {
                        if ((string) fxe.Arguments["HEAT_NO"] ==
                            Convert.ToString(HeatNumberToLong(CIterator.HeatNumber))) {
                            l.msg(fxe.ToString());
                            m_lastHotIronMass = Convert.ToDouble(fxe.Arguments["HM_WEIGHT"]);
                            m_lastHotIronCarbon = VerifyHICP(Convert.ToDouble(fxe.Arguments["ANA_C"]));
                            CIterator.DataCurrentHeat.IronMass = m_lastHotIronMass;
                            CIterator.DataCurrentHeat.IronCarbonPercent = m_lastHotIronCarbon;
                        }
                        else {
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                HeatNumberToLong(CIterator.HeatNumber), fxe.Arguments["HEAT_NO"]
                                );
                        }
                    }
                }

                if (newEvent is HeatChangeEvent) {
                    var heatChangeEvent = newEvent as HeatChangeEvent;

                    if (CIterator.HeatNumber != heatChangeEvent.HeatNumber) {
                        if (CIterator.FirstHeating) {
                            CIterator.StartHeating();
                            l.msg("Start First Heating");
                        }
                        else {
                            //CIterator.ResetHeating(CIterator.CurrentHeatResult); // y
                            CIterator.StartHeating();
                            Reset();
                            l.msg("Reset Heating");
                        }
                        CIterator.HeatNumber = heatChangeEvent.HeatNumber;
                        l.msg("Number Heat: {0}", CIterator.HeatNumber);
                    }
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
                    //CIterator.AddCarbonToQueue(HeatNumberToShort(vse.HeatNumber), vse.C);
                }
                if (newEvent is ScrapEvent) {
                    var scrapEvent = newEvent as ScrapEvent;
                    if (scrapEvent.ConverterNumber == Program.ConverterNumber) {
                        CIterator.DataCurrentHeat.ScrapMass = scrapEvent.TotalWeight;
                        m_lastScrapMass = scrapEvent.TotalWeight;
                        CIterator.DataCurrentHeat.ScrapMass = m_lastScrapMass;
                        l.msg("Scrap mass: {0}", CIterator.DataCurrentHeat.ScrapMass);
                    }
                }
                //if (newEvent is MixerAnalysisEvent)
                //{
                //    var mixerAnalysisEvent = newEvent as MixerAnalysisEvent;
                //    CIterator.DataCurrentHeat.IronCarbonPercent = mixerAnalysisEvent.C;
                //    l.msg("Iron carbon Percent: {0}", CIterator.DataCurrentHeat.IronCarbonPercent);
                //    if (CIterator.DataCurrentHeat.IronCarbonPercent <= 0)
                //    {
                //        CIterator.DataCurrentHeat.IronCarbonPercent = CIterator.DefHotIronCarbonPercent;
                //        l.err("Iron carbon Percent is bad, default value: {0}", CIterator.DataCurrentHeat.IronCarbonPercent);
                //    }
                //}
                //if (newEvent is HotMetalLadleEvent)
                //{
                //    var hotMetalLadleEvent = newEvent as HotMetalLadleEvent;
                //    if (hotMetalLadleEvent.ConverterNumber == Program.ConverterNumber)
                //    {
                //        CIterator.DataCurrentHeat.IronMass = hotMetalLadleEvent.HotMetalTotalWeight;
                //        l.msg("Iron mass: {0}", CIterator.DataCurrentHeat.IronMass);
                //        if (CIterator.DataCurrentHeat.IronMass <= 0)
                //        {
                //            CIterator.DataCurrentHeat.IronMass = CIterator.DefHotIronMass;
                //            l.err("Iron mass is bad, default value: {0}", CIterator.DataCurrentHeat.IronMass);
                //        }
                //    }
                //}
                if (newEvent is LanceEvent) {
                    var lanceEvent = newEvent as LanceEvent;
                    CIterator.DataSmoothCurrent.OxygenVolumeRate.Add(lanceEvent.O2Flow);
                    l.msg("Oxygen volume rate: {0}", lanceEvent.O2Flow);
                    CIterator.DataSmoothCurrent.OxygenVolumeCurrent.Add(lanceEvent.O2TotalVol);
                    l.msg("Oxygen volume current: {0}", lanceEvent.O2TotalVol);
                    CIterator.DataSmoothCurrent.HeightLanceCentimeters.Add((double) lanceEvent.LanceHeight);
                    CIterator.CalculateLanceSpeed(lanceEvent.LanceHeight);
                        // фиксация данных по скорости и положению фурмы для старта многофакторной модели
                    l.msg("Height lance: {0}", lanceEvent.LanceHeight);

                    if (m_lanceHeigth != lanceEvent.LanceHeight) {
                        CIterator.Iterate(CIterator.DataCurrentHeat);
                        m_lanceHeigth = lanceEvent.LanceHeight;
                    }
                }
                if (newEvent is OffGasAnalysisEvent) {
                    var offGasAnalysisEvent = newEvent as OffGasAnalysisEvent;
                    CIterator.DataSmoothCurrent.CarbonMonoxideVolumePercentPrevious.Add(
                        m_carbonMonoxideVolumePercentPrevious);
                    m_carbonMonoxideVolumePercentPrevious = offGasAnalysisEvent.CO;
                    CIterator.DataSmoothCurrent.CarbonOxideVolumePercentPrevious.Add(m_carbonOxideVolumePercentPrevious);
                    m_carbonOxideVolumePercentPrevious = offGasAnalysisEvent.CO2;
                    CIterator.DataSmoothCurrent.CarbonMonoxideVolumePercent.Add(offGasAnalysisEvent.CO);
                    CIterator.DataSmoothCurrent.CarbonOxideVolumePercent.Add(offGasAnalysisEvent.CO2);
                }
                if (newEvent is OffGasEvent) {
                    var offGasEvent = newEvent as OffGasEvent;
                    CIterator.DataSmoothCurrent.OffgasVolumeRate.Add(offGasEvent.OffGasFlow);
                    CIterator.DataCurrentHeat = CIterator.DataSmoothCurrent.GetHeatData(CIterator.DataCurrentHeat,
                                                                                        CIterator.PeriodSec);
                    CIterator.DataCurrentHeat.IronMass = m_lastHotIronMass;
                    CIterator.DataCurrentHeat.ScrapMass = m_lastScrapMass;
                    CIterator.DataCurrentHeat.IronCarbonPercent = m_lastHotIronCarbon;
                    //CIterator.Iterate(CIterator.DataCurrentHeat);
                    l.msg("Iterate");
                    l.msg("[Heat number: {0}][Carbone calculation percent: {1}][Carbone calculation mass: {2}]",
                          CIterator.HeatNumber,
                          CIterator.RemainCarbonPercent,
                          CIterator.RemainCarbonMass
                        );
                }
                if (newEvent is BlowingEvent) {
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