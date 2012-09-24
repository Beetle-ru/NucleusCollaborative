using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

using OPC.Common;
using OPC.Data.Interface;
using OPC.Data;
using DirectOPCClient.MainGate;
using System.ServiceModel;
using Client;
using Converter;

namespace DirectOPCClient {
   public partial class MainForm : System.Windows.Forms.Form {
      //--------------------------------------------------------------------------
      // NamensRegelung:   ACT_CHGLIDHM         allgemeine Punkte
      //                   ACT_C1_O2VOL_TOTAL   punkts, exist for each Converter
      //--------------------------------------------------------------------------
      //                                            0             1              2              3              4        5           6              7           8              9
      private readonly string[] evtNamensLi = { "O2VOL_TOTAL","O2VOL_IGNITION","SKULLCUTTING","SLAGBLOWING","BLOWING","REBLOWING","BLOWINTERRUPT","O2VOL_RUE","HEATING_SCRAP","OFFGASFLOW" 
      //         10           11         12         13        14     15          16
         ,"TIGELSTELLUNG","NEWHEATID","TOHMWGT","TOSCRAP","TOADD","TOALLOY","CNT_MATNAMEN"};
      private string sLfdEvtMsg,sLfdEvtErr;

      private bool DoStoreToCore(int iIdxInPointLi) {
         bool bErg = true;

         sLfdEvtMsg = sLfdEvtErr = "";
         try {
            if (pointLi[iIdxInPointLi].bIsTakeOver) {             // jetzt: Action!
               foreach (var iLfdIdxInPointLi in groupLi[pointLi[iIdxInPointLi].iIdxInGroupLi].iIdxInPointLi) {
                  itemclsPointInfo = pointLi[iLfdIdxInPointLi];   // wegen DEBUG
                  if (itemclsPointInfo.opcItem != null) {
                     if (itemclsPointInfo.opcItem.DataValue != null) {
                        if (itemclsPointInfo.bIsTakeOver) {
                           for (var i = 0; i < evtNamensLi.GetLength(0); i++) {
                              if (evtNamensLi[i] == itemclsPointInfo.sAliasInfoTeil) {
                                 switch (i) {
                                    case 0:
                                       DoLanceEvent(iIdxInPointLi);
                                       break;
                                    case 1:
                                       DoIgnitionEvent(iIdxInPointLi);
                                       break;
                                    case 2:
                                       DoTorkretingEvent(iIdxInPointLi);
                                       break;
                                    case 3:
                                       DoSlagBlowingEvent(iIdxInPointLi);
                                       break;
                                    case 4:
                                       DoBlowingEvent(iIdxInPointLi);
                                       break;
                                    case 5:
                                       DoReBlowingEvent(iIdxInPointLi);
                                       break;
                                    case 6:
                                       DoBlowingInterruptEvent(iIdxInPointLi);
                                       break;
                                    case 7:
                                       DoResetO2TotalVolEvent(iIdxInPointLi);
                                       break;
                                    case 8:
                                       DoHeatingScrapEvent(iIdxInPointLi);
                                       break;
                                    case 9:
                                       DoOffGasEvent(iIdxInPointLi);
                                       break;
                                    case 10:
                                       DoConverterAngleEvent(iIdxInPointLi);
                                       break;
                                    case 11:
                                       DoHeatChangeEvent(iIdxInPointLi);
                                       break;
                                    case 12:
                                       DoHotMetalLadleEvent(iIdxInPointLi);
                                       break;
                                    case 13:
                                       DoScrapEvent(iIdxInPointLi);
                                       break;
                                    case 14:
                                       DoTrackKonvAdditions(iIdxInPointLi);
                                       break;
                                    case 15:
                                       DoTrackPfannenLegierungs(iIdxInPointLi);
                                       break;
                                    case 16:
                                       DoTrackAdditionNames(iIdxInPointLi);
                                       break;
                                    }
                                 printEvent(itemclsPointInfo.sAlias);
                                 }
                              }
                           }
                        }
                     }
                  }
               }
            }
         catch (Exception ex) {
            bErg = false;
            sP = "Ошибка DoStoreToCore pointLi[" + iIdxInPointLi + "],Exc: " + ex.Message;
            AddLogg(sP);
            }
         return bErg;
         }

