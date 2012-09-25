using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataGathering
{
    public class Lance
    {
        public Lance()
        {
        }

        private int m_Id;

        public int Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }

        private int m_FusionId;

        public int FusionId
        {
            get { return m_FusionId; }
            set { m_FusionId = value; }
        }

        private DateTime m_Date;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        

        private double m_O2Vol;

        public double O2Vol
        {
            get { return m_O2Vol; }
            set { m_O2Vol = value; }
        }

        private double m_O2Flow;

        public double O2Flow
        {
            get { return m_O2Flow; }
            set { m_O2Flow = value; }
        }

        private double m_O2Pressure;

        public double O2Pressure
        {
            get { return m_O2Pressure; }
            set { m_O2Pressure = value; }
        }

        private int m_Height;

        public int Height
        {
            get { return m_Height; }
            set { m_Height = value; }
        }

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
