using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;

namespace LOneProcessor {
    internal delegate void Handler();

    internal static class EventLoop {
        public static int Period = 1; // sec
        public static List<Handler> HandlerList;
        private static System.Timers.Timer m_iterateTimer;
        private static bool m_isRuned;

        public static void Init() {
            m_iterateTimer = new System.Timers.Timer();
            m_iterateTimer.Elapsed += new ElapsedEventHandler(IterateTimeOut);
            HandlerList = new List<Handler>();
            m_isRuned = false;
        }

        public static void RunLoop() {
            m_iterateTimer.Interval = Period;
            m_iterateTimer.Enabled = true;
            m_isRuned = true;
        }

        public static void StopLoop() {
            m_isRuned = false;
        }

        private static void IterateTimeOut(object source, ElapsedEventArgs e) {
            m_iterateTimer.Enabled = false;

            if (HandlerList.Any()) {
                var delayTime = 1000/HandlerList.Count;
                foreach (var handler in HandlerList) {
                    Thread.Sleep(delayTime);
                    handler();
                }
            }

            m_iterateTimer.Enabled = m_isRuned;
        }
    }
}