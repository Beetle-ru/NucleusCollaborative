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
            Console.Clear();
            Console.CursorTop = (int)(Console.BufferHeight * 0.5);
            PrintSLine('*');
            PrintSLine('#', "Start info");
            LoadCfg("AppNode.cfg");
            Console.WriteLine();

            RunAll();
            StartConsoleStream();
            Controll();
            StopAll();
        }
    }
}
