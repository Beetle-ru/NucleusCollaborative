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
            using (var l = new Logger("SublanceGenerator Listener"))
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
                    if (Iterator.HeatNumber != hce.HeatNumber)
                    {
                        l.msg("Heat Changed. New Heat ID: {0}", hce.HeatNumber);
                        Iterator.Renit();
                        Iterator.HeatNumber = hce.HeatNumber;
                        
                    }
                    else
                    {
                        l.msg("Heat No Changed. Heat ID: {0}", hce.HeatNumber);
                    }

                }
                if (evt is CalculatedCarboneEvent)
                {
                    var cce = evt as CalculatedCarboneEvent;
                    //if (Iterator.Ck != cce.CarbonePercent)
                    //{
                    //    //Console.WriteLine("Ck = " + Iterator.Ck);
                    //    Iterator.Ck = cce.CarbonePercent;
                    //    Iterator.Iterate(); ///!!!
                    //}

                }
                if (evt is ModeLanceEvent)
                {
                    var mle = evt as ModeLanceEvent;
                    l.msg("Lance mode changed {0}, O2 mode {1}", mle.LanceMode, mle.O2FlowMode);
                    if (mle.LanceMode == mle.O2FlowMode)
                    {
                        Iterator.LanceMod = mle.LanceMode;
                    }
                    else
                    {
                        Iterator.LanceMod = -1;
                    }
                }
                if (evt is SublanceStartEvent)
                {
                    var sse = evt as SublanceStartEvent;
                    if (sse.SublanceStartFlag == 1)
                    {
                        l.msg("Sublance begin metering");
                    }
                    if (sse.SublanceStartFlag == 0)
                    {
                        var str = String.Format("Receive end metering signal\n");
                        
                        if (Iterator.IsBeganMetering)
                        {
                            //Iterator.EndMetering();
                            //Iterator.BlowingEndRequest();
                            Iterator.EndMeteringAlow = true;
                            if (Iterator.EndMeteringAccept)
                            {
                                Iterator.EndMetering();

                                str += String.Format("Sublance end metering");
                            }
                        }
                        l.msg(str);
                    }
                }
                //if (evt is MeteringCounterEvent)
                //{
                //    var mce = evt as MeteringCounterEvent;
                //    if (mce.Cnt != Iterator.MeteringCounter)
                //    {
                //        Iterator.MeteringCounter = mce.Cnt;
                //        var str = String.Format("Metering counter changed");
                //        if (Iterator.IsBeganMetering)
                //        {
                //            Iterator.EndMetering();
                //            str += String.Format("Sublance end metering of Metering counter");
                //        }
                //        l.msg(str);
                //    }
                //}
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1"))
                    {
                        if ((string)fxe.Arguments["SHEATNO"] == Convert.ToString(HeatNumberToLong(Iterator.HeatNumber)))
                        {
                            l.msg("Iron Correction from Pipe: {0}\n", fxe.Arguments["NWGH_NETTO"]);
                            Iterator.HotMetallMass = Convert.ToDouble(fxe.Arguments["NWGH_NETTO"]);
                        }
                        else
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                HeatNumberToLong(Iterator.HeatNumber), fxe.Arguments["SHEATNO"]
                                );
                    }
                    if (fxe.Operation.StartsWith("ConverterUI.TargetValues"))
                    {
                        var key = "C";
                        l.msg(fxe.ToString());
                        if (fxe.Arguments.ContainsKey(key))
                        {
                            try
                            {
                                Iterator.TargetCk = (double)fxe.Arguments[key];
                            }
                            catch (Exception e)
                            {
                                l.err("ConverterUI.TargetValues - {1} : \n{0}", e.ToString(), key);
                            }
                        }
                        key = "Cu";
                        if (fxe.Arguments.ContainsKey(key))
                        {
                            try
                            {
                                Iterator.TargetCku = (double)fxe.Arguments[key];
                            }
                            catch (Exception e)
                            {
                                l.err("ConverterUI.TargetValues - {1} : \n{0}", e.ToString(), key);
                            }
                        }
                    }
                    if (fxe.Operation.StartsWith("ConverterUI.ZondAccept"))
                    {
                        var key = "SId";
                        l.msg(fxe.ToString());
                        try
                        {
                            if (Iterator.SIdK == (Guid)fxe.Arguments[key])
                            {
                                Iterator.BeginMetering();
                            }
                        }
                        catch (Exception e)
                        {
                            l.err("ConverterUI.ZondAccept - {1} : \n{0}", e.ToString(), key);
                        }
                    }
                    if (fxe.Operation.StartsWith("CorrectionCT.EndMeteringAccept"))
                    {
                        l.msg(fxe.ToString());
                        Iterator.EndMeteringAccept = true;
                    }
                    if (fxe.Operation.StartsWith("OPC.SublanceHeigth"))
                    {
                        var key = "Heigth";
                        //l.msg(fxe.ToString());
                        try
                        {
                            var sublanceHeigthNow = (Int16)fxe.Arguments[key];
                            var derivative = sublanceHeigthNow - Iterator.SublanceHeigth;
                            Iterator.SublanceHeigth = (Int16)fxe.Arguments[key];
                            if (Iterator.SublanceRaised(derivative,sublanceHeigthNow,Iterator.SublanceTreshold))
                            {
                                Iterator.EndMeteringAlow = true;
                                if (Iterator.EndMeteringAccept)
                                {
                                    Iterator.EndMetering();
                                    l.msg("Sublance end metering");
                                }
                            }
                            
                           
                        }
                        catch (Exception e)
                        {
                            l.err("OPC.SublanceHeigth - {1} : \n{0}", e.ToString(), key);
                        }
                    }

                    if (fxe.Operation.StartsWith("CarbonSwitcher.Result"))
                    {
                        var key = "C";
                        //InstantLogger.msg(fxe.ToString());
                        try
                        {
                            //Carbon = (double)fxe.Arguments[key];
                            if (Iterator.Ck != (double)fxe.Arguments[key])
                            {
                                //Console.WriteLine("Ck = " + Iterator.Ck);
                                Iterator.Ck = (double)fxe.Arguments[key];
                                Iterator.Iterate(); ///!!!
                            }
                        }
                        catch (Exception e)
                        {
                            InstantLogger.err("CPlusProcessor.Result - {1} : \n{0}", e.ToString(), key);
                        }
                    }
                    

                    //if (fxe.Operation.StartsWith("ConverterUI.BlowingEndResponce"))
                    //{
                    //    var key = "SId";
                    //    l.msg(fxe.ToString());
                    //    try
                    //    {
                    //        if (Iterator.SIdK == (Guid)fxe.Arguments[key])
                    //        {
                    //            //Iterator.BeginMetering();
                    //            key = "EndNow";
                    //            if ((bool)fxe.Arguments[key])
                    //            {
                    //                Iterator.DoStopBlow();
                    //                Iterator.EndMetering();
                    //            }
                    //            else
                    //            {
                    //                Iterator.EndMetering();
                    //            }
                    //        }
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        l.err("ConverterUI.BlowingEndResponce - {1} : \n{0}", e.ToString(), key);
                    //    }
                    //}
                }
            }
        }
    }
}
