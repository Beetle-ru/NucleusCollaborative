using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Data;

namespace Models
{
    public class Charging
    {
        private Data.Model.ChargingInput mInputData;

        public Charging(Data.Model.ChargingInput aInputData)
        {
            mInputData = aInputData;
        }
        /// <summary>
        /// Returns ratio between DOlomit MgO and Dolomit S MgO if both of them are available.
        /// Expects Dolomit S as Slag former 1.
        /// </summary>
        /// <returns></returns>
        public static float? GetDomolitReplacementCoef()
        {
            DTO.MINP_GD_MaterialDTO lDolom_DTO = Data.MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.Dolomite];
            DTO.MINP_GD_MaterialDTO lDolomS_DTO = Data.MINP.MINP_GD_ModelMaterials[Common.Enumerations.MINP_GD_Material_ModelMaterial.SlagFormer1];

            if (lDolom_DTO != null && lDolomS_DTO != null)
            {
                try
                {
                    return (float?)(lDolom_DTO.MINP_GD_MaterialItems.Single(aR => aR.MINP_GD_MaterialElement.Index == Common.ElementIndex.Slag_MgO).Amount_p /
                        lDolomS_DTO.MINP_GD_MaterialItems.Single(aR => aR.MINP_GD_MaterialElement.Index == Common.ElementIndex.Slag_MgO).Amount_p);
                }
                catch { };
            }

            return null;
        }

