using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using ConnectionProvider;
using Converter;
using Implements;

namespace HunterEvents {
    internal class Program {
        public static Client MainGate;
        public const char Separator = ';';
        public const string Dir = "CapturedEvents";
        public const string Patch = Dir + "\\EventsList.csv";
        public static List<CEDataFormat> CapturedEvents;
        public static Timer SaveTimer = new Timer(60*1000);

        private static void Main(string[] args) {
            Init();
            Console.WriteLine("For exit press \"Enter\"");
            Console.ReadLine();
        }

        public static void Init() {
            CapturedEvents = new List<CEDataFormat>();
            var o = new FlexEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();
            Load();
            SaveTimer.Elapsed += new ElapsedEventHandler(SaveTimeOut);
            SaveTimer.Enabled = true;
        }

        public static void Save() {
            Directory.CreateDirectory(Dir);
            using (Logger l = new Logger("Save")) {
                var strings = new List<string>();
                strings.Add(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                                          Separator,
                                          "Operation",
                                          "Count",
                                          "CountDayAverage",
                                          "LastCaptureTime",
                                          "ArgumentKey",
                                          "ArgumentType",
                                          "ExampleValue",
                                          "Comment"
                                ));


                foreach (var capturedEvent in CapturedEvents) {
                    strings.Add(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                                              Separator,
                                              capturedEvent.Operation,
                                              capturedEvent.CountTotal,
                                              capturedEvent.CountDayAverage,
                                              capturedEvent.CaptureTime,
                                              "",
                                              "",
                                              "",
                                              capturedEvent.Comment
                                    ));
                    foreach (var att in capturedEvent.AttList) {
                        strings.Add(String.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}",
                                                  Separator,
                                                  "",
                                                  "",
                                                  "",
                                                  "",
                                                  att.Key,
                                                  att.Type,
                                                  att.ExampleValue,
                                                  att.Comment
                                        ));
                    }
                }
                try {
                    File.WriteAllLines(Patch, strings);
                    //l.msg("Data saved");
                }
                catch (Exception e) {
                    l.err("Cannot write the file: {0}, call exeption: {1}", Patch, e.ToString());
                    return;
                    //throw;
                }
            }
        }

        public static void Load() {
            using (Logger l = new Logger("LoadMatrix")) {
                CapturedEvents = new List<CEDataFormat>();
                string[] strings;
                try {
                    strings = File.ReadAllLines(Patch);
                }
                catch {
                    strings = new string[0];
                    l.err("Cannot read the file: {0}", Patch);
                    return;
                }

                try {
                    for (int strCnt = 1; strCnt < strings.Count(); strCnt++) {
                        string[] values = strings[strCnt].Split(Separator);
                        if (values.Any()) {
                            if (values[0] != "") {
                                var capturedEvent = new CEDataFormat();
                                capturedEvent.Operation = values[0];
                                capturedEvent.CountTotal = Convertion.StrToInt64(values[1]);
                                capturedEvent.CountDayAverage = Convertion.StrToInt64(values[2]);
                                capturedEvent.CaptureTime = Convertion.StrToDateTime(values[3]);
                                capturedEvent.Comment = values[7];
                                CapturedEvents.Add(capturedEvent);
                            }
                            else {
                                var att = new AttDataFormat();
                                att.Key = values[4];
                                att.Type = values[5];
                                att.ExampleValue = values[6];
                                att.Comment = values[7];
                                CapturedEvents[CapturedEvents.Count - 1].AttList.Add(att);
                            }
                        }
                    }
                }
                catch (Exception e) {
                    l.err("Cannot read the file: {0}, bad format call exeption: {1}", Patch, e.ToString());
                    return;
                    //throw e;
                }
            }
        }

        public static void Update(FlexEvent flx) {
            var isUpdated = false;
            foreach (var capturedEvent in CapturedEvents) {
                if (capturedEvent.Operation == flx.Operation) {
                    isUpdated = true;
                    capturedEvent.CountTotal++;
                    capturedEvent.CountDayAverage = 0;
                    capturedEvent.CaptureTime = DateTime.Now;
                    for (int arg = 0; arg < flx.Arguments.Count; arg++) {
                        var key = flx.Arguments.ElementAt(arg).Key;
                        var argIsFound = false;
                        var type = flx.Arguments[key].GetType().ToString();
                        var exampleValue = flx.Arguments[key].ToString();
                        foreach (var att in capturedEvent.AttList) {
                            if (att.Key == key) {
                                att.Type = type;
                                att.ExampleValue = exampleValue;
                                argIsFound = true;
                            }
                        }
                        if (!argIsFound) {
                            var att = new AttDataFormat();
                            att.Key = key;
                            att.Type = type;
                            att.ExampleValue = exampleValue;
                            capturedEvent.AttList.Add(att);
                        }
                    }
                }
            }
            if (!isUpdated) {
                InstantLogger.msg("Captured new event: {0}", flx.Operation);
                var capturedEvent = new CEDataFormat();
                capturedEvent.Operation = flx.Operation;
                CapturedEvents.Add(capturedEvent);
                Update(flx);
            }
        }

        public static void SaveTimeOut(object source, ElapsedEventArgs e) {
            Console.Write("*");
            Save();
        }
    }
}