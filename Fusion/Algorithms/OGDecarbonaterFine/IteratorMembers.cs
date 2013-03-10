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
        public const string ArchDir = "OGDecarbonaterFineArch";
        public const char Separator = ';';
        public const int MatrixLength = 20; // размер матрикса

        // Vars
        public static HeatDataReceiver Receiver;
        public static Timer IterateTimer = new Timer(IntervalSec*1000);
        public static RecalculateData CurrentState;
        public static List<InputData> InputDataBuffer;
        public static XimTable HimMaterials;
        public static string ArchFileName;
        public static SupportMaterials MaterialsZeroLevel;
        public static string CSVHimFilePath;// = "HimMaterials.csv"; // файл с химиями
        public static List<MFOGDFData> Matrix; // матрикс для расчета коэффицентов для поправки
        public static List<MFOGDFData> QueueWaitCarbon; // очередь ожидания углерода
        public static string MatrixFileName; //файл матрикса
    }
}