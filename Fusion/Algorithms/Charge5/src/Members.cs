using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;
using Charge5Classes;
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
        public static string DefaultPattern;
        public static List<CSVTableParser> Tables;
        public static CSVTableParser InitTbl;
        public static List<string> TablePaths;
        public const int MaxTables = 7;
        public const string PIName = "Path.init";
        public static bool CalcModeIsAutomatic;

        public const int IntervalSec = 1; // интервал расчетов
        public static Timer IterateTimer = new Timer(IntervalSec * 1000);

        public static InData AutoInData;
        private static InData m_autoInDataPrevious; // для отслеживания изменений
        public static bool IsRefrashData; // обновлены данные для пересчета

        public static int ConverterNumber;
    }
}