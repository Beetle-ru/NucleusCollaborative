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
    class Program
    {
        public static List<Process> PrecessList;
        public const string WorkingDirectory = "AppsData";
        public static List<Application> AppList;
        public static int ActiveApp = -1;
        public static System.Timers.Timer ConsoleStreaTimer = new Timer(300);
        static void Main(string[] args)
        {
            Console.Clear();
            Console.CursorTop = (int)(Console.BufferHeight/2);
            LoadCfg("AppNode.cfg");
            RunAll();
            StartConsoleStream();
            Controll();
            StopAll();
        }

        private static void Controll()
        {
            Console.WriteLine("For exit press key \"Q\" or \"Enter\" or \"Escape\"");
            while (true)
            {
                var cki = Console.ReadKey(true);
                if (
                    (cki.Key == ConsoleKey.Q) ||
                    (cki.Key == ConsoleKey.Enter) ||
                    (cki.Key == ConsoleKey.Escape)
                    )
                {
                    Console.Clear();
                    StopAll();
                    AppExit();
                }
                else if (cki.Key == ConsoleKey.S)
                {
                    ActiveApp = -1;
                    Console.Clear();
                }
                else
                {
                    var fKey = false;
                    if (cki.Key == ConsoleKey.F1) { ActiveApp = 0; fKey = true; }
                    else if (cki.Key == ConsoleKey.F2) { ActiveApp = 1; fKey = true; }
                    else if (cki.Key == ConsoleKey.F3) { ActiveApp = 2; fKey = true; }
                    else if (cki.Key == ConsoleKey.F4) { ActiveApp = 3; fKey = true; }
                    else if (cki.Key == ConsoleKey.F5) { ActiveApp = 4; fKey = true; }
                    else if (cki.Key == ConsoleKey.F6) { ActiveApp = 5; fKey = true; }
                    else if (cki.Key == ConsoleKey.F7) { ActiveApp = 6; fKey = true; }
                    else if (cki.Key == ConsoleKey.F8) { ActiveApp = 7; fKey = true; }
                    else if (cki.Key == ConsoleKey.F9) { ActiveApp = 8; fKey = true; }
                    else if (cki.Key == ConsoleKey.F10) { ActiveApp = 9; fKey = true; }
                    else if (cki.Key == ConsoleKey.F11) { ActiveApp = 10; fKey = true; }
                    else if (cki.Key == ConsoleKey.F12) { ActiveApp = 11; fKey = true; }
                    if (((cki.Modifiers & ConsoleModifiers.Shift) != 0) && fKey) ActiveApp += 13;
                    Console.Clear();
                    Console.WriteLine("Swith console F{0}", ActiveApp + 1);
                }
            }
        }

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
                        AppList[AppList.Count -1].FileName = strings[i];
                        AppList[AppList.Count - 1].WorkingDirectory = WorkingDirectory;
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
                ThreadPool.QueueUserWorkItem(AppList[index].ThreadPoolCallback, index);
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

        public static void ConsoleIterateTimeOut(object source, ElapsedEventArgs e)
        {
            if (ActiveApp >= 0)
            {
                if (AppList.Count > ActiveApp)
                {
                    AppList[ActiveApp].StreaWriter();
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
                    //Console.Clear();
                    Console.SetCursorPosition(0,0);
                    Console.WriteLine("Status screen\n");
                    PrintStatusAll();
                }
            }
        }
    }
}
