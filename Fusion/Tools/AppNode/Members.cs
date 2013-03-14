﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace AppNode {
    internal partial class Program {
        public static List<Process> PrecessList;
        public const string WorkingDirectory = "AppsData";
        public static List<Application> AppList = new List<Application>();
        public static int ActiveApp = -1;
        public static System.Timers.Timer ConsoleStreamTimer = new Timer(300);
        public static System.Timers.Timer ReincornatorTimer = new Timer(1000);
        public static bool SwitchScreen;
        public static bool RefrashScreen = true;
        public static string InfoBuffer = "";
        public static bool IsWritingConsole;
        public const string CfgPath = "AppNode.cfg";
    }
}