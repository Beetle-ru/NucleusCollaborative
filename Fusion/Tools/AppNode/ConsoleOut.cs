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
        public static void ConsoleIterateTimeOut(object source, ElapsedEventArgs e) {
            ConsoleStreamTimer.Enabled = false;
            OutPutConsole();
            ConsoleStreamTimer.Enabled = true;
        }

        public static bool OutPutConsole() {
            var status = true;
            if (!IsWritingConsole) {
                IsWritingConsole = true;
                if (RefrashScreen) {
                    if (ActiveApp >= 0) {
                        if (AppList.Count > ActiveApp)
                            AppList[ActiveApp].StreaWriter(SwitchScreen);
                        else {
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
                            PrintInfo(InfoBuffer);
                        }
                    }
                    SwitchScreen = false;
                }
                IsWritingConsole = false;
            }
            else
                status = false;
            return status;
        }

        public static void RefrashConsoleNow() {
            while (!OutPutConsole()) Thread.Sleep(100);
        }

        public static void PrintInfo(string buffer) {
            var startPos = Console.WindowHeight - (int) (Console.WindowHeight*0.333);
            Console.SetCursorPosition(0, startPos);
            PrintSLine('*', "Info");
            var lineWidth = Console.WindowWidth - 2;
            var splt = buffer.Split('\n');
            if (splt.Any())
                buffer = splt.Aggregate("", (current, s) => current + s.PadRight(lineWidth));
            for (int strNum = Console.CursorTop; strNum < Console.WindowHeight - 1; strNum++) {
                string line;
                if (buffer.Count() > lineWidth) {
                    line = buffer.Substring(0, lineWidth);
                    buffer = buffer.Remove(0, lineWidth);
                }
                else {
                    line = buffer;
                    buffer = "";
                }

                line = String.Format("{0}", line).PadRight(lineWidth);
                Console.Write("*{0}*", line);
            }
        }
    }
}