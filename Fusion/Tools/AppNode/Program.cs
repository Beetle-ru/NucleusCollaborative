using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace AppNode
{
    class Program
    {
        public static List<Process> PrecessList;
        public const string WorkingDirectory = "OutPut";
        static void Main(string[] args)
        {
            LoadCfg("AppNode.cfg");
            RunAll();
            Console.WriteLine("For exit press \"Enter\"");
            Console.ReadLine();
            StopAll();
        }

        public static void LoadCfg(string path)
        {
            Directory.CreateDirectory(WorkingDirectory);
            PrecessList = new List<Process>();
            string[] strings;
            try
            {
                strings = File.ReadAllLines(path);
            }
            catch
            {
                strings = new string[0];
                Console.WriteLine("Cannot read the file: {0}", path);
                return;
            }

            try
            {
                for (int i = 0; i < strings.Count(); i++)
                {
                    if(File.Exists(strings[i]))
                    {
                        PrecessList.Add(new Process());
                        PrecessList[PrecessList.Count - 1].StartInfo.FileName = strings[i];
                        PrecessList[PrecessList.Count - 1].StartInfo.UseShellExecute = false;
                        PrecessList[PrecessList.Count - 1].StartInfo.RedirectStandardOutput = true;
                        PrecessList[PrecessList.Count - 1].StartInfo.WorkingDirectory = WorkingDirectory;
                        Console.WriteLine("Application added, path = {0}", strings[i]);
                    }
                    else
                    {
                        Console.WriteLine("###Application not found: {0}", strings[i]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("###Cannot read the file: {0}, bad format call exeption: {1}", path, e.ToString());
                throw e;
            }
        }

        public static void RunAll()
        {
            foreach (var process in PrecessList)
            {
                process.Start();
            }
        }

        public static void StopAll()
        {
            foreach (var process in PrecessList)
            {
                Console.WriteLine("Kill process -->> " + process.ProcessName);
                process.Kill();
            }
        }
    }
}
