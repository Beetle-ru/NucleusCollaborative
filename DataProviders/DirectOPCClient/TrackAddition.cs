using System;
using Converter;

namespace DirectOPCClient {
   public partial class MainForm : System.Windows.Forms.Form {
      public class Addition {
         public string MaterialName = string.Empty;
         public string PhaseNo = string.Empty;
         public string Destination = string.Empty;
         public DateTime Date = DateTime.MinValue;
         public int PortionWeight = int.MinValue;
         public int TotalWeight = int.MinValue;
         }
      public Addition[,] AdditionsKonvLi,AdditionsPfanneLi,AdditionsPfanneDoseLi;
      public string[,] AdditionNamesLi,LegierungNamesLi;
      //public int[] KonvBunkLi = { 5,6,7,8,9,10,11,12,0,0,0 };
      //public int[] PfanneBunkLi = { 1,2,3,4,7,13,14,15,16,17,0 };
      //public int[] PfanneDoseBunkLi = { 1,2,3,4,13,14,15,16,17 };
      public bool bNoFirstKonvAdditionsTO = false;         // um klarer zu sehen (widrig der deafaultValue)
      public bool bNoFirstPfannenLegTO = false;            // um klarer zu sehen (widrig der deafaultValue)

      private bool DoTrackKonvAdditions(int iIdxInPointLi) {
         int iActWeight,iActPortionWeight,iCnvNr,iLfdPos;
         string sMatName;
         bool bErg = true;

         try {
            iCnvNr = pointLi[iIdxInPointLi].iCnvNr;   // ������� ����������� ���������� 'ACT_C2_TOADD'=2550
            if ((j = getPoint("TOADD",iCnvNr)) >= 0) {
               AddLogg("������� ����������� ���������� 'ACT_C" + iCnvNr + "_TOADD'=" + pointLi[j].iDataValue);
               }
            for (iLfdPos = 0; iLfdPos < 10; iLfdPos++) {
               //AddLogg("DEBUG K" + iCnvNr + " iLfdBunk=" + iLfdBunk);
               sAliasInfoTeil = "ADDWGT" + (iLfdPos); // wegen DEBUG
               j = findPoint(sAliasInfoTeil,iCnvNr);
               if (j >= 0) {
                  if (pointLi[j].iDataValue != null) {
                     iActWeight = pointLi[j].iDataValue;
                     if (!bNoFirstKonvAdditionsTO) {// das waere Anlauf
                        //AddLogg("DEBUG K" + iCnvNr + " iLfdBunk=" + iLfdBunk + " Anlauf AdditionsKonvLi[" + iLfdPos +"," + (iCnvNr - 1) + "]=" + iActWeight);
                        AdditionsKonvLi[iLfdPos,iCnvNr - 1].TotalWeight = iActWeight;
                        AddLogg("K" + iCnvNr + " " + sAliasInfoTeil + " '" +
                                AdditionNamesLi[iLfdPos,iCnvNr - 1] +
                                "' ��� ��� ����������� " + iActWeight);
                        }
                     else {
                        // das waere NormalLauf
                        iActPortionWeight = iActWeight - AdditionsKonvLi[iLfdPos,iCnvNr - 1].TotalWeight;
                        sMatName = AdditionNamesLi[iLfdPos,iCnvNr - 1];
                        if (iActPortionWeight > 0) {
                           AdditionsEvent evAdditions = new AdditionsEvent();
                           evAdditions.iCnvNr = iCnvNr;
                           evAdditions.Date = DateTime.Now;
                           evAdditions.Destination = "CV";
                           evAdditions.MaterialName = sMatName;
                           evAdditions.StringNo = iLfdPos;
                           evAdditions.PortionWeight = iActPortionWeight;
                           evAdditions.TotalWeight = iActWeight;
                           sLfdEvtMsg = evAdditions.ToString();
                           mainGate.PushEvent(evAdditions);
                           //AddLogg("DEBUG K" + iCnvNr + " iLfdBunk=" + iLfdBunk + " NormalLauf AdditionsKonvLi[" +iLfdPos + "," + (iCnvNr - 1) + "]=" + iActWeight);
                           AddLogg("DEBUG K" + iCnvNr + " " + evAdditions.ToString());
                           AddLogg("K" + iCnvNr + " " + sAliasInfoTeil + " '" + sMatName + "' ���� " +
                                   AdditionsKonvLi[iLfdPos,iCnvNr - 1].TotalWeight +
                                   " ��������� " + iActPortionWeight + " ����� " + iActWeight);
                           }
                        else {
                           if (iActPortionWeight == 0) {
                              }
                           else {
                              AddLogg("K" + iCnvNr + " " + sAliasInfoTeil + " '" + sMatName + "' ���� " +
                                      AdditionsKonvLi[iLfdPos,iCnvNr - 1].TotalWeight +
                                      " ��������� " + iActPortionWeight + " ?");
                              }
                           }
                        AdditionsKonvLi[iLfdPos,iCnvNr - 1].TotalWeight = iActWeight;  // aeltern
                        }
                     }
                  }
               else {
                  AddLogg("��� DoTrackAdditionNames �� ������� ����� K" + pointLi[iIdxInPointLi].iCnvNr +
                          sAliasInfoTeil);
                  bErg = false;
                  }
               }
            }
         catch (Exception ex) {
            bErg = false;
            sP = "������ DoTrackKonvAdditions pointLi[" + iIdxInPointLi + "],Exc: " + ex.Message;
            AddLogg(sP);
            }
         bNoFirstKonvAdditionsTO = true;
         return bErg;
         }

