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
                    if (fKey)
                    {
                        Console.Clear();
                        Console.WriteLine("Swith console F{0}", ActiveApp + 1);
                        SwitchScreen = true;
                    }
                }
            }
        }
    }
}