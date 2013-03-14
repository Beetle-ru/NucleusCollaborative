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
        public static void LoadCfg(string path) {
            ClearInfo();
            Directory.CreateDirectory(WorkingDirectory);
            //AppList = new List<Application>();
            string[] strings;
            try {
                strings = File.ReadAllLines(path);
            }
            catch {
                strings = new string[0];
                WriteInfo(String.Format("Cannot read the file: {0}", path));
                RefrashConsoleNow();
                return;
            }

            var existAppsIndex = 0;
            try {
                for (int i = 0; i < strings.Count(); i++) {
                    const char separator = ';';
                    var splt = strings[i].Split(separator);

                    var execPatch = splt.Any() ? splt[0] : strings[i];
                    var delay = splt.Count() > 1 ? Int32.Parse(splt[1]) : 0;

                    if (File.Exists(execPatch)) {
                        //AppList.Add(new Application());
                        //AppList[AppList.Count - 1].FileName = execPatch;
                        //AppList[AppList.Count - 1].WorkingDirectory = WorkingDirectory;
                        //AppList[AppList.Count - 1].NumberApp = AppList.Count - 1;
                        //AppList[AppList.Count - 1].DelayAfterExecute = delay;
                        var appDescr = new Application();
                        appDescr.FileName = execPatch;
                        appDescr.WorkingDirectory = WorkingDirectory;
                        appDescr.DelayAfterExecute = delay;
                        InsertApp(appDescr, existAppsIndex++);
                        WriteInfo(String.Format("Application added, path = {0}", strings[i]));
                    }
                    else {
                        WriteInfo(String.Format("###Application not found: {0}", strings[i]));
                        RefrashConsoleNow();
                    }
                }
            }
            catch (Exception e) {
                WriteInfo(String.Format("###Cannot read the file: {0}, bad format call exeption: {1}", path,
                                        e.ToString()));
                RefrashConsoleNow();
                throw e;
            }
            RemoveExcessApps(existAppsIndex);
            PrintInfo(InfoBuffer);
        }
    }
}