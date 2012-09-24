using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Implements;

namespace JSONClient
{
    class UDPTools
    {
        private int m_recv;
        private byte[] m_data;
        private readonly IPEndPoint m_ipep;
        private Socket m_newsock;
        private readonly IPEndPoint m_sender;
        private EndPoint m_remote;

        private Thread m_udpServerThread;

        private delegate int ListenerDelegate(byte[] receive, int receiveData);

        internal delegate int SubscribeDelegate(string message);

        private ListenerDelegate m_ld;
        private SubscribeDelegate m_sd;

        public UDPTools(int port)
        {
            m_data = new byte[1024];
            m_ipep = new IPEndPoint(IPAddress.Any, port);
            m_newsock = new Socket(AddressFamily.InterNetwork,
                           SocketType.Dgram, ProtocolType.Udp);
            m_newsock.Bind(m_ipep);
            m_sender = new IPEndPoint(IPAddress.Any, 0);
            m_remote = (EndPoint)(m_sender);

            m_udpServerThread = new Thread(UDPServer);

            m_ld = OnData;

        }
        public void StartUDPServer()
        {
            m_udpServerThread.Start();
        }

        private void UDPServer()
        {

           /* Console.WriteLine("Waiting for a client...");

            m_recv = m_newsock.ReceiveFrom(m_data, ref m_remote);

            Console.WriteLine("Message received from {0}:", m_remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(m_data, 0, m_recv));

            string welcome = "Welcome to my test server";
            m_data = Encoding.ASCII.GetBytes(welcome);
            m_newsock.SendTo(m_data, m_data.Length, SocketFlags.None, m_remote);*/
            while (true)
            {
                m_data = new byte[1024];
                try
                {
                    m_recv = m_newsock.ReceiveFrom(m_data, ref m_remote);
                    m_ld.Invoke(m_data, m_recv);
                }
                catch (Exception e)
                {
                    InstantLogger.err("udp receive error");
                    m_remote = (EndPoint)(m_sender);
                    //Logger.err("udp receive return err code: {0}", e.ToString());
                    //throw;
                }
                
                //Console.WriteLine(Encoding.ASCII.GetString(m_data, 0, m_recv));
                //m_newsock.SendTo(m_data, m_recv, SocketFlags.None, m_remote);
            }
         }
        public int Subscribe(SubscribeDelegate sd)
        {
            m_sd = sd;
            return 0;
        }
        public int OnData(byte[] data, int receiveBytes)
        {
           // Console.WriteLine("UDP receive - {0}", Encoding.ASCII.GetString(data,0,receiveBytes));
            try
            {
                m_sd.Invoke(Encoding.ASCII.GetString(data, 0, receiveBytes));
            }
            catch (Exception)
            {
                InstantLogger.err("udp receive not subscribe listener");
                //throw;
            }
            
            return 0;
        }
        
        public int Send(string message)
        {
            m_data = new byte[1024];
            for (int i = 0; i < message.Length; i++)
            {
                if (m_data.Length > message.Length)
                {
                    m_data[i] = (byte)message[i];
                }
                
            }
            try
            {
                m_newsock.SendTo(m_data, message.Length, SocketFlags.None, m_remote);
            }
            catch (Exception e)
            {
                InstantLogger.err("udp send err");
                //Logger.err("udp send return err code: {0}", e.ToString());
                //throw;
            }
            
            return 0;
        }
    }
}
