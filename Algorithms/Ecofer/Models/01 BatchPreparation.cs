using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data;

namespace Models
{
    public class BatchPreparation
    {
        private Data.Model.BatchPreparationInput mInputData;
        public string OutputCSVFileName { get; set; }

        public BatchPreparation(Data.Model.BatchPreparationInput aInputData)
        {
            mInputData = aInputData;
            OutputCSVFileName = null;
        }

        public Data.Model.BatchPreparationOutput Run()
        {
            Data.Model.BatchPreparationOutput lOutputData = new Data.Model.BatchPreparationOutput();

            float lSuma_podilu_SZ = 0;
            float[] lStredni_SZ = new float[Global.MATERIALELEMENTS_COUNT];
            float[] lPodil_SZ = new float[Global.HOTMETAL_COUNT];
            float lSuma_podilu_SROT = 0;
            float[] lStredni_Srot = new float[Global.MATERIALELEMENTS_COUNT];
            float[] lPodil_Srot = new float[Global.SCRAPYARDS_COUNT];

            float lm_SZ = 300000;
            float lm_Srot = 10000;
            float lKrok_m_Srot = Common.Global.Step_m_Scrap_kg;

            float lH_SZ = 0;
            float lH_Srot = 0;
            float lm_SiO2 = 0;
            float lm_MnO = 0;
            float lm_Al2O3 = 0;
            float lm_CaO = 0;
            float lm_Struska = 0;
            float lm_MgO = 0;
            float lT_Struska = 0;
            float lH_Struskotvorne = 0;
            float lH_Struskotvorne_Vystup = 0;
            float lm_FeO = 0;
            float lm_Fe_k_oxidaci = 0;

            float lH_Si_oxidace = 0;
            float lH_Mn_oxidace = 0;
            float lH_Al_oxidace = 0;
            float lH_Fe_oxidace = 0;
            float lH_C_oxidace = 0;
            float lH_Koks = 0;
            float lH_Vsazka = 0;
            float lm_Ocel = 0;
            float lH_Ocel = 0;
            float lH_Odprasky = 0;

            float lm_Dolomite = 0;
            float lm_Lime = 0;

            #region Initialization
            for (int i = 0; i < Global.HOTMETAL_COUNT; i++) lStredni_SZ[i] = 0;
            for (int i = 0; i < Global.HOTMETAL_COUNT; i++) lPodil_SZ[i] = (mInputData.HotMetals_t[i] > 0 && mInputData.HotMetals[i] != null) ? mInputData.HotMetals_t[i] * 1000 : 0;
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++) lStredni_Srot[i] = 0;
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++) lPodil_Srot[i] = (mInputData.Scraps_t[i] > 0 && mInputData.Scraps[i] != null) ? mInputData.Scraps_t[i] * 1000 : 0;
            #endregion

            // *****************************
            // Model calculation
            // *****************************
            #region Stredni SZ (R 1..5)
            for (int i = 0; i < Global.HOTMETAL_COUNT; i++)
            {
                if (lPodil_SZ[i] == 0) continue;

                lSuma_podilu_SZ += lPodil_SZ[i];

                if (mInputData.HotMetals[i] == null) throw new ApplicationException(String.Format("Unknown material for hot metal [{0}] - amount of {1} units.", i + 1, lPodil_SZ[i]));

                for (int iElement = 0; iElement < Global.MATERIALELEMENTS_COUNT; iElement++)
                {
                    DTO.MINP_GD_MaterialItemsDTO lMaterialItem = mInputData.HotMetals[i].MINP_GD_MaterialItems.SingleOrDefault(aR => aR.MINP_GD_MaterialElement.Index == iElement);

                    if (lMaterialItem != null)
                    {
                        lStredni_SZ[iElement] += lPodil_SZ[i] * (float)lMaterialItem.Amount_p;
                    }
                }
            }

