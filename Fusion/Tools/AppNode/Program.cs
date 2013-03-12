using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace AppNode {
    internal partial class Program {
        private static void Main(string[] args) {
            ConsolePrepare();
            LoadCfg(CfgPath);
            ExecuteAll();
            StartConsoleStream();
            StartReincornator();
            Controll();
            KillAll();
        }
    }
}