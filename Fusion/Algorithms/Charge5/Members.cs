using System.Collections.Generic;
using System.Configuration;
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
        public static Client MainGate;
        public static Configuration MainConf;
        public static char Separator;
        public static string StorePath;
        public static List<CSVTableParser> Tables;
        public static CSVTableParser InitTbl;
        public static List<string> TablePaths;
        public const int MaxTables = 7;
        public const string PIName = "Path.init";
    }
}