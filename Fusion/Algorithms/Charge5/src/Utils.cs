using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;

namespace Charge5 {
    internal partial class Program {
        public static List<string> ScanStore(string path) {
            return Directory.GetDirectories(StorePath).ToList();
        }

        public static List<string> GetNamesFromAddress(List<string> paths) {
            var names = new List<string>();
            foreach (var path in paths) {
                if (File.Exists(String.Format("{0}\\{1}", path, PIName))) {
                    var pathSplt = path.Split('\\');
                    if (pathSplt.Any())
                        names.Add(pathSplt[pathSplt.Count() - 1]);
                }
            }
            return names;
        }

        public static List<CSVTableParser> LoadTables(string name, ref CSVTableParser initbl) {
            var tables = new List<CSVTableParser>();
            //var initbl = new CSVTableParser();


            for (int i = 0; i < MaxTables; i++)
                tables.Add(new CSVTableParser());
            var isFound = false;
            foreach (var tablePath in TablePaths) {
                var tpsplt = tablePath.Split('\\');
                foreach (var s in tpsplt) {
                    if (s == name) {
                        var pathInit = String.Format("{0}\\{1}", tablePath, PIName);
                        if (File.Exists(pathInit)) {
                            isFound = true;
                            initbl.FileName = pathInit;
                            initbl.Separator = Separator;
                            Charge5Classes.Descriptions.SetDescriptionPI(ref initbl);
                            initbl.Load();
                        }
                    }
                }
                if (isFound) {
                    foreach (var row in initbl.Rows) {
                        var tb = new CSVTableParser {
                                                        FileName =
                                                            String.Format("{0}\\{1}.csv", tablePath,
                                                                          row.Cell["TableName"]),
                                                        Separator = Separator
                                                    };
                        Charge5Classes.Descriptions.SetDescriptionTBL(ref tb);
                        tb.Load();
                        tables[(int) row.Cell["Index"]] = tb;
                    }
                }
            }
            if (isFound)
                return tables;
            else
                return null;
        }


        public static void SaveTables(string name, CSVTableParser inittbl, List<CSVTableParser> tables) {
            var currentPathName = StorePath + "\\" + name;
            Directory.CreateDirectory(currentPathName);

            foreach (var row in inittbl.Rows) {
                tables[(int) row.Cell["Index"]].FileName = String.Format("{0}\\{1}.csv", currentPathName,
                                                                         (row.Cell["TableName"]));
                tables[(int) row.Cell["Index"]].Separator = Separator;
                tables[(int) row.Cell["Index"]].Save();
            }
            inittbl.FileName = currentPathName + "\\" + PIName;
            inittbl.Separator = Separator;
            inittbl.Save();
        }

        public static void RemooveTables(string name) {
            if (name == "default") throw new Exception("Remoove \"default\" aborted");
            var currentPathName = StorePath + "\\" + name;
            Directory.Delete(currentPathName, true);
        }
    }
}