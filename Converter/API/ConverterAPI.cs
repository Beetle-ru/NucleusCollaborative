using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Converter.API
{
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Single,
    InstanceContextMode = InstanceContextMode.PerCall)]

    public class ConverterAPI : IConverterAPI
    {

        public Int64 GetHeatNumber()
        {
            return Module.Instance._Heat.Number;
        }

        public int GetConverterNumber()
        {
            return Module.Instance._Heat.AggregateNumber;
        }

        public int GetTeamNumber()
        {
            return Module.Instance._Heat.Number == -1 ? -1 : Module.Instance._Heat.TeamNumber; // нет 
        }

        public string GetGrade()
        {
            return Module.Instance._Heat.Number == -1 ? string.Empty : Module.Instance._Heat.Grade;
        }

        public SteelAttributes GetActualSteelAttributes()
        {
            return Module.Instance._Heat.Number == -1 ? null : Module.Instance._Heat.Actual;
        }

        public SteelAttributes GetPlannedSteelAttributes()
        {
            return Module.Instance._Heat.Number == -1 ? null : Module.Instance._Heat.Planned; // есть температура из контроллера, углерода пока нет
        }

        public DateTime GetBlowingStartTime()
        {
            return Module.Instance._Heat.Number == -1 ? DateTime.MinValue : Module.Instance._Heat.StartDate;
        }

        public List<OffGasEvent> GetOffGasEvents()
        {
            return Module.Instance._Heat.Number == -1 ? null : Module.Instance._Heat.OffGasHistory;
        }

        public List<LanceEvent> GetLanceEvents()
        {
            return Module.Instance._Heat.Number == -1 ? null : Module.Instance._Heat.LanceHistory;
        }

        public List<OffGasAnalysisEvent> GetOffGasAnalysisEvents()
        {
            return Module.Instance._Heat.Number == -1 ? null : Module.Instance._Heat.OffGasAnalysisHistory;
        }

        public List<string> GetBlowingSchemas()
        {
            List<string> res = new List<string>();
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\dat"))
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\dat");
            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "\\dat");
            FileInfo[] files = di.GetFiles("*.heatScript");
            foreach (FileInfo fi in files)
            {
                res.Add(fi.Name.Replace(fi.Extension, ""));
            }
            return res;
        }

        public comBlowingSchemaEvent[] LoadBlowingSchema(string schemaName)
        {
            StoredScheme result = new StoredScheme();
            try
            {
                using (FileStream fs = new FileInfo(
                    string.Format("{0}dat\\{1}.heatScript", System.AppDomain.CurrentDomain.BaseDirectory, schemaName)).OpenRead())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.AssemblyFormat  = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                    formatter.Binder = new PreMergeToMergedDeserializationBinder();
                    result = (StoredScheme)formatter.Deserialize(fs);
                    fs.Close();
                }
            }
            catch { };
            return result.BlowingSchemas;
        }

        public void SaveBlowingSchema(string schemaName, comBlowingSchemaEvent[] schemas)
        {
            StoredScheme str = new StoredScheme();
            str.BlowingSchemas = schemas;
            if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\dat"))
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + "\\dat");

            using (FileStream fs = new FileInfo(
                string.Format("{0}\\dat\\{1}.heatScript", System.AppDomain.CurrentDomain.BaseDirectory, schemaName)).Create())
            {
                new BinaryFormatter().Serialize(fs, str);
                fs.Close();
            }
        }

        public void RunBlowingSchema(string schemaName)
        {
            Module.Instance.PushEvent(new comO2FlowRateEvent() { O2TotalVol = 22000, SublanceStartO2Vol = 17000 });
            Module.Instance._Heat.BlowingScheme = LoadBlowingSchema(schemaName);
            Module.Instance._Heat.CurrentBlowingScheme = -1;
        }
    }
}


