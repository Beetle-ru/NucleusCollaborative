using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.ServiceModel;
using Converter;

namespace OffGasAnalysis
{
    class GasManager
    {
        GasListener gl5153 = null;
        GasListener gl5155 = null;
        GasListener gl5157 = null;

        Socket gs5154 = null;
        Socket gs5156 = null;
        Socket gs5158 = null;
        ConnectionProvider.Client mainGate1;
        ConnectionProvider.Client mainGate2;
        ConnectionProvider.Client mainGate3;

        object Lock = new object();
 
        DateTime DDE1lastTime = DateTime.Now;
        DateTime DDE2lastTime = DateTime.Now.AddSeconds(10);
        DateTime DDE3lastTime = DateTime.Now.AddSeconds(20);

        DateTime Core1lastTime = DateTime.Now;
        DateTime Core2lastTime = DateTime.Now.AddSeconds(10);
        DateTime Core3lastTime = DateTime.Now.AddSeconds(20);


        TcpClient DDEClient = null;

        public void Start()
        {
            //gs5154 = InitRequestSocket("224.0.1.1", 5154);
            //gs5156 = InitRequestSocket("224.0.1.1", 5156);
            //gs5158 = InitRequestSocket("224.0.1.1", 5158);

            DDEClient = new TcpClient();
            //DDEClient.Connect("smkbof1", 1419);

            mainGate1 = new ConnectionProvider.Client("Converter1");
            mainGate2 = new ConnectionProvider.Client("Converter2");
            mainGate3 = new ConnectionProvider.Client("Converter3");

            gl5153 = new GasListener("224.0.1.1", 5153);
            gl5153.OnOffGas += new OnOffGasEventHandler(onConverter1);
            gl5153.OnOffGas += new OnOffGasEventHandler(onConverter1DDE);

            gl5155 = new GasListener("224.0.1.1", 5155);
            gl5155.OnOffGas += new OnOffGasEventHandler(onConverter2);
            gl5155.OnOffGas += new OnOffGasEventHandler(onConverter2DDE);

            gl5157 = new GasListener("224.0.1.1", 5157);
            gl5157.OnOffGas += new OnOffGasEventHandler(onConverter3);
            gl5157.OnOffGas += new OnOffGasEventHandler(onConverter3DDE);

            //var GroupAddress = IPAddress.Parse("224.168.100.2");
            //var GroupPort = 11000;

            //Thread thread = new System.Threading.Thread(RequestsThread);
            //thread.Start();
        }

        private void sendToSMK(byte[] data)
        {
            lock (Lock)
            {
                if (!DDEClient.Connected)
                {
                    try // тупа рубим все ексепшины - нам надо ити дальше пака не подымится сервак
                    {
                        DDEClient.Close();
                        DDEClient = new TcpClient();
                        DDEClient.Connect("smkbof1", 1419);
                    }
                    catch { }
                }

                if (DDEClient.Connected)
                {
                    try // тупа рубим все ексепшины - нам надо ити дальше пака не подымится сервак
                    {
                        DDEClient.Client.Send(data);
                    }
                    catch { }
                }
            }
        }

        private string formatSMKData(GasData data, int converteNumber)
        {
            return String.Format(":GRANAT;{0};K{8};Blasen {1};{2,7:0.00};{3,7:0.00};{4,7:0.00};{5,7:0.00};{6,7:0.00};{7,7:0.00}",
                DateTime.Now.ToString("HH:mm:ss"),
                1,
                data.H2, data.CO, data.N2, data.O2, data.AR, data.CO2,
                converteNumber);
        }

        private void onConverter1(GasData data)
        {
            try
            {
                if ((DateTime.Now - Core1lastTime).TotalMilliseconds > 500)
                {
                    mainGate1.PushEvent(new OffGasAnalysisEvent()
                        {
                            iCnvNr = 1,
                            Ar = data.AR,
                            CO = data.CO,
                            CO2 = data.CO2,
                            H2 = data.H2,
                            N2 = data.N2,
                            O2 = data.O2,
                            Time = DateTime.Now
                        });
                    Core1lastTime = DateTime.Now;
                }
            }
            catch { }
            //Console.WriteLine(data.ToString());
        }

