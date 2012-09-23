using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Converter;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using ConnectionProvider.MainGate;
using Core;
using CommonTypes;
using ConnectionProvider;

namespace ConverterUI
{
    class EventsListener : IEventListener
    {
        bool isBlowing = false;

        public void OnEvent(BaseEvent newEvent)
        {
            HeatInfo heatInfo = Helper.HeatInfo;
            //=================== для тестирования ========================
            if (heatInfo.CurrentConverterNumber == 0)
                heatInfo.CurrentConverterNumber = 2;
            //=============================================================

            if (newEvent is HeatChangeEvent)
            {
                HeatChangeEvent heatChangeEvent = newEvent as HeatChangeEvent;
                Helper.NewHeat(heatChangeEvent.HeatNumber, heatChangeEvent.iCnvNr, heatChangeEvent.Time);

                //Helper.HeatInfo.Number = 
            }
            if (newEvent is ConverterAngleEvent)
            {
                ConverterAngleEvent caEvent = newEvent as ConverterAngleEvent;
                heatInfo.ConverterAngleHistory.Add(caEvent);
                heatInfo.AggregateAngle.Value = caEvent.Angle;
                heatInfo.CurrentConverterAngle = caEvent.Angle;
            }
            if (newEvent is visBlowingEvent)
            {
                visBlowingEvent vbEvent = newEvent as visBlowingEvent;
                heatInfo.LanceHeight = vbEvent.RealLanceHeight;
                heatInfo.OFlow = vbEvent.RealO2Flow;
                heatInfo.ValveValue = vbEvent.ValveValue;
                heatInfo.O2Pressure = vbEvent.O2Pressure;
                heatInfo.NSlagBlowingFlow = vbEvent.NSlagBlowingFlow;
                heatInfo.NSlagBlowingPressure = vbEvent.NSlagBlowingPressure;
            }
            if (newEvent is visBlowingFlowRatesEvent)
            {
                visBlowingFlowRatesEvent vbfrEvent = newEvent as visBlowingFlowRatesEvent;
                heatInfo.LanceNFlow = vbfrEvent.LanceNFlow;
                heatInfo.BoilerNFlow = vbfrEvent.BoilerNFlow;
                heatInfo.CandleGasFlow = vbfrEvent.CandleGasFlow;
                heatInfo.CandleGasPressure = vbfrEvent.CandleGasPressure;
                heatInfo.CandleFire = vbfrEvent.CandleFire;
                heatInfo.CandleSparkFire = vbfrEvent.CandleSparkFire;
                heatInfo.CandleFireError = vbfrEvent.CandleFireError;
            }
            if (newEvent is visBlowingHeatEvent)
            {
                heatInfo.Number = (newEvent as visBlowingHeatEvent).CurrentHeatNumber;
            }
            if (newEvent is LanceEvent)
            {
                LanceEvent lEvent = newEvent as LanceEvent;
                heatInfo.LanceHistory.Add(lEvent);
                if (isBlowing)
                {
                    Helper.UpdateTrends(lEvent);
                }
                heatInfo.LanceRealHeight = lEvent.LanceHeight;
                heatInfo.ORealFlow = lEvent.O2Flow;
                heatInfo.TotalO2Volume = lEvent.O2TotalVol;
                heatInfo.O2RightLanceWaterInput = lEvent.O2RightLanceWaterInput;
                heatInfo.O2RightLanceWaterPressure = lEvent.O2RightLanceWaterPressure;
                heatInfo.O2RightLanceWaterTempInput = lEvent.O2RightLanceWaterTempInput;
                heatInfo.O2RightLanceWaterOutput = lEvent.O2RightLanceWaterOutput;
                heatInfo.O2RightLanceWaterTempOutput = lEvent.O2RightLanceWaterTempOutput;
                heatInfo.O2RightLanceWaterTempDiff = lEvent.O2RightLanceWaterInput - lEvent.O2RightLanceWaterTempOutput;
                heatInfo.O2Summary = lEvent.O2Summary;
                heatInfo.O2Pressure = lEvent.O2Pressure;

            }
            if (newEvent is OffGasEvent)
            {
                heatInfo.OffGasHistory.Add((OffGasEvent)newEvent);
                heatInfo.OffGassQ = (newEvent as OffGasEvent).OffGasFlow;
                if ((newEvent as OffGasEvent).OffGasFlow > 0)
                {
                    isBlowing = true;
                    if (heatInfo.BlowingStartTime == null)
                    {
                        heatInfo.BlowingStartTime = DateTime.Now;
                    }
                }
                else
                {
                    isBlowing = false;
                    heatInfo.BlowingStartTime = null;
                }
            }
            if (newEvent is OffGasAnalysisEvent)
            {
                OffGasAnalysisEvent ogaEvent = newEvent as OffGasAnalysisEvent;
                heatInfo.OffGasAnalysisHistory.Add(ogaEvent);
                heatInfo.OffGassAr = ogaEvent.Ar;
                heatInfo.OffGassCO = ogaEvent.CO;
                heatInfo.OffGassCO2 = ogaEvent.CO2;
                heatInfo.OffGassH2 = ogaEvent.H2;
                heatInfo.OffGassN2 = ogaEvent.N2;
                heatInfo.OffGassO2 = ogaEvent.O2;
                if (isBlowing)
                {
                    Helper.UpdateTrends(ogaEvent);
                }
            }
            if (newEvent is SublanceCEvent)
            {
                heatInfo.SublanceC = (newEvent as SublanceCEvent).C;
            }
            if (newEvent is SublanceTemperatureEvent)
            {
                heatInfo.SublanceTemperature = (newEvent as SublanceTemperatureEvent).SublanceTemperature;
            }
            if (newEvent is SublanceOxidationEvent)
            {
                heatInfo.SublanceOxidation = (newEvent as SublanceOxidationEvent).O2InSteel;
            }
            if (newEvent is SublanceStartEvent)
            {
                if ((newEvent as SublanceStartEvent).SublanceStartFlag == 1)
                {
                    heatInfo.SublanceHeight = 100;
                    heatInfo.SublanceStartTime = DateTime.Now;
                }
                else
                {
                    heatInfo.SublanceHeight = 0;
                }
                heatInfo.SublanceDown = (newEvent as SublanceStartEvent).SublanceStartFlag;
            }
            if (newEvent is SublanceTemperatureEvent)
            {
                heatInfo.SublanceC = (newEvent as SublanceTemperatureEvent).SublanceTemperature;
            }
            if (newEvent is visSublanceEvent)
            {
                visSublanceEvent vsEvent = newEvent as visSublanceEvent;
                heatInfo.SublanceAl = vsEvent.Al;
                heatInfo.SublanceHeight = vsEvent.Height;
                heatInfo.SublanceMetalLevel = vsEvent.MetalLevel;
                heatInfo.SublanceTotalO2 = vsEvent.TotalO2Vol;
                heatInfo.PPM = vsEvent.PPM;
            }
            if (newEvent is visAdditionBunkersEvent)
            {
                visAdditionBunkersEvent vabEvent = newEvent as visAdditionBunkersEvent;
                heatInfo.Bunker12MaterialName = vabEvent.Bunker12MaterialName;
                heatInfo.Bunker11MaterialName = vabEvent.Bunker11MaterialName;
                heatInfo.Bunker10MaterialName = vabEvent.Bunker10MaterialName;
                heatInfo.Bunker9_2MaterialName = vabEvent.Bunker9_2MaterialName;
                heatInfo.Bunker9_1MaterialName = vabEvent.Bunker9_1MaterialName;
                heatInfo.Bunker8_2MaterialName = vabEvent.Bunker8_2MaterialName;
                heatInfo.Bunker8_1MaterialName = vabEvent.Bunker8_1MaterialName;
                heatInfo.Bunker7MaterialName = vabEvent.Bunker7MaterialName;
                heatInfo.Bunker6MaterialName = vabEvent.Bunker6MaterialName;
                heatInfo.Bunker5MaterialName = vabEvent.Bunker5MaterialName;

                if (vabEvent.Bunker12FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale7CurrentBunker))
                    {
                        switch (heatInfo.Scale7CurrentBunker)
                        {
                            case "Bunker12":
                                heatInfo.Scale7Material1Weight += heatInfo.Scale7Weight;
                                break;
                            case "Bunker11":
                                heatInfo.Scale7Material2Weight += heatInfo.Scale7Weight;
                                break;
                            case "Bunker10":
                                heatInfo.Scale7Material3Weight += heatInfo.Scale7Weight;
                                break;
                        }
                    }
                    heatInfo.Scale7CurrentBunker = "Bunker12";
                }
                heatInfo.Bunker12FeederVibrating = vabEvent.Bunker12FeederVibrating;
                if (vabEvent.Bunker11FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale7CurrentBunker))
                    {
                        switch (heatInfo.Scale7CurrentBunker)
                        {
                            case "Bunker12":
                                heatInfo.Scale7Material1Weight += heatInfo.Scale7Weight;
                                break;
                            case "Bunker11":
                                heatInfo.Scale7Material2Weight += heatInfo.Scale7Weight;
                                break;
                            case "Bunker10":
                                heatInfo.Scale7Material3Weight += heatInfo.Scale7Weight;
                                break;
                        }
                    }
                    heatInfo.Scale7CurrentBunker = "Bunker11";
                }
                heatInfo.Bunker11FeederVibrating = vabEvent.Bunker11FeederVibrating;
                if (vabEvent.Bunker10FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale7CurrentBunker))
                    {
                        switch (heatInfo.Scale7CurrentBunker)
                        {
                            case "Bunker12":
                                heatInfo.Scale7Material1Weight += heatInfo.Scale7Weight;
                                break;
                            case "Bunker11":
                                heatInfo.Scale7Material2Weight += heatInfo.Scale7Weight;
                                break;
                            case "Bunker10":
                                heatInfo.Scale7Material3Weight += heatInfo.Scale7Weight;
                                break;
                        }
                    }
                    heatInfo.Scale7CurrentBunker = "Bunker10";
                }
                heatInfo.Bunker10FeederVibrating = vabEvent.Bunker10FeederVibrating;


                if (vabEvent.Bunker9_2FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale6CurrentBunker))
                    {
                        heatInfo.Scale6Material1Weight += heatInfo.Scale6Weight;
                    }
                    heatInfo.Scale6CurrentBunker = "Bunker9_2";
                }
                heatInfo.Bunker9_2FeederVibrating = vabEvent.Bunker9_2FeederVibrating;

                if (vabEvent.Bunker9_1FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale6CurrentBunker))
                    {
                        heatInfo.Scale6Material1Weight += heatInfo.Scale6Weight;
                    }
                    heatInfo.Scale6CurrentBunker = "Bunker9_1";
                }
                heatInfo.Bunker9_1FeederVibrating = vabEvent.Bunker9_1FeederVibrating;

                if (vabEvent.Bunker8_1FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale5CurrentBunker))
                    {
                        heatInfo.Scale5Material1Weight += heatInfo.Scale5Weight;
                    }
                    heatInfo.Scale5CurrentBunker = "Bunker8_2";
                }
                heatInfo.Bunker8_2FeederVibrating = vabEvent.Bunker8_2FeederVibrating;
                if (vabEvent.Bunker8_1FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale5CurrentBunker))
                    {
                        heatInfo.Scale5Material1Weight += heatInfo.Scale5Weight;
                    }
                    heatInfo.Scale5CurrentBunker = "Bunker8_1";
                }
                heatInfo.Bunker8_1FeederVibrating = vabEvent.Bunker8_1FeederVibrating;

                if (vabEvent.Bunker7FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale4CurrentBunker))
                    {
                        heatInfo.Scale4Material1Weight += heatInfo.Scale4Weight;
                    }
                    heatInfo.Scale4CurrentBunker = "Bunker7";
                }
                heatInfo.Bunker7FeederVibrating = vabEvent.Bunker7FeederVibrating;

                if (vabEvent.Bunker6FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale3CurrentBunker))
                    {
                        switch (heatInfo.Scale3CurrentBunker)
                        {
                            case "Bunker6":
                                heatInfo.Scale3Material1Weight += heatInfo.Scale3Weight;
                                break;
                            case "Bunker5":
                                heatInfo.Scale3Material2Weight += heatInfo.Scale3Weight;
                                break;
                        }
                    }
                    heatInfo.Scale3CurrentBunker = "Bunker6";
                }
                heatInfo.Bunker6FeederVibrating = vabEvent.Bunker6FeederVibrating;
                if (vabEvent.Bunker5FeederVibrating)
                {
                    if (!string.IsNullOrEmpty(heatInfo.Scale3CurrentBunker))
                    {
                        switch (heatInfo.Scale3CurrentBunker)
                        {
                            case "Bunker6":
                                heatInfo.Scale3Material1Weight += heatInfo.Scale3Weight;
                                break;
                            case "Bunker5":
                                heatInfo.Scale3Material2Weight += heatInfo.Scale3Weight;
                                break;
                        }
                    }
                    heatInfo.Scale3CurrentBunker = "Bunker5";
                }
                heatInfo.Bunker5FeederVibrating = vabEvent.Bunker5FeederVibrating;
            }
            if (newEvent is visAdditionScalesEvent)
            {
                visAdditionScalesEvent vasEvent = newEvent as visAdditionScalesEvent;
                heatInfo.Scale3Weight = vasEvent.Scale3Weight;
                heatInfo.Scale4Weight = vasEvent.Scale4Weight;
                heatInfo.Scale5Weight = vasEvent.Scale5Weight;
                heatInfo.Scale6Weight = vasEvent.Scale6Weight;
                heatInfo.Scale7Weight = vasEvent.Scale7Weight;

                heatInfo.Scale3Opened = vasEvent.Scale3Opened;
                if (heatInfo.Scale3Opened)
                {

                    heatInfo.Bunker1Material3Weight = heatInfo.Scale3Material1Weight;
                    heatInfo.Scale3Material1Weight = 0;
                    heatInfo.Bunker1Material4Weight = heatInfo.Scale3Material2Weight;
                    heatInfo.Scale3Material2Weight = 0;
                }
                heatInfo.Scale4Opened = vasEvent.Scale4Opened;
                if (heatInfo.Scale4Opened)
                {
                    heatInfo.Bunker1Material2Weight = heatInfo.Scale4Material1Weight;
                    heatInfo.Scale4Material1Weight = 0;
                }
                heatInfo.Scale5Opened = vasEvent.Scale5Opened;
                if (heatInfo.Scale5Opened)
                {
                    heatInfo.Bunker1Material1Weight = heatInfo.Scale5Material1Weight;
                    heatInfo.Scale5Material1Weight = 0;
                }
                heatInfo.Scale6Opened = vasEvent.Scale6Opened;
                if (heatInfo.Scale6Opened)
                {
                    heatInfo.Bunker2Material4Weight = heatInfo.Scale6Material1Weight;
                    heatInfo.Scale6Material1Weight = 0;
                }
                heatInfo.Scale7Opened = vasEvent.Scale7Opened;
                if (heatInfo.Scale7Opened)
                {
                    heatInfo.Bunker2Material1Weight = heatInfo.Scale7Material1Weight;
                    heatInfo.Scale7Material1Weight = 0;
                    heatInfo.Bunker2Material2Weight = heatInfo.Scale7Material2Weight;
                    heatInfo.Scale7Material2Weight = 0;
                    heatInfo.Bunker2Material3Weight = heatInfo.Scale7Material3Weight;
                    heatInfo.Scale7Material3Weight = 0;
                }
            }
            if (newEvent is BlowingEvent)
            {
                BlowingEvent be = newEvent as BlowingEvent;
                if (be.BlowingFlag == 1)
                {
                    isBlowing = true;
                }
                else
                {
                    isBlowing = false;
                }
            }
        }
    }
}