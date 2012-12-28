using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AppNode
{
    class Application
    {
        //public Process Proc = new Process();
        //public Process. Proc = new Process();
        public string FileName;
        public string WorkingDirectory;

        public void ThreadPoolCallback(Object threadContext)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.UseShellExecute = false;
            //proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.WorkingDirectory = WorkingDirectory;
            proc.Start();
            proc.WaitForExit();
        }
    }
}
