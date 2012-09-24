using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using Converter;
using Implements;

namespace TransferModelOutput
{
    internal static class Program
    {
        public static ConnectionProvider.Client CoreGate;
        private static Random rnd = new Random();

        private static Double randomFromTo(Double low, Double high)
        {
            if (low >= high) throw new Exception("randomFromTo: invalid margins specified");
            var d = (high - low)/1000;
            return low + d*rnd.Next(1001);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var l = new Logger("TransferModelOutput"))
            {
                try
                {
                    var pointStyle = new NumberFormatInfo();
                    pointStyle.NumberDecimalSeparator = ".";
                    var fex = new ConnectionProvider.FlexHelper("Model.Dynamic.Output.PerSecond");
                    CoreGate = new ConnectionProvider.Client();
                    var conf = ConfigurationManager.OpenExeConfiguration("");
                    var settings = conf.AppSettings.Settings;
                    do
                    {
                        foreach (KeyValueConfigurationElement kvel in settings)
                        {
                            string[] marg = kvel.Value.Split(':');
                            fex.AddArg(kvel.Key,
                                       randomFromTo(Convert.ToDouble(marg[0], pointStyle),
                                                    Convert.ToDouble(marg[1], pointStyle)));
                        }
                        fex.Fire(CoreGate);
                        l.msg("Event fired>\n{0}", fex.evt);
                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write(".");
                            System.Threading.Thread.Sleep(300);
                        }
                        fex.ClearArgs();
                        Console.Write(">");
                    } while ("q" != Console.ReadLine());
                    Console.WriteLine("Done");
                }
                catch (Exception e)
                {
                    l.err("TransferModelOutput.Main exception {0}", e);
                }
            }
        }
    }
}