using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Threading;

namespace PingLib {
    public delegate void NetStatusDeel(bool netOnline, string address);

    public class NetMonitor {
        private Ping m_pingSender;
        private Thread m_pingThread;
        public NetStatusDeel NetStatusChange;
        public bool NetOnline;
        public int Timeout;
        public string IPAddress;
        public int SleepMs;

        public NetMonitor() {
            m_pingSender = new Ping();
            Timeout = 120;
            IPAddress = "127.0.0.1";
            SleepMs = 1000;

            m_pingThread = new Thread(PingSenderThreadHandler);
            m_pingThread.IsBackground = true;
            m_pingThread.Start();
        }

        private bool Ping() {
            try {
                var pingResult = m_pingSender.Send(IPAddress);

                if (pingResult != null && pingResult.Status == IPStatus.Success)
                    return true;
            }
            catch (Exception) {
                return false;
            }
            return false;
        }

        private void PingSenderThreadHandler(object status) {
            NetOnline = Ping();
            PullNetStatusChange(NetOnline);
            while (true) {
                var online = Ping();
                if (online != NetOnline) {
                    NetOnline = online;
                    PullNetStatusChange(NetOnline);
                }
                Thread.Sleep(SleepMs);
            }
        }

        private void PullNetStatusChange(bool online) {
            if (NetStatusChange != null) NetStatusChange(online, IPAddress);
        }
    }
}