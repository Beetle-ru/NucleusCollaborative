using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;

namespace Charge5
{
    internal partial class Program
    {
        public static void SetDescriptionPI(ref CSVTableParser table)
        {
            table.Description.Add(new ColumnPath() { ColumnName = "Index", ColumnType = typeof(int) });
            table.Description.Add(new ColumnPath() { ColumnName = "TableName", ColumnType = typeof(string) });
        }
        public static void SetDescriptionTBL(ref CSVTableParser table)
        {
            table.Description.Add(new ColumnPath() { ColumnName = "MinSiHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MaxSiHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MinTHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MaxTHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassScrap", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassLime", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassDolom", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "UVSMassDolom", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassFOM", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "UVSMassFOM", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassDolomS", ColumnType = typeof(double) });
        }
    }
}