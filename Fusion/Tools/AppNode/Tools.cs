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
        public static void LoadCfg(string path)
        {
            Directory.CreateDirectory(WorkingDirectory);
            AppList = new List<Application>();
            string[] strings;
            try
            {
                strings = File.ReadAllLines(path);
            }
            catch
            {
                strings = new string[0];
                Console.WriteLine("Cannot read the file: {0}", path);
                return;
            }

            try
            {
                for (int i = 0; i < strings.Count(); i++)
                {
                    if (File.Exists(strings[i]))
                    {
                        AppList.Add(new Application());
                        AppList[AppList.Count - 1].FileName = strings[i];
                        AppList[AppList.Count - 1].WorkingDirectory = WorkingDirectory;
                        AppList[AppList.Count - 1].NumberApp = AppList.Count - 1;
                        Console.WriteLine("Application added, path = {0}", strings[i]);
                    }
                    else
                    {
                        Console.WriteLine("###Application not found: {0}", strings[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("###Cannot read the file: {0}, bad format call exeption: {1}", path, e.ToString());
                throw e;
            }
        }

        public static void RunAll()
        {
            for (int index = 0; index < AppList.Count; index++)
            {
                AppList[index].ExecProc();
            }
        }

        public static void StopAll()
        {
            for (int index = 0; index < AppList.Count; index++)
            {
                var application = AppList[index];
                application.KillProc();
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
            ConsoleStreaTimer.Elapsed += new ElapsedEventHandler(ConsoleIterateTimeOut);
            ConsoleStreaTimer.Enabled = true;
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
            Console.WriteLine();
        }

        private static void ConsolePrepare()
        {
            Console.Clear();
            Console.CursorTop = (int)(Console.BufferHeight * 0.5);
            PrintSLine('*');
            PrintSLine('#', "Start info");
        }

        public static void KillCurrentProcess()
        {
            Console.SetCursorPosition(0, 0);

            if (ActiveApp < AppList.Count)
            {
                AppList[ActiveApp].KillProc();
            }
            else
            {
                Console.WriteLine("On this screen application is not binding");
            }
        }

        public static int KillPtocessById(int procId)
        {
            Console.SetCursorPosition(0, 0);

            foreach (var application in AppList)
            {
                if (application.PubProc.Id == procId)
                {
                    application.KillProc();
                    return application.NumberApp;
                }
            }
            return Int32.MaxValue;
        }

        public static void ExecCurrentProcess()
        {
            Console.SetCursorPosition(0, 0);
            ExecuteByNumber(ActiveApp);
        }

        public static void ExecuteByNumber(int appNumber, bool restar = false)
        {
            Console.SetCursorPosition(0, 0);
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
            }
            else
            {
                Console.WriteLine("Application is not binding to {0:000} number", appNumber);
            }
        }

        public static void ClearDownAndSetCursor()
        {
            Console.SetCursorPosition(0, Console.BufferHeight - 3);
            PrintSLine(' '); PrintSLine(' ');
            Console.SetCursorPosition(0, Console.BufferHeight - 3);
        }
    }
}