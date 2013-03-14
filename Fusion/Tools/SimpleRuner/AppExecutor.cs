using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace SimpleRuner {
    internal class AppExecutor {
        private Thread m_appThread = new Thread(AppHandler);
        public static string StdOut;
        public static bool IsRun = false;
        private static string m_fileName;
        private static string m_arguments;

        public AppExecutor(string fileName, string arguments) {
            m_fileName = fileName;
            m_arguments = arguments;
            m_appThread.IsBackground = true;
            m_appThread.Start();
        }

        private static void AppHandler(object state) {
            var procH = new Process();
            procH.StartInfo.FileName = m_fileName;
            procH.StartInfo.Arguments = m_arguments;
            procH.StartInfo.UseShellExecute = false;
            procH.StartInfo.RedirectStandardOutput = true;
            //procH.StartInfo.CreateNoWindow = true;
            procH.Start();

            while (IsRun)
                StdOut += procH.StandardOutput.ReadLine() + "\n";
            procH.Close();

            procH.WaitForExit();
        }
    }
}