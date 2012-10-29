﻿using System;
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
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("ConverterUI.TargetValues"))
                    {
                        var key = "C";
                        l.msg(fxe.ToString());
                        if (fxe.Arguments.ContainsKey(key))
                        {
                            try
                            {
                                Program.Data.TargetC = (double)fxe.Arguments[key];
                            }
                            catch (Exception e)
                            {
                                l.err("ConverterUI.TargetValues - {1} : \n{0}", e.ToString(), key);
                            }
                        }
                        key = "T";
                        if (fxe.Arguments.ContainsKey(key))
                        {
                            try
                            {
                                Program.Data.TargetT = (int)fxe.Arguments[key];
                            }
                            catch (Exception e)
                            {
                                l.err("ConverterUI.TargetValues - {1} : \n{0}", e.ToString(), key);
                            }
                        }
                    }
                }

            }
        }
    }
}