        private void onConverter2(GasData data)
        {
            try
            {
                if ((DateTime.Now - Core2lastTime).TotalMilliseconds > 500)
                {
                    mainGate2.PushEvent(new OffGasAnalysisEvent()
                    {
                        iCnvNr = 2,
                        Ar = data.AR,
                        CO = data.CO,
                        CO2 = data.CO2,
                        H2 = data.H2,
                        N2 = data.N2,
                        O2 = data.O2,
                        Time = DateTime.Now
                    });
                    Core2lastTime = DateTime.Now;
                }
            }
            catch { }
            //Console.WriteLine(data.ToString());
        }

        private void onConverter3(GasData data)
        {
            try
            {
                if ((DateTime.Now - Core3lastTime).TotalMilliseconds > 500)
                {
                    mainGate2.PushEvent(new OffGasAnalysisEvent()
                    {
                        iCnvNr = 3,
                        Ar = data.AR,
                        CO = data.CO,
                        CO2 = data.CO2,
                        H2 = data.H2,
                        N2 = data.N2,
                        O2 = data.O2,
                        Time = DateTime.Now
                    });
                    Core3lastTime = DateTime.Now;
                }
            }
            catch { }
            //Console.WriteLine(data.ToString());
        }

        private void onConverter1DDE(GasData data)
        {
            try
            {
                if ((DateTime.Now - DDE1lastTime).TotalSeconds > 30)
                {
                    DDE1lastTime = DateTime.Now;
                    string logMessage = formatSMKData(data, 1);
                    Console.WriteLine(logMessage);
                    sendToSMK(System.Text.ASCIIEncoding.ASCII.GetBytes(logMessage));
                }
            }
            catch { }
        }

        private void onConverter2DDE(GasData data)
        {
            try
            {
                if ((DateTime.Now - DDE2lastTime).TotalSeconds > 30)
                {
                    string logMessage = formatSMKData(data, 2);
                    DDE2lastTime = DateTime.Now;
                    Console.WriteLine(logMessage);
                    sendToSMK(System.Text.ASCIIEncoding.ASCII.GetBytes(logMessage));
                }
            }
            catch { }
        }

        private void onConverter3DDE(GasData data)
        {
            try
            {
                if ((DateTime.Now - DDE3lastTime).TotalSeconds > 30)
                {
                    string logMessage = formatSMKData(data, 3);
                    DDE3lastTime = DateTime.Now;
                    Console.WriteLine(logMessage);
                    sendToSMK(System.Text.ASCIIEncoding.ASCII.GetBytes(logMessage));
                }
            }
            catch { }
        }


        private Socket InitRequestSocket(string IP, int port)
        {
            IPAddress ip = IPAddress.Parse(IP);

            Socket s = new Socket(AddressFamily.InterNetwork,
                            SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.IP,
                SocketOptionName.AddMembership, new MulticastOption(ip, IPAddress.Any));

            s.SetSocketOption(SocketOptionLevel.IP,
                SocketOptionName.MulticastTimeToLive, 2);
            IPEndPoint ipep = new IPEndPoint(ip, port);
            s.Connect(ipep);
            return s;
        }

        private void RequestsThread()
        {
            byte[] packetData = new byte[6];
            int packetNumber = 0;
            while (true)
            {
                packetNumber++;
                if (packetNumber > 256) packetNumber = 1;
                packetData[0] = 1;
                packetData[1] = 30;
                packetData[2] = 31;

                packetData[3] = (byte)((packetNumber >> 4) + 30);
                packetData[4] = (byte)((packetNumber & 0x0F) + 30);

                packetData[5] = 4;

                UpdateReqiest(packetData);
                Thread.Sleep(500);
            }
        }

        void UpdateReqiest(byte[] packetData)
        {
            /*
            gs5154.Send(packetData);
            gs5156.Send(packetData);
            gs5158.Send(packetData);
             * */

		}
    }
}
