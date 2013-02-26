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

namespace OGDecarbonaterFine
{
    class Listener : IEventListener
    {

        public Int64 CHeatNumber;
        public int LanceHeithPrevious;
        

        public Listener()
        {
            InstantLogger.log("Listener", "Started\n", InstantLogger.TypeMessage.important);
        }
        public Int64 HeatNumberToShort(Int64 heatNLong)
        {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNLong, 10000, out reminder);
            return res * 1000 + reminder;
        }

        public Int64 HeatNumberToLong(Int64 heatNShort)
        {
            Int64 reminder = 0;
            Int64 res = Math.DivRem(heatNShort, 10000, out reminder);
            return res * 100000 + reminder;
        }
        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is LanceEvent)
                {
                    var le = evt as LanceEvent;

                }
                if (evt is BlowingEvent)
                {
                    var be = evt as BlowingEvent;
                    Iterator.Receiver.HeatIsStarted = be.BlowingFlag == 1;
                }
                //if (evt is OffGasAnalysisEvent)
                //{
                //    var ogae = evt as OffGasAnalysisEvent;
                //    Iterator.Receiver.CO.Add(ogae.CO);
                //    Iterator.Receiver.CO2.Add(ogae.CO2);
                //    Iterator.Receiver.N2.Add(ogae.N2);
                //    Iterator.Receiver.O2.Add(ogae.O2);
                //    Iterator.Receiver.H2.Add(ogae.H2);
                //}
                if (evt is OffGasEvent)
                {
                    var oge = evt as OffGasEvent;
                    Iterator.Receiver.OffGasV.Add(oge.OffGasFlow);
                    Iterator.Receiver.OffGasT.Add(oge.OffGasTemp);
                }
                if (evt is DecompressionOffGasEvent)
                {
                    var doge = evt as DecompressionOffGasEvent;
                    Iterator.Receiver.OffGasDecompression.Add(doge.Decompression);
                }
                if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    if (CHeatNumber != hce.HeatNumber)
                    {
                        CHeatNumber = hce.HeatNumber;
                        Iterator.Reset();
                        Iterator.CurrentState.HeatNumber = hce.HeatNumber;
                        l.msg("Heat Changed. New Heat ID: {0}\n", Iterator.CurrentState.HeatNumber);
                    }
                    else
                    {
                        l.msg("Heat No Changed. Heat ID: {0}\n", hce.HeatNumber);
                    }
                }

                if (evt is SublanceStartEvent)
                {
                    var sse = evt as SublanceStartEvent;
                    if (sse.SublanceStartFlag == 1)
                    {

                    }
                    if (sse.SublanceStartFlag == 0)
                    {
                        //
                    }
                }

                if (evt is visSpectrluksEvent) // углерод со спектролюкса
                {
                    var vse = evt as visSpectrluksEvent;
                }

                if (evt is ScrapEvent)
                {
                    var se = evt as ScrapEvent;
                    if (se.ConverterNumber == Program.ConverterNumber)
                    {
                        Iterator.CurrentState.MSc = se.TotalWeight;
                        l.msg("Scrap mass: {0}", Iterator.CurrentState.MSc);
                    }
                }

                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("UDP.OffGasAnalysisEvent"))
                    {
                        var fxh = new FlexHelper(fxe);

                        Iterator.Receiver.H2.Add(fxh.GetDbl("H2"));
                        Iterator.Receiver.O2.Add(fxh.GetDbl("O2"));
                        Iterator.Receiver.CO.Add(fxh.GetDbl("CO"));
                        Iterator.Receiver.CO2.Add(fxh.GetDbl("CO2"));
                        Iterator.Receiver.N2.Add(fxh.GetDbl("N2"));
                        Iterator.Receiver.Ar.Add(fxh.GetDbl("Ar"));

                        if (fxh.GetDbl("Branch") == 1)
                        {
                            Iterator.CurrentState.OffGasTransportDelay = (int)Math.Round(fxh.GetDbl("TransportDelay1"));
                        } else if (fxh.GetDbl("Branch") == 2)
                        {
                            Iterator.CurrentState.OffGasTransportDelay = (int)Math.Round(fxh.GetDbl("TransportDelay2"));
                        }
                    }

                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1"))
                    {
                        if ((string)fxe.Arguments["SHEATNO"] == Convert.ToString(HeatNumberToLong(Iterator.CurrentState.HeatNumber)))
                        {
                            l.msg("Iron Correction from Pipe: {0}\n", fxe.Arguments["NWGH_NETTO"]);
                            var hotIronMass = Convert.ToDouble(fxe.Arguments["NWGH_NETTO"]) * 1000;
                            Iterator.CurrentState.MHi = hotIronMass;
                        }
                        else
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                Iterator.CurrentState.HeatNumber, fxe.Arguments["SHEATNO"]
                                );
                    }

                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_XIMIRON"))
                    {
                        if ((string)fxe.Arguments["HEAT_NO"] == Convert.ToString(HeatNumberToLong(Iterator.CurrentState.HeatNumber)))
                        {
                            l.msg(fxe.ToString());
                            var hotIronMass = Convert.ToDouble(fxe.Arguments["HM_WEIGHT"]);
                            var hotIronCarbon = Convert.ToDouble(fxe.Arguments["ANA_C"]);
                            Iterator.CurrentState.MHi = hotIronMass;
                            Iterator.CurrentState.PCHi = hotIronCarbon;
                        }
                        else
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                HeatNumberToLong(Iterator.CurrentState.HeatNumber), fxe.Arguments["HEAT_NO"]
                                );
                    }
                }
            }
        }
    }
}
