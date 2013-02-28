using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implements;

namespace OGDecarbonaterFine
{
    class XimTable
    {
        private Dictionary<string,Dictionary<string,double>> m_table; // <material,<him element, value>>

        public void LoadFromCSV(string file)
        {
            m_table = new Dictionary<string, Dictionary<string, double>>();
            var loader = new CSVTableParser();
            loader.Description = GetDescription();
            loader.FileName = file;
            loader.Separator = ';';
            loader.Load();
            foreach (var row in loader.Rows)
            {
                var himDic = new Dictionary<string, double>();
                foreach (var o in row.Cell)
                {
                    if (o.Key != "MaterialName")
                    {
                        himDic.Add((string)o.Key,(double)o.Value);
                    }
                }
                m_table.Add((string)row.Cell["MaterialName"], himDic);
            }
        }
        private List<ColumnPath> GetDescription()
        {
            var desc = new List<ColumnPath>();

            desc.Add(new ColumnPath() {ColumnName = "MaterialName", ColumnType = typeof(string)});
            desc.Add(new ColumnPath() { ColumnName = "CaO", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "MgO", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "SiO2", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "Fe2O3", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "Al2O3", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "S", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "PMPP", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "C", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "CO2", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "P", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "Al", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "H", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "O", ColumnType = typeof(double) });
            desc.Add(new ColumnPath() { ColumnName = "N", ColumnType = typeof(double) });

            return desc;
        }

        public double GetHimValue(string material, string himElement)
        {
            if (!m_table.ContainsKey(material)) return 0;
            if (!m_table[material].ContainsKey(himElement)) return 0;

            return m_table[material][himElement];
        }
    }
}
