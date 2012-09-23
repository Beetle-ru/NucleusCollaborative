using System;
using System.Configuration;
using System.Reflection;

namespace DBWriterTT
{
    class Program
    {
        static void Main()
        {
            var mainConf = ConfigurationManager.OpenExeConfiguration("");
            var moduleName = mainConf.AppSettings.Settings["Module"].Value;
            var configurationSectionGroup = mainConf.SectionGroups["system.serviceModel"];
            if (configurationSectionGroup == null) return;
            var clientSection = (System.ServiceModel.Configuration.ClientSection)configurationSectionGroup.Sections["client"];
            
            if  (clientSection.Endpoints.Count == 0 )
            {
                Console.WriteLine("DbWriterTT не настоен на прием событий");
                return;
            }
            Assembly.LoadFrom(moduleName);
          //  for (var i = 0; i < clientSection.Endpoints.Count; i++)
           // {
                 var unitName = clientSection.Endpoints[0].Name;
                 Console.WriteLine(string.Format("Страт DbWriterTT для {0} ...", unitName));
                 new DbWriter().Start(unitName);
                 Console.WriteLine(string.Format("DbWriterTT для {0} стартовал успешно...", unitName));
           // }
            Console.ReadLine();
        }
    }
}