        public Data.Model.ChargingOutput Run()
        {
            Data.Model.ChargingOutput lOutputData = new Data.Model.ChargingOutput();

            lOutputData.ReplaceDolomitCoef = null;

            float lSuma_m_SZ_real = 0;
            float[] lStredni_SZ = new float[Global.MATERIALELEMENTS_COUNT];
            float[] lPodil_SZ = new float[Global.HOTMETAL_COUNT];
            float lSuma_m_SROT_real = 0;
            float[] lStredni_Srot = new float[Global.MATERIALELEMENTS_COUNT];
            float[] lPodil_Srot = new float[Global.SCRAPYARDS_COUNT];

            float lH_SZ = 0;
            float lH_Srot = 0;
            float lH_Struskotvorne = 0;
            float lH_Struskotvorne_Vystup = 0;

            float lm_SiO2 = 0;
            float lm_MnO = 0;
            float lm_Al2O3 = 0;
            float lm_CaO = 0;
            float lm_Struska = 0;
            float lm_MgO = 0;
            float lm_FeO = 0;
            float lm_Fe_k_oxidaci = 0;

            float lm_Ocel = 0;
            float lH_Si_oxidace = 0;
            float lH_Mn_oxidace = 0;
            float lH_Al_oxidace = 0;
            float lH_Fe_oxidace = 0;
            float lH_C_oxidace = 0;
            float lH_Koks = 0;
            float lH_Vsazka = 0;
            float lH_Ocel = 0;
            float lH_Odprasky = 0;
            float lH_Ztraty = Common.Global.H_Ztraty;

            float lV_Oxygen_C = 0;
            float lV_Oxygen_Si = 0;
            float lV_Oxygen_Mn = 0;
            float lV_Oxygen_P = 0;
            float lV_Oxygen_Al = 0;
            float lV_Oxygen_Fe = 0;
            float lV_Oxygen_Total = 0;

            float lT_Forecast = 0;
            float lT_Reference = Global.T_Reference;

            // initialization in kg
            for (int i = 0; i < Global.HOTMETAL_COUNT; i++) lStredni_SZ[i] = 0;
            for (int i = 0; i < Global.HOTMETAL_COUNT; i++) lPodil_SZ[i] = (mInputData.HotMetals_t[i] > 0 && mInputData.HotMetals[i] != null) ? mInputData.HotMetals_t[i] * 1000 : 0;
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++) lStredni_Srot[i] = 0;
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++) lPodil_Srot[i] = (mInputData.Scraps_t[i] > 0 && mInputData.Scraps[i] != null) ? mInputData.Scraps_t[i] * 1000 : 0;

            // *****************************
            // Model calculation
            // *****************************
            #region Stredni SZ (R 1..5)
            for (int i = 0; i < Global.HOTMETAL_COUNT; i++)
            {
                if (lPodil_SZ[i] == 0) continue;

                lSuma_m_SZ_real += lPodil_SZ[i];

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
                lStredni_SZ[iElement] = lStredni_SZ[iElement] / lSuma_m_SZ_real;
            }
            #endregion
            #region Stredni SROT (R 6..10)
            for (int i = 0; i < Global.SCRAPYARDS_COUNT; i++)
            {
                if (lPodil_Srot[i] == 0) continue;

                lSuma_m_SROT_real += lPodil_Srot[i];

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
                lStredni_Srot[iElement] = lStredni_Srot[iElement] / lSuma_m_SROT_real;
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

            #region Calculation of lime and dolomite (R 11..21, R30)
            lm_SiO2 = (lSuma_m_SZ_real * lStredni_SZ[1] / MINP.ConversionVector(1) + lSuma_m_SROT_real * lStredni_Srot[1] / MINP.ConversionVector(1)) * MINP.Mm(51) / MINP.Mm(1)
                + lSuma_m_SZ_real * lStredni_SZ[51] / MINP.ConversionVector(51) + lSuma_m_SROT_real * lStredni_Srot[51] / MINP.ConversionVector(51);
            lm_MnO = (lSuma_m_SZ_real * lStredni_SZ[2] / MINP.ConversionVector(2) + lSuma_m_SROT_real * lStredni_Srot[2] / MINP.ConversionVector(2)) * MINP.Mm(53) / MINP.Mm(2)
                + lSuma_m_SZ_real * lStredni_SZ[53] / MINP.ConversionVector(53) + lSuma_m_SROT_real * lStredni_Srot[53] / MINP.ConversionVector(53);
            lm_Al2O3 = (lSuma_m_SZ_real * lStredni_SZ[5] / MINP.ConversionVector(5) + lSuma_m_SROT_real * lStredni_Srot[5] / MINP.ConversionVector(5)) * MINP.Mm(62) / MINP.Mm(5) / 2
                + lSuma_m_SZ_real * lStredni_SZ[62] / MINP.ConversionVector(62) + lSuma_m_SROT_real * lStredni_Srot[62] / MINP.ConversionVector(62);
            lm_CaO = lm_SiO2 * mInputData.Basicity;
            lm_Struska = (lm_CaO + lm_SiO2 + lm_MnO + lm_Al2O3) * 100f / (100 - mInputData.FeO_p - mInputData.MgO_p);
            lm_MgO = lm_Struska * mInputData.MgO_p / 100f;
            // R 16
            float lm_CaOX = lm_CaO
                - mInputData.S1_kg * MINP.FP(mInputData.S1, 50) / MINP.ConversionVector(50) * MINP.FP(mInputData.S1, 67) / MINP.ConversionVector(67)
                - mInputData.S2_kg * MINP.FP(mInputData.S2, 50) / MINP.ConversionVector(50) * MINP.FP(mInputData.S2, 67) / MINP.ConversionVector(67)
                - mInputData.FOM_kg * MINP.FP(mInputData.FOM, 50) / MINP.ConversionVector(50) * MINP.FP(mInputData.FOM, 67) / MINP.ConversionVector(67)
                - lSuma_m_SZ_real * lStredni_SZ[50] / MINP.ConversionVector(50) - lSuma_m_SROT_real * lStredni_Srot[50] / MINP.ConversionVector(50);

            float lm_MgOX = lm_MgO
                - mInputData.S1_kg * MINP.FP(mInputData.S1, 63) / MINP.ConversionVector(63) * MINP.FP(mInputData.S1, 67) / MINP.ConversionVector(67)
                - mInputData.S2_kg * MINP.FP(mInputData.S2, 63) / MINP.ConversionVector(63) * MINP.FP(mInputData.S2, 67) / MINP.ConversionVector(67)
                - mInputData.FOM_kg * MINP.FP(mInputData.FOM, 63) / MINP.ConversionVector(63) * MINP.FP(mInputData.FOM, 67) / MINP.ConversionVector(67)
                - lSuma_m_SZ_real * lStredni_SZ[63] / MINP.ConversionVector(63) - lSuma_m_SROT_real * lStredni_Srot[63] / MINP.ConversionVector(63);

            // R 18
            float lm_Dolomite = (lm_CaOX / MINP.FP(mInputData.Lime, 50) * MINP.ConversionVector(50) - lm_MgOX / MINP.FP(mInputData.Lime, 63) * MINP.ConversionVector(63))
                / (MINP.FP(mInputData.Dolomite, 50) / MINP.FP(mInputData.Lime, 50) - MINP.FP(mInputData.Dolomite, 63) / MINP.FP(mInputData.Lime, 63));
            float lm_Lime = (lm_CaOX / MINP.FP(mInputData.Dolomite, 50) * MINP.ConversionVector(50) - lm_MgOX / MINP.FP(mInputData.Dolomite, 63) * MINP.ConversionVector(63))
                / (MINP.FP(mInputData.Lime, 50) / MINP.FP(mInputData.Dolomite, 50) - MINP.FP(mInputData.Lime, 63) / MINP.FP(mInputData.Dolomite, 63));

            // R20
            lm_Dolomite = lm_Dolomite * 100 / MINP.FP(mInputData.Dolomite, 67);
            lm_Lime = lm_Lime * 100 / MINP.FP(mInputData.Lime, 67);

            // More then reqested conditions
            if (lm_CaOX < 0 && lm_MgOX < 0)
            {
                lOutputData.ErrCode = -1;
                lm_Lime = 0;
                lm_Dolomite = 0;
            }
            else if (lm_CaOX < 0 && lm_MgOX > 0)
            {
                lOutputData.ErrCode = -2;
                lm_Dolomite = 0;
                lm_Lime = 0;
            }
            else if (lm_CaOX > 0 && lm_MgOX < 0)
            {
                lOutputData.ErrCode = -3;
                lm_Dolomite = 0;
                lm_Lime = lm_CaOX / MINP.FP(mInputData.Lime, 50) / MINP.FP(mInputData.Lime, 67);
            }
            else
            {
                lOutputData.ErrCode = 0;
            }

            if (lm_Dolomite < 0)
            {
                lm_Dolomite = 0;
                lOutputData.ErrCode = -2;
            }
            if (lm_Lime < 0)
            {
                lm_Lime = 0;
                lOutputData.ErrCode = -3;
            }
            #endregion
            #region Calculation of temperature (R 22..24)
            if (lT_StredniSZ_69 > lStredni_SZ[72])
                lH_SZ = lSuma_m_SZ_real * (lStredni_SZ[70] / MINP.ConversionVector(70) + lStredni_SZ[71] / MINP.ConversionVector(71) * (lT_StredniSZ_69 - lStredni_SZ[72]));
            else
                lH_SZ = lSuma_m_SZ_real * (lStredni_SZ[70] / MINP.ConversionVector(70) / lStredni_SZ[72]) * lT_StredniSZ_69;
            
            lH_Srot = lSuma_m_SROT_real * (lStredni_Srot[70] / MINP.ConversionVector(70) / lStredni_Srot[72]) * lT_StredniSrot_69;
            // R24
            lm_FeO = lSuma_m_SZ_real * lStredni_SZ[61] / MINP.ConversionVector(61) + lSuma_m_SROT_real * lStredni_Srot[61] / MINP.ConversionVector(61);
            lm_Fe_k_oxidaci = (lm_Struska * mInputData.FeO_p / 100f + Global.M_Odprasky * MINP.FP(mInputData.Odprasky, 61) / MINP.ConversionVector(61) - lm_FeO) * MINP.Mm(32) / MINP.Mm(61);
            #endregion
            #region Entalpie chemickych reakci (R 25..35)
            lH_Si_oxidace = (lSuma_m_SZ_real * lStredni_SZ[1] / MINP.ConversionVector(1) + lSuma_m_SROT_real * lStredni_Srot[1] / MINP.ConversionVector(1)) * MINP.E_Ox1(1) * MINP.Eta_Ox1(1);
            lH_Mn_oxidace = (lSuma_m_SZ_real * lStredni_SZ[2] / MINP.ConversionVector(2) + lSuma_m_SROT_real * lStredni_Srot[2] / MINP.ConversionVector(2)) * MINP.E_Ox1(2) * MINP.Eta_Ox1(2);
            lH_Al_oxidace = (lSuma_m_SZ_real * lStredni_SZ[5] / MINP.ConversionVector(5) + lSuma_m_SROT_real * lStredni_Srot[5] / MINP.ConversionVector(5)) * MINP.E_Ox1(5) * MINP.Eta_Ox1(5);
            lH_Fe_oxidace = lm_Fe_k_oxidaci * MINP.E_Ox1(32) * MINP.Eta_Ox1(32);
            lH_C_oxidace = (lSuma_m_SZ_real * lStredni_SZ[0] / MINP.ConversionVector(0) + lSuma_m_SROT_real * lStredni_Srot[0] / MINP.ConversionVector(0)) * (MINP.E_Ox1(0) * MINP.Eta_Ox1(0) + Global.PostCombustion * MINP.E_Ox2(0) * MINP.Eta_Ox2(0));

            lH_Koks = mInputData.Coke_kg * MINP.FP(mInputData.Coke, 0) / MINP.ConversionVector(0) * (MINP.E_Ox1(0) * MINP.Eta_Ox1(0) + Global.PostCombustion * MINP.E_Ox2(0) * MINP.Eta_Ox2(0));
            
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
            // R 31
            lH_Vsazka = lH_SZ + lH_Srot + lH_Struskotvorne + lH_Si_oxidace + lH_Mn_oxidace + lH_Al_oxidace + lH_Fe_oxidace + lH_C_oxidace + lH_Koks;

            lm_Ocel =
                lSuma_m_SZ_real * (100 - MINP.Sum(lStredni_SZ, 0, 5) - MINP.Sum(lStredni_SZ, 50, 64)) / MINP.ConversionVector(0)
                + lSuma_m_SROT_real * (100 - MINP.Sum(lStredni_Srot, 0, 5) - MINP.Sum(lStredni_Srot, 50, 64)) / MINP.ConversionVector(0)
                - lm_Fe_k_oxidaci
                - Global.M_Odprasky * (MINP.FP(mInputData.Odprasky, 32) / MINP.ConversionVector(32) + MINP.FP(mInputData.Odprasky, 60) / MINP.ConversionVector(60));

            lH_Odprasky = Global.M_Odprasky * (MINP.FP(mInputData.Odprasky, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.Steel, 72)) + MINP.FP(mInputData.Odprasky, 70) / MINP.ConversionVector(70));
            lH_Ocel = lm_Ocel * (MINP.FP(mInputData.Steel, 71) / MINP.ConversionVector(71) * (mInputData.Final_Temperature - MINP.FP(mInputData.Steel, 72)) + MINP.FP(mInputData.Steel, 70) / MINP.ConversionVector(70));

            lT_Forecast = mInputData.Final_Temperature 
                + (lH_Vsazka - lH_Ocel - lH_Struskotvorne_Vystup - Global.H_Akumulace - Global.TauTavby * Global.ZtratovyVykon / 60 - lH_Odprasky)
                / 
                (
                    lm_Ocel * MINP.FP(mInputData.Steel, 71) / MINP.ConversionVector(71) 
                    + Global.M_Odprasky * MINP.FP(mInputData.Odprasky, 71) / MINP.ConversionVector(71) 
                    + lm_Struska * MINP.FP(mInputData.StrStr, 71) / MINP.ConversionVector(71)
                );

            #endregion
            #region Calculation of Oxygen (R 42..51)
            lV_Oxygen_C = (lSuma_m_SZ_real * lStredni_SZ[0] / MINP.ConversionVector(0) + lSuma_m_SROT_real * lStredni_Srot[0] / MINP.ConversionVector(0) + mInputData.Coke_kg * MINP.FP(mInputData.Coke, 0) / MINP.ConversionVector(0))
                / MINP.Mm(0) * MINP.O2_Stechio(0) * (1 + Global.PostCombustion);
            lV_Oxygen_Si = (lSuma_m_SZ_real * lStredni_SZ[1] / MINP.ConversionVector(1) + lSuma_m_SROT_real * lStredni_Srot[1] / MINP.ConversionVector(1))
                / MINP.Mm(1) * MINP.O2_Stechio(1);
            lV_Oxygen_Mn = (lSuma_m_SZ_real * lStredni_SZ[2] / MINP.ConversionVector(2) + lSuma_m_SROT_real * lStredni_Srot[2] / MINP.ConversionVector(2))
                / MINP.Mm(2) * MINP.O2_Stechio(2);
            lV_Oxygen_P = (lSuma_m_SZ_real * lStredni_SZ[3] / MINP.ConversionVector(3) + lSuma_m_SROT_real * lStredni_Srot[3] / MINP.ConversionVector(3))
                / MINP.Mm(3) * MINP.O2_Stechio(3);
            lV_Oxygen_Al = (lSuma_m_SZ_real * lStredni_SZ[5] / MINP.ConversionVector(5) + lSuma_m_SROT_real * lStredni_Srot[5] / MINP.ConversionVector(5))
                / MINP.Mm(5) * MINP.O2_Stechio(5);
            lV_Oxygen_Fe = lm_Fe_k_oxidaci / MINP.Mm(32) * MINP.O2_Stechio(32);
            lV_Oxygen_Total = lV_Oxygen_C + lV_Oxygen_Si + lV_Oxygen_Mn + lV_Oxygen_P + lV_Oxygen_Al + lV_Oxygen_Fe;

            lOutputData.OxygenAmountTotal1stStep_Nm3 = lV_Oxygen_Total;
            lOutputData.OxygenAmountC_Nm3 = lV_Oxygen_C;
            lOutputData.OxygenAmountSi_Nm3 = lV_Oxygen_Si;
            lOutputData.OxygenAmountMn_Nm3 = lV_Oxygen_Mn;
            lOutputData.OxygenAmountP_Nm3 = lV_Oxygen_P;
            lOutputData.OxygenAmountAl_Nm3 = lV_Oxygen_Al;
            lOutputData.OxygenAmountFe_Nm3 = lV_Oxygen_Fe;
            lOutputData.ForecastTemperature_C = lT_Forecast;

            if (lT_Forecast <= mInputData.Final_Temperature - 5)
            {
                lV_Oxygen_Total = lV_Oxygen_Total + (mInputData.Final_Temperature - lT_Forecast) * Global.Coef_O2_Heating;
            }
            #endregion
            
            lOutputData.OxygenAmountTotalEnd_Nm3 = (int)Math.Round(lV_Oxygen_Total);

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

            lOutputData.m_lime = lm_Lime;
            lOutputData.m_dolomite = lm_Dolomite;

            return lOutputData;
        }
    }
}
