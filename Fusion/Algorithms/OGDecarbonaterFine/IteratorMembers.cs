using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    internal static partial class Iterator
    {
        // Consts
        public const int PeriodSec = 3; // время сглаживания
        public const int IntervalSec = 1; // интервал расчетов
        public const string CSVHimFilePath = "HimMaterials.csv"; // файл с химиями
        public const string ArchDir = "OGDecarbonaterFineArch";

        // Vars
        public static HeatDataReceiver Receiver;
        public static Timer IterateTimer = new Timer(IntervalSec*1000);
        public static RecalculateData CurrentState;
        public static List<InputData> InputDataBuffer;
        public static XimTable HimMaterials;
        public static string ArchFileName;
    }
}