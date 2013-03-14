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
        public static void ReincornatorTimeOut(object source, ElapsedEventArgs e) {
            ReincornatorTimer.Enabled = false;

            const int verifyTreshold = 10;
            const int verifyDelay = 300;
            const int restartTreshold = 10;

            foreach (var application in AppList) {
                if (application.GetAutomaticRestartStatus() && !application.ProcessIsRun()) {
                    WriteInfo(String.Format("Application \"{0}\" not run", application.FileName));
                    if (!VeryByExec(application, verifyTreshold, verifyDelay)) {
                        application.ExecProc();
                        if (!VeryByExec(application, verifyTreshold, verifyDelay)) {
                            application.SetManuaRestart();
                            WriteInfo(String.Format("Application \"{0}\" is dead", application.FileName));
                        }
                    }
                }

                //if (application.IsAutomaticRestart && application.NeedRestart)
                //{
                //    //application.RestartProc();
                //    var restartTry = 0;
                //    while (application.NeedRestart)
                //    {
                //        if (restartTry > restartTreshold)
                //        {
                //            application.IsAutomaticRestart = false;
                //        }
                //        application.RestartProc();
                //        Thread.Sleep(verifyDelay);
                //        restartTry++;
                //    }
                //}
            }

            ReincornatorTimer.Enabled = true;
        }

        private static bool VeryByExec(Application application, int verifyTreshold, int verifyDelay) {
            var verifyTry = 0;
            while (!application.ProcessIsRun()) {
                if (verifyTry > verifyTreshold)
                    return false;
                else {
                    verifyTry++;
                    Thread.Sleep(verifyDelay);
                }
            }
            return true;
        }
    }
}