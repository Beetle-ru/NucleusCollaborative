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

namespace CorrectionCT
{
    class Listener : IEventListener
    {
        public Int64 CurrentHeatNumber;
        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        

        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is HeatChangeEvent)
                {
                    var hce = evt as HeatChangeEvent;
                    if (hce.HeatNumber != CurrentHeatNumber)
                    {
                        CurrentHeatNumber = hce.HeatNumber;
                        Program.Reset();
                    }
                }
                if (evt is SublanceTemperatureEvent)
                {
                    var ste = evt as SublanceTemperatureEvent;

                    //Program.WaitSublanceData.Enabled = false;
                    

                    const int maxT = 1770;
                    const int minT = 1550;
                    if ((ste.SublanceTemperature < maxT) && (ste.SublanceTemperature > minT))
                    {
                        Program.WaitSublanceData.Enabled = false;
                        Program.Data.CurrentT = ste.SublanceTemperature;

                        Program.Data.CurrentC = Program.FixedCalcCarbone;
                        l.msg("Calc carbone = {0}", Program.FixedCalcCarbone);

                        l.msg("SublanceTemperature = " + ste.SublanceTemperature);
                        Program.Iterator();
                    }
                    else
                    {
                        if (ste.SublanceTemperature == 1111)
                        {
                            Program.IsUncorrectMetering = true;
                        }
                        Program.IsUncorrectMetering = true;
                        l.err("Uncorrect temperature data = " + ste.SublanceTemperature);
                    }
                }
                if (evt is SublanceCEvent)
                {
                    var sce = evt as SublanceCEvent;
                    //Program.WaitSublanceData.Enabled = false;
                    //Program.Data.CurrentC = sce.C;
                    l.msg("sce.C = " + sce.C);
                    Program.Iterator();
                }
                if (evt is BlowingEvent)
                {
                    var be = evt as BlowingEvent;
                    Program.CurrentOxygen = be.O2TotalVol;
                    Program.Iterator();
                }
                if (evt is SublanceStartEvent)
                {
                    var sse = evt as SublanceStartEvent;
                    if (sse.SublanceStartFlag == 1)
                    {
                        l.msg("Sublance begin metering");
                        const int uvm = 3;
                        if (Program.LanceMode == uvm)
                        {
                            Program.WaitSublanceData.Interval = Program.MeteringWaitTimeUVM * 1000;
                            Program.WaitSublanceData.Enabled = true;
                        }
                        else
                        {
                            Program.WaitSublanceData.Interval = Program.MeteringWaitTimeManual * 1000;
                            Program.WaitSublanceData.Enabled = true;
                        }
                    }
                    if (sse.SublanceStartFlag == 0)
                    {
                       
                    }
                }
                if (evt is ModeLanceEvent)
                {
                    var mle = evt as ModeLanceEvent;
                    Program.LanceMode = mle.LanceMode;
                }
                if (evt is CalculatedCarboneEvent)
                {
                    var cce = evt as CalculatedCarboneEvent;
                    Program.CurrentCalcCarbone = cce.CarbonePercent;

                }
                if (evt is FixDataMfactorModelEvent)
                {
                    Program.FixedCalcCarbone = Program.CurrentCalcCarbone;
                }
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("ConverterUI.TargetValues"))
                    {
                        var key = "C";
                        l.msg(fxe.ToString());
                        try
                        {
                            Program.Data.TargetC = (double)fxe.Arguments[key];
                            //Program.Data.CurrentC = (double)fxe.Arguments[key];
                        }
                        catch (Exception e)
                        {
                            l.err("ConverterUI.TargetValues - {1} : \n{0}", e.ToString(), key);
                        }

                        key = "T";
                        try
                        {
                            Program.Data.TargetT = (int)fxe.Arguments[key];
                        }
                        catch (Exception e)
                        {
                            l.err("ConverterUI.TargetValues - {1} : \n{0}", e.ToString(), key);
                        }
                    }
                    if (fxe.Operation.StartsWith("ConverterUI.RBBAccept"))
                    {
                        var key = "SId";
                        l.msg(fxe.ToString());
                        try
                        {
                            //if (Program.SidB == (Guid)fxe.Arguments[key])
                            //{
                                key = "AutomaticStop";
                                Program.AutomaticStop = (bool) fxe.Arguments[key];

                                key = "EndNow";
                                if ((bool) fxe.Arguments[key])
                                {
                                    Program.EndNowHandler();
                                }
                                else
                                {
                                    Program.EndMeteringAccept();
                                }

                                //key = "EndBlowingOxygen";
                                //Program.EndBlowingOxygen = (int) fxe.Arguments[key];
                            //}
                        }
                        catch (Exception e)
                        {
                            l.err("ConverterUI.RBBAccept - {1} : \n{0}", e.ToString(), key);
                        }
                    }

                    if (fxe.Operation.StartsWith("CPlusProcessor.Result"))
                    {
                        var key = "C";
                        //InstantLogger.msg(fxe.ToString());
                        try
                        {
                            //Carbon = (double)fxe.Arguments[key];
                            Program.CurrentCalcCarbone = (double)fxe.Arguments[key];
                        }
                        catch (Exception e)
                        {
                            InstantLogger.err("CPlusProcessor.Result - {1} : \n{0}", e.ToString(), key);
                        }
                    }
                }

            }
        }
    }
}
