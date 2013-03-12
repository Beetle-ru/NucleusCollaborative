using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;

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
        private const int WatchdogSendPeriodMs = 5000;
        

        #endregion

        #region VarRegion 

        private static Client m_mainGate;

        private static bool m_heatIsStarted;
        private static double m_co, m_co2;
        private static readonly System.Threading.Timer OffgasTimeout;
        private static readonly System.Threading.Timer WatchDogSendTimeout;

        private static bool m_offgasEventAbsent;

        private static int m_watchdogSendValue;
        private static int m_wdReceivePLC1, m_wdReceivePLC2, m_wdReceivePLC3, m_wdReceivePLC01;

        #endregion

        #region MainHandlerRegion

        public static void MainHandler()
        {
            var allRight = true;
            var offgasOkay = !m_offgasEventAbsent;
            var description = new List<string>();
            
            if (m_offgasEventAbsent)
            {
                allRight = false;
                description.Add(String.Format("OffgasEvent absent more than {0} second", MaxSecondDelayOffGasEvent));
            }
            
            var fex = new FlexHelper("L1.Keeper");
            
            fex.AddArg("AllRight", allRight);
            fex.AddArg("OffgasOkay", offgasOkay);
            fex.AddComplexArg("Description", description);

            if (!allRight) InstantLogger.err(fex.evt.ToString());
            fex.Fire(m_mainGate);
        }

        #endregion

        #region VeryfiRegion

        private static void VeryfiGasAnalysis()
        {

        }

        #endregion

        #region SetRegion

        static public void SetMainGate(Client mainGate)
        {
            m_mainGate = mainGate;
        }
        public static void SetGasAnalysis(double co, double co2)
        {
            m_co = co;
            m_co2 = co2;

            OffgasTimeout.Change(MaxSecondDelayOffGasEvent * 1000, 0);
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

        private static void WatchDogSendTimeoutHandler(object state)
        {
            var fex = new FlexHelper("OPC.WatchdogsForL1");

            fex.AddArg("WDPLC1", m_watchdogSendValue);
            fex.AddArg("WDPLC2", m_watchdogSendValue);
            fex.AddArg("WDPLC3", m_watchdogSendValue);
            //fex.AddArg("WDPLC01", m_watchdogValue);
            fex.Fire(m_mainGate);

            if (m_watchdogSendValue < 999) m_watchdogSendValue++;
            else m_watchdogSendValue = 0;
        }

        #endregion

        #region EventsHandler
        public static void EventsHandler(BaseEvent evt, Logger l)
        {
            if (evt is BlowingEvent)
            {
                var be = evt as BlowingEvent;
                Keeper.SetBlowingStatus(be.BlowingFlag == 1);
            }
            if (evt is FlexEvent)
            {
                var fxe = evt as FlexEvent;
                if (fxe.Operation.StartsWith("UDP.OffGasAnalysisEvent"))
                {
                    var fxh = new FlexHelper(fxe);

                    var co = fxh.GetDbl("CO");
                    var co2 = fxh.GetDbl("CO2");

                    SetGasAnalysis(co, co2);

                }
            }
        }
        #endregion

        static Keeper()
        {
            OffgasTimeout = new Timer(OffgasTimeoutHandler);
            WatchDogSendTimeout = new Timer(WatchDogSendTimeoutHandler);
            WatchDogSendTimeout.Change(WatchdogSendPeriodMs, WatchdogSendPeriodMs);
        }
    }
}
