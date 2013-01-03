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
        private TimeSpan m_previousProcTime = new TimeSpan();

        public void ThreadPoolCallback(Object threadContext)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardInput = true;
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
                var outstr = "";
                outstr += String.Format("| {0}", PubProc.ProcessName).PadRight(20);
                outstr += String.Format("| {0}", PubProc.Id).PadRight(7);
                if (!PubProc.HasExited) outstr += String.Format("| Run ");
                else outstr += String.Format("| --- ");
                outstr += String.Format("| {0}", PubProc.PagedMemorySize64).PadRight(15);

                var cpu = (int)(Math.Round((PubProc.TotalProcessorTime.TotalSeconds - m_previousProcTime.TotalSeconds) * 100));
                m_previousProcTime = PubProc.TotalProcessorTime;
                if (cpu > 100) cpu = 100;
                if (cpu < 0) cpu = 0;
                outstr += String.Format("| {0}", cpu).PadRight(6);
                Console.WriteLine(outstr);
            }
        }
        public static void PrintStatusHeader()
        {
            var outstr = "";
            outstr += String.Format("| Process name").PadRight(20);
            outstr += String.Format("| Id").PadRight(7); ;
            outstr += String.Format("|State");
            outstr += String.Format("| Memory").PadRight(15);
            outstr += String.Format("| CPU").PadRight(6);
            Console.WriteLine(outstr);
        }

        
        
        // nice song )) little jimmy osmond tweedle dee
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

        public void StreaWriter(bool writeAll = false)
        {
            int startPos;
            if (writeAll)
            {
                startPos = Stream.Count < Console.WindowHeight ? 0 : Stream.Count - Console.WindowHeight;
            }
            else
            {
                startPos = (Stream.Count) - m_streamChanged;
            }
            m_streamChanged = 0;
            for (int i = startPos; i < Stream.Count; i++)
            {
                Console.WriteLine(Stream[i]);
            }
        }
    }
}
