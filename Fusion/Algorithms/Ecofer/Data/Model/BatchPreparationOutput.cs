using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class BatchPreparationOutput
    {
        public int? CalculationHotMetal_t { get; set; }
        public int? CalculationScrap_t { get; set; }
        public int? CalculationTotal_t { get; set; }
        public int[] HotMetal_t { get; set; }
        public int SlagLime_kg { get; set; }
        public int SlagDolomit_kg { get; set; }
        public int SlagFOM_kg { get; set; }
        public int SlagS1_kg { get; set; }
        public int SlagS2_kg { get; set; }
        public int Coke_kg { get; set; }
        public int[] Scrap_kg { get; set; }
        public double FinalCu_p { get; set; }
        public double FinalMo_p { get; set; }
        public double FinalNi_p { get; set; }
        public double FinalW_p { get; set; }
        public double FinalCo_p { get; set; }
        public double FinalAs_p { get; set; }
        public double FinalSb_p { get; set; }
        public double FinalSn_p { get; set; }

        public double H_Vsazka;
        public double H_SZ;
        public double H_Srot;
        public double H_Ocel;
        public double H_Struskotvorne;
        public double H_Struskotvorne_Vystup;
        public double H_Si_oxidace;
        public double H_Mn_oxidace;
        public double H_Al_oxidace;
        public double H_Fe_oxidace;
        public double H_C_oxidace;
        public double H_Koks;
        public double H_Odprasky;
        public double T_Struska;
    }
}
