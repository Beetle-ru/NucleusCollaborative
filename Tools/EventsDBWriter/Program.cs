using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;


namespace Tools.DB
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            EventDBWriter eventDB = new EventDBWriter(mainConf.AppSettings.Settings["Module"].Value, int.Parse(mainConf.AppSettings.Settings["UnitNumber"].Value), mainConf.AppSettings.Settings["ChannelName"].Value);
            eventDB.Start();
            Console.WriteLine("Связь установлена. Слушаем события ...");
            Console.ReadLine();
        }
    }
}

