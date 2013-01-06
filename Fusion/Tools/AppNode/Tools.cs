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
        public static void ExecuteAll()
        {
            foreach (Application application in AppList)
            {
                var executed = application.ExecProc();
                application.SetAutomaticRestart();
                if (executed) {ExecDelay(application.DelayAfterExecute); WriteInfo("Complete");}
                
            }
        }

        public static void KillAll()
        {
            foreach (Application application in AppList)
            {
                application.KillProc();
                application.SetManuaRestart();
            }
        }

        public static void RestartAll()
        {
            foreach (Application application in AppList)
            {
                application.RestartProc();
                application.SetAutomaticRestart();
                ExecDelay(application.DelayAfterExecute);
            }
        }

        private static void ExecDelay(int delay)
        {
            if (delay > 0)
            {
                WriteInfo(String.Format("Execute delay {0}ms, wait ...", delay));
                RefrashConsoleNow();
                Thread.Sleep(delay);
            }
        }
        public static void PrintStatusAll()
        {
            Application.PrintStatusHeader();
            foreach (var application in AppList)
            {
                application.PrintStatusProc();
            }
        }

        public static void AppExit()
        {
            System.Environment.Exit(0);
        }

        public static void StartConsoleStream()
        {
            ConsoleStreamTimer.Elapsed += new ElapsedEventHandler(ConsoleIterateTimeOut);
            ConsoleStreamTimer.Enabled = true;
        }

        public static void StartReincornator()
        {
            ReincornatorTimer.Elapsed += new ElapsedEventHandler(ReincornatorTimeOut);
            ReincornatorTimer.Enabled = true;
        }

        private static void PrintSLine(char c)
        {
            for (var i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write(c);
            }
        }

        private static void PrintSLine(char c, string msg)
        {
            var frmMsg = String.Format("[ {0} ]", msg);
            var lengthMsg = frmMsg.Count();
            var lengthLine = Console.BufferWidth;
            var msgIsWrite = false;
            for (var i = 0; i < lengthLine; i++)
            {
                if (i > (lengthLine * 0.5) - (lengthMsg * 0.5) && !msgIsWrite)
                {
                    Console.Write(frmMsg);
                    i += lengthMsg;
                    msgIsWrite = true;
                }
                Console.Write(c);
            }
        }

        private static void ConsolePrepare()
        {
            Console.Clear();
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
        }

        public static void KillCurrentProcess()
        {
            KillProcessByNumber(ActiveApp);
        }


        public static void KillProcessByNumber(int appNumber)
        {
            if (appNumber < AppList.Count)
            {
                AppList[appNumber].KillProc();
                AppList[appNumber].SetManuaRestart();
            }
            else
            {
                WriteInfo(String.Format("Application is not binding to {0:000} number", appNumber));
            }
            
        }

        public static void ExecCurrentProcess()
        {
            ExecuteByNumber(ActiveApp);
        }

        public static void ExecuteByNumber(int appNumber, bool restar = false)
        {
            if (appNumber < AppList.Count)
            {
                if (restar)
                {
                    AppList[appNumber].RestartProc();
                }
                else
                {
                    AppList[appNumber].ExecProc();
                }
                AppList[appNumber].SetAutomaticRestart();
            }
            else
            {
                WriteInfo(String.Format("Application is not binding to {0:000} number", appNumber));
            }
        }

        public static void WriteInfo(string msg)
        {
            var lineWidth = Console.WindowWidth - 2;
            var splt = msg.Split('\n');
            if (splt.Any())
            {
                string result = "";
                foreach (string s in splt)
                    result = result + s.PadRight(lineWidth);
                InfoBuffer += result;
            }
            else
            {
                InfoBuffer += msg.PadRight(lineWidth);
            }
            if ((InfoBuffer.Count() / lineWidth) > (Console.WindowHeight * 0.333)-2)
            {
                InfoBuffer = InfoBuffer.Remove(0, lineWidth);
            }
        }

        public static void ClearInfo()
        {
            InfoBuffer = "";
        }
    }
}