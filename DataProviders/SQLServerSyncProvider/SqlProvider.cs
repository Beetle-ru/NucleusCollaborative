using System;
using System.Configuration;
using System.Timers;
using ConnectionProvider;
using Esms;

namespace SQLServerSyncProvider
{
    public class SqlProvider
    {
        private static Client _mainGate;
        private static DBLayer _dB;
        private static readonly Timer CheckTimer = new Timer(1000);

        public static void Main()
        {
            using (CheckTimer)
            {
                _mainGate = new Client();
                _mainGate.Subscribe();
                 _dB = new DBLayer(Convert.ToInt32(ConfigurationManager.AppSettings["UnitNumber"]));
                CheckTimer.Elapsed += CheckTimerElapsed;
                CheckTimer.Start();
                Console.WriteLine("Sync started. Press any key to stop.");
                Console.ReadKey();
                CheckTimer.Stop();
            }
        }

        private static void CheckTimerElapsed(object sender, ElapsedEventArgs e)
        {
            CheckTimer.Stop();
            CheckScrapLoadForUpdates();
            CheckTimer.Start();
         }

        private static void CheckScrapLoadForUpdates()
        {
            var dsetZavacs = _dB.ScrapLoad();
            if (dsetZavacs.Tables.Count == 0 || dsetZavacs.Tables[0].Rows.Count == 0) return;
            var start = GetDateTimeValueAsMinDefault(dsetZavacs.Tables[0].Rows[0]["Time_Start"].ToString());
            var end = GetDateTimeValueAsMaxDefault(dsetZavacs.Tables[0].Rows[0]["Time_End"].ToString());
            var ves = GetIntValue(dsetZavacs.Tables[0].Rows[0]["Ves_Nr"].ToString());
            var dsetWeight = _dB.ScrapWeight(start, end, ves);
            if (dsetWeight.Tables.Count == 0 || dsetWeight.Tables[0].Rows.Count == 0) return;
            for (var i = 0; i < dsetWeight.Tables[0].Rows.Count; i++)
            {
                var scrapLoad = new ScrapLoadEvent
                                    {
                                        Time = GetDateTimeValueAsMinDefault(dsetWeight.Tables[0].Rows[i]["Date_t"].ToString()),
                                        Id = GetIntValue(dsetZavacs.Tables[0].Rows[0]["Zavalk_Nr"].ToString()),
                                        ChargeNumber = GetIntValue(dsetZavacs.Tables[0].Rows[0]["Charge_Nr"].ToString()),
                                        TankNumber = GetIntValue(dsetZavacs.Tables[0].Rows[0]["Tank_Nr"].ToString()),
                                        TaskNumber = GetIntValue(dsetZavacs.Tables[0].Rows[0]["Task_Nr"].ToString()),
                                        Weight = GetFloatValue(dsetWeight.Tables[0].Rows[i]["Weight"].ToString())/100
                                    };
                scrapLoad = _dB.ScrapName(scrapLoad, GetIntValue(dsetWeight.Tables[0].Rows[i]["Skrap_Code"].ToString()));
                Console.WriteLine("ScrapLoadEvent pushed");
                _mainGate.PushEvent(scrapLoad);
            }
        }


        // фэйк для скарапа
        private static void CheckScrapLoadForUpdates2()
        {
            var dsetZavacs = _dB.ScrapLoad2();
            if (dsetZavacs.Tables.Count == 0 || dsetZavacs.Tables[0].Rows.Count == 0) return;
            for (var j = 0; j < dsetZavacs.Tables[0].Rows.Count; j++)
            {
                var start = GetDateTimeValueAsMinDefault(dsetZavacs.Tables[0].Rows[j]["Time_Start"].ToString());
                var end = GetDateTimeValueAsMaxDefault(dsetZavacs.Tables[0].Rows[j]["Time_End"].ToString());
                var ves = GetIntValue(dsetZavacs.Tables[0].Rows[j]["Ves_Nr"].ToString());
                var dsetWeight = _dB.ScrapWeight(start, end, ves);
                if (dsetWeight.Tables.Count == 0 || dsetWeight.Tables[0].Rows.Count == 0) continue;
                for (var i = 0; i < dsetWeight.Tables[0].Rows.Count; i++)
                {
                    var scrapLoad = new ScrapLoadEvent
                    {
                        Time = GetDateTimeValueAsMinDefault(dsetWeight.Tables[0].Rows[i]["Date_t"].ToString()),
                        Id = GetIntValue(dsetZavacs.Tables[0].Rows[j]["Zavalk_Nr"].ToString()),
                        ChargeNumber = GetIntValue(dsetZavacs.Tables[0].Rows[j]["Charge_Nr"].ToString()),
                        TankNumber = GetIntValue(dsetZavacs.Tables[0].Rows[j]["Tank_Nr"].ToString()),
                        TaskNumber = GetIntValue(dsetZavacs.Tables[0].Rows[j]["Task_Nr"].ToString()),
                        Weight = GetFloatValue(dsetWeight.Tables[0].Rows[i]["Weight"].ToString())/100
                    };
                    scrapLoad = _dB.ScrapName(scrapLoad, GetIntValue(dsetWeight.Tables[0].Rows[i]["Skrap_Code"].ToString()));
                    Console.WriteLine("ScrapLoadEvent pushed");
                    _mainGate.PushEvent(scrapLoad);
                }
            }
        }
        
        private static float GetFloatValue(string str)
        {
            float temp;
            return float.TryParse(str, out temp) ? temp : 0;
        }

        private static int GetIntValue(string str)
        {
            int temp;
            return int.TryParse(str, out temp) ? temp : 0;
        }

        private static DateTime GetDateTimeValueAsMinDefault(string str)
        {
            DateTime temp;
            return DateTime.TryParse(str, out temp) ? temp : DateTime.MinValue;
        }

        private static DateTime GetDateTimeValueAsMaxDefault(string str)
        {
            DateTime temp;
            return DateTime.TryParse(str, out temp) ? temp : DateTime.MaxValue;
        }

    }
}