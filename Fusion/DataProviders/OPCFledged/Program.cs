using System;
using ConnectionProvider;
using System.Threading;
using Converter;
using Implements;

namespace OPCFledged
{
    //public Dictionary n_table = new Dictionary(Of string, string);

    class Program
    {
        private static ConnectionProvider.Client m_listenGate;
        public static ConnectionProvider.Client m_pushGate;
        public static OpcConnector OPCCon;

        static void Main(string[] args)
        {
            using (Logger l = new Logger("Fledged"))
            {
                try
                {
                    var o = new HeatChangeEvent(); /// нужно чтобы сборка загрузилась
                    m_pushGate = new ConnectionProvider.Client();

                    var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
                    var progId = mainConf.AppSettings.Settings["OPCServerProgID"].Value;
                    var addrFmt = mainConf.AppSettings.Settings["OPCAddressFormat"].Value;
                    var dest = mainConf.AppSettings.Settings["OPCDestination"].Value;
                    var convSchema = Convert.ToInt32(mainConf.AppSettings.Settings["OPCConvSchema"].Value);
                    var reqUpdateRateMs = Convert.ToInt32(mainConf.AppSettings.Settings["OPCCReqUpdateRate_ms"].Value);
                    l.msg("OPC Fledged started with ProgID=[{0}] for {1} aids conv schema {2}", progId, dest, convSchema);
                    OPCCon = new OpcConnector(progId, dest, addrFmt, convSchema, reqUpdateRateMs);

                    var receiverThread = new Thread(Receiver);
                    receiverThread.Start();

                    Console.WriteLine("Нажмите <ENTER> для выхода.");
                    Console.ReadLine();
                    OPCCon.CloseConnection();
                    l.msg("OPC Fledged exit");
                }
                catch(Exception e)
                {
                    l.err("Exception <{0}> -- execution terminated", e.ToString());
                    
                }
                catch
                {
                    l.err("Unknown exception -- execution terminated");
                }
            }
        }

        static void Receiver(object state)
        {
            using (Logger l = new Logger("CoreListener"))
            {
                var o = new HeatChangeEvent();
                var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
                m_listenGate = new ConnectionProvider.Client(new CoreListener(
                    mainConf.AppSettings.Settings["OPCAddressFormat"].Value,
                    mainConf.AppSettings.Settings["OPCDestination"].Value
                    ));
                m_listenGate.Subscribe();
            }
        }
    }
}
