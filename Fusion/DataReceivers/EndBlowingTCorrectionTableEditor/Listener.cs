﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media;
using ConnectionProvider;
using Core;
using Converter;
using CommonTypes;
using ConnectionProvider.MainGate;
using Implements;


namespace EndBlowingTCorrectionTableEditor
{
    internal class Listener : IEventListener {
        public Listener() {
            InstantLogger.log("Listener", "Started", InstantLogger.TypeMessage.important);
        }


        public void OnEvent(BaseEvent evt) {
            using (var l = new Logger("Listener")) {
                if (evt is FlexEvent) {
                    var fxe = evt as FlexEvent;
                    if (fxe.Operation.StartsWith("SQL.Corrections")) {
                        var fex = new FlexHelper(fxe);
                        if (fex.GetStr(MainWindow.ArgCommandName) == "GetScheme") {
                            if (fex.GetStr(MainWindow.ArgErrorCodeName) == "S_OK") {
                                var itemLst = (List<int>)fex.GetComplexArg("ITEM", typeof(List<int>));
                                var cminLst = (List<double>)fex.GetComplexArg("CMIN", typeof(List<double>));
                                var cmaxLst = (List<double>)fex.GetComplexArg("CMAX", typeof(List<double>));
                                var oxygenLst = (List<double>)fex.GetComplexArg("OXYGEN", typeof(List<double>));
                                var heatinLst = (List<double>)fex.GetComplexArg("HEATING", typeof(List<double>));
                                Pointer.PMainWindow.Dispatcher.Invoke(new Action(delegate() {
                                                                                     Pointer.PMainWindow.TableData = new List<TableRow>();
                                                                                     for (int i = 0; i < fex.GetInt(MainWindow.ArgCountName); i++) {
                                                                                         var tr = new TableRow();
                                                                                         tr.Item = itemLst[i];
                                                                                         tr.CMin = cminLst[i];
                                                                                         tr.CMax = cmaxLst[i];
                                                                                         tr.Oxygen = oxygenLst[i];
                                                                                         tr.Heating = heatinLst[i];

                                                                                         Pointer.PMainWindow.TableData.Add(tr);
                                                                                         
                                                                                     }
                                                                                     Pointer.PMainWindow.dgScheme.ItemsSource = Pointer.PMainWindow.TableData;
                                                                                     Pointer.PMainWindow.dgScheme.Items.Refresh();
                                                                                 }));
                            }
                        }
                    } 
                   
                }
            }
        }
    }
}