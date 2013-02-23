using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HeatCharge;
using Implements;
using Converter;

namespace AlgorithmsUI
{
    public class WordPool<X> : Dictionary<string, X>
    {
        private readonly X nullValue;
        public WordPool(X nv)
        {
            nullValue = nv;
        }
        public X GetWord(string Key)
        {
            if (this.ContainsKey(Key)) return this[Key];
            return nullValue;
        }
        public void SetWord(string Key, X Value)
        {
            if (this.ContainsKey(Key))
            {
                this[Key] = Value;
            }
            else if (Value.Equals(nullValue))
            {
            }
            else
            {
                this.Add(Key, Value);
            }
        }
    }
    static class Program
    {
        public static MixtureInitial face;
        public static CoreListener listener;
        public static ConnectionProvider.Client MainGate;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Logger l = new Logger("AlgorithmsUI"))
            {
                var o = new FlexEvent("suka"); 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                    
                var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
                var formId = Convert.ToUInt32(mainConf.AppSettings.Settings["FormToOpen"].Value);
                listener = new CoreListener();
                MainGate = new ConnectionProvider.Client(listener);
                MainGate.Subscribe();
                switch (formId)
                {
                    case 0:
                        Application.Run(new Form1());
                        break;
                    case 1:
                        face = new MixtureInitial();
                        listener.Init();
                        Application.Run(face);
                        break;
                }
            }
        }
    }
}
