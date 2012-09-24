using System;
using ConnectionProvider;
using System.Threading;
using Converter;
using Implements;

namespace OPCDirectWriter
{


    class Program
    {
        private static ConnectionProvider.Client m_mainGate;
        public static OpcConnector OPCCon;

        static void Main(string[] args)
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
                OPCCon = new OpcConnector(
                mainConf.AppSettings.Settings["OPCServerProgID"].Value,
                mainConf.AppSettings.Settings["PLCName"].Value,
                mainConf.AppSettings.Settings["OPCAddressFormat"].Value
            );

            var receiverThread = new Thread(Receiver);
            receiverThread.Start();

            InstantLogger.log("Нажмите <ENTER> для выхода.");
            Console.ReadLine();
            OPCCon.CloseConnection();
        }

        static void Receiver(object state)
        {
            var o = new HeatChangeEvent();
            m_mainGate = new ConnectionProvider.Client(new Listener());
            m_mainGate.Subscribe();
        }
    }
}
