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
        private static bool m_isNotfiredPrognosis; // по прогнозу не стреля не стреляли
        public static Guid SIdK; // куркинский sId для проверки подтверждения
        public static bool IsBeganMetering; // запустили измерение
        public static int LanceMod; // режим управления фурмой
        public static int MeteringCounter; // счетчик замеров
        public static bool EndMeteringAccept; // конец замера подтвержден
        public static bool EndMeteringAlow; // конец замера разрешен
        public static int SublanceHeigth; // высота зонда
        public const int SublanceTreshold = 2000; // порог при котором зонд считается поднятым
        public static int PeriodNumber; // период углерода 1 - грубая однофакторная модель, 2 - многофакторная, 3 - дофук(не реализован)
        public static DateTime LastIterateTime; // последнее время - нужно для прогноза замера
        public static int SecondFromBeginMFM; // количество секунд от старта многофакторной модели, нужно для прогноза замера 
        public static double X1, Y1, Xn, Yn; // вхоные данные для предсказывающего уравнения
        public const double ReactionTime = 25.0 - 10; // время реакции системы
        public static double PrognosisMeterTime; //прогнозируемое время замера
        public static bool Item1IsFixed; // зафиксированы значения X1, Y1
        
        public static void Init()
        {
            Oxigen = new RollingAverage();
            CarbonMonoxide = new RollingAverage();
            HotMetallMass = 0;
            HeatNumber = 0;
            m_oxygenStartValue = 0;
            m_isNotfiredB = true;
            m_isNotfiredK = true;
            m_isNotfiredPrognosis = true;
            TargetCk = -999; // не достигнем
            TargetCku = 0; // трубка 0
            Ck = 0;
            ZondIsAccepted = false;
            SIdK = SIdGen(); // присваиваем id текущей сессии для куркина
            IsBeganMetering = false;
            EndMeteringAccept = false;
            EndMeteringAlow = false;
            SublanceHeigth = Int32.MaxValue;
            PeriodNumber = 0;
            LastIterateTime = new DateTime();
            SecondFromBeginMFM = 0;
            X1 = 0.0;
            Y1 = 0.0;
            Xn = 0.0;
            Yn = 0.0;
            PrognosisMeterTime = 0.0;
            Item1IsFixed = false;
        }
        public static void Renit()
        {
            StopBlowFlagRelease();
            EndMetering();
            Init();
        }
        public static void Iterate()
        {
            using (var l = new Logger("Iterate"))
            {
                var oxy = Oxigen.Average(SmoothInterval);
                var co = CarbonMonoxide.Average(SmoothInterval);
                if (VerificateB(oxy, co, HotMetallMass) && m_isNotfiredB) // рекомендация кислорода на продувку
                {
                    var fex = new ConnectionProvider.FlexHelper("SublanceGenerator.RecommendMetering.B");
                    fex.AddArg("OxygenStartValue", m_oxygenStartValue);
                    fex.AddArg("CurrentOxygen", oxy);
                    fex.AddArg("CurrentCo", co);
                    fex.AddArg("CurrentHotMetallMass", HotMetallMass);
                    fex.Fire(Program.MainGate);
                    string msg = String.Format("SublanceStart fired: \n{0}", fex.evt.ToString());
                    l.msg(msg);
                    m_isNotfiredB = false;
                }
                if (VerificateK(TargetCk, TargetCku, Ck) && (PeriodNumber == 2) && m_isNotfiredK) // команда на старт зонда по углероду и предуставке
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
                //PeriodNumber = 2;
                if ((PeriodNumber == 2) && m_isNotfiredPrognosis) // начинаем по старту многофакторной
                {
                    if (VerificatePrognosis(TargetCk, ReactionTime, Ck)) // команда на старт зонда по прогнозу
                    {
                        var fex = new ConnectionProvider.FlexHelper("SublanceGenerator.RecommendMetering.Prognosis");
                        fex.AddArg("TargetCk", TargetCk);
                        fex.AddArg("Ck", Ck);
                        fex.AddArg("CurrentOxygen", oxy);
                        fex.Fire(Program.MainGate);
                        string msg = String.Format("RecommendMetering.Prognosis fired: \n{0}", fex.evt.ToString());
                        l.msg(msg);
                        m_isNotfiredPrognosis = false;
                    }
                }
                if(EndMeteringAccept && EndMeteringAlow)
                {
                    EndMetering();
                    EndMeteringAccept = false;
                    EndMeteringAlow = false;
                    l.msg("End metering by iterator");
                }
            }
        }
        private static bool VerificateB(double oxigen, double carbonMonoxide, double hotMetallMass) // проверка по бубнову
        {
            const int transformValue = 64;
            const int coTreshold = 15;

            m_oxygenStartValue = OxygenStartValue(hotMetallMass, transformValue);
            //return (m_oxygenStartValue < oxigen) && (carbonMonoxide < coTreshold) && (hotMetallMass != 0);
            return (hotMetallMass != 0);
        }
        private static bool VerificateK(double targetCk, double targetCku, double ck)
        {
            return ((targetCk <= ck) && (ck <= (targetCk + targetCku))); // recommend metering
        }

        private static bool VerificatePrognosis(double targetCk, double reactionTime, double cNow)
        {
            const int item1SecFix = 3;
            const int startCalcSec = 10;
            if (targetCk < 0) return false;
            if (LastIterateTime.Ticks != 0) // проверка на первый запуск в текущей плавке
            {
                var currentSecond = DateTime.Now.Second;
                var deltaSec = Math.Abs(LastIterateTime.Second - currentSecond);
                LastIterateTime = DateTime.Now;
                //Console.WriteLine(deltaSec);
                if (deltaSec >= 1) // чтоб не чаще 1 раза в секунду
                {
                    SecondFromBeginMFM += deltaSec;

                    if (!Item1IsFixed && SecondFromBeginMFM >= item1SecFix) // секунда на которой фиксируем первые значения
                    {
                        X1 = SecondFromBeginMFM;
                        Y1 = cNow;
                        Item1IsFixed = true;
                    }
                    if (SecondFromBeginMFM < startCalcSec) return false; // если текущая секунда меньше заданной, то не считаем, воизбежание ложных срабатываний

                    Xn = SecondFromBeginMFM;
                    Yn = cNow;
                    var X = (((Y1 + ((Y1 - Yn) / (Xn - X1)) * X1) - targetCk) * (Xn - X1)) / (Y1 - Yn); // время когда углерод попадет в цель
                    var Xrt = X - reactionTime; // время с учетом времени реакции, может быть и отрицательным если опоздали с замером

                    InstantLogger.msg("CurrentSecond = {0}; StartZondSecond = {1}", SecondFromBeginMFM, Xrt);
                    //var epsilon = 3;
                    //if (Math.Abs(SecondFromBeginMFM - Xrt) < epsilon)
                    //{
                    //    return true;
                    //}
                    if (SecondFromBeginMFM >= Xrt)
                    {
                        return true;
                    }
                }
            }
            else // 0 секунда
            {
                LastIterateTime = DateTime.Now;
                X1 = 0;
                Y1 = cNow;
            }
            return false; // recommend metering
        }

        private static double OxygenStartValue(double hotMetallMass, int transformValue)
        {
            return hotMetallMass*transformValue;
        }
        private static Guid SIdGen()
        {
            //return Guid.NewGuid();
            return new Guid();
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
        //public static void BlowingEndRequest()
        //{
        //    var fex = new ConnectionProvider.FlexHelper("SublanceGenerator.BlowingEndRequest");
        //    fex.AddArg("SId", SIdK);
        //    fex.Fire(Program.MainGate);
        //}
        public static void DoStopBlow()
        {
            var fex = new ConnectionProvider.FlexHelper("OPC.ComEndBlowing");
            fex.AddArg("EndBlowingSignal", 1);
            fex.Fire(Program.MainGate);
            InstantLogger.log(fex.evt.ToString());
        }
        public static void StopBlowFlagRelease()
        {
            var fex = new ConnectionProvider.FlexHelper("OPC.ComEndBlowing");
            fex.AddArg("EndBlowingSignal", 0);
            fex.Fire(Program.MainGate);
            InstantLogger.log(fex.evt.ToString());
        }
        public static bool SublanceRaised(double derivative, int heigth, int treshold)
        {
            return (derivative > 0) && (heigth >= treshold);
        }
    }
}
