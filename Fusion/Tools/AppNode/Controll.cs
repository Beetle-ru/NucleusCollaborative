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
        private static void Controll()
        {
            WriteInfo("For exit press key \"Q\" or \"Escape\"");
            while (true)
            {
                RefrashScreen = true;
                var cki = Console.ReadKey(true);
                if (
                    (cki.Key == ConsoleKey.Q) ||
                    (cki.Key == ConsoleKey.Escape)
                    )
                {
                    RefrashScreen = false;
                    Console.Clear();
                    StopAll();
                    AppExit();
                }
                else if (cki.Key == ConsoleKey.S)
                {
                    ActiveApp = -1;
                    Console.Clear();
                }
                else if (cki.Key == ConsoleKey.K)
                {
                    if (ActiveApp >= 0)
                    {
                        KillCurrentProcess();
                    }
                    else
                    {
                        RefrashScreen = false;

                        WriteInfo("For kill please write application number: ");
                        PrintInfo(InfoBuffer);
                        
                        var strNumber = Console.ReadLine();
                        Console.Clear();
                        int appNumber;
                        if (Int32.TryParse(strNumber, out appNumber))
                        {
                            //KillPtocessById(id);
                            KillPtocessByNumber(appNumber);
                        }
                        else
                        {
                            WriteInfo("Uncorrect number of application");
                            PrintInfo(InfoBuffer);
                        }
                        RefrashScreen = true;
                    }
                }
                else if (cki.Key == ConsoleKey.R)
                {
                    if (ActiveApp >= 0)
                    {
                        ExecuteByNumber(ActiveApp, true);
                        Console.Clear();
                    }
                    else
                    {
                        RefrashScreen = false;

                        WriteInfo("For restart please write process number: ");
                        PrintInfo(InfoBuffer);

                        var strNumber = Console.ReadLine();
                        Console.Clear();
                        int appNumber;
                        if (Int32.TryParse(strNumber, out appNumber))
                        {
                            ExecuteByNumber(appNumber, true);
                        }
                        else
                        {
                            WriteInfo("Uncorrect number of application");
                            PrintInfo(InfoBuffer);
                        }
                        RefrashScreen = true;
                    }
                }
                else if (cki.Key == ConsoleKey.E)
                {
                    if (ActiveApp >= 0)
                    {
                        ExecCurrentProcess();
                        Console.Clear();
                    }
                    else
                    {
                        RefrashScreen = false;

                        WriteInfo("For execute please write № application: ");
                        PrintInfo(InfoBuffer);

                        var strId = Console.ReadLine();
                        Console.Clear();
                        int aN;
                        if (Int32.TryParse(strId, out aN))
                        {
                            ExecuteByNumber(aN);
                        }
                        else
                        {
                            WriteInfo("Uncorrect Id");
                            PrintInfo(InfoBuffer);
                        }
                        RefrashScreen = true;
                    }
                }
                else if (cki.Key == ConsoleKey.H)
                {
                    ClearInfo();
                    WriteInfo("Help:");
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
                    if (((cki.Modifiers & ConsoleModifiers.Shift) != 0) && fKey) ActiveApp += 12;
                    if (fKey)
                    {
                        Console.Clear();
                        WriteInfo(String.Format("Swith console F{0}", ActiveApp + 1));
                        SwitchScreen = true;
                    }
                }
            }
        }
    }
}