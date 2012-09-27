using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implements;

namespace SublanceGenerator
{
    
    static class Iterator
    {
        public static RollingAverage Oxigen;
        public static RollingAverage CarbonMonoxide;
        public static double HotMetallMass;
        public static Int64 HeatNumber;
        private const int SmoothInterval = 15;
        private static double m_oxygenStartValue ;
        private static bool m_isNotfired;
        public static void Init()
        {
            Oxigen = new RollingAverage();
            CarbonMonoxide = new RollingAverage();
            HotMetallMass = 300;
            HeatNumber = 0;
            m_oxygenStartValue = 0;
            m_isNotfired = true;
        }
        public static void Renit()
        {
            Init();
        }
        public static void Iterate()
        {
            using (var l = new Logger("Iterate"))
            {
                var oxy = Oxigen.Average(SmoothInterval);
                var co = CarbonMonoxide.Average(SmoothInterval);
                if (Verificate(oxy, co, HotMetallMass) && m_isNotfired)
                {
                    var fex = new ConnectionProvider.FlexHelper("Model.SublanceStart");
                    fex.AddArg("OxygenStartValue", m_oxygenStartValue);
                    fex.AddArg("CurrentOxygen", oxy);
                    fex.AddArg("CurrentCo", co);
                    fex.AddArg("CurrentHotMetallMass", HotMetallMass);
                    fex.Fire(Program.MainGate);
                    string msg = String.Format("SublanceStart fired: \n{0}", fex.evt.ToString());
                    l.msg(msg);
                    m_isNotfired = false;
                }
            }
        }
        private static bool Verificate(double oxigen, double carbonMonoxide, double hotMetallMass)
        {
            const int transformValue = 64;
            const int coTreshold = 15;
            m_oxygenStartValue = OxygenStartValue(hotMetallMass, transformValue);
            return (m_oxygenStartValue < oxigen) && (carbonMonoxide < coTreshold);
        }
        private static double OxygenStartValue(double hotMetallMass, int transformValue)
        {
            return hotMetallMass*transformValue;
        }
    }
}
