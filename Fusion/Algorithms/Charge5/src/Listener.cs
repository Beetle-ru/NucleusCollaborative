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
using Charge5Classes;
//using System.ServiceModel;
//using System.Windows.Forms;

namespace Charge5
{
    class Listener : IEventListener
    {
        public Listener()
        {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }
        

        public void OnEvent(BaseEvent evt)
        {
            using (var l = new Logger("Listener"))
            {
                if (evt is FlexEvent)
                {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("UI.GetPatternNames"))
                    {
                        l.msg(fxe.ToString());
                        Program.TablePaths = Program.ScanStore(Program.StorePath);
                        var names = Program.GetNamesFromAddress(Program.TablePaths);
                        var fex = new FlexHelper("Charge5.PatternNames");
                        for (int i = 0; i < names.Count; i++)
                        {
                            fex.AddArg(i.ToString(),names[i]);
                        }
                        fex.Fire(Program.MainGate);
                    }
                    
                    if (fxe.Operation.StartsWith("UI.LoadPreset"))
                    {
                        l.msg(fxe.ToString());
                        try
                        {
                            Program.Tables = Program.LoadTables((string)fxe.Arguments["Name"], ref Program.InitTbl);
                        }
                        catch (Exception e)
                        {
                            l.err("UI.LoadPreset: \n{0}", e.ToString());
                        }

                        var fex = new FlexHelper("Charge5.RespLoadPreset");
                        if (Program.Tables == null)
                        {
                            l.err("pattern not loaded");
                            fex.AddArg("Loaded", false);
                        }
                        else
                        {
                            fex.AddArg("Loaded", true);
                        }
                        fex.Fire(Program.MainGate);
                    }

                    if (fxe.Operation.StartsWith("UI.Calc"))
                    {
                        var inData = new InData();
                        var outData = new OutData();
                        l.msg(fxe.ToString());
                        try
                        {
                            inData.SteelType = (int)fxe.Arguments["SteelType"];
                            inData.MHi = (int)fxe.Arguments["MHi"];
                            inData.MSc = (int)fxe.Arguments["MSc"];
                            inData.SiHi = (double)fxe.Arguments["SiHi"];
                            inData.THi = (int)fxe.Arguments["THi"];
                            inData.IsProcessingUVS = (bool)fxe.Arguments["IsProcessingUVS"];
                            var table = Program.Tables[inData.SteelType];
                            Program.Alg(table, inData, out outData);
                            var fex = new FlexHelper("Charge5.ResultCalc");
                            
                            fex.AddArg("MDlm", outData.MDlm);
                            fex.AddArg("MDlms", outData.MDlms);
                            fex.AddArg("MFom", outData.MFom);
                            fex.AddArg("MHi", outData.MHi);
                            fex.AddArg("MLi", outData.MLi);
                            fex.AddArg("MSc", outData.MSc);
                            fex.AddArg("IsFound", outData.IsFound);

                            fex.Fire(Program.MainGate);
                        }
                        catch (Exception e)
                        {
                            l.err("UI.Calc: \n{0}", e.ToString());
                        }
                    }

                    if (fxe.Operation.StartsWith("UI.GetPreset"))
                    {
                        l.msg(fxe.ToString());
                        try
                        {
                            var name = (string) fxe.Arguments["Name"];
                            Program.LoadTables(name, ref Program.InitTbl);
                            CSVTP_FlexEventConverter.AppName = "Charge5";
                            var flex = CSVTP_FlexEventConverter.PackToFlex(name, Program.InitTbl, Program.Tables);
                            var fex = new FlexHelper(flex.Operation);
                            fex.evt.Arguments = flex.Arguments;
                            fex.Fire(Program.MainGate);
                        }
                        catch (Exception e)
                        {
                            l.err("UI.GetPreset: \n{0}", e.ToString());
                        }
                    }
                }
            }
        }
    }
}
