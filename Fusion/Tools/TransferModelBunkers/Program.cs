using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Converter;
using Implements;

namespace TransferModelBunkers
{
    class Program
    {
        private static ConnectionProvider.Client m_listenGate;
        public static ConnectionProvider.Client m_pushGate;
        static void Main(string[] args)
        {
            var o = new HeatChangeEvent();
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();
            m_pushGate = new ConnectionProvider.Client();
            Thread.Sleep(1000);
            // запрашиваем привязку бункеров к материалам
            m_pushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(BoundNameMaterialsEvent).Name });
            // навески
            m_pushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(visAdditionTotalEvent).Name });
            Thread.Sleep(1000);
            // текущий номер плавки
            m_pushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(HeatChangeEvent).Name });
            Console.WriteLine("TransferModelBunkers is running, press enter to exit");
            Console.ReadLine();
            Console.WriteLine("Bye!");
        }
    }
}
