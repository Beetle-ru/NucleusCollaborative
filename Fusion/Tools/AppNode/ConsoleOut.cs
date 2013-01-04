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
    internal partial class Program
    {
        public static void ConsoleIterateTimeOut(object source, ElapsedEventArgs e)
        {
            if (RefrashScreen)
            {
                if (ActiveApp >= 0)
                {
                    if (AppList.Count > ActiveApp)
                    {
                        AppList[ActiveApp].StreaWriter(SwitchScreen);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Application not run on F{0} console", ActiveApp + 1);
                    }
                }
                else // Admin screens
                {
                    if (ActiveApp == -1) // status
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Status screen\n");
                        PrintStatusAll();
                    }
                }
                SwitchScreen = false;
            }
        }
    }
}