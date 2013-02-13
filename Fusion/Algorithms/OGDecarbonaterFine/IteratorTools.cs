using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    internal static partial class Iterator
    {

        public static void Init()
        {
            Reset();

            IterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            IterateTimer.Enabled = true;
        }

        public static void Reset()
        {
            HDSmoother = new HeatDataSmoother(PeriodSec);
            CurrentState = new HeatData();
            Console.WriteLine("Reset");
        }

        static public void PushCarbon(double carbon)
        {
            const double tresholdCarbon = 0.00;
            carbon = carbon < tresholdCarbon ? tresholdCarbon : carbon; // ограничение на углерод

            var fex = new ConnectionProvider.FlexHelper("OGDecarbonaterFine.Result");
            fex.AddArg("C", carbon);
            fex.Fire(Program.MainGate);
        }


    }
}