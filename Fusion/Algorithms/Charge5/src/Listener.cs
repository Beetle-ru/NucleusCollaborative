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

namespace Charge5 {
    internal class Listener : IEventListener {
        public Int64 CHeatNumber;

        public Listener() {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }


        public void OnEvent(BaseEvent evt) {
            using (var l = new Logger("Listener")) {
                #region сбор данных для автоматического режима не FlexEvent

                if (evt is HeatChangeEvent) {
                    var hce = evt as HeatChangeEvent;
                    if (CHeatNumber != hce.HeatNumber) {
                        CHeatNumber = hce.HeatNumber;
                        Program.Reset();
                        l.msg("Heat Changed. New Heat ID: {0}\n", CHeatNumber);
                        Program.Saver.HeatNumber = CHeatNumber;
                    }
                    else
                        l.msg("Heat No Changed. Heat ID: {0}\n", hce.HeatNumber);
                }

                if (evt is ScrapEvent) {
                    var scrapEvent = evt as ScrapEvent;
                    if (scrapEvent.ConverterNumber == Program.ConverterNumber) {
                        Program.AutoInData.MSc = scrapEvent.TotalWeight;
                        l.msg("Scrap mass: {0}", Program.AutoInData.MSc);
                        Program.IsRefrashData = true;
                    }
                }

                #endregion

                if (evt is FlexEvent) {
                    #region интерфейс для визухи

                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("UI.GetNamePatterns")) {
                        l.msg(fxe.ToString());
                        Program.TablePaths = Program.ScanStore(Program.StorePath);
                        var names = Program.GetNamesFromAddress(Program.TablePaths);
                        var fex = new FlexHelper("Charge5.PatternNames");
                        for (int i = 0; i < names.Count; i++)
                            fex.AddArg(i.ToString(), names[i]);
                        fex.Fire(Program.MainGate);
                    }

                    if (fxe.Operation.StartsWith("UI.LoadPattern")) {
                        l.msg(fxe.ToString());
                        try {
                            Program.Tables = Program.LoadTables((string) fxe.Arguments["Name"], ref Program.InitTbl);
                        }
                        catch (Exception e) {
                            l.err("UI.LoadPattern: \n{0}", e.ToString());
                        }

                        var fex = new FlexHelper("Charge5.RespLoadPattern");
                        if (Program.Tables == null) {
                            l.err("pattern not loaded");
                            fex.AddArg("Loaded", false);
                        }
                        else
                            fex.AddArg("Loaded", true);
                        fex.Fire(Program.MainGate);
                    }

                    if (fxe.Operation.StartsWith("UI.Calc")) {
                        var inData = new InData();
                        var outData = new OutData();

                        l.msg(fxe.ToString());
                        try {
                            inData.SteelType = (int) fxe.Arguments["SteelType"]; //

                            inData.MHi = (int) fxe.Arguments["MHi"];
                            inData.MSc = (int) fxe.Arguments["MSc"];
                            inData.SiHi = (double) fxe.Arguments["SiHi"];
                            inData.THi = (int) fxe.Arguments["THi"];

                            inData.IsProcessingUVS = (bool) fxe.Arguments["IsProcessingUVS"]; //
                            var table = Program.Tables[inData.SteelType];
                            Program.Alg(table, inData, out outData);
                            Program.SendResultCalc(outData);
                        }
                        catch (Exception e) {
                            l.err("UI.Calc: \n{0}", e.ToString());
                        }
                    }

                    if (fxe.Operation.StartsWith("UI.GetPattern")) {
                        l.msg(fxe.ToString());
                        try {
                            var name = (string) fxe.Arguments["Name"];
                            Program.LoadTables(name, ref Program.InitTbl);
                            CSVTP_FlexEventConverter.AppName = "Charge5";
                            var flex = CSVTP_FlexEventConverter.PackToFlex(name, Program.InitTbl, Program.Tables);
                            var fex = new FlexHelper(flex.Operation);
                            fex.evt.Arguments = flex.Arguments;
                            fex.Fire(Program.MainGate);
                        }
                        catch (Exception e) {
                            l.err("UI.GetPattern: \n{0}", e.ToString());
                        }
                    }

                    if (fxe.Operation.StartsWith("UI.Tables")) {
                        var fex = new FlexHelper("Charge5.SavePatternResp");
                        l.msg(fxe.ToString());
                        try {
                            var patternName = "";
                            Charge5Classes.CSVTP_FlexEventConverter.UnpackFromFlex(
                                fxe,
                                ref Program.InitTbl,
                                ref Program.Tables,
                                ref patternName
                                );
                            Program.SaveTables(patternName, Program.InitTbl, Program.Tables);
                            fex.AddArg("Saved", true);
                        }
                        catch (Exception e) {
                            l.err("UI.GetPattern: \n{0}", e.ToString());
                            fex.AddArg("Saved", false);
                        }
                        fex.Fire(Program.MainGate);
                    }

                    if (fxe.Operation.StartsWith("UI.RemoovePattern")) {
                        var fex = new FlexHelper("Charge5.RemoovePatternResp");
                        l.msg(fxe.ToString());
                        try {
                            Program.RemooveTables((string) fxe.Arguments["Name"]);
                            fex.AddArg("Remooved", true);
                        }
                        catch (Exception e) {
                            l.err("UI.RemoovePattern: \n{0}", e.ToString());
                            fex.AddArg("Remooved", false);
                        }
                        fex.Fire(Program.MainGate);
                    }

                    #endregion

                    #region интерфейс для визухи в автоматическом режиме

                    if (fxe.Operation.StartsWith("UI.ModeCalc")) {
                        l.msg(fxe.ToString());
                        try {
                            Program.CalcModeIsAutomatic = (bool) fxe.Arguments["IsAutomatic"];
                            Program.IsRefrashData = true;
                        }
                        catch (Exception e) {
                            l.err("UI.CalcMode: \n{0}", e.ToString());
                        }
                    }

                    if (fxe.Operation.StartsWith("UI.DataCalc")) {
                        l.msg(fxe.ToString());
                        try {
                            var steelType = (int) fxe.Arguments["SteelType"];
                            if ((steelType >= 0) && (steelType <= 6)) // имеем только 7 типов стали
                            {
                                Program.AutoInData.SteelType = steelType;
                                Program.IsRefrashData = true;
                            }
                            else
                                throw new Exception("Не верное значение типа стали, >7 или <0");
                            Program.AutoInData.IsProcessingUVS = (bool) fxe.Arguments["IsProcessingUVS"];
                        }
                        catch (Exception e) {
                            l.err("UI.CalcData: \n{0}", e.ToString());
                        }
                    }

                    #endregion

                    #region сбор данных для автоматического режима FlexEvent

                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_WGHIRON1")) {
                        if ((string) fxe.Arguments["SHEATNO"] == Convert.ToString(HeatNumberToLong(CHeatNumber))) {
                            l.msg("Iron Correction from Pipe: {0}\n", fxe.Arguments["NWGH_NETTO"]);
                            Program.AutoInData.MHi =
                                (int) Math.Round(Convert.ToDouble(fxe.Arguments["NWGH_NETTO"])*1000);
                            Program.IsRefrashData = true;
                        }
                        else {
                            l.msg(
                                "Iron Correction from Pipe: wrong heat number - expected {0} found {1}",
                                CHeatNumber, fxe.Arguments["SHEATNO"]
                                );
                        }
                    }

                    if (fxe.Operation.StartsWith("PipeCatcher.Call.PCK_DATA.PGET_XIMIRON")) {
                        if ((string) fxe.Arguments["HEAT_NO"] == Convert.ToString(HeatNumberToLong(CHeatNumber))) {
                            l.msg("Xim Iron from Pipe: T = {0}, Si = {1}\n", fxe.Arguments["HM_TEMP"],
                                  fxe.Arguments["ANA_SI"]);
                            Program.AutoInData.SiHi = Convert.ToDouble(fxe.Arguments["ANA_SI"]);
                            Program.AutoInData.THi = Convert.ToInt32(fxe.Arguments["HM_TEMP"]);
                            Program.IsRefrashData = true;
                        }
                        else {
                            l.msg(
                                "Xim Iron from Pipe: wrong heat number - expected {0} found {1}",
                                CHeatNumber, fxe.Arguments["HEAT_NO"]
                                );
                        }
                    }

                    #endregion
                }
            }
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
    }
}