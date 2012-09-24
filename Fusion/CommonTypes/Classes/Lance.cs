using System;

namespace CommonTypes.Classes
{
    [Serializable]
    public class Lance
    {
        public DateTime Date { set; get; }
        public string PhaseNo { set; get; }
        public double O2Vol { set; get; }
        public double O2Flow { set; get; }
        public double O2Pressure { set; get; }
        public int Height { set; get; }
        public int Id { get; set; }
        public int FusionId { get; set; }
        public double O2RightLanceLeck { set; get; }              // O2 прав.фурма течь      # ACT_CX_LECK_R
        public double O2RightLanceWaterPressure { set; get; }    // O2 прав.фурма давление воды # ACT_CX_PWASS_R
        public double O2RightLanceWaterInput { set; get; }       // O2 прав.фурма Q воды вход      # ACT_CX_QWASSZU_R
        public double O2RightLanceWaterOutput { set; get; }      // O2 прав.фурма Q воды слив      # ACT_CX_QWASSAB_R
        public double O2RightLanceWaterTempInput { set; get; }   // O2 прав.фурма T воды вход      # ACT_CX_TWASSZU_R
        public double O2RightLanceWaterTempOutput { set; get; }   // O2 прав.фурма T воды слив     # ACT_CX_TWASSAB_R
        public double O2RightLanceGewWeight { set; get; }        // O2 прав.фурма вес           # ACT_CX_GEWLANCE_R
        public double O2RightLanceGewBaer { set; get; }          // O2 прав.фурма настыль       # ACT_CX_GEWBAER_R
        public double O2LeftLanceWaterInput { set; get; }    // O2 лев.фурма Q воды вход     # ACT_CX_QWASSZU_L
        public double O2LeftLanceWaterOutput { set; get; }   // O2 лев.фурма Q воды слив     # ACT_CX_QWASSAB_L
        public double O2LeftLanceWaterTempInput { set; get; }   // O2 лев.фурма T воды вход  # ACT_CX_TWASSZU_L
        public double O2LeftLanceWaterTempOutput { set; get; }   // O2 лев.фурма T воды слив # ACT_CX_TWASSAB_L
        public double O2LeftLanceLeck { set; get; }              // O2 лев.фурма течь        # ACT_CX_LECK_L
        public double O2LeftLanceGewWeight { set; get; }         // O2 лев.фурма вес            # ACT_CX_GEWLANCE_L
        public double O2LeftLanceGewBaer { set; get; }           // O2 лев.фурма настыль        # ACT_CX_GEWBAER_L
        public double O2LeftLanceWaterPressure { set; get; }    // O2 лев.фурма давление воды   # ACT_CX_PWASS_L
    }
}
