﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Implements;

namespace OGDecarbonaterFine
{
    internal static partial class Iterator
    {
        public static HeatDataReceiver Receiver;
        public const int PeriodSec = 3; // время сглаживания
        public const int IntervalSec = 1; // интервал расчетов
        public static Timer IterateTimer = new Timer(IntervalSec*1000);
        public static RecalculateData CurrentState;
        public static List<InputData> InputDataBuffer;
    }
}