using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    [Serializable]
    public class HotMetal
    {
        float m_C;

        public float C { get { return m_C; } set { m_C = value; } }

        float m_Si;

        public float Si { get { return m_Si; } set { m_Si = value; } }

        float m_Mn;

        public float Mn { get { return m_Mn; } set { m_Mn = value; } }

        float m_P;

        public float P { get { return m_P; } set { m_P = value; } }

        float m_S;

        public float S { get { return m_S; } set { m_S = value; } }

        float m_Ti;

        public float Ti { get { return m_Ti; } set { m_Ti = value; } }

        int m_Temperature;

        public int Temperature { get { return m_Temperature; } set { m_Temperature = value; } }

        int m_Weight;

        public int Weight { get { return m_Weight; } set { m_Weight = value; } }

        public HotMetal() { }
    }
}
