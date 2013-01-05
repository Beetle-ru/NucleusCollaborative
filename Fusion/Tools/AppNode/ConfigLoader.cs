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
                WriteInfo(String.Format("###Cannot read the file: {0}, bad format call exeption: {1}", path,
                                        e.ToString()));
                PrintInfo(InfoBuffer);
                throw e;
            }
            PrintInfo(InfoBuffer);
        }
    }
}