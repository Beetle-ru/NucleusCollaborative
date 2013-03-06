using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LOneProcessor.SubSystems
{
    /// <summary>
    /// Следить за состоянием Watchdogs, нарушениями во входящих данных
    /// уведомлять приложения о чрезвычайной ситуации
    /// </summary>
    static class Keeper
    {
        public static void Handler()
        {
            Console.WriteLine("Keeper");
        }

        public static void SetGasAnalysis(double co, double co2)
        {
            
        }
        
        public static void SetBlowingStatus()
        {

        }
    }
}
