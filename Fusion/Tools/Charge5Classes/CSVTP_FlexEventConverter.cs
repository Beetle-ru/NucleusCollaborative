using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Converter;
using Implements;

namespace Charge5Classes
{
    public class CSVTP_FlexEventConverter
    {
        public const string InitTblFlexName = "InitTbl";
        public static string AppName = Process.GetCurrentProcess().ProcessName;
        public static FlexEvent PackToFlex(string name, CSVTableParser inittbl, List<CSVTableParser> tables)
        {
            var mainFlex = new FlexEvent(AppName + ".Tables." + name);
            var inittblFlex = new FlexEvent(InitTblFlexName);
            var tablesFlexList = new List<FlexEvent>();
            for (int i = 0; i < tables.Count; i++)
            {
                tablesFlexList.Add(new FlexEvent());
            }
            foreach (var row in inittbl.Rows)
            {
                var keyIndex = inittbl.Description[0].ColumnName;
                var keyName = inittbl.Description[1].ColumnName;
                var tbIndex = (int)row.Cell[keyIndex];
                inittblFlex.Arguments.Add(tbIndex.ToString(), row.Cell[keyName]);
                var tableFlex = new FlexEvent((string)row.Cell[keyName]);
                for (int index = 0; index < tables[tbIndex].Rows.Count; index++)
                {
                    var tableRow = tables[tbIndex].Rows[index];
                    var tableRowFlex = new FlexEvent(index.ToString());
                    foreach (var columnPath in tables[tbIndex].Description)
                    {
                        var keyCollumn = columnPath.ColumnName;
                        tableRowFlex.Arguments.Add(keyCollumn, tableRow.Cell[keyCollumn]);
                    }
                    tableFlex.Arguments.Add(index.ToString(), tableRowFlex);
                }
                mainFlex.Arguments.Add(tableFlex.Operation, tableFlex);
            }
            mainFlex.Arguments.Add(inittblFlex.Operation, inittblFlex);
            return mainFlex;
        }

        public static void UnpackFromFlex(FlexEvent mainFlex, ref CSVTableParser inittbl, ref List<CSVTableParser> tables, ref string name)
        {
            inittbl.Rows = new List<Row>();
            for (int i = 0; i < tables.Count; i++)
            {
                tables[i].Rows = new List<Row>();
            }

            try
            {
                var inittblFlex = (FlexEvent)mainFlex.Arguments[InitTblFlexName];
                name = mainFlex.Operation.Split('.').Last();
                foreach (var initTblPath in inittblFlex.Arguments)
                {
                    var tbIndex = Int32.Parse(initTblPath.Key);
                    var tbName = (string)initTblPath.Value;
                    var tableFlex = (FlexEvent)mainFlex.Arguments[tbName];

                    var inittblRow = new Row();
                    inittblRow.Cell.Add(inittbl.Description[0].ColumnName, tbIndex);
                    inittblRow.Cell.Add(inittbl.Description[1].ColumnName, tbName);
                    inittbl.Rows.Add(inittblRow);

                    foreach (var trfObj in tableFlex.Arguments)
                    {
                        var tableRowFlex = (FlexEvent) trfObj.Value;
                        var tableRow = new Row();
                        foreach (var columnPath in tables[tbIndex].Description)
                        {
                            tableRow.Cell.Add(columnPath.ColumnName, tableRowFlex.Arguments[columnPath.ColumnName]);
                        }
                        tables[tbIndex].Rows.Add(tableRow);
                    }
                }
            }
            catch (Exception e)
            {
                InstantLogger.err("CSVTP_FlexEventConverter.UnpackFromFlex:\n{0}", e.ToString());
            }
        }
    }
}
