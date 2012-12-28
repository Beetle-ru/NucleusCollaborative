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
        public const string WorkingDirectory = "AppsData";
        public static Thread ControllThread = new Thread(HControllThread);
        //public static Thread ProcessThread = new Thread(HProcessThread);
        public static List<Application> AppList;
        static void Main(string[] args)
        {
            ControllThread.IsBackground = true;
            ControllThread.Start();
            //ProcessThread.IsBackground = true;
            //ProcessThread.Start();
            //LoadCfg("AppNode.cfg");
            //RunAll();
            LoadCfgPool("AppNode.cfg");
            RunAllPool();
            Console.WriteLine("For exit press \"Enter\"");
            Console.ReadLine();
            StopAllPool();
            //StopAll();
        }

        //private static void HProcessThread(object state)
        //{
        //    string path = "AppNode.cfg";
        //    List<Process> precessList;
        //    Directory.CreateDirectory(WorkingDirectory);
        //    precessList = new List<Process>();
        //    string[] strings;
        //    try
        //    {
        //        strings = File.ReadAllLines(path);
        //    }
        //    catch
        //    {
        //        strings = new string[0];
        //        Console.WriteLine("Cannot read the file: {0}", path);
        //        return;
        //    }

        //    try
        //    {
        //        for (int i = 0; i < strings.Count(); i++)
        //        {
        //            if (File.Exists(strings[i]))
        //            {
        //                precessList.Add(new Process());
        //                precessList[precessList.Count - 1].StartInfo.FileName = strings[i];
        //                precessList[precessList.Count - 1].StartInfo.UseShellExecute = false;
        //                precessList[precessList.Count - 1].StartInfo.RedirectStandardOutput = true;
        //                precessList[precessList.Count - 1].StartInfo.WorkingDirectory = WorkingDirectory;
        //                precessList[precessList.Count - 1].Start();
        //                //precessList[precessList.Count - 1].WaitForExit();
        //                Console.WriteLine("Application added, path = {0}", strings[i]);
        //            }
        //            else
        //            {
        //                Console.WriteLine("###Application not found: {0}", strings[i]);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("###Cannot read the file: {0}, bad format call exeption: {1}", path, e.ToString());
        //        throw e;
        //    }
        //    foreach (var process in precessList)
        //    {
        //        process.WaitForExit();
        //    }
        //    //while (true)
        //    //{
                
        //    //}
        //}
        private static void HControllThread(object state)
        {
            Console.WriteLine("For exit press \"q\"");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.KeyChar == 'q')
                {
                    //StopAll();
                    System.Environment.Exit(0);
                }
            }
        }

        public static void LoadCfgPool(string path)
        {
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
                Console.WriteLine("Cannot read the file: {0}", path);
                return;
            }

            try
            {
                for (int i = 0; i < strings.Count(); i++)
                {
                    if (File.Exists(strings[i]))
                    {
                        AppList.Add(new Application());
                        AppList[AppList.Count -1].FileName = strings[i];
                        AppList[AppList.Count - 1].WorkingDirectory = WorkingDirectory;
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
                        //PrecessList[PrecessList.Count - 1].StartInfo.RedirectStandardOutput = true;
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

        public static void RunAllPool()
        {
            for (int index = 0; index < AppList.Count; index++)
            {
                ThreadPool.QueueUserWorkItem(AppList[index].ThreadPoolCallback, index);
            }
        }

        public static void RunAll()
        {
            foreach (var process in PrecessList)
            {
                process.Start();
            }
        }

        public static void StopAllPool()
        {
            for (int index = 0; index < AppList.Count; index++)
            {
                var application = AppList[index];
                //application.Proc.Kill();
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
