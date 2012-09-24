using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Model
{
    public class DynamicOutput
    {
        public float m_Tavby;
        public float E_Tavby;
        public float T_Tavby;
        public float FP_C;
        public float[] FP_Tavby;
        public float[] m_SlozkaTavby;

        public float m_Struska;
        public float m_Kov;
        public float[] FP_Struska;
        public float[] FP_Kov;
        public float[] m_SlozkaStruska;
        public float[] m_SlozkaKov;
        public float[] c_Struska;
        public float[] c_Kov;

        public DateTime StartTime;
        public TimeSpan Duration;
        public DateTime ActualTime;

        public float E_C_oxidace;
        public float E_Si_oxidace;
        public float E_Mn_oxidace;
        public float E_Al_oxidace;
        public float E_Fe_oxidace;
    }
}
