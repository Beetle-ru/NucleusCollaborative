using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public static class Global
    {
        static Global()
        {
            M3_TZ_A = new Dictionary<Enumerations.M3ElementEnum, double>();
            M3_TZ_A.Add(Enumerations.M3ElementEnum.Si, -902500);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.Mn, -770370);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.Al, -1122690);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.Cr, -755047);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.P, -592430);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.Ti, -938000);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.V, -708090);
            M3_TZ_A.Add(Enumerations.M3ElementEnum.Fe, -479970);

            M3_TZ_C = new Dictionary<Enumerations.M3ElementEnum, double>();
            M3_TZ_C.Add(Enumerations.M3ElementEnum.Si, 173.72f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.Mn, 176.1f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.Al, 217.21f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.Cr, 171.27f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.P, 171.65f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.Ti, 175.52f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.V, 156.32f);
            M3_TZ_C.Add(Enumerations.M3ElementEnum.Fe, 99.14f);

            M3_KF = new Dictionary<Enumerations.M3ElementEnum, double>();
            M3_KF.Add(Enumerations.M3ElementEnum.Si, 0.001f);
            M3_KF.Add(Enumerations.M3ElementEnum.Mn, 1);
            M3_KF.Add(Enumerations.M3ElementEnum.Al, 0.1f);
            M3_KF.Add(Enumerations.M3ElementEnum.Cr, 1);
            M3_KF.Add(Enumerations.M3ElementEnum.P, 10000);
            M3_KF.Add(Enumerations.M3ElementEnum.Ti, 1);
            M3_KF.Add(Enumerations.M3ElementEnum.V, 1);
            M3_KF.Add(Enumerations.M3ElementEnum.Fe, 1);

            M3_c_kov_min_p = new Dictionary<Enumerations.M3ElementEnum, double>();
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.Si, 0);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.Mn, 0);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.Al, 0);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.Cr, 0);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.P, 0.005f);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.Ti, 0);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.V, 0);
            M3_c_kov_min_p.Add(Enumerations.M3ElementEnum.Fe, 0);
        }

        public static int CONVERTER_NO = 1;

        public static int MATERIALELEMENTS_COUNT = 74;
        public static int MATERIALELEMENTS_STEEL_COUNT = 50;
        public static int MATERIALELEMENTS_STEEL_TO_FE_COUNT = 33;
        public static int MATERIALELEMENTS_SLAG_COUNT = 15;
        public static int MATERIALELEMENTS_SLAG_STARTINDEX = 50;
        public static int MATERIALELEMENTS_STEELANDSLAG_COUNT = 65;
        public static int HOTMETAL_COUNT = 3;
        public static int SCRAPYARDS_COUNT = 12;

        public static int ProcessPattern_OxygenAmount_Main_Delta = 5000;
        public static int ProcessPattern_OxygenAmount_TempMeas_Delta = 500;
        public static int ProcessPattern_OxygenAmount_Correction_Delta = 1000;
        public static int ProcessPattern_OxygenAmount_End_Delta = 1000;

        public static Dictionary<Enumerations.M3ElementEnum, double> M3_c_kov_min_p;
        public static Dictionary<Enumerations.M3ElementEnum, double> M3_TZ_A;
        public static Dictionary<Enumerations.M3ElementEnum, double> M3_TZ_C;
        public static Dictionary<Enumerations.M3ElementEnum, double> M3_KF;

        public static double M3_K_273 = 273;
        public static double M3_K_8314 = 8.314f;

        public static bool M3_GenerateOutputFile = true;
        public static string M3_GenerateOutputFileDirectory = @"C:\ECOFER";
        public static int Application_L2BatchPrepairing_HeatAimDataDays = -7;

        // MODEL PARAMETERS

        public static double Step_m_Scrap_kg = 100;
        public static double Coef_O2_Heating = 500f / 15; // m3/°C
        public static double PostCombustion = 0.05f;
        public static double T_Reference = 1550;
        public static double H_Ztraty = 0;

        public static double M3_SimulationWasteGas_Podil_na_C = 0.95f;
        public static double M3_SimulationWasteGas_C_kov_zlom = 0.0015f;
        public static double M3_V_Wastegas = 0.24f; //1.4f;

        public static double M3_End_Condition_O2_Min = 90;
        public static double M3_End_Condition_O2_Aim = 100;
        public static double M3_End_Condition_O2_Max = 100;
        public static double M3_End_Condition_K2_Min = 90;
        public static double M3_End_Condition_K2_Aim = 100;
        public static double M3_End_Condition_K2_Max = 105;

        public static int M3_DeltaT_s = 5;

        public static double M3_O_C = 5000;  // m3/%C
        public static double M3_O_T = 33;    // m3/°C

        public static bool M3_Stat_C_ON = false;
        public static double M3_Stat_C_konec = 0.04f;
        public static double M3_Stat_C_konec_random = 0.01f;
        public static double M3_Stat_OpozdeniKonceFoukani = 50;

        public static bool M3_Stat_T_ON = true;
        public static double M3_Stat_Ztratovy_vykon_lin = 7.92f * 0.0625f; // kWh/min/K
        public static double M3_Stat_Ztratovy_vykon_kvad = 0.0528f * 0.0625f; // kWh/min/K/K
        public static double M3_Stat_Ztratovy_vykon_kub = 0.000634f * 0.0625f; // kWh/min/K/K/K
        public static double M3_Stat_T_korekce = 1600;

        public static bool M3_CO2_Buffer = true;

        public static double M_Odprasky = 3000;// 3000; // kg
        public static double TauTavby = 15; // min
        public static double ZtratovyVykon = 300; // kWh/min
        public static double H_Akumulace = 18000; //kWh    

        public static double MA_Step = 0.05f;
        public static double MA_Posun = 1;
        public static double MA_DeltaK = 10;
        public static double MA_Eps = 10;

        public static double M2_Dolom_Temp = 40;

        public static double SIM_WastegasFlow_Conversion = 0.002857f;
    }
}
