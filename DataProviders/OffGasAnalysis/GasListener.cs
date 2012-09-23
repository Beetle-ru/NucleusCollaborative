using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace OffGasAnalysis
{

    public delegate void OnOffGasEventHandler(GasData gasData);

    class GasListener : IDisposable
    {
        private Socket m_socketList = null;
        private Thread m_ListenerThread = null;
        private byte[] m_ReadingBuf = null;
        private bool m_ListenerThreadHasToClose = false;

        public GasListener(string IP, int port)
        {
            m_ReadingBuf = new byte[2000];
            m_socketList = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            m_socketList.Bind(new IPEndPoint(IPAddress.Any, port)); // локальная конечная точка
            m_socketList.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
            m_socketList.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse(IP), IPAddress.Any));
            m_ListenerThread = new Thread(ListenerTrend);
            m_ListenerThread.Start();
        }

        private void ListenerTrend()
        {
            while (!m_ListenerThreadHasToClose)
            {
                int len = m_socketList.Receive(m_ReadingBuf);
                if (OnOffGas != null)
                    OnOffGas(new GasData(m_ReadingBuf));
            }
        }

        public event OnOffGasEventHandler OnOffGas;

        #region IDisposable Members

        public void Dispose()
        {
            if (!m_ListenerThreadHasToClose)
            {
                m_ListenerThreadHasToClose = true;
                if (!m_ListenerThread.Join(10000)) m_ListenerThread.Abort();
            }
        }

        #endregion
    }
}