            for (int iElement = 0; iElement < Global.MATERIALELEMENTS_COUNT; iElement++)
            {
                lStredni_SZ[iElement] = lStredni_SZ[iElement] / lSuma_podilu_SZ;
            }
            #endregion
            #region Stredni SROT (R 6..10)
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++)
            {
                if (lPodil_Srot[i] == 0) continue;

                lSuma_podilu_SROT += lPodil_Srot[i];

                if (mInputData.Scraps[i] == null) throw new ApplicationException(String.Format("Unknown material in scrap yard [{0}] - amount of {1} units.", i + 1, lPodil_Srot[i]));

                for (int iElement = 0; iElement < Global.MATERIALELEMENTS_COUNT; iElement++)
                {
                    DTO.MINP_GD_MaterialItemsDTO lMaterialItem = mInputData.Scraps[i].MINP_GD_MaterialItems.SingleOrDefault(aR => aR.MINP_GD_MaterialElement.Index == iElement);

                    if (lMaterialItem != null)
                    {
                        lStredni_Srot[iElement] += lPodil_Srot[i] * (float)lMaterialItem.Amount_p;
                    }
                }
            }

            for (int iElement = 0; iElement < Global.MATERIALELEMENTS_COUNT; iElement++)
            {
                lStredni_Srot[iElement] = lStredni_Srot[iElement] / lSuma_podilu_SROT;
            }
            #endregion

            // temperatures - real x [69]
            float lT_StredniSZ_69 = (mInputData.HotMetal_Temperature.HasValue && mInputData.HotMetal_Temperature.Value != 0) ? mInputData.HotMetal_Temperature.Value : lStredni_SZ[69];
            float lT_StredniSrot_69 = (mInputData.Scrap_Temperature.HasValue && mInputData.Scrap_Temperature.Value != 0) ? mInputData.Scrap_Temperature.Value : lStredni_Srot[69];
            float lT_Lime_69 = MINP.FP(mInputData.Lime, 69);
            float lT_FOM_69 = MINP.FP(mInputData.FOM, 69);
            float lT_S1_69 = MINP.FP(mInputData.S1, 69);
            float lT_S2_69 = MINP.FP(mInputData.S2, 69);
            float lT_Coke_69 = MINP.FP(mInputData.Coke, 69);
            float lT_Dolomit_69 = MINP.FP(mInputData.Dolomite, 69);

            bool lAmountEnough = false;
            bool lLastCalculationFinished = false;

            System.IO.StreamWriter lSW = null;

            if (OutputCSVFileName != null)
            {
                lSW = new System.IO.StreamWriter(OutputCSVFileName);
                lSW.WriteLine("m_Srot;H_Vsazka;H_Ocel;H_Srot");
            }

            while (!lAmountEnough || !lLastCalculationFinished)
            {
                #region Vypocet entalpii, hmotnosti strusky a srotu (R 14..25)
                // R 14
                if (lT_StredniSZ_69 > lStredni_SZ[72])
                    lH_SZ = lm_SZ * (lStredni_SZ[70] / MINP.ConversionVector(70) + lStredni_SZ[71] / MINP.ConversionVector(71)) * (lT_StredniSZ_69 - lStredni_SZ[72]);
                else
                    lH_SZ = lm_SZ * (lStredni_SZ[70] / MINP.ConversionVector(70) / lStredni_SZ[72]) * lT_StredniSZ_69;
                
                // R 15
                lH_Srot = lm_Srot * (lStredni_Srot[70] / MINP.ConversionVector(70) / lStredni_Srot[72]) * lT_StredniSrot_69;

                // R 16
                lm_SiO2 = (lm_SZ * lStredni_SZ[1] / MINP.ConversionVector(1) + lm_Srot * lStredni_Srot[1] / MINP.ConversionVector(1)) * MINP.Mm(51) / MINP.Mm(1)
                    + lm_SZ * lStredni_SZ[51] / MINP.ConversionVector(51) + lm_Srot * lStredni_Srot[51] / MINP.ConversionVector(51);
                lm_MnO = (lm_SZ * lStredni_SZ[2] / MINP.ConversionVector(2) + lm_Srot * lStredni_Srot[2] / MINP.ConversionVector(2)) * MINP.Mm(53) / MINP.Mm(2)
                    +lm_SZ * lStredni_SZ[53] / MINP.ConversionVector(53) + lm_Srot * lStredni_Srot[53] / MINP.ConversionVector(53);
                lm_Al2O3 = (lm_SZ * lStredni_SZ[5] / MINP.ConversionVector(5) + lm_Srot * lStredni_Srot[5] / MINP.ConversionVector(5)) * MINP.Mm(62) / MINP.Mm(5) / 2
                    +lm_SZ * lStredni_SZ[62] / MINP.ConversionVector(62) + lm_Srot * lStredni_Srot[62] / MINP.ConversionVector(62);

                // R 19
                lm_CaO = lm_SiO2 * mInputData.Basicity;
                lm_Struska = (lm_CaO + lm_SiO2 + lm_MnO + lm_Al2O3) * 100f / (100 - mInputData.FeO_p - mInputData.MgO_p);
                lm_MgO = lm_Struska * mInputData.MgO_p / 100f;
                lT_Struska = mInputData.Final_Temperature;
                
                // R24
                lm_FeO = lm_SZ * lStredni_SZ[61] / MINP.ConversionVector(61) + lm_Srot * lStredni_Srot[61] / MINP.ConversionVector(61);
                lm_Fe_k_oxidaci = (lm_Struska * mInputData.FeO_p / 100f + Global.M_Odprasky * MINP.FP(mInputData.Odprasky, 61) / MINP.ConversionVector(61) - lm_FeO) * MINP.Mm(32) / MINP.Mm(61);
                #endregion
                #region Entalpie chemických reakcí (R 26..35)
                lH_Si_oxidace = (lm_SZ * lStredni_SZ[1] / MINP.ConversionVector(1) + lm_Srot * lStredni_Srot[1] / MINP.ConversionVector(1)) * MINP.E_Ox1(1) * MINP.Eta_Ox1(1);
                lH_Mn_oxidace = (lm_SZ * lStredni_SZ[2] / MINP.ConversionVector(2) + lm_Srot * lStredni_Srot[2] / MINP.ConversionVector(2)) * MINP.E_Ox1(2) * MINP.Eta_Ox1(2);
                lH_Al_oxidace = (lm_SZ * lStredni_SZ[5] / MINP.ConversionVector(5) + lm_Srot * lStredni_Srot[5] / MINP.ConversionVector(5)) * MINP.E_Ox1(5) * MINP.Eta_Ox1(5);
                lH_Fe_oxidace = lm_Fe_k_oxidaci * MINP.E_Ox1(32) * MINP.Eta_Ox1(32);
                lH_C_oxidace = (lm_SZ * lStredni_SZ[0] / MINP.ConversionVector(0) + lm_Srot * lStredni_Srot[0] / MINP.ConversionVector(0)) * (MINP.E_Ox1(0) * MINP.Eta_Ox1(0) + Global.PostCombustion * MINP.E_Ox2(0) * MINP.Eta_Ox2(0));

                lH_Koks = mInputData.Coke_kg * MINP.FP(mInputData.Coke, 0) / MINP.ConversionVector(0) * (MINP.E_Ox1(0) * MINP.Eta_Ox1(0) + Global.PostCombustion * MINP.E_Ox2(0) * MINP.Eta_Ox2(0));

                #endregion

                #region Výpočet strusky (R 56..61, R 31..35)
                float lm_CaOX = lm_CaO
                    - mInputData.S1_kg * MINP.FP(mInputData.S1, 50) / MINP.ConversionVector(50) * MINP.FP(mInputData.S1, 67) / MINP.ConversionVector(67)
                    - mInputData.S2_kg * MINP.FP(mInputData.S2, 50) / MINP.ConversionVector(50) * MINP.FP(mInputData.S2, 67) / MINP.ConversionVector(67)
                    - mInputData.FOM_kg * MINP.FP(mInputData.FOM, 50) / MINP.ConversionVector(50) * MINP.FP(mInputData.FOM, 67) / MINP.ConversionVector(67)
                    - lm_SZ * lStredni_SZ[50] / MINP.ConversionVector(50) - lm_Srot * lStredni_Srot[50] / MINP.ConversionVector(50);
                float lm_MgOX = lm_MgO
                    - mInputData.S1_kg * MINP.FP(mInputData.S1, 63) / MINP.ConversionVector(63) * MINP.FP(mInputData.S1, 67) / MINP.ConversionVector(67)
                    - mInputData.S2_kg * MINP.FP(mInputData.S2, 63) / MINP.ConversionVector(63) * MINP.FP(mInputData.S2, 67) / MINP.ConversionVector(67)
                    - mInputData.FOM_kg * MINP.FP(mInputData.FOM, 63) / MINP.ConversionVector(63) * MINP.FP(mInputData.FOM, 67) / MINP.ConversionVector(67)
                    - lm_SZ * lStredni_SZ[63] / MINP.ConversionVector(63) - lm_Srot * lStredni_Srot[63] / MINP.ConversionVector(63);

                lm_Dolomite = (lm_CaOX / MINP.FP(mInputData.Lime, 50) * MINP.ConversionVector(50) - lm_MgOX / MINP.FP(mInputData.Lime, 63) * MINP.ConversionVector(63))
                    / (MINP.FP(mInputData.Dolomite, 50) / MINP.FP(mInputData.Lime, 50) - MINP.FP(mInputData.Dolomite, 63) / MINP.FP(mInputData.Lime, 63));
                lm_Lime = (lm_CaOX / MINP.FP(mInputData.Dolomite, 50) * MINP.ConversionVector(50) - lm_MgOX / MINP.FP(mInputData.Dolomite, 63) * MINP.ConversionVector(63))
                    / (MINP.FP(mInputData.Lime, 50) / MINP.FP(mInputData.Dolomite, 50) - MINP.FP(mInputData.Lime, 63) / MINP.FP(mInputData.Dolomite, 63));

                lm_Dolomite = lm_Dolomite * 100 / MINP.FP(mInputData.Dolomite, 67);
                lm_Lime = lm_Lime * 100 / MINP.FP(mInputData.Lime, 67);
                // R 61
                lH_Struskotvorne =
                    mInputData.FOM_kg * (MINP.FP(mInputData.FOM, 70) / MINP.ConversionVector(70) / MINP.FP(mInputData.FOM, 72)) * lT_FOM_69
                    + lm_Dolomite * (MINP.FP(mInputData.Dolomite, 70) / MINP.ConversionVector(70) / MINP.FP(mInputData.Dolomite, 72)) * lT_Dolomit_69
                    + lm_Lime * (MINP.FP(mInputData.Lime, 70) / MINP.ConversionVector(70) / MINP.FP(mInputData.Lime, 72)) * lT_Lime_69
                    + ((mInputData.S1 != null) ? (mInputData.S1_kg * (MINP.FP(mInputData.S1, 70) / MINP.ConversionVector(70) / MINP.FP(mInputData.S1, 72)) * lT_S1_69) : 0)
                    + ((mInputData.S2 != null) ? (mInputData.S2_kg * (MINP.FP(mInputData.S2, 70) / MINP.ConversionVector(70) / MINP.FP(mInputData.S2, 72)) * lT_S2_69) : 0);
                lH_Struskotvorne_Vystup =
                    mInputData.FOM_kg * (MINP.FP(mInputData.FOM, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.FOM, 72)) + MINP.FP(mInputData.FOM, 70) / MINP.ConversionVector(70))
                    + lm_Dolomite * (MINP.FP(mInputData.Dolomite, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.Dolomite, 72)) + MINP.FP(mInputData.Dolomite, 70) / MINP.ConversionVector(70))
                    + lm_Lime * (MINP.FP(mInputData.Lime, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.Lime, 72)) + MINP.FP(mInputData.Lime, 70) / MINP.ConversionVector(70))
                    + ((mInputData.S1 != null) ? (mInputData.S1_kg * (MINP.FP(mInputData.S1, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.S1, 72)) + MINP.FP(mInputData.S1, 70) / MINP.ConversionVector(70))) : 0)
                    + ((mInputData.S2 != null) ? (mInputData.S2_kg * (MINP.FP(mInputData.S2, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.S2, 72)) + MINP.FP(mInputData.S2, 70) / MINP.ConversionVector(70))) : 0);
                // R 32
                lH_Vsazka = lH_SZ + lH_Srot + lH_Struskotvorne + lH_Si_oxidace + lH_Mn_oxidace + lH_Al_oxidace + lH_Fe_oxidace + lH_C_oxidace + lH_Koks;

                // R 33
                lm_Ocel =
                    lm_SZ * (100 - MINP.Sum(lStredni_SZ, 0, 5) - MINP.Sum(lStredni_SZ, 50, 64)) / MINP.ConversionVector(0)
                    + lm_Srot * (100 - MINP.Sum(lStredni_Srot, 0, 5) - MINP.Sum(lStredni_Srot, 50, 64)) / MINP.ConversionVector(0)
                    - lm_Fe_k_oxidaci
                    - Global.M_Odprasky * (MINP.FP(mInputData.Odprasky, 32) / MINP.ConversionVector(32) + MINP.FP(mInputData.Odprasky, 60) / MINP.ConversionVector(60));
                lH_Odprasky = Global.M_Odprasky * (MINP.FP(mInputData.Odprasky, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.Steel, 72)) + MINP.FP(mInputData.Odprasky, 70) / MINP.ConversionVector(70));
                lH_Ocel = lm_Ocel * (MINP.FP(mInputData.Steel, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.Steel, 72)) + MINP.FP(mInputData.Steel, 70) / MINP.ConversionVector(70));
                #endregion

                if (lAmountEnough)
                {
                    lLastCalculationFinished = true;
                    continue;
                }

                #region Enough, Last calculation? (R 36)
                if (lH_Vsazka > lH_Ocel + Global.H_Akumulace + Global.TauTavby * Global.ZtratovyVykon / 60 + lH_Odprasky + lH_Struskotvorne_Vystup)
                {
                    lm_Srot += lKrok_m_Srot;
                }
                else
                {
                    lAmountEnough = true;

                    // real amounts
                    float lCoef = 0;

                    if (mInputData.HotMetal_t.HasValue)
                    {
                        lCoef = mInputData.HotMetal_t.Value / lm_SZ;
                    }
                    else if (mInputData.Scrap_t.HasValue)
                    {
                        lCoef = mInputData.Scrap_t.Value / lm_Srot;
                    }
                    else if (mInputData.Total_t.HasValue)
                    {
                        lCoef = mInputData.Total_t.Value / lm_Ocel;
                    }

                    lm_SZ = lm_SZ * lCoef * 1000;
                    lm_Srot = lm_Srot * lCoef * 1000;
                    lm_Ocel = lm_Ocel * lCoef * 1000;
                }
                #endregion

                if (OutputCSVFileName != null)
                {
                    lSW.WriteLine("{0};{1};{2};{3}", lm_Srot, lH_Vsazka, lH_Ocel, lH_Srot);
                }
            }

            if (OutputCSVFileName != null)
            {
                lSW.Close();
            }

            #region Prepocet a vysledek (R 37..55)

            lm_SZ = lm_SZ / 1000;
            lm_Srot = lm_Srot / 1000;
            lm_Ocel = lm_Ocel / 1000;

            lOutputData.CalculationHotMetal_t = (int)Math.Round(lm_SZ);
            lOutputData.CalculationScrap_t = (int)Math.Round(lm_Srot);
            lOutputData.CalculationTotal_t = (int)Math.Round(lm_Ocel);

            lOutputData.Scrap_kg = new int[Global.SCRAPYARDS_COUNT];

            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++)
            {
                lOutputData.Scrap_kg[i] = (int)Math.Round(lm_Srot * lPodil_Srot[i] / lSuma_podilu_SROT);
            }

            lOutputData.HotMetal_t = new int[Global.HOTMETAL_COUNT];

            for (int i = 0; i < Global.HOTMETAL_COUNT; i++)
            {
                lOutputData.HotMetal_t[i] = (int)Math.Round(lm_SZ * lPodil_SZ[i] / lSuma_podilu_SZ);
            }

            // doprovodne prvky
            int[] lIndexes = new int[] { 6, 8, 9, 14, 16, 20, 21, 23 };

            for (int i = 0; i < lIndexes.Length; i++)
            {
                float lResult = (lm_SZ * lStredni_SZ[lIndexes[i]] + lm_Srot * lStredni_Srot[lIndexes[i]]) / lm_Ocel;

                switch (i)
                {
                    case 0: lOutputData.FinalCu_p = lResult; break;
                    case 1: lOutputData.FinalMo_p = lResult; break;
                    case 2: lOutputData.FinalNi_p = lResult; break;
                    case 3: lOutputData.FinalCo_p = lResult; break;
                    case 4: lOutputData.FinalW_p = lResult; break;
                    case 5: lOutputData.FinalAs_p = lResult; break;
                    case 6: lOutputData.FinalSn_p = lResult; break;
                    case 7: lOutputData.FinalSb_p = lResult; break;
                }
            }

            #endregion
            #region Slag (R 2-51 .. R 2-56)

            lOutputData.Coke_kg = mInputData.Coke_kg;
            lOutputData.SlagLime_kg = (int)Math.Round(lm_Lime);
            lOutputData.SlagDolomit_kg = (int)Math.Round(lm_Dolomite);
            lOutputData.SlagFOM_kg = mInputData.FOM_kg;
            lOutputData.SlagS1_kg = mInputData.S1_kg;
            lOutputData.SlagS2_kg = mInputData.S2_kg;
            #endregion

            lOutputData.H_Vsazka = lH_Vsazka;
            lOutputData.H_SZ = lH_SZ;
            lOutputData.H_Srot = lH_Srot;
            lOutputData.H_Ocel = lH_Ocel;
            lOutputData.H_Struskotvorne = lH_Struskotvorne;
            lOutputData.H_Struskotvorne_Vystup = lH_Struskotvorne_Vystup;
            lOutputData.H_Si_oxidace = lH_Si_oxidace;
            lOutputData.H_Mn_oxidace = lH_Mn_oxidace;
            lOutputData.H_Al_oxidace = lH_Al_oxidace;
            lOutputData.H_Fe_oxidace = lH_Fe_oxidace;
            lOutputData.H_C_oxidace = lH_C_oxidace;
            lOutputData.H_Koks = lH_Koks;
            lOutputData.H_Odprasky = lH_Odprasky;
            lOutputData.T_Struska = lT_Struska;

            return lOutputData;
        }
    }
}
