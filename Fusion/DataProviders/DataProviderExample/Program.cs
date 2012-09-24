using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Core;
using System.Threading;
using Converter;
using System.Globalization;
using System.Reflection;
using System.IO;

namespace Client
{
    class Program
    {
        static ConnectionProvider.Client mainGate;
        static void Main(string[] args)
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var conf = new System.ServiceModel.Configuration.ClientSection();

            //((System.ServiceModel.Configuration.ClientSection)mainConf.SectionGroups["system.serviceModel"].Sections["client"]).Endpoints[0]

            mainGate = new ConnectionProvider.Client();
            mainGate.Subscribe();
            Console.WriteLine("Press Enter");
            Console.ReadLine();
            mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = 1001 });
            Console.WriteLine("Press Enter");
            Console.ReadLine();
            mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = 100 });
            Console.WriteLine("Press Enter");
            Console.ReadLine();
            mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = 101 });
            Console.WriteLine("Press Enter");
            Console.ReadLine();
            CultureInfo culture = CultureInfo.InvariantCulture;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            // load spim-generated data from embedded resource file
            //const string spimDataName = ;
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Press Enter");
                Console.ReadLine();
               // mainGate.PushEvent(new Esms.FingersStateEvent { FingersOpened = true, Time = DateTime.Now });
            }
                 //mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = 1001, iCnvNr = DateTime.Now.Second % 2 + 1, Time = DateTime.Now });
                 //Console.ReadLine();
                 mainGate.PushEvent(new LanceEvent() { O2TotalVol = 20 });
                 Console.ReadLine();
                 for (int i = 0; i < 1000; i++)
                 {
                     Thread.Sleep(100);
                     mainGate.PushEvent(new LanceEvent() { O2TotalVol = i * 20 });
                     
                 }
                 Console.ReadLine();
        }

        public static void thr()
        {
            for (int i = 1; i < 20; i++)
            {
                mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = i, iCnvNr = 1 });
            }
        }
    }

    class Sendor
    {
        ConnectionProvider.Client mainGate;

        public Sendor(ConnectionProvider.Client mainGate)
        {
            this.mainGate = mainGate;
        }

        public void thr()
        {
            for (int i = 1; i < 20; i++)
            {
                mainGate.PushEvent(new HeatChangeEvent() { HeatNumber = i, iCnvNr = 1 });
                Thread.Sleep(0);
            }
        }
    }
}
