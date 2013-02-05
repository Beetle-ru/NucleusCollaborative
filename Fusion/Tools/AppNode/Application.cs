using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AppNode
{
    class Application
    {
        public Process PubProc;
        public string FileName;
        public string WorkingDirectory;
        public int NumberApp;
        public int DelayAfterExecute;
        public int ExecCount;
        private bool m_isAutomaticRestart = true;
        public List<string> Stream = new List<string>();
        private int m_streamChanged;
        private TimeSpan m_previousProcTime = new TimeSpan();
        public bool NeedRestart;
        private int m_errRestarts;
        private const int ErrRestartsTreshold = 10;
        private bool m_isRestarting;

        public void ThreadPoolCallback(Object threadContext)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.ErrorDialog = false;
            proc.EnableRaisingEvents = true;
            proc.Exited += new EventHandler(ProcessExited);
            proc.ErrorDataReceived += new DataReceivedEventHandler(ErrorDataHandler);
            proc.OutputDataReceived += new DataReceivedEventHandler(OutputDataHandler);
            proc.StartInfo.WorkingDirectory = WorkingDirectory;
            proc.Start();
            PubProc = Process.GetProcessById(proc.Id);
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            //while (!proc.HasExited)
            //{
            //    StreamRotator(proc.StandardOutput.ReadLine());
            //}
            proc.WaitForExit();
        }
        public void KillProc()
        {
            if (PubProc != null)
            {
                if (!PubProc.HasExited)
                {
                    //Program.WriteInfo(String.Format("kill {0}", PubProc.Id));
                    //Program.PrintInfo(Program.InfoBuffer);
                    try
                    {
                        PubProc.Kill();
                    }
                    catch (Exception e )
                    {
                        Program.WriteInfo(String.Format("kill {0} exeption {1}", PubProc.Id, e.Message));
                        
                    }
                    
                }
            }
        }

        public bool ExecProc()
        {
            if (PubProc == null || PubProc.HasExited)
            {
                ThreadPool.QueueUserWorkItem(ThreadPoolCallback);
                ExecCount++;
                Program.WriteInfo("Execute process");
                m_isRestarting = false;
                return true;
            }
            return false;
        }

        public void RestartProc()
        {
            KillProc();
            m_isRestarting = true;
            NeedRestart = false;
            Program.WriteInfo("Restarting process...");
        }

        public bool ProcessIsRun()
        {
            if (PubProc != null)
            {
                return !PubProc.HasExited;
            }
            else return false;
        }

        public void PrintStatusProc()
        {
            if (PubProc != null)
            {
                var outstr = "";

                outstr += String.Format("| {0:000}", NumberApp).PadRight(6);
                outstr += String.Format("| {0}", PubProc.ProcessName).PadRight(30);
                outstr += !PubProc.HasExited
                              ? String.Format("| {0}", PubProc.Id).PadRight(7)
                              : String.Format("| ---- ").PadRight(7);
                outstr += !PubProc.HasExited ? String.Format("| Run ") : String.Format("| --- ");
                outstr += !PubProc.HasExited 
                    ? String.Format("| {0}", PubProc.PagedMemorySize64).PadRight(15)
                    : String.Format("| ------------ ").PadRight(15);
               
                int cpu = 0;
                if (!PubProc.HasExited)
                {
                    cpu = (int)(Math.Round((PubProc.TotalProcessorTime.TotalSeconds - m_previousProcTime.TotalSeconds) * 100));
                    m_previousProcTime = PubProc.TotalProcessorTime;
                }
                
                if (cpu > 100) cpu = 100;
                if (cpu < 0) cpu = 0;
                outstr += !PubProc.HasExited 
                    ? String.Format("| {0}", cpu).PadRight(6)
                    : String.Format("| --- ").PadRight(6);

                outstr += String.Format("| {0} ", ExecCount).PadRight(7);

                Console.WriteLine(outstr);
            }
        }

        public static void PrintStatusHeader()
        {
            var outstr = "";
            outstr += String.Format("|  №").PadRight(6);
            outstr += String.Format("| Process name").PadRight(30);
            outstr += String.Format("| Id").PadRight(7); ;
            outstr += String.Format("|State");
            outstr += String.Format("| Memory").PadRight(15);
            outstr += String.Format("| CPU").PadRight(6);
            outstr += String.Format("|Execs").PadRight(7);
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
            if (!PubProc.HasExited)
            {
                int startPos;
                if (writeAll)
                {
                    startPos = Stream.Count < Console.WindowHeight ? 0 : Stream.Count - Console.WindowHeight;
                }
                else
                {
                    startPos = Stream.Count < m_streamChanged ? 0: Stream.Count - m_streamChanged;
                }
                m_streamChanged = 0;
                for (int i = startPos; i < Stream.Count; i++)
                {
                    Console.WriteLine(Stream[i]);
                }
            }
            else
            {
                Console.SetCursorPosition(0,1);
                var msg = String.Format("Application \"{0}\" is not run", FileName).PadRight(Console.BufferWidth);
                Console.WriteLine(msg);
            }
        }
        private void ErrorDataHandler(object sendingProcess, DataReceivedEventArgs errLine)
        {
            if (!String.IsNullOrEmpty(errLine.Data))
            {


                //NeedRestart = true;
                //Console.SetCursorPosition(0, 0);

                if (m_isAutomaticRestart && !PubProc.HasExited)
                {
                    Program.WriteInfo(String.Format("Process (\"{0}\", ProcId = {1}) error {2}", PubProc.ProcessName, PubProc.Id, m_errRestarts));
                    KillProc();
                    PubProc.WaitForExit();
                    //NeedRestart = true;
                    Thread.Sleep(300);
                    m_errRestarts++;
                    if (ErrRestartsTreshold < m_errRestarts)
                    {
                        m_isAutomaticRestart = false;
                        m_errRestarts = 0;
                    }
                }

                //Thread.Sleep(300);
            }
            //Console.Clear();
        }

        private void ProcessExited(object sender, System.EventArgs e)
        {
            if (m_isRestarting)
            {
                ExecProc();
                Program.WriteInfo("Restarting complete");
            }
            else
            {
                Program.WriteInfo("Process killed");
            }
            //Console.Clear();
            //Program.PrintInfo(Program.InfoBuffer);
        }


        private void OutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                StreamRotator(outLine.Data);
            }
        }

        public void SetAutomaticRestart()
        {
            m_isAutomaticRestart = true;
        }

        public void SetManuaRestart()
        {
            m_isAutomaticRestart = false;
        }

        public bool GetAutomaticRestartStatus()
        {
            return m_isAutomaticRestart;
        }


    }
}
