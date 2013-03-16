using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CommonTypes;
using ConnectionProvider;
using Converter;
using Implements;

namespace LOneProcessor.SubSystems {
    /// <summary>
    /// Следить за состоянием Watchdogs, нарушениями во входящих данных
    /// уведомлять приложения о чрезвычайной ситуации
    /// </summary>
    internal static class Keeper {
        #region ConstRegion

        private const int MaxSecondDelayOffGasEvent = 10; // Если за это время не пришло ни одно событие по газанализу выдается ошибка
        private const int MaxSecondDelayWatchdogChange = 15; 

        private const int WatchdogSendPeriodMs = 5000;

        #endregion

        #region VarRegion 

        private static Client m_mainGate;

        private static bool m_heatIsStarted;
        private static double m_co, m_co2;
        private static readonly System.Threading.Timer OffgasTimeout;
        private static readonly System.Threading.Timer WatchdogSendTimeout;
        private static readonly System.Threading.Timer WatchdogReceiveTimer;

        private static bool m_offgasEventAbsent;
        private static bool m_wd1Timeout, m_wd2Timeout, m_wd3Timeout, m_wd01Timeout;

        private static int m_watchdogSendValue;
        private static int m_wdReceiveValuePLC1, m_wdReceiveValuePLC2, m_wdReceiveValuePLC3, m_wdReceiveValuePLC01;
        private static int m_wdRTimeOutCntPLC1, m_wdRTimeOutCntPLC2, m_wdRTimeOutCntPLC3, m_wdRTimeOutCntPLC01;

        #endregion

        #region MainHandlerRegion

        public static void MainHandler() {
            var allRight = true;
            var offgasOkay = !m_offgasEventAbsent;
            var watchdogOkay = !(m_wd1Timeout || m_wd2Timeout || m_wd3Timeout || m_wd01Timeout);
            var description = new List<string>();

            if (m_offgasEventAbsent) {
                allRight = false;
                description.Add(String.Format("OffgasEvent absent more than {0} second", MaxSecondDelayOffGasEvent));
            }

            if (!watchdogOkay)
            {
                allRight = false;
                description.Add(String.Format("Something from watchdog did not change more than {0} second", MaxSecondDelayWatchdogChange));
            }

            var fex = new FlexHelper("L1.Keeper");

            fex.AddArg("AllRight", allRight);
            fex.AddArg("OffgasOkay", offgasOkay);
            fex.AddArg("WatchdogOkay", watchdogOkay);
            
            fex.AddArg("Wd1Timeout", m_wd1Timeout);
            fex.AddArg("Wd2Timeout", m_wd2Timeout);
            fex.AddArg("Wd3Timeout", m_wd3Timeout);
            fex.AddArg("Wd01Timeout", m_wd01Timeout);
            
            fex.AddComplexArg("Description", description);

            if (!allRight) InstantLogger.err(fex.evt.ToString());
            fex.Fire(m_mainGate);
        }

        #endregion

        #region VeryfiRegion

        private static void VeryfiGasAnalysis() {}

        #endregion

        #region SetRegion

        public static void SetMainGate(Client mainGate) {
            m_mainGate = mainGate;
        }

        private static void SetGasAnalysis(double co, double co2) {
            m_co = co;
            m_co2 = co2;

            OffgasTimeout.Change(MaxSecondDelayOffGasEvent*1000, 0);
            VeryfiGasAnalysis();
        }

        private static void SetWatchdogReceive(int wd1, int wd2, int wd3, int wd01) {
            if (wd1 != m_wdReceiveValuePLC1) {
                m_wdReceiveValuePLC1 = wd1;
                m_wdRTimeOutCntPLC1 = 0;
            }

            if (wd2 != m_wdReceiveValuePLC2)
            {
                m_wdReceiveValuePLC2 = wd2;
                m_wdRTimeOutCntPLC2 = 0;
            }

            if (wd3 != m_wdReceiveValuePLC3)
            {
                m_wdReceiveValuePLC3 = wd3;
                m_wdRTimeOutCntPLC3 = 0;
            }

            if (wd01 != m_wdReceiveValuePLC01)
            {
                m_wdReceiveValuePLC01 = wd01;
                m_wdRTimeOutCntPLC01 = 0;
            }

        }

        private static void SetBlowingStatus(bool heatIsStarted) {
            m_heatIsStarted = heatIsStarted;
        }

        #endregion

        #region TimerRegion

        private static void OffgasTimeoutHandler(object state) {
            m_offgasEventAbsent = ((!m_offgasEventAbsent) && m_heatIsStarted);
        }

        private static void WatchdogReceiveTimerHandler(object state) {
            m_wdRTimeOutCntPLC1++;
            m_wdRTimeOutCntPLC2++;
            m_wdRTimeOutCntPLC3++;
            m_wdRTimeOutCntPLC01++;
            
            m_wd1Timeout = m_wdRTimeOutCntPLC1 > MaxSecondDelayWatchdogChange;
            m_wd2Timeout = m_wdRTimeOutCntPLC2 > MaxSecondDelayWatchdogChange;
            m_wd3Timeout = m_wdRTimeOutCntPLC3 > MaxSecondDelayWatchdogChange;
            m_wd01Timeout = m_wdRTimeOutCntPLC01 > MaxSecondDelayWatchdogChange;
        }

        private static void WatchDogSendTimeoutHandler(object state) {
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

        public static void EventsHandler(BaseEvent evt, Logger l) {
            if (evt is BlowingEvent) {
                var be = evt as BlowingEvent;
                Keeper.SetBlowingStatus(be.BlowingFlag == 1);
            } else 
            
            if (evt is FlexEvent) {
                var fxe = evt as FlexEvent;
                if (fxe.Operation.StartsWith("UDP.OffGasAnalysisEvent")) {
                    var fxh = new FlexHelper(fxe);

                    var co = fxh.GetDbl("CO");
                    var co2 = fxh.GetDbl("CO2");

                    SetGasAnalysis(co, co2);
                } else 
                
                if (fxe.Operation.StartsWith("OPC.WatchdogsFromL1"))
                {
                    var fxh = new FlexHelper(fxe);
                    var wd1 = fxh.GetInt("WDPLC1");
                    var wd2 = fxh.GetInt("WDPLC2");
                    var wd3 = fxh.GetInt("WDPLC3");
                    var wd01 = fxh.GetInt("WDPLC01");

                    SetWatchdogReceive(wd1, wd2, wd3, wd01);
                }
            } 

        }

        #endregion

        static Keeper() {
            OffgasTimeout = new Timer(OffgasTimeoutHandler);
            WatchdogSendTimeout = new Timer(WatchDogSendTimeoutHandler);
            WatchdogSendTimeout.Change(WatchdogSendPeriodMs, WatchdogSendPeriodMs);

            WatchdogReceiveTimer = new Timer(WatchdogReceiveTimerHandler);
            WatchdogReceiveTimer.Change(0, 1000);
        }

        //#region Reset

        //private static void Reset() {
        //    m_offgasEventAbsent = false;
        //}

        //#endregion
    }
}