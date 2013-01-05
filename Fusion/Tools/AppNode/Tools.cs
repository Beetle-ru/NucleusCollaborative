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
            ClearInfo();
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
                WriteInfo(String.Format("Cannot read the file: {0}", path));
                PrintInfo(InfoBuffer);
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
                        WriteInfo(String.Format("Application added, path = {0}", strings[i]));
                    }
                    else
                    {
                        WriteInfo(String.Format("###Application not found: {0}", strings[i]));
                        PrintInfo(InfoBuffer);
                    }
                }
            }
            catch (Exception e)
            {
                WriteInfo(String.Format("###Cannot read the file: {0}, bad format call exeption: {1}", path, e.ToString()));
                PrintInfo(InfoBuffer);
                throw e;
            }
            PrintInfo(InfoBuffer);
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
        }

        private static void ConsolePrepare()
        {
            Console.Clear();
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
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

        public static void KillPtocessById(int procId)
        {
           // Console.SetCursorPosition(0, 0);

            var isFound = false;
            foreach (var application in AppList)
            {
                if (application.PubProc.Id == procId)
                {
                    application.KillProc();
                    isFound = true;
                }
            }
            if (!isFound) WriteInfo("Id not found");
        }

        public static void KillPtocessByNumber(int appNumber)
        {
            if (appNumber < AppList.Count)
            {
                AppList[appNumber].KillProc();
            }
            else
            {
                WriteInfo(String.Format("Application is not binding to {0:000} number", appNumber));
            }
            
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
                WriteInfo(String.Format("Application is not binding to {0:000} number", appNumber));
            }
        }

        public static void WriteInfo(string msg)
        {
            var lineWidth = Console.WindowWidth - 2;
            //InfoBuffer += msg;
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