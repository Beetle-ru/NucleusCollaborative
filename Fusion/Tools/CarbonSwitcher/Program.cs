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

            if (ModelList[Cfg.SecondModel].IsStarted)
            {
                if (!ModelList[Cfg.SecondModel].IsFixed)
                {
                    if (ModelList[Cfg.SecondModel].C > 0)
                    {
                        //FireCarbon(ModelList[Cfg.SecondModel].C, 2);


                        var currentSecond = DateTime.Now.Second;
                        if (Math.Abs(LastIterateSecond - currentSecond) >= 1) // чтоб не чаще 1 раза в секунду
                        {
                            
                            var secondCarbon = ModelList[Cfg.FirstModel].C * KFirst + ModelList[Cfg.SecondModel].C * KSecond;
                            Implements.InstantLogger.msg("CReal = {0}; Cmixed = {1}; K1 = {2}; K2 = {3}; K1 + K2 = {4}", ModelList[Cfg.SecondModel].C, secondCarbon, KFirst, KSecond, KFirst + KSecond);
                            var periodSwitch = KSecond < 1 ? -2 : 2; // если еще не переключились, то -2
                            FireCarbon(secondCarbon, periodSwitch);

                            LastIterateSecond = currentSecond;
                            if (Math.Round(KFirst - SwitchSpeed, 3) > 0.0) KFirst -= SwitchSpeed;
                            else KFirst = 0.0;
                            KFirst = Math.Round(KFirst, 5);
                            KSecond = Math.Round(1.0 - KFirst, 5);
                            //Console.Write("#");
                        }
                    }
                }
                else
                {
                    if (!ModelList[Cfg.SecondModel].IsFiredFixed)
                    {
                        var fex = new FlexHelper("CarbonSwitcher.DataFix");
                        fex.AddArg("C", ModelList[Cfg.SecondModel].C);
                        fex.Fire(Program.MainGate);
                        ModelList[Cfg.SecondModel].IsFiredFixed = true;
                    }
                }
            }
            else
            {
                FireCarbon(ModelList[Cfg.FirstModel].C,1);
                KFirst = 1;
            }
        }

        public static void FireCarbon(double c, int periodlNumber)
        {
            var fex = new FlexHelper("CarbonSwitcher.Result");
            fex.AddArg("C", c);
            fex.AddArg("PeriodlNumber", periodlNumber);
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