      private bool DoLanceEvent(int iIdxInPointLi) {
         bool bErg = false;

         if (bIsBlasen(pointLi[iIdxInPointLi].iCnvNr)) {
            try {
               LanceEvent evLanceEvent = new LanceEvent();
               evLanceEvent.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
               j = findPoint("O2VOL_TOTAL",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evLanceEvent.O2TotalVol = pointLi[j].iDataValue;
                  j = findPoint("LANCEPOS",pointLi[iIdxInPointLi].iCnvNr);
                  if (j >= 0) {
                     evLanceEvent.LanceHeight = pointLi[j].iDataValue;
                     j = findPoint("O2FLOW",pointLi[iIdxInPointLi].iCnvNr);
                     if (j >= 0) {
                        evLanceEvent.O2Flow = pointLi[j].iDataValue;
                        j = findPoint("O2PRESSURE",pointLi[iIdxInPointLi].iCnvNr);
                        if (j >= 0) {
                           evLanceEvent.O2Pressure = pointLi[j].iDataValue;
                           j = findPoint("OPLANCE",pointLi[iIdxInPointLi].iCnvNr);
                           if (j >= 0) {
                              evLanceEvent.LanceMode = pointLi[j].iDataValue;
                              j = findPoint("OPO2FLOW",pointLi[iIdxInPointLi].iCnvNr);
                              if (j >= 0) {
                                 evLanceEvent.O2FlowMode = pointLi[j].iDataValue;
                                 j = findPoint("QWASSZU_L",pointLi[iIdxInPointLi].iCnvNr);
                                 if (j >= 0) {
                                    evLanceEvent.O2LeftLanceWaterInput = pointLi[j].iDataValue;
                                    j = findPoint("QWASSAB_L",pointLi[iIdxInPointLi].iCnvNr);
                                    if (j >= 0) {
                                       evLanceEvent.O2LeftLanceWaterOutput = pointLi[j].iDataValue;
                                       j = findPoint("TWASSZU_L",pointLi[iIdxInPointLi].iCnvNr);
                                       if (j >= 0) {
                                          evLanceEvent.O2LeftLanceWaterTempInput = pointLi[j].iDataValue;
                                          j = findPoint("TWASSAB_L",pointLi[iIdxInPointLi].iCnvNr);
                                          if (j >= 0) {
                                             evLanceEvent.O2LeftLanceWaterTempOutput = pointLi[j].iDataValue;
                                             j = findPoint("LECK_L",pointLi[iIdxInPointLi].iCnvNr);
                                             if (j >= 0) {
                                                evLanceEvent.O2LeftLanceLeck = pointLi[j].iDataValue;
                                                j = findPoint("LECK_R",pointLi[iIdxInPointLi].iCnvNr);
                                                if (j >= 0) {
                                                   evLanceEvent.O2RightLanceLeck = pointLi[j].iDataValue;
                                                   j = findPoint("PWASS_L",pointLi[iIdxInPointLi].iCnvNr);
                                                   if (j >= 0) {
                                                      evLanceEvent.O2LeftLanceWaterPressure = pointLi[j].iDataValue;
                                                      j = findPoint("PWASS_R",pointLi[iIdxInPointLi].iCnvNr);
                                                      if (j >= 0) {
                                                         evLanceEvent.O2RightLanceWaterPressure = pointLi[j].iDataValue;
                                                         j = findPoint("QWASSZU_R",pointLi[iIdxInPointLi].iCnvNr);
                                                         if (j >= 0) {
                                                            evLanceEvent.O2RightLanceWaterInput = pointLi[j].iDataValue;
                                                            j = findPoint("QWASSAB_R",pointLi[iIdxInPointLi].iCnvNr);
                                                            if (j >= 0) {
                                                               evLanceEvent.O2RightLanceWaterOutput =
                                                                  pointLi[j].iDataValue;
                                                               j = findPoint("TWASSZU_R",pointLi[iIdxInPointLi].iCnvNr);
                                                               if (j >= 0) {
                                                                  evLanceEvent.O2RightLanceWaterTempInput =
                                                                     pointLi[j].iDataValue;
                                                                  j = findPoint("TWASSAB_R",
                                                                                pointLi[iIdxInPointLi].iCnvNr);
                                                                  if (j >= 0) {
                                                                     evLanceEvent.O2RightLanceWaterTempOutput =
                                                                        pointLi[j].iDataValue;
                                                                     j = findPoint("GEWLANCE_L",
                                                                                   pointLi[iIdxInPointLi].iCnvNr);
                                                                     if (j >= 0) {
                                                                        evLanceEvent.O2LeftLanceGewWeight =
                                                                           pointLi[j].iDataValue;
                                                                        j = findPoint("GEWBAER_L",
                                                                                      pointLi[iIdxInPointLi].iCnvNr);
                                                                        if (j >= 0) {
                                                                           evLanceEvent.O2LeftLanceGewBaer =
                                                                              pointLi[j].iDataValue;
                                                                           j = findPoint("GEWLANCE_R",
                                                                                         pointLi[iIdxInPointLi].iCnvNr);
                                                                           if (j >= 0) {
                                                                              evLanceEvent.O2RightLanceGewWeight =
                                                                                 pointLi[j].iDataValue;
                                                                              j = findPoint("GEWBAER_R",
                                                                                            pointLi[iIdxInPointLi].
                                                                                               iCnvNr);
                                                                              if (j >= 0) {
                                                                                 evLanceEvent.O2RightLanceGewBaer =
                                                                                    pointLi[j].iDataValue;
                                                                                 j = findPoint("BATHLEVELMAN",
                                                                                               pointLi[iIdxInPointLi].
                                                                                                  iCnvNr);
                                                                                 if (j >= 0) {
                                                                                    evLanceEvent.BathLevel =
                                                                                       pointLi[j].iDataValue;
                                                                                    sLfdEvtMsg = evLanceEvent.ToString();
                                                                                    mainGate.PushEvent(evLanceEvent);
                                                                                    //AddLogg(evLanceEvent.ToString());
                                                                                 }
                                                                                 else {
                                                                                    AddLogg(
                                                                                       "При LanceEvent не найдена точка K" +
                                                                                       pointLi[iIdxInPointLi].iCnvNr +
                                                                                       " BATHLEVELMAN");
                                                                                    }
                                                                                 }
                                                                              else {
                                                                                 AddLogg(
                                                                                    "При LanceEvent не найдена точка K" +
                                                                                    pointLi[iIdxInPointLi].iCnvNr +
                                                                                    " GEWBAER_R");
                                                                                 }
                                                                              }
                                                                           else {
                                                                              AddLogg(
                                                                                 "При LanceEvent не найдена точка K" +
                                                                                 pointLi[iIdxInPointLi].iCnvNr +
                                                                                 " GEWLANCE_R");
                                                                              }
                                                                           }
                                                                        else {
                                                                           AddLogg("При LanceEvent не найдена точка K" +
                                                                                   pointLi[iIdxInPointLi].iCnvNr +
                                                                                   " GEWBAER_L");
                                                                           }
                                                                        bErg = true;
                                                                        }
                                                                     else {
                                                                        AddLogg("При LanceEvent не найдена точка K" +
                                                                                pointLi[iIdxInPointLi].iCnvNr +
                                                                                " GEWLANCE_L");
                                                                        }
                                                                     }
                                                                  else {
                                                                     AddLogg("При LanceEvent не найдена точка K" +
                                                                             pointLi[iIdxInPointLi].iCnvNr +
                                                                             " TWASSAB_R");
                                                                     }
                                                                  }
                                                               else {
                                                                  AddLogg("При LanceEvent не найдена точка K" +
                                                                          pointLi[iIdxInPointLi].iCnvNr + " TWASSZU_R");
                                                                  }
                                                               }
                                                            else {
                                                               AddLogg("При LanceEvent не найдена точка K" +
                                                                       pointLi[iIdxInPointLi].iCnvNr + " QWASSAB_R");
                                                               }
                                                            }
                                                         else {
                                                            AddLogg("При LanceEvent не найдена точка K" +
                                                                    pointLi[iIdxInPointLi].iCnvNr + " QWASSZU_R");
                                                            }
                                                         }
                                                      else {
                                                         AddLogg("При LanceEvent не найдена точка K" +
                                                                 pointLi[iIdxInPointLi].iCnvNr + " PWASS_R");
                                                         }
                                                      }
                                                   else {
                                                      AddLogg("При LanceEvent не найдена точка K" +
                                                              pointLi[iIdxInPointLi].iCnvNr + " PWASS_L");
                                                      }
                                                   }
                                                else {
                                                   AddLogg("При LanceEvent не найдена точка K" +
                                                           pointLi[iIdxInPointLi].iCnvNr + " LECK_R");
                                                   }
                                                }
                                             else {
                                                AddLogg("При LanceEvent не найдена точка K" +
                                                        pointLi[iIdxInPointLi].iCnvNr + " LECK_L");
                                                }
                                             }
                                          else {
                                             AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr +
                                                     " TWASSAB_L");
                                             }
                                          }
                                       else {
                                          AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr +
                                                  " TWASSZU_L");
                                          }
                                       }
                                    else {
                                       AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr +
                                               " QWASSAB_L");
                                       }
                                    }
                                 else {
                                    AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr +
                                            " QWASSZU_L");
                                    }
                                 }
                              else {
                                 AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr +
                                         " OPO2FLOW");
                                 }
                              }
                           else {
                              AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " OPLANCE");
                              }
                           }
                        else {
                           AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2PRESSURE");
                           }
                        }
                     else {
                        AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2FLOW");
                        }
                     }
                  else {
                     AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOL_TOTAL");
                     }
                  }
               else {
                  AddLogg("При LanceEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " LANCEPOS");
                  }
               }
            catch (Exception eXc) {
               sLfdEvtErr = eXc.Message;
               AddLogg("При LanceEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
               }
            }
         return bErg;
         }

      private bool DoIgnitionEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            IgnitionEvent evIgnition = new IgnitionEvent();
            evIgnition.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("O2VOL_IGNITION",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evIgnition.O2IgnitionVol = pointLi[j].iDataValue;
               sLfdEvtMsg = evIgnition.ToString();
               mainGate.PushEvent(evIgnition);
               AddLogg(evIgnition.ToString());
               bErg = true;
               }
            else {
               AddLogg("При IgnitionEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOL_IGNITION");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При IgnitionEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoTorkretingEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            TorkretingEvent evTorkreting = new TorkretingEvent();
            evTorkreting.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("O2VOLTORK",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evTorkreting.O2TorkVol = pointLi[j].iDataValue;
               j = findPoint("SKULLCUTTING",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evTorkreting.TorkretingFlag = pointLi[j].iDataValue;
                  sLfdEvtMsg = evTorkreting.ToString();
                  mainGate.PushEvent(evTorkreting);
                  AddLogg(evTorkreting.ToString());
                  bErg = true;
                  }
               else {
                  AddLogg("При TorkretingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " SKULLCUTTING");
                  }
               }
            else {
               AddLogg("При TorkretingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOLTORK");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При TorkretingEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoSlagBlowingEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            SlagBlowingEvent evSlagBlowing = new SlagBlowingEvent();
            evSlagBlowing.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("NVOLSLAGBLOW",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evSlagBlowing.NVol = pointLi[j].iDataValue;
               j = findPoint("SLAGBLOWING",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evSlagBlowing.SlagBlowingFlag = pointLi[j].iDataValue;
                  sLfdEvtMsg = evSlagBlowing.ToString();
                  mainGate.PushEvent(evSlagBlowing);
                  AddLogg(evSlagBlowing.ToString());
                  bErg = true;
                  }
               else {
                  AddLogg("При SlagBlowingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " SLAGBLOWING");
                  }
               }
            else {
               AddLogg("При SlagBlowingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " NVOLSLAGBLOW");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При SlagBlowingEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoBlowingEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            BlowingEvent evBlowing = new BlowingEvent();
            evBlowing.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("O2VOL_TOTAL",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evBlowing.O2TotalVol = pointLi[j].iDataValue;
               j = findPoint("BLOWING",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evBlowing.BlowingFlag = pointLi[j].iDataValue;
                  sLfdEvtMsg = evBlowing.ToString();
                  mainGate.PushEvent(evBlowing);
                  AddLogg(evBlowing.ToString());
                  bErg = true;
                  }
               else {
                  AddLogg("При BlowingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " BLOWING");
                  }
               }
            else {
               AddLogg("При BlowingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOL_TOTAL");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При BlowingEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoReBlowingEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            ReBlowingEvent evReBlowing = new ReBlowingEvent();
            evReBlowing.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("O2VOL_TOTAL",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evReBlowing.O2TotalVol = pointLi[j].iDataValue;
               j = findPoint("REBLOWING",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evReBlowing.BlowingFlag = pointLi[j].iDataValue;
                  sLfdEvtMsg = evReBlowing.ToString();
                  mainGate.PushEvent(evReBlowing);
                  AddLogg(evReBlowing.ToString());
                  bErg = true;
                  }
               else {
                  AddLogg("При ReBlowingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " REBLOWING");
                  }
               }
            else {
               AddLogg("При ReBlowingEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOL_TOTAL");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При ReBlowingEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoBlowingInterruptEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            BlowingInterruptEvent evBlowingInterrupt = new BlowingInterruptEvent();
            evBlowingInterrupt.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("O2VOL_TOTAL",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evBlowingInterrupt.O2TotalVol = pointLi[j].iDataValue;
               j = findPoint("BLOWINTERRUPT",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evBlowingInterrupt.BlowingInterruptFlag = pointLi[j].iDataValue;
                  sLfdEvtMsg = evBlowingInterrupt.ToString();
                  mainGate.PushEvent(evBlowingInterrupt);
                  AddLogg(evBlowingInterrupt.ToString());
                  bErg = true;
                  }
               else {
                  AddLogg("При BlowingInterruptEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " BLOWINTERRUPT");
                  }
               }
            else {
               AddLogg("При BlowingInterruptEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOL_TOTAL");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При BlowingInterruptEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoResetO2TotalVolEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            ResetO2TotalVolEvent evResetO2TotalVol = new ResetO2TotalVolEvent();
            evResetO2TotalVol.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("O2VOL_TOTAL_RUE",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evResetO2TotalVol.O2TotalVol = pointLi[j].iDataValue;
               sLfdEvtMsg = evResetO2TotalVol.ToString();
               mainGate.PushEvent(evResetO2TotalVol);
               AddLogg(evResetO2TotalVol.ToString());
               bErg = true;
               }
            else {
               AddLogg("При ResetO2TotalVolEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " O2VOL_TOTAL_RUE");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При ResetO2TotalVolEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoHeatingScrapEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            HeatingScrapEvent evHeatingScrap = new HeatingScrapEvent();
            evHeatingScrap.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("HEATING_SCRAP",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evHeatingScrap.HeatingScrapFlag = pointLi[j].iDataValue;
               sLfdEvtMsg = evHeatingScrap.ToString();
               mainGate.PushEvent(evHeatingScrap);
               AddLogg(evHeatingScrap.ToString());
               bErg = true;
               }
            else {
               AddLogg("При HeatingScrapEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " HEATING_SCRAP");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При HeatingScrapEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoOffGasEvent(int iIdxInPointLi) {
         bool bErg = false;

         if (bIsBlasen(pointLi[iIdxInPointLi].iCnvNr)) {
            try {
               OffGasEvent evOffGas = new OffGasEvent();
               evOffGas.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
               j = findPoint("OFFGASCOUNTER",pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  evOffGas.OffGasCounter = pointLi[j].iDataValue;
                  j = findPoint("OFFGASFLOW",pointLi[iIdxInPointLi].iCnvNr);
                  if (j >= 0) {
                     evOffGas.OffGasFlow = pointLi[j].iDataValue; // was asFloat
                     j = findPoint("OFFGASTEMP",pointLi[iIdxInPointLi].iCnvNr);
                     if (j >= 0) {
                        evOffGas.OffGasTemp = pointLi[j].iDataValue;
                        j = findPoint("GASFILTERCONTROLPOS",pointLi[iIdxInPointLi].iCnvNr);
                        if (j >= 0) {
                           evOffGas.OffGasFilterControlPos = pointLi[j].iDataValue;
                           j = findPoint("HOODPOS",pointLi[iIdxInPointLi].iCnvNr);
                           if (j >= 0) {
                              evOffGas.OffGasHoodPos = pointLi[j].iDataValue;
                              sLfdEvtMsg = evOffGas.ToString();
                              mainGate.PushEvent(evOffGas);
                              //AddLogg(evOffGas.ToString());
                              bErg = true;
                              }
                           else {
                              AddLogg("При OffGasEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " HOODPOS");
                              }
                           }
                        else {
                           AddLogg("При OffGasEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr +
                                   " GASFILTERCONTROLPOS");
                           }
                        }
                     else {
                        AddLogg("При OffGasEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " OFFGASTEMP");
                        }
                     }
                  else {
                     AddLogg("При OffGasEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " OFFGASFLOW");
                     }
                  }
               else {
                  AddLogg("При OffGasEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " OFFGASCOUNTER");
                  }
               }
            catch (Exception eXc) {
               sLfdEvtErr = eXc.Message;
               AddLogg("При OffGasEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
               }
            }
         return bErg;
         }

      private bool DoConverterAngleEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            ConverterAngleEvent evConverterAngle = new ConverterAngleEvent();
            evConverterAngle.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("TIGELSTELLUNG",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evConverterAngle.Angle = pointLi[j].iDataValue;
               sLfdEvtMsg = evConverterAngle.ToString();
               mainGate.PushEvent(evConverterAngle);
               //AddLogg(evConverterAngle.ToString());
               bErg = true;
               }
            else {
               AddLogg("При ConverterAngleEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " TIGELSTELLUNG");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При ConverterAngleEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoHeatChangeEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            HeatChangeEvent evHeatChange = new HeatChangeEvent();
            evHeatChange.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            j = findPoint("NEWHEATID",pointLi[iIdxInPointLi].iCnvNr);
            if (j >= 0) {
               evHeatChange.HeatNumber = pointLi[j].iDataValue;
               sLfdEvtMsg = evHeatChange.ToString();
               mainGate.PushEvent(evHeatChange);
               bErg = true;//К2 Смена плавки 218521 -> 218522
               AddLogg("K" + pointLi[iIdxInPointLi].iCnvNr + " Смена плавки,новый номер "+pointLi[j].iDataValue);
               }
            else {
               AddLogg("При HeatChangeEvent не найдена точка K" + pointLi[iIdxInPointLi].iCnvNr + " NEWHEATID");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При HeatChangeEvent K" + pointLi[iIdxInPointLi].iCnvNr + " Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoHotMetalLadleEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            HotMetalLadleEvent evHotMetalLadle = new HotMetalLadleEvent();
            j = findPoint("CHGLIDHM",0);
            if (j >= 0) {
               evHotMetalLadle.LadleNumber = pointLi[j].iDataValue;
               j = findPoint("CHGLIDHMKONVNR",0);
               if (j >= 0) {
                  evHotMetalLadle.iCnvNr = pointLi[j].iDataValue;
                  j = findPoint("POURINGTLCNO0",0);
                  if (j >= 0) {
                     evHotMetalLadle.MixerNumberPortion1 = pointLi[j].iDataValue;
                     j = findPoint("POURINGTLCNO1",0);
                     if (j >= 0) {
                        evHotMetalLadle.MixerNumberPortion2 = pointLi[j].iDataValue;
                        j = findPoint("POURINGTLCNO2",0);
                        if (j >= 0) {
                           evHotMetalLadle.MixerNumberPortion3 = pointLi[j].iDataValue;
                           j = findPoint("POURINGHMWGT0",0);
                           if (j >= 0) {
                              evHotMetalLadle.WeightPortion1 = pointLi[j].iDataValue;
                              j = findPoint("POURINGHMWGT1",0);
                              if (j >= 0) {
                                 evHotMetalLadle.WeightPortion2 = pointLi[j].iDataValue;
                                 j = findPoint("POURINGHMWGT2",0);
                                 if (j >= 0) {
                                    evHotMetalLadle.WeightPortion3 = pointLi[j].iDataValue;
                                    j = findPoint("POURINGHMWGTTOTAL",0);
                                    if (j >= 0) {
                                       evHotMetalLadle.HotMetalTotalWeight = pointLi[j].iDataValue;
                                       j = findPoint("HMLDLTEMP",0);
                                       if (j >= 0) {
                                          evHotMetalLadle.HotMetalTemperature = pointLi[j].iDataValue;
                                          sLfdEvtMsg = evHotMetalLadle.ToString();
                                          mainGate.PushEvent(evHotMetalLadle);
                                          AddLogg(evHotMetalLadle.ToString());
                                          bErg = true;
                                          }
                                       else {
                                          AddLogg("При HotMetalLadleEvent не найдена точка HMLDLTEMP");
                                          }
                                       }
                                    else {
                                       AddLogg("При HotMetalLadleEvent не найдена точка POURINGHMWGTTOTAL");
                                       }
                                    }
                                 else {
                                    AddLogg("При HotMetalLadleEvent не найдена точка POURINGHMWGT2");
                                    }
                                 }
                              else {
                                 AddLogg("При HotMetalLadleEvent не найдена точка POURINGHMWGT1");
                                 }
                              }
                           else {
                              AddLogg("При HotMetalLadleEvent не найдена точка POURINGHMWGT0");
                              }
                           }
                        else {
                           AddLogg("При HotMetalLadleEvent не найдена точка POURINGTLCNO2");
                           }
                        }
                     else {
                        AddLogg("При HotMetalLadleEvent не найдена точка POURINGTLCNO1");
                        }
                     }
                  else {
                     AddLogg("При HotMetalLadleEvent не найдена точка POURINGTLCNO0");
                     }
                  }
               else {
                  AddLogg("При HotMetalLadleEvent не найдена точка CHGLIDHMKONVNR");
                  }
               }
            else {
               AddLogg("При HotMetalLadleEvent не найдена точка CHGLIDHM");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При HotMetalLadleEvent Exception: " + eXc.Message);
            }
         return bErg;
         }

      private bool DoScrapEvent(int iIdxInPointLi) {
         bool bErg = false;

         try {
            ScrapEvent evScrap = new ScrapEvent();
            j = findPoint("SCRBUCKETID",0);
            if (j >= 0) {
               evScrap.BucketNumber = pointLi[j].iDataValue;
               j = findPoint("SCRKONVNR",0);
               if (j >= 0) {
                  evScrap.ConverterNumber = pointLi[j].iDataValue;
                  //evScrap.iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
                  evScrap.iCnvNr = evScrap.ConverterNumber;
                  j = findPoint("SCRMATID0",0);
                  if (j >= 0) {
                     evScrap.ScrapType1 = pointLi[j].iDataValue;
                     j = findPoint("SCRMATID1",0);
                     if (j >= 0) {
                        evScrap.ScrapType2 = pointLi[j].iDataValue;
                        j = findPoint("SCRMATID2",0);
                        if (j >= 0) {
                           evScrap.ScrapType3 = pointLi[j].iDataValue;
                           j = findPoint("SCRMATID3",0);
                           if (j >= 0) {
                              evScrap.ScrapType4 = pointLi[j].iDataValue;
                              j = findPoint("SCRMATID4",0);
                              if (j >= 0) {
                                 evScrap.ScrapType5 = pointLi[j].iDataValue;
                                 j = findPoint("SCRMATID5",0);
                                 if (j >= 0) {
                                    evScrap.ScrapType6 = pointLi[j].iDataValue;
                                    j = findPoint("SCRMATID6",0);
                                    if (j >= 0) {
                                       evScrap.ScrapType7 = pointLi[j].iDataValue;
                                       j = findPoint("SCRMATID7",0);
                                       if (j >= 0) {
                                          evScrap.ScrapType8 = pointLi[j].iDataValue;
                                          j = findPoint("SCRWGT0",0);
                                          if (j >= 0) {
                                             evScrap.Weight1 = pointLi[j].iDataValue;
                                             j = findPoint("SCRWGT1",0);
                                             if (j >= 0) {
                                                evScrap.Weight2 = pointLi[j].iDataValue;
                                                j = findPoint("SCRWGT2",0);
                                                if (j >= 0) {
                                                   evScrap.Weight3 = pointLi[j].iDataValue;
                                                   j = findPoint("SCRWGT3",0);
                                                   if (j >= 0) {
                                                      evScrap.Weight4 = pointLi[j].iDataValue;
                                                      j = findPoint("SCRWGT4",0);
                                                      if (j >= 0) {
                                                         evScrap.Weight5 = pointLi[j].iDataValue;
                                                         j = findPoint("SCRWGT5",0);
                                                         if (j >= 0) {
                                                            evScrap.Weight6 = pointLi[j].iDataValue;
                                                            j = findPoint("SCRWGT6",0);
                                                            if (j >= 0) {
                                                               evScrap.Weight7 = pointLi[j].iDataValue;
                                                               j = findPoint("SCRWGT7",0);
                                                               if (j >= 0) {
                                                                  evScrap.Weight8 = pointLi[j].iDataValue;
                                                                  j = findPoint("SCRWGTTOTAL",0);
                                                                  if (j >= 0) {
                                                                     evScrap.TotalWeight = pointLi[j].iDataValue;
                                                                     sLfdEvtMsg = evScrap.ToString();
                                                                     mainGate.PushEvent(evScrap);
                                                                     AddLogg(evScrap.ToString());
                                                                     bErg = true;
                                                                     }
                                                                  else {
                                                                     AddLogg("При ScrapEvent не найдена точка SCRWGTTOTAL");
                                                                     }
                                                                  }
                                                               else {
                                                                  AddLogg("При ScrapEvent не найдена точка SCRWGT7");
                                                                  }
                                                               }
                                                            else {
                                                               AddLogg("При ScrapEvent не найдена точка SCRWGT6");
                                                               }
                                                            }
                                                         else {
                                                            AddLogg("При ScrapEvent не найдена точка SCRWGT5");
                                                            }
                                                         }
                                                      else {
                                                         AddLogg("При ScrapEvent не найдена точка SCRWGT4");
                                                         }
                                                      }
                                                   else {
                                                      AddLogg("При ScrapEvent не найдена точка SCRWGT3");
                                                      }
                                                   }
                                                else {
                                                   AddLogg("При ScrapEvent не найдена точка SCRWGT2");
                                                   }
                                                }
                                             else {
                                                AddLogg("При ScrapEvent не найдена точка SCRWGT1");
                                                }
                                             }
                                          else {
                                             AddLogg("При ScrapEvent не найдена точка SCRWGT0");
                                             }
                                          }
                                       else {
                                          AddLogg("При ScrapEvent не найдена точка SCRMATID7");
                                          }
                                       }
                                    else {
                                       AddLogg("При ScrapEvent не найдена точка SCRMATID6");
                                       }
                                    }
                                 else {
                                    AddLogg("При ScrapEvent не найдена точка SCRMATID5");
                                    }
                                 }
                              else {
                                 AddLogg("При ScrapEvent не найдена точка SCRMATID4");
                                 }
                              }
                           else {
                              AddLogg("При ScrapEvent не найдена точка SCRMATID3");
                              }
                           }
                        else {
                           AddLogg("При ScrapEvent не найдена точка SCRMATID2");
                           }
                        }
                     else {
                        AddLogg("При ScrapEvent не найдена точка SCRMATID1");
                        }
                     }
                  else {
                     AddLogg("При ScrapEvent не найдена точка SCRMATID0");
                     }
                  }
               else {
                  AddLogg("При ScrapEvent не найдена точка SCRKONVNR");
                  }
               }
            else {
               AddLogg("При ScrapEvent не найдена точка SCRBUCKETID");
               }
            }
         catch (Exception eXc) {
            sLfdEvtErr = eXc.Message;
            AddLogg("При ScrapEvent Exception: " + eXc.Message);
            }
         return bErg;
         }

      private void printEvent(string sAlias) {

         if (sAlias == sAliasToControl) {
            setTextInTxbViaDelegate(ref txbEventText,sLfdEvtMsg);
            setTextInTxbViaDelegate(ref txbCoreResult,(sLfdEvtErr == "") ? "Ok" : sLfdEvtErr);
            }
         }

      private int findPoint(string sAliasInfoTeil,int iCnvNr) {
         int iErg = -1;

         for (int i = 0; i < pointLi.Count; i++) {
            if (pointLi[i].sAliasInfoTeil == sAliasInfoTeil && pointLi[i].iCnvNr == iCnvNr) {
               iErg = i;
               break;
               }
            }
         return iErg;
         }

      private bool bIsBlasen(int iCnvNr) {
         int iBlasenStatus;
         bool bErg = false;

         int iErg = findPoint("BLOWING",iCnvNr);
         if (iErg >= 0) {
            iBlasenStatus = pointLi[iErg].iDataValue;
            if (iBlasenStatus == 0) {
               iErg = findPoint("REBLOWING",iCnvNr);
               if (iErg >= 0) {
                  iBlasenStatus = pointLi[iErg].iDataValue;
                  }
               else {
                  AddLogg("При bIsBlasen не найдена точка K" + iCnvNr + " REBLOWING");
                  }
               }
            if (iBlasenStatus > 0) bErg = true;
            }
         else {
            AddLogg("При bIsBlasen не найдена точка K" + iCnvNr + " BLOWING");
            }
         return bErg;
         }
      }	// class MainForm

   }	// namespace DirectOPCClient