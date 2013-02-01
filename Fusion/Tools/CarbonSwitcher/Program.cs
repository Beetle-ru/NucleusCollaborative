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
        public const int SupportModels = 4;
        public static Conf Cfg;
        public static Configuration MainConf;
        public static double KFirst; // K первого углерода для плавного перехода
        public static double KSecond; // K второго углерода для плавного перехода
        public const double SwitchSpeed = 0.2; // скорость плавного перехода
        public static int LastIterateSecond;
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

            KFirst = 1; // сначала показываем первую модель
            KSecond = 1 - KFirst;
            LastIterateSecond = 0;
        }

        public static void Iterate()
        {
            //var currentS2 = DateTime.Now.Second;
            //Console.WriteLine("now = {0}; last = {2}; delta = {1}", currentS2, Math.Abs(LastIterateSecond - currentS2), LastIterateSecond);
            //if (Math.Abs(LastIterateSecond - currentS2) >= 1)
            //{
            //    LastIterateSecond = currentS2;
            //    Console.Write("#");
            //}
            if (ModelList[Cfg.SecondModel].IsStarted)
            {
                if (!ModelList[Cfg.SecondModel].IsFixed)
                {
                    //FireCarbon(ModelList[Cfg.SecondModel].C);
                    FireCarbon(ModelList[Cfg.FirstModel].C * KFirst + ModelList[Cfg.SecondModel].C * KSecond);
                    KSecond = 1 - KFirst;
                    var currentSecond = DateTime.Now.Second;
                    if (Math.Abs(LastIterateSecond - currentSecond) >= 1) // чтоб не чаще 1 раза в секунду
                    {
                        LastIterateSecond = currentSecond;
                        if (KFirst > 0) KFirst -= SwitchSpeed;
                        else KFirst = 0;
                        //Console.Write("#");
                    }
                    
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
                KFirst = 1;
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
