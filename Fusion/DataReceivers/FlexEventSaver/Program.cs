using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converter;

namespace FlexEventSaver
{
    class Program
    {
        public static string m_startsWith;
        static ConnectionProvider.Client m_listenGate;
        static void Main(string[] args)
        {
            var o = new TestEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();
            var conf = ConfigurationManager.OpenExeConfiguration("");
            var settings = conf.AppSettings.Settings;
            m_startsWith = Convert.ToString(settings["?"].Value);
            Console.WriteLine("FlexEventSaver started");
            Console.ReadLine();
        }
    }
}
