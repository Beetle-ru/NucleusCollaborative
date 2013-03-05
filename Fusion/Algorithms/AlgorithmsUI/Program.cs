using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using HeatCharge;
using Implements;
using Converter;
using Oracle.DataAccess.Client;

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
        public static long makeKey()
        {
            System.Guid g = new System.Guid();
            g = Guid.NewGuid();
            string s = g.ToString();
            long res = 0;
            for (var i = 0; i < 7; i++)
            {
                if (s.ElementAt(i) < '9') res += s.ElementAt(i) - '0';
                else if (s.ElementAt(i) < 'f') res += s.ElementAt(i) - 'a' + 10;
                res *= 16;
            }
            return res;
        }
#if DB_IS_ORACLE
        public static OracleConnection OraConn;
        public static OracleCommand OraCmd;
        //public static OracleConnection OraConnX;
        //public static OracleCommand OraCmdX;
        public static OracleDataReader OraReader;
#endif
        public static bool slagFormerIsMaxG;
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
#if DB_IS_ORACLE
                var str = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["ConnectionString"].Value;
                OraConn = new OracleConnection(str);
                OraCmd = OraConn.CreateCommand();
                OraCmd.CommandType = System.Data.CommandType.Text;
                OraCmd.BindByName = true;
                //OraConnX = new OracleConnection(str);
                //OraCmdX = OraConnX.CreateCommand();
                //OraCmdX.CommandType = System.Data.CommandType.Text;
#endif
                var o = new FlexEvent("suka"); 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                    
                var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
                slagFormerIsMaxG = (Convert.ToUInt32(mainConf.AppSettings.Settings["UseMaxG"].Value) == 1);
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
