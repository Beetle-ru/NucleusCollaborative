using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeatPassport
{
    class Program
    {
        private static HeatPassportManager m_HeatPassportManager1;
        private static HeatPassportManager m_HeatPassportManager2;
        private static HeatPassportManager m_HeatPassportManager3;

        static void Main(string[] args)
        {
            if (IsInConfig("Converter1"))
            {
                m_HeatPassportManager1 = new HeatPassportManager(1);
                m_HeatPassportManager1.Start();
                Console.WriteLine("HeatPassportManager для конвертера 1 стартовал успешно...");
            }

            if (IsInConfig("Converter2"))
            {
                m_HeatPassportManager2 = new HeatPassportManager(2);
                m_HeatPassportManager2.Start();
                Console.WriteLine("HeatPassportManager для конвертера 2 стартовал успешно...");
            }

            if (IsInConfig("Converter3"))
            {
                m_HeatPassportManager3 = new HeatPassportManager(3);
                m_HeatPassportManager3.Start();
                Console.WriteLine("HeatPassportManager для конвертера 3 стартовал успешно...");
            }

            Console.WriteLine("Нажмите на любую клавишу ...");
            Console.ReadLine();
        }

        private static bool IsInConfig(string Destination)
        {
            var mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            System.ServiceModel.Configuration.ClientSection clientSection = (System.ServiceModel.Configuration.ClientSection)mainConf.SectionGroups["system.serviceModel"].Sections["client"];
            for (int i = 0; i < clientSection.Endpoints.Count; i++)
            {
                if (clientSection.Endpoints[i].Name == Destination)
                    return true;
            }
            return false;
        }
    }
}
