using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using ConnectionProvider;
using Converter;
using System.Configuration;
using HeatCharge;
using Implements;

namespace OffGasDecarbonater {
    internal class Program {
        public static Client PushGate;
        private static Client m_listenGate;
        public static int ConverterNumber;

        private static void Main(string[] args) {
            try {
                ConverterNumber = Convertion.StrToInt32(
                    (string) ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["converterNumber"].Value);
            }
            catch (Exception e) {
                InstantLogger.err("Bad config called error: {0}", e.ToString());
                throw e;
            }

            CIterator.Init();


            var o = new HeatChangeEvent();
            PushGate = new Client();
            m_listenGate = new Client(new Listener());
            m_listenGate.Subscribe();

            CIterator.IterateTimer.Elapsed += new ElapsedEventHandler(CIterator.IterateTimeOut);
            CIterator.IterateTimer.Enabled = true;

            Console.WriteLine("Carbone processor is running, press enter to exit");
            Console.ReadLine();
        }
    }
}