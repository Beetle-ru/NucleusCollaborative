using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Simulation
{
    public class RealTimeResult
    {
        public int M_Scrap_t;
        public int M_HotMetal_t;
        public int O2_Main;
        public int O2_Correction;
        public int O2_M2_FirstStep;
        public int O2_M2_Calc;
        public float O2_M2_TForecast;
        public DateTime? T1_Time;
        public int T1_Measured;
        public int T1;
        public DateTime? T2_Time;
        public int T2_Measured;
        public int T2;
        public float T1C1_Measured;
        public float T1C1;
        public float T2C2_Measured;
        public float T2C2;
        public float C_Model_Correction;
        public DateTime? Steel_Sample_Time;
        public float Steel_Mn_Sample;
        public float Steel_Mn;
        public float Steel_Si_Sample;
        public float Steel_Si;
        public DateTime? Slag_Sample_Time;
        public float Slag_FeO_Sample;
        public float Slag_FeO;
        public float Slag_SiO2_Sample;
        public float Slag_SiO2;
        public float Slag_CaO_Sample;
        public float Slag_CaO;
        public float Slag_B_Sample;
        public float Slag_B;

        public float HM2_Vsazka;
        public float HM2_C_oxidace;
        public float HM2_Koks;
        public float HM2_Si_oxidace;
        public float HM2_Mn_oxidace;
        public float HM2_Al_oxidace;
        public float HM2_Fe_oxidace;

        // total
        public float HM3_Vsazka;
        public float HM3_C_oxidace;
        public float HM3_Si_oxidace;
        public float HM3_Mn_oxidace;
        public float HM3_Al_oxidace;
        public float HM3_Fe_oxidace;
    }
}
