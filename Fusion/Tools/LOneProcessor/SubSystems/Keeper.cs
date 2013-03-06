using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConnectionProvider;

namespace LOneProcessor.SubSystems
{
    /// <summary>
    /// Следить за состоянием Watchdogs, нарушениями во входящих данных
    /// уведомлять приложения о чрезвычайной ситуации
    /// </summary>
    static class Keeper
    {
        #region ConstRegion

        private const int MaxSecondDelayOffGasEvent = 10; // Если за это время не пришло ни одно событие по газанализу выдается ошибка
        

        #endregion

        #region VarRegion 

        public static Client MainGate;

        private static bool m_heatIsStarted;
        private static double m_co, m_co2;
        private static System.Threading.Timer m_offgasTimeout;

        private static bool m_offgasEventAbsent;

        #endregion

        #region HandlerRegion

        public static void Handler()
        {
            var allRight = true;
            var description = new List<string>();
            
            if (m_offgasEventAbsent)
            {
                allRight = false;
                description.Add(String.Format("OffgasEvent absent more than {0} second", MaxSecondDelayOffGasEvent));
            }
            
            var fex = new FlexHelper("L1.Keeper");
            
            fex.AddArg("AllRight", allRight);
            fex.AddComplexArg("Description", description);
            
            Console.WriteLine(fex.evt);
        }

        #endregion

        #region VeryfiRegion

        private static void VeryfiGasAnalysis()
        {

        }

        #endregion

        #region SetRegion

        public static void SetGasAnalysis(double co, double co2)
        {
            m_co = co;
            m_co2 = co2;

            m_offgasTimeout.Change(MaxSecondDelayOffGasEvent * 1000, 0);
            VeryfiGasAnalysis();
        }
        
        public static void SetBlowingStatus(bool heatIsStarted)
        {
            m_heatIsStarted = heatIsStarted;
        }

        #endregion

        #region TimerRegion

        private static void OffgasTimeoutHandler(object state)
        {
            m_offgasEventAbsent = ((!m_offgasEventAbsent) && m_heatIsStarted);
        }

        #endregion

        static Keeper()
        {
            m_offgasTimeout = new Timer(OffgasTimeoutHandler);
        }
    }
}
