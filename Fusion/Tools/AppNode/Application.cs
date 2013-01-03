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
        public Process PubProc;
        public string FileName;
        public string WorkingDirectory;
        public List<string> Stream = new List<string>();
        private int m_streamChanged;
        //private bool AllowToPrint = true;
        //private bool AllowToPrintChange = true;

        public void ThreadPoolCallback(Object threadContext)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.WorkingDirectory = WorkingDirectory;
            proc.Start();
            PubProc = Process.GetProcessById(proc.Id);
            while (true)
            {
                StreamRotator(proc.StandardOutput.ReadLine());
            }
            proc.WaitForExit();
        }
        public void KillProc()
        {
            if (PubProc != null)
            {
                if (!PubProc.HasExited)
                {
                    Console.WriteLine("kill {0}", PubProc.Id);
                    PubProc.Kill();
                }
            }
        }
        public void PrintStatusProc()
        {
            if (PubProc != null)
            {
                var str = "";
                PrintSLine('#');
                str += String.Format("Process name => {0}\n", PubProc.ProcessName);
                str += String.Format("Process Id => {0}\n", PubProc.Id);
                str += String.Format("Process is run => {0}\n", (!PubProc.HasExited).ToString());
                Console.Write(str);
                PrintSLine('*');
            }
        }
        private void PrintSLine(char c)
        {
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.Write(c);
            }
        }

        private void StreamRotator(string str)
        {
            Stream.Add(str);
            if (m_streamChanged < Stream.Count) m_streamChanged++;
            var diff = Stream.Count - Console.BufferHeight;
            for (int i = 0; i < diff; i++)
            {
                Stream.RemoveAt(0);
            }
        }

        public void StreaWriter()
        {
            var startPos = (Stream.Count) - m_streamChanged;
            m_streamChanged = 0;
            for (int i = startPos; i < Stream.Count; i++)
            {
                Console.WriteLine(Stream[i]);
            }
        }
    }
}
