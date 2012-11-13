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
                    Program.Data.CurrentT = ste.SublanceTemperature;
                    Program.Iterator();
                }
                if (evt is SublanceCEvent)
                {
                    var sce = evt as SublanceCEvent;
                    Program.Data.CurrentC = sce.C;
                    Program.Iterator();
                }
                if (evt is BlowingEvent)
                {
                    var be = evt as BlowingEvent;
                    Program.CurrentOxygen = be.O2TotalVol;
                    Program.Iterator();
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
                            if (Program.SidB == (Guid)fxe.Arguments[key])
                            {
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
                            }
                        }
                        catch (Exception e)
                        {
                            l.err("ConverterUI.RBBAccept - {1} : \n{0}", e.ToString(), key);
                        }
                    }
                }

            }
        }
    }
}
