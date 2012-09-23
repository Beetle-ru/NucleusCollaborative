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
        public float FinalCu_p { get; set; }
        public float FinalMo_p { get; set; }
        public float FinalNi_p { get; set; }
        public float FinalW_p { get; set; }
        public float FinalCo_p { get; set; }
        public float FinalAs_p { get; set; }
        public float FinalSb_p { get; set; }
        public float FinalSn_p { get; set; }

        public float H_Vsazka;
        public float H_SZ;
        public float H_Srot;
        public float H_Ocel;
        public float H_Struskotvorne;
        public float H_Struskotvorne_Vystup;
        public float H_Si_oxidace;
        public float H_Mn_oxidace;
        public float H_Al_oxidace;
        public float H_Fe_oxidace;
        public float H_C_oxidace;
        public float H_Koks;
        public float H_Odprasky;
        public float T_Struska;
    }
}
