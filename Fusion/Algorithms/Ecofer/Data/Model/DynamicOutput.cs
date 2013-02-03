using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class DynamicOutput
    {
        public double m_Tavby;
        public double E_Tavby;
        public double T_Tavby;
        public double FP_C;
        public double[] FP_Tavby;
        public double[] m_SlozkaTavby;

        public double m_Struska;
        public double m_Kov;
        public double[] FP_Struska;
        public double[] FP_Kov;
        public double[] m_SlozkaStruska;
        public double[] m_SlozkaKov;
        public double[] c_Struska;
        public double[] c_Kov;

        public DateTime StartTime;
        public TimeSpan Duration;
        public DateTime ActualTime;

        public double E_C_oxidace;
        public double E_Si_oxidace;
        public double E_Mn_oxidace;
        public double E_Al_oxidace;
        public double E_Fe_oxidace;
    }
}
