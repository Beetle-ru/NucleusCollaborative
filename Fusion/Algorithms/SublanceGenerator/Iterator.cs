using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;
using Implements;

namespace SublanceGenerator
{
    
    static class Iterator
    {
        public static RollingAverage Oxigen;
        public static RollingAverage CarbonMonoxide;
        public static double HotMetallMass;
        public static Int64 HeatNumber;
        public static double TargetCk; // целевой углерод куркина
        public static double TargetCku; // целевая уставка на углерод куркина
        public static double Ck; // углерод куркина
        public static bool ZondIsAccepted; // замер подтвержден
        private const int SmoothInterval = 15;
        private static double m_oxygenStartValue ;
        private static bool m_isNotfiredB; // бубноский не стреляли
        private static bool m_isNotfiredK; // куркинский не стреляли
        public static Guid SIdK; // куркинский sId для проверки подтверждения
        public static bool IsBeganMetering; // запустили измерение
        public static int LanceMod; // режим управления фурмой
        public static int MeteringCounter; // счетчик замеров
        public static void Init()
        {
            Oxigen = new RollingAverage();
            CarbonMonoxide = new RollingAverage();
            HotMetallMass = 300;
            HeatNumber = 0;
            m_oxygenStartValue = 0;
            m_isNotfiredB = true;
            m_isNotfiredK = true;
            TargetCk = -999; // не достигнем
            TargetCku = 0; // трубка 0
            Ck = 0;
            ZondIsAccepted = false;
            SIdK = SIdGen(); // присваиваем id текущей сессии для куркина
            IsBeganMetering = false;
        }
        public static void Renit()
        {
            EndMetering();
            Init();
        }
        public static void Iterate()
        {
            using (var l = new Logger("Iterate"))
            {
                var oxy = Oxigen.Average(SmoothInterval);
                var co = CarbonMonoxide.Average(SmoothInterval);
                if (VerificateB(oxy, co, HotMetallMass) && m_isNotfiredB)
                {
                    var fex = new ConnectionProvider.FlexHelper("Model.SublanceStart");
                    fex.AddArg("OxygenStartValue", m_oxygenStartValue);
                    fex.AddArg("CurrentOxygen", oxy);
                    fex.AddArg("CurrentCo", co);
                    fex.AddArg("CurrentHotMetallMass", HotMetallMass);
                    fex.Fire(Program.MainGate);
                    string msg = String.Format("SublanceStart fired: \n{0}", fex.evt.ToString());
                    l.msg(msg);
                    m_isNotfiredB = false;
                }
                if (VerificateK(TargetCk, TargetCku, Ck) && m_isNotfiredK)
                {
                    var fex = new ConnectionProvider.FlexHelper("SublanceGenerator.RecommendMetering.K");
                    fex.AddArg("TargetCk", TargetCk);
                    fex.AddArg("TargetCku", TargetCku);
                    fex.AddArg("Ck", Ck);
                    fex.AddArg("CurrentOxygen", oxy);
                    fex.AddArg("SId", SIdK);
                    fex.Fire(Program.MainGate);
                    string msg = String.Format("RecommendMetering.K fired: \n{0}", fex.evt.ToString());
                    l.msg(msg);
                    m_isNotfiredK = false;
                }
            }
        }
        private static bool VerificateB(double oxigen, double carbonMonoxide, double hotMetallMass) // проверка по бубнову
        {
            const int transformValue = 64;
            const int coTreshold = 15;
            m_oxygenStartValue = OxygenStartValue(hotMetallMass, transformValue);
            return (m_oxygenStartValue < oxigen) && (carbonMonoxide < coTreshold);
        }
        private static bool VerificateK(double targetCk, double targetCku, double ck)
        {
            return ((targetCk < ck) && (ck < (targetCk + targetCku))); // recommend metering
        }
        private static double OxygenStartValue(double hotMetallMass, int transformValue)
        {
            return hotMetallMass*transformValue;
        }
        private static Guid SIdGen()
        {
            return Guid.NewGuid();
        }
        public static void BeginMetering()
        {
            Program.MainGate.PushEvent(new comO2FlowRateEvent() { SublanceStartO2Vol = 1});
            IsBeganMetering = true;
            //if (LanceMod == 3)
            //{
            //    Program.MainGate.PushEvent(new comPrepareMeteringEvent() {StartPrepareMetering = true});
            //    Program.MainGate.PushEvent(new comMeteringEvent() {StartMetering = true});
            //    IsBeganMetering = true;
            //}
        }
        public static void EndMetering()
        {
            Program.MainGate.PushEvent(new comO2FlowRateEvent() { SublanceStartO2Vol = 0 });
            IsBeganMetering = false;
            //if (LanceMod == 3)
            //{
            //    Program.MainGate.PushEvent(new comMeteringEvent() {StartMetering = false});
            //    Program.MainGate.PushEvent(new comPrepareMeteringEvent() {StartPrepareMetering = false});
            //    IsBeganMetering = false;
            //}
        }
    }
}