      private bool DoTrackPfannenLegierungs(int iIdxInPointLi) {
         int iActWeight,iActPortionWeight,iCnvNr,iLfdPos,iActDryDur;
         string sMatName;
         bool bErg = true;

         try {
            iCnvNr = pointLi[iIdxInPointLi].iCnvNr;
            if ((j = getPoint("TOALLOY",iCnvNr)) >= 0) {
               AddLogg("������� ����������� ���������� 'ACT_C" + iCnvNr + "_TOALLOY'=" + pointLi[j].iDataValue);
               }
            for (iLfdPos = 0; iLfdPos < 10; iLfdPos++) {
               sAliasInfoTeil = "ALLOYWGT" + (iLfdPos); // wegen DEBUG
               j = findPoint(sAliasInfoTeil,iCnvNr);
               if (j >= 0) {
                  iActWeight = pointLi[j].iDataValue;
                  string sDurAliasInfoTeil = "ALLOY_DRYDUR" + (iLfdPos); // wegen DEBUG
                  j = findPoint(sDurAliasInfoTeil,iCnvNr);
                  if (j >= 0) {
                     iActDryDur = pointLi[j].iDataValue;
                     if (!bNoFirstPfannenLegTO) {// das waere Anlauf
                        //AddLogg("DEBUG K" + iCnvNr + " iLfdBunk=" + iLfdBunk + " Anlauf AdditionsKonvLi[" + iLfdPos +"," + (iCnvNr - 1) + "]=" + iActWeight);
                        AdditionsPfanneLi[iLfdPos,iCnvNr - 1].TotalWeight = iActWeight;
                        AddLogg("K" + iCnvNr + " " + sAliasInfoTeil + " '" +
                                LegierungNamesLi[iLfdPos,iCnvNr - 1] +
                                "' ���(����) ��� ����������� " + iActWeight);
                        }
                     else {
                        // das waere NormalLauf
                        iActPortionWeight = iActWeight - AdditionsPfanneLi[iLfdPos,iCnvNr - 1].TotalWeight;
                        sMatName = LegierungNamesLi[iLfdPos,iCnvNr - 1];
                        if (iActPortionWeight > 0) {
                           AdditionsEvent evAdditions = new AdditionsEvent();
                           evAdditions.iCnvNr = iCnvNr;
                           evAdditions.Date = DateTime.Now;
                           evAdditions.Destination = "LDL";
                           evAdditions.MaterialName = sMatName;
                           evAdditions.StringNo = iLfdPos;
                           evAdditions.PortionWeight = iActPortionWeight;
                           evAdditions.TotalWeight = iActWeight;
                           evAdditions.DryingDuration = iActDryDur;
                           sLfdEvtMsg = evAdditions.ToString();
                           mainGate.PushEvent(evAdditions);
                           //AddLogg("DEBUG K" + iCnvNr + " iLfdBunk=" + iLfdBunk + " NormalLauf AdditionsKonvLi[" +iLfdPos + "," + (iCnvNr - 1) + "]=" + iActWeight);
                           AddLogg("DEBUG K" + iCnvNr + " " + evAdditions.ToString());
                           AddLogg("K" + iCnvNr + " " + sAliasInfoTeil + "(����) '" + sMatName + "' ���� " +
                                   AdditionsPfanneLi[iLfdPos,iCnvNr - 1].TotalWeight +
                                   " ��������� " + iActPortionWeight + " ����� " + iActWeight + " ����� " + iActDryDur);
                           }
                        else {
                           if (iActPortionWeight == 0) {
                              }
                           else {
                              AddLogg("K" + iCnvNr + " " + sAliasInfoTeil + "(����) '" + sMatName + "' ���� " +
                                      AdditionsPfanneLi[iLfdPos,iCnvNr - 1].TotalWeight +
                                      " ��������� " + iActPortionWeight + " ?");
                              }
                           }
                        AdditionsPfanneLi[iLfdPos,iCnvNr - 1].TotalWeight = iActWeight;  // aeltern
                        }
                     }
                  else {
                     AddLogg("��� DoTrackAdditionNames �� ������� ����� K" + pointLi[iIdxInPointLi].iCnvNr +
                             sDurAliasInfoTeil);
                     bErg = false;
                     }
                  }
               else {
                  AddLogg("��� DoTrackAdditionNames �� ������� ����� K" + pointLi[iIdxInPointLi].iCnvNr +
                          sAliasInfoTeil);
                  bErg = false;
                  }
               }
            }
         catch (Exception ex) {
            bErg = false;
            sP = "������ DoTrackKonvAdditions pointLi[" + iIdxInPointLi + "],Exc: " + ex.Message;
            AddLogg(sP);
            }
         bNoFirstPfannenLegTO = true;
         return bErg;
         }
      // haelt interne NamensListe fresh
      private bool DoTrackAdditionNames(int iIdxInPointLi) {
         string sMatName;
         bool bErg = true;

         try {
            for (i = 0; i < 10; i++) {                   // ����� ����� �� ����������
               sAliasInfoTeil = "NAME_BUKNV" + i;        // wegen DEBUG
               //AddLogg("DEBUG DoTrackAdditionNames iCnvNr=" + pointLi[iIdxInPointLi].iCnvNr + " " + sAliasInfoTeil);
               //try {
               //   AddLogg("DEBUG K" + pointLi[iIdxInPointLi].iCnvNr + " " + sAliasInfoTeil +
               //           " DoTrackAdditionNames AdditionNamesLi[" + i + "," + (pointLi[iIdxInPointLi].iCnvNr - 1) +
               //           "]=" + AdditionNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1]);
               //   }
               //catch (Exception ex) {
               //   AddLogg("DEBUG K" + pointLi[iIdxInPointLi].iCnvNr + " " + sAliasInfoTeil +
               //           " DoTrackAdditionNames: Exception beim Zugriff zum AdditionNamesLi[" + i + "," + (pointLi[iIdxInPointLi].iCnvNr - 1) + "]");
               //   }
               j = findPoint(sAliasInfoTeil,pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  if (pointLi[j].sDataValue != null) {
                     sMatName = pointLi[j].sDataValue.Trim();
                     //AddLogg("DEBUG K" + pointLi[iIdxInPointLi].iCnvNr + " " + sAliasInfoTeil +" DoTrackAdditionNames AdditionNamesLi[" + i + "," + (pointLi[iIdxInPointLi].iCnvNr - 1) +"]=" + sMatName);
                     if (AdditionNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1] == null ||
                         AdditionNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1].Trim() == "") {
                        AddLogg("K" + pointLi[iIdxInPointLi].iCnvNr + ",��������� ������" + i + " ��������� ��� " + sMatName);
                        }
                     else {
                        AddLogg("K" + pointLi[iIdxInPointLi].iCnvNr + ",��������� ������" + i + " �������� ��� " + AdditionNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1] + " �� " + sMatName);
                        }
                     AdditionNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1] = sMatName;
                     }
                  }
               else {
                  AddLogg("��� DoTrackAdditionNames �� ������� ����� K" + pointLi[iIdxInPointLi].iCnvNr + " _NAME_BUKNV" + i);
                  }
               }
            for (i = 0; i < 10; i++) {                   // ����� ����� �� �����
               sAliasInfoTeil = "NAME_BUPF" + i;         // wegen DEBUG
               j = findPoint(sAliasInfoTeil,pointLi[iIdxInPointLi].iCnvNr);
               if (j >= 0) {
                  if (pointLi[j].sDataValue != null) {
                     sMatName = pointLi[j].sDataValue.Trim();
                     if (LegierungNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1] == null ||
                         LegierungNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1].Trim() == "") {
                        AddLogg("K" + pointLi[iIdxInPointLi].iCnvNr + ",���� ������" + i + " ��������� ��� " + sMatName);
                        }
                     else {
                        AddLogg("K" + pointLi[iIdxInPointLi].iCnvNr + ",���� ������" + i + " �������� ��� " + LegierungNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1] + " �� " + sMatName);
                        }
                     LegierungNamesLi[i,pointLi[iIdxInPointLi].iCnvNr - 1] = sMatName;
                     }
                  }
               else {
                  AddLogg("��� DoTrackAdditionNames �� ������� ����� K" + pointLi[iIdxInPointLi].iCnvNr + " _NAME_BUPF" + i);
                  }
               }
            }
         catch (Exception ex) {
            bErg = false;
            sP = "������ DoTrackAdditionNames pointLi[" + iIdxInPointLi + "],Exc: " + ex.Message;
            AddLogg(sP);
            }
         return bErg;
         }

      private bool InitAdditionsLists() {
         bool bErg = true;

         try {
            if (AdditionsKonvLi == null) {
               //AddLogg("������������� AdditionsKonvLi");
               AdditionsKonvLi = new Addition[11,3];
               for (i = 0; i < AdditionsKonvLi.GetLength(0); i++) {
                  for (j = 0; j < AdditionsKonvLi.GetLength(1); j++) {
                     AdditionsKonvLi[i,j] = new Addition();
                     }
                  }
               }
            if (AdditionsPfanneLi == null) {
               //AddLogg("������������� AdditionsPfanneLi");
               AdditionsPfanneLi = new Addition[11,3];
               for (i = 0; i < AdditionsPfanneLi.GetLength(0); i++) {
                  for (j = 0; j < AdditionsPfanneLi.GetLength(1); j++) {
                     AdditionsPfanneLi[i,j] = new Addition();
                     }
                  }
               }
            if (AdditionsPfanneDoseLi == null) {
               //AddLogg("������������� AdditionsPfanneDoseLi");
               AdditionsPfanneDoseLi = new Addition[11,3];
               for (i = 0; i < AdditionsPfanneDoseLi.GetLength(0); i++) {
                  for (j = 0; j < AdditionsPfanneDoseLi.GetLength(1); j++) {
                     AdditionsPfanneDoseLi[i,j] = new Addition();
                     }
                  }
               }

            if (AdditionNamesLi == null) {
               //AddLogg("������������� AdditionNamesLi");
               AdditionNamesLi = new string[17,3];
               for (i = 0; i < AdditionNamesLi.GetLength(0); i++) {
                  for (j = 0; j < AdditionNamesLi.GetLength(1); j++) {
                     AdditionNamesLi[i,j] = "";
                     }
                  }
               }

            if (LegierungNamesLi == null) {
               //AddLogg("������������� LegierungNamesLi");
               LegierungNamesLi = new string[17,3];
               for (i = 0; i < LegierungNamesLi.GetLength(0); i++) {
                  for (j = 0; j < LegierungNamesLi.GetLength(1); j++) {
                     LegierungNamesLi[i,j] = "";
                     }
                  }
               }
            }
         catch (Exception ex) {
            bErg = false;
            sP = "������ InitAdditionsLists,Exc: " + ex.Message;
            AddLogg(sP);
            }
         return bErg;
         }

      private int getPoint(string sParAliasInfoTeil,int iCnvNr) {
         int iRes = -1;

         int j = findPoint(sParAliasInfoTeil,iCnvNr);
         if (j >= 0) {
            iRes = j;
            }
         else {
            AddLogg("��� getPoint �� ������� ����� K" + iCnvNr + " " + sParAliasInfoTeil);
            }
         return iRes;
         }

      }	// class MainForm

   }	// namespace DirectOPCClient