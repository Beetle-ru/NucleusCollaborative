using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace AppNode
{
    partial class Program
    {
        static void Main(string[] args)
        {
            ConsolePrepare();
            LoadCfg("AppNode.cfg");
            RunAll();
            StartConsoleStream();
            Controll();
            StopAll();
        }
    }
}
