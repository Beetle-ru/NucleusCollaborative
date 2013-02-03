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
        public double O2_M2_TForecast;
        public DateTime? T1_Time;
        public int T1_Measured;
        public int T1;
        public DateTime? T2_Time;
        public int T2_Measured;
        public int T2;
        public double T1C1_Measured;
        public double T1C1;
        public double T2C2_Measured;
        public double T2C2;
        public double C_Model_Correction;
        public DateTime? Steel_Sample_Time;
        public double Steel_Mn_Sample;
        public double Steel_Mn;
        public double Steel_Si_Sample;
        public double Steel_Si;
        public DateTime? Slag_Sample_Time;
        public double Slag_FeO_Sample;
        public double Slag_FeO;
        public double Slag_SiO2_Sample;
        public double Slag_SiO2;
        public double Slag_CaO_Sample;
        public double Slag_CaO;
        public double Slag_B_Sample;
        public double Slag_B;

        public double HM2_Vsazka;
        public double HM2_C_oxidace;
        public double HM2_Koks;
        public double HM2_Si_oxidace;
        public double HM2_Mn_oxidace;
        public double HM2_Al_oxidace;
        public double HM2_Fe_oxidace;

        // total
        public double HM3_Vsazka;
        public double HM3_C_oxidace;
        public double HM3_Si_oxidace;
        public double HM3_Mn_oxidace;
        public double HM3_Al_oxidace;
        public double HM3_Fe_oxidace;
    }
}
