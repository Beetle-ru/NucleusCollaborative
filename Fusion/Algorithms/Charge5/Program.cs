using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using ConnectionProvider;
using Converter;
using Implements;
using System.IO;

namespace Charge5
{
    class Program
    {
        public static Client MainGate;
        public static Configuration MainConf;
        public static char Separator;
        public static string StorePath;
        public static List<CSVTableParser> Tables;
        public static CSVTableParser InitTbl;
        public static List<string> TablePaths;
        public const int MaxTables = 7;
        public const string PIName = "Path.init";

        static void Main(string[] args)
        {
            Init();
            Console.WriteLine("Charge5 is running, press enter to exit");
            Console.ReadLine();
        }
        static void Init()
        {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            Separator = MainConf.AppSettings.Settings["separator"].Value.ToArray()[0];
            StorePath = MainConf.AppSettings.Settings["StorePath"].Value;

            InitTbl = new CSVTableParser();
            
            TablePaths = ScanStore(StorePath);
            Tables = LoadTables("default", ref InitTbl);
            //SaveTables("new", InitTbl, Tables);

            //////////////////////////////////
            CSVTP_FlexEventConverter.AppName = "Charge5";
            var tableFlex = CSVTP_FlexEventConverter.PackToFlex("newToFlex", InitTbl, Tables);
            var name = "";
            CSVTP_FlexEventConverter.UnpackFromFlex(tableFlex, ref InitTbl, ref Tables, ref name);
            Console.WriteLine("Pare: {0}", name);
            SaveTables("newFromFlex", InitTbl, Tables);

        }

        public static List<string> ScanStore(string path)
        {
            return Directory.GetDirectories(StorePath).ToList();
        }

        public static List<CSVTableParser> LoadTables(string name, ref CSVTableParser initbl)
        {
            var tables = new List<CSVTableParser>();
            //var initbl = new CSVTableParser();
            
            
            for (int i = 0; i < MaxTables; i++)
            {
                tables.Add(new CSVTableParser());
            }
            var isFound = false;
            foreach (var tablePath in TablePaths)
            {
                var tpsplt = tablePath.Split('\\');
                foreach (var s in tpsplt)
                {
                    if (s == name)
                    {
                        var pathInit = String.Format("{0}\\{1}", tablePath, PIName);
                        if (File.Exists(pathInit))
                        {
                            isFound = true;
                            initbl.FileName = pathInit;
                            initbl.Separator = Separator;
                            SetDescriptionPI(ref initbl);
                            initbl.Load();
                        }
                    }
                }
                if (isFound)
                {
                    foreach (var row in initbl.Rows)
                    {
                        var tb = new CSVTableParser
                                     {
                                         FileName = String.Format("{0}\\{1}.csv", tablePath, row.Cell["TableName"]),
                                         Separator = Separator
                                     };
                        SetDescriptionTBL(ref tb);
                        tb.Load();
                        tables[(int) row.Cell["Index"]] = tb;
                    }

                }
            }
            return tables;
        }


        public static void SaveTables(string name, CSVTableParser inittbl, List<CSVTableParser> tables)
        {
            var currentPathName = StorePath + "\\" + name;
            Directory.CreateDirectory(currentPathName);

            foreach (var row in inittbl.Rows)
            {
                tables[(int) row.Cell["Index"]].FileName = String.Format("{0}\\{1}.csv", currentPathName, (row.Cell["TableName"]));
                tables[(int) row.Cell["Index"]].Separator = Separator;
                tables[(int) row.Cell["Index"]].Save();
            }
            inittbl.FileName = currentPathName + "\\" + PIName;
            inittbl.Separator = Separator;
            inittbl.Save();
        }
        public static void SetDescriptionPI(ref CSVTableParser table)
        {
            table.Description.Add(new ColumnPath() { ColumnName = "Index", ColumnType = typeof(int) });
            table.Description.Add(new ColumnPath() { ColumnName = "TableName", ColumnType = typeof(string) });
        }
        public static void SetDescriptionTBL(ref CSVTableParser table)
        {
            table.Description.Add(new ColumnPath() { ColumnName = "MinSiHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MaxSiHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MinTHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MaxTHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassHotIron", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassScrap", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassLime", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassDolom", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "UVSMassDolom", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassFOM", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "UVSMassFOM", ColumnType = typeof(double) });
            table.Description.Add(new ColumnPath() { ColumnName = "MassDolomS", ColumnType = typeof(double) });
        }
    }
}
