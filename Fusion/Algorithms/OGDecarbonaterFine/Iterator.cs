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
            CalcAll(); // пересчитываем все параметры
            WriteFile(CurrentState.GetDataLine(), ArchFileName); // пишем текущий расчет в файл
        }

        public static void IterateTimeOut(object source, ElapsedEventArgs e)
        {
            //Receiver.HeatIsStarted = true; // для отладки
            if (Receiver.HeatIsStarted)
            {
                Iterate();
                Console.Write("*");
            }
            else Console.Write(".");
        }
    }
}
