using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;

namespace CarbonSwitcher
{
    class Program
    {
        public static Client MainGate;
        public static List<Models> ModelList;
        public const int SupportModels = 3;
        public static Conf Cfg;
        public static Configuration MainConf;
        static void Main(string[] args)
        {
            Init();
            Console.WriteLine("Press Enter for exit");
            Console.ReadLine();
        }

        public static void Init()
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();
            
            Cfg = new Conf();
            Cfg.FirstModel = Int32.Parse(MainConf.AppSettings.Settings["DefFirstModel"].Value);
            Cfg.SecondModel = Int32.Parse(MainConf.AppSettings.Settings["DefSecondModel"].Value);

            Reset();
        }

        public static void Reset()
        {
            ModelList = new List<Models>();
            for (int i = 0; i < SupportModels; i++)
            {
                ModelList.Add(new Models());
            }
        }

        public static void Iterate()
        {
            if (ModelList[Cfg.SecondModel].IsStarted)
            {
                if (!ModelList[Cfg.SecondModel].IsFixed)
                {
                    FireCarbon(ModelList[Cfg.SecondModel].C);
                }
                else
                {
                    if (!ModelList[Cfg.SecondModel].IsFiredFixed)
                    {
                        var fex = new FlexHelper("CarbonSwitcher.DataFix");
                        fex.Fire(Program.MainGate);
                        ModelList[Cfg.SecondModel].IsFiredFixed = true;
                    }
                }
            }
            else
            {
                FireCarbon(ModelList[Cfg.FirstModel].C);
            }
        }

        public static void FireCarbon(double c)
        {
            var fex = new FlexHelper("CarbonSwitcher.Result");
            fex.AddArg("C", c);
            fex.Fire(Program.MainGate);
        }
    }

    class Models
    {
        public double C;
        public bool IsStarted;
        public bool IsFixed;
        public bool IsFiredFixed;
    }

    class Conf
    {
        public int FirstModel;
        public int SecondModel;
        //public int ThirdModel;
    }
}
