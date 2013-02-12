using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Implements
{
    public class Clock
    {
        private static DateTime cTime = DateTime.Now;
        private static object tmLock = new object();
        public bool nextDay()
        {
            var result = false;
            lock (tmLock)
            {
                if (cTime.Day != DateTime.Now.Day)
                {
                    cTime = DateTime.Now;
                    result = true;
                }
            }
            return result;
        }
        
    }
    public class Logger : IDisposable
    {
        private static InstantLogger.TypeMessage[] c_msgType = { InstantLogger.TypeMessage.unimportant, InstantLogger.TypeMessage.normal, InstantLogger.TypeMessage.important };
        private static InstantLogger.TypeMessage[] c_errType = { InstantLogger.TypeMessage.error, InstantLogger.TypeMessage.terror };

        private class LoggerAttributes
        {
            private static uint _lcount = 0;
            public Stack<string> path;
            public InstantLogger.TypeMessage msgType, errType;

            public LoggerAttributes()
            {
                path = new Stack<string>();
                msgType = c_msgType[_lcount % c_msgType.Count()];
                errType = c_errType[_lcount % c_errType.Count()];
                _lcount++;
            }
        };

        private static Dictionary<Thread, LoggerAttributes> c_logData = new Dictionary<Thread, LoggerAttributes>();

        private static LoggerAttributes LoggerData()
        {
            var thr = Thread.CurrentThread;
            if (!c_logData.ContainsKey(thr))
            {
                c_logData.Add(thr, new LoggerAttributes());
            }
            return c_logData[thr];
        }

        private Object m_Lock = null;

        public virtual void Dispose()
        {
            if (m_Lock != null)
            {
                Monitor.Exit(m_Lock);
                m_Lock = null;
            }
            LoggerData().path.Pop();
        }

        public Logger(string Title)
        {
            LoggerData().path.Push(Title);
        }

        public Logger(string Title, ref object Lock)
        {
            m_Lock = Lock;
            if (Lock != null) Monitor.Enter(m_Lock);
            LoggerData().path.Push(Title);
        }

        private static string makeHeader(string Hdr)
        {
            string header = Hdr;
            for (int i = 0; i < LoggerData().path.Count(); i++)
            {
                header += "::" + LoggerData().path.ElementAt(i);
            }
            return header;
        }

        // Error reporting helper
        public void err(string fmt, params object[] objs)
        {
            InstantLogger.log(string.Format(fmt, objs), makeHeader("***ERROR "), LoggerData().errType);
        }

        public void err(string fmt)
        {
            err(fmt, new object[] {});
        }

        // Info (trace) messages processing helper
        public void msg(string fmt, params object[] objs)
        {
            InstantLogger.log(string.Format(fmt, objs), makeHeader("***TRACE "), LoggerData().msgType);
        }

        public void msg(string fmt)
        {
            msg(fmt, new object[] {});
        }
    }


    public static class InstantLogger
    { 
        private static object consoleLocker = new object();
        public static object fileLocker = new object();
        private static object processLocker = new object();
        private static string path = "logs";
        private static string logFileName()
        {
            return path + @"\" + logNameGenerate();
        }
        private static StreamWriter logFile;
        public static bool writeLogFile = true;
        private static bool writeLogFileInitialised = false;
        public static bool writeLogConsole = true;

        public enum TypeMessage
        {
            unimportant,
            normal,
            important,
            warning,
            caution,
            danger,
            error,
            terror,
            death
        };

        public static void log(string message)
        {
            if (writeLogFile)
            {
                try
                {
                    if (!writeLogFileInitialised)
                        LogFileInit();
                    lock (fileLocker)
                    {
                        logFile.Write(message + "\n");
                    }
                }
                catch (Exception e)
                {
                    lock (fileLocker)
                    {
                        Console.WriteLine("***logger exception:");
                        Console.WriteLine("{0}", e);
                    }
                }
            }
            if (writeLogConsole)
            {
                lock (consoleLocker)
                {
                    Console.Write(message + "\n");
                }
            }
        }

        public static void log(string content, string header = "Message", TypeMessage type = TypeMessage.normal)
        {
            ConsoleColor headerForegroundColor;
            ConsoleColor headerBackgroundColor;
            ConsoleColor contentForegroundColor;
            ConsoleColor contentBackgroundColor;

            string timeNow = DateTime.Now.ToString();
            lock (processLocker)
            {
                ConsoleColor currentForegroundColor = Console.ForegroundColor;
                ConsoleColor currentBackgroundColor = Console.BackgroundColor;

                switch (type)
                {
                    case TypeMessage.unimportant:
                        headerForegroundColor = ConsoleColor.DarkCyan;
                        headerBackgroundColor = ConsoleColor.Black;
                        contentForegroundColor = ConsoleColor.DarkGray;
                        contentBackgroundColor = ConsoleColor.Black;
                        header = "~~~   " + header + "   ~~~";
                        break;
                    case TypeMessage.normal:
                        headerForegroundColor = ConsoleColor.White;
                        headerBackgroundColor = ConsoleColor.DarkBlue;
                        contentForegroundColor = ConsoleColor.Gray;
                        contentBackgroundColor = ConsoleColor.Black;
                        header = "-==   " + header + "   ==-";
                        break;
                    case TypeMessage.important:
                        headerForegroundColor = ConsoleColor.White;
                        headerBackgroundColor = ConsoleColor.Blue;
                        contentForegroundColor = ConsoleColor.DarkYellow;
                        contentBackgroundColor = ConsoleColor.Black;
                        header = "<<<   " + header + "   >>>";
                        break;
                    case TypeMessage.warning:
                        headerForegroundColor = ConsoleColor.Yellow;
                        headerBackgroundColor = ConsoleColor.Blue;
                        contentForegroundColor = ConsoleColor.Yellow;
                        contentBackgroundColor = ConsoleColor.Black;
                        header = "[[[   " + header + "   ]]]";
                        break;
                    case TypeMessage.caution:
                        headerForegroundColor = ConsoleColor.Yellow;
                        headerBackgroundColor = ConsoleColor.DarkRed;
                        contentForegroundColor = ConsoleColor.Yellow;
                        contentBackgroundColor = ConsoleColor.DarkMagenta;
                        header = "<[[   " + header + "   ]]>";
                        break;
                    case TypeMessage.danger:
                        headerForegroundColor = ConsoleColor.Yellow;
                        headerBackgroundColor = ConsoleColor.Red;
                        contentForegroundColor = ConsoleColor.Yellow;
                        contentBackgroundColor = ConsoleColor.DarkRed;
                        header = "<<{   " + header + "   }>>";
                        break;
                    case TypeMessage.error:
                        headerForegroundColor = ConsoleColor.White;
                        headerBackgroundColor = ConsoleColor.Red;
                        contentForegroundColor = ConsoleColor.White;
                        contentBackgroundColor = ConsoleColor.DarkRed;
                        header = "+++   " + header + "   +++";
                        break;
                    case TypeMessage.terror:
                        headerForegroundColor = ConsoleColor.Black;
                        headerBackgroundColor = ConsoleColor.Yellow;
                        contentForegroundColor = ConsoleColor.Black;
                        contentBackgroundColor = ConsoleColor.Red;
                        header = "***   " + header + "   ***";
                        break;
                    case TypeMessage.death:
                        headerForegroundColor = ConsoleColor.Black;
                        headerBackgroundColor = ConsoleColor.White;
                        contentForegroundColor = ConsoleColor.Black;
                        contentBackgroundColor = ConsoleColor.Yellow;
                        header = "!!!   " + header + "   !!!";
                        break;
                    default:
                        headerForegroundColor = ConsoleColor.Green;
                        headerBackgroundColor = ConsoleColor.DarkBlue;
                        contentForegroundColor = ConsoleColor.Blue;
                        contentBackgroundColor = ConsoleColor.DarkGreen;
                        header = "___   " + header + "   ___";
                        break;
                }
                if (writeLogFile)
                {
                    try
                    {
                        if (!writeLogFileInitialised)
                            LogFileInit();
                        lock (fileLocker)
                        {
                            logFile.Write(".......   " + header + " (" + timeNow + ") \n");
                            logFile.Write(content + "\n");
                        }
                    }
                    catch (Exception e)
                    {
                        lock (fileLocker)
                        {
                            Console.WriteLine("***logger exception:");
                            Console.WriteLine("{0}", e);
                        }
                    }
                }
                if (writeLogConsole)
                {
                    lock (consoleLocker)
                    {
                        Console.ForegroundColor = headerForegroundColor;
                        Console.BackgroundColor = headerBackgroundColor;

                        Console.WriteLine(".......   " + header + " (" + timeNow + ")");
                        Console.ForegroundColor = contentForegroundColor;
                        Console.BackgroundColor = contentBackgroundColor;
                        Console.WriteLine(content);
                    }
                }
                Console.ForegroundColor = currentForegroundColor;
                Console.BackgroundColor = currentBackgroundColor;
            }
        }

        // Error reporting helper
        public static void err(string fmt, params object[] objs)
        {
            log(string.Format(fmt, objs), "ERROR***", TypeMessage.error);
        }

        public static void err(string fmt)
        {
            err(fmt, new object[] {});
        }

        // Info (trace) messages processing helper
        public static void msg(string fmt, params object[] objs)
        {
            log(string.Format(fmt, objs), "TRACE***", TypeMessage.unimportant);
        }

        public static void msg(string fmt)
        {
            msg(fmt, new object[] {});
        }

        public static void config(bool writeLogConsoleOn = true, bool writeLogFileOn = true)
        {
            writeLogConsole = writeLogConsoleOn;
            writeLogFile = writeLogFileOn;
        }

        public static void configWriteConsole(bool writeLogConsoleOn = true)
        {
            writeLogConsole = writeLogConsoleOn;
        }

        public static void configWriteFile(bool writeLogFileOn = true)
        {
            writeLogFile = writeLogFileOn;
        }


        private static string logNameGenerate()
        {
            var dt = DateTime.Now;
            string timeLine = String.Format("Y{0}M{1}D{2}H{3}m{4}S{5}", dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            timeLine = timeLine + ".log";
            return String.Format("{0}_{1}",Process.GetCurrentProcess().ProcessName,timeLine);
        }

        public static void LogFileInit()
        {
            System.IO.Directory.CreateDirectory(path);
            logFile = File.CreateText(logFileName());
            logFile.AutoFlush = true;
            writeLogFileInitialised = true;
        }
    }
}
