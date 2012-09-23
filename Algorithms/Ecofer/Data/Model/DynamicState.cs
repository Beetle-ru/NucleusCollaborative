using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Data.Model
{
    public class DynamicState
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

        // for actual step
        public Dictionary<Enumerations.M3ElementEnum, float> E_Elements;
        public float E_C;
    }
}
