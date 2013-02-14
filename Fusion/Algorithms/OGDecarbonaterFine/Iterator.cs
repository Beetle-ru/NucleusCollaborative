using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    static partial class Iterator
    {
        public static void Iterate()
        {
            SyncPushInputData(); // синхронизируя проталкиваем последние данные
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            if (Receiver.HeatIsStarted)
            {
                Iterate();
                Console.Write("*");
            }
            else Console.Write(".");
        }
    }
}
