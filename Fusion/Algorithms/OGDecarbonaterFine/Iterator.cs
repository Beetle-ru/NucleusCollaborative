using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    static class Iterator
    {
        public static HeatDataSmoother HDSmoother;
        public const int PeriodSec = 3; // время сглаживания
        public const int IntervalSec = 1; // интервал расчетов
        public static Timer IterateTimer = new Timer(IntervalSec * 1000);
        public static HeatData CurrentState;

        public static void Init()
        {
            Reset();

            IterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            IterateTimer.Enabled = true;
        }

        public static void Reset()
        {
            HDSmoother = new HeatDataSmoother();
            CurrentState = new HeatData();
            Console.WriteLine("Reset");
        }

        public static void Iterate()
        {

        }

        static public void PushCarbon(double carbon)
        {
            const double tresholdCarbon = 0.00;
            carbon = carbon < tresholdCarbon ? tresholdCarbon : carbon; // ограничение на углерод

            var fex = new ConnectionProvider.FlexHelper("OGDecarbonaterFine.Result");
            fex.AddArg("C", carbon);
            fex.Fire(Program.MainGate); 
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            Iterate();
            Console.Write(".");
        }

    }

    class HeatDataSmoother
    {
        public RollingAverage CO;
        public RollingAverage CO2;
        public double LanceHeigth;
        public double Oxygen;
        public bool HeatIsStarted;

        public HeatDataSmoother(int lengthBuff = 50)
        {
            CO = new RollingAverage(lengthBuff);
            CO2 = new RollingAverage(lengthBuff);
            LanceHeigth = 0;
            Oxygen = 0.0;
            HeatIsStarted = false;
        }
    }
}
