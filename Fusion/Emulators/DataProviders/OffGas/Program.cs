using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Core;
using System.Threading;
using Tools.PldParser;
using Tools.Emulator;
using Client.MainGate;
namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //InstanceContext context = new InstanceContext();
            System.Threading.Thread thread = new Thread(new System.Threading.ThreadStart(ProccesAsync));
            thread.IsBackground = true;
            thread.Start();
            Console.WriteLine("The client(DataProvider) is ready.");
            Console.WriteLine("Press <ENTER> to terminate.");
            Console.WriteLine();
            Console.ReadLine();
        }

        public static void ProccesAsync()
        {
            MainGateClient mainGate = new MainGateClient(new InstanceContext(new DummyListener()));
            /*System.IO.DirectoryInfo dir= new System.IO.DirectoryInfo(@"..\..\..\..\..");
            Console.WriteLine("{0}", dir.FullName);
            Console.ReadLine();*/
            PldParser pldParser = new PldParser(@"..\..\..\..\..\Emulators\Data\pldx\C1_01.10.2011.pldx");
            for (;;)
            {
                foreach (Fusion fusion in pldParser.Fusions)
                {
                    foreach (TrendPoint point in fusion.Points)
                    {
                        mainGate.PushEvent(new OffGasEvent(point.H2,point.O2,point.CO,point.CO2,point.N2,point.Ar));
                        Console.WriteLine("{0}", string.Format("H2={0} O2={1} CO={2} CO2={3} N2={4} Ar={5} send",
                                  point.H2,point.O2,point.CO,point.CO2,point.N2,point.Ar));
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }
        }
    }
}
