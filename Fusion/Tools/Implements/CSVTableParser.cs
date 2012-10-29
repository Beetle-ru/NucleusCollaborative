using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Implements
{
    public class CSVTableParser
    {
        public List<ColumnPath> Description = new List<ColumnPath>();
        public String FileName;
        public List<Row> Rows = new List<Row>();
        public char Separator = ';';
        private Row m_row = new Row();

        public void ColumnCreator()
        {
            Rows = new List<Row>();
            m_row = new Row();
            foreach (ColumnPath columnPath in Description)
            {
                if (!m_row.Cell.ContainsKey(columnPath.ColumnName))
                {
                    //m_row.Cell.Add(columnPath.ColumnName, Activator.CreateInstance(columnPath.ColumnType));
                    if (columnPath.ColumnType == typeof(int))
                    {
                        m_row.Cell.Add(columnPath.ColumnName, 0);
                    }
                    
                    if (columnPath.ColumnType == typeof(double))
                    {
                        m_row.Cell.Add(columnPath.ColumnName, 0.0);
                    }

                    if (columnPath.ColumnType == typeof(string))
                    {
                        m_row.Cell.Add(columnPath.ColumnName, "");
                    }
                }
            }
        }

        public void Load()
        {
            string[] strings;
            try
            {
                strings = File.ReadAllLines(FileName);
            }
            catch
            {
                strings = new string[0];
                InstantLogger.err("Cannot read the file: {0}", FileName);
                //Console.WriteLine("Cannot read the file: {0}", FileName);
                return;
            }

            try
            {
                if (strings.Any())
                {
                    ColumnCreator();
                    string[] headers = strings[0].Split(Separator);
                    for (int strCnt = 1; strCnt < strings.Count(); strCnt++)
                    {
                        string[] values = strings[strCnt].Split(Separator);
                        if (values.Any())
                        {
                            Rows.Add(m_row);
                            var currentRow = Rows.Count - 1;
                            for (int colNumber = 0; colNumber < headers.Count(); colNumber++)
                            {
                                if (colNumber < values.Count())
                                {
                                    var colName = headers[colNumber];
                                    if (Rows[currentRow].Cell.ContainsKey(colName))
                                    {
                                        var currentType = Rows[currentRow].Cell[colName].GetType();
                                        Rows[currentRow].Cell[colName] = UniverConv(values[colNumber],currentType);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File: {0}\nis empty", FileName);
                    return;
                }

            }
            catch (Exception e)
            {
                InstantLogger.err("Cannot parce the file: {0}, bad format call exeption: {1}", FileName, e.ToString());
                //Console.WriteLine("Cannot parce the file: {0}, bad format call exeption: {1}", FileName, e.ToString());
                return;
            }
        }

        private object UniverConv(string str, Type type)
        {
            
            if (type == typeof(int))
            {
                try
                {
                    return Int32.Parse(str);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            if (type == typeof(double))
            {
                try
                {
                    return Double.Parse(str);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            if (type == typeof(string))
            {
                return str;
            }
            return new object();
        }

        public override string ToString()
        {
            var str = String.Format("{0}\n", base.ToString());
            string strTypes = "", strColNames = "", strParsedValues = "";
            foreach (var columnPath in Description)
            {
                strTypes += String.Format("{0} | ", columnPath.ColumnType);
                strColNames += String.Format("{0} | ", columnPath.ColumnName);
            }
            str += String.Format("{0}\n{1}\n\n", strTypes, strColNames);

            strColNames = "";

            if (Rows.Any())
            {
                for (int i = 0; i < Rows[0].Cell.Count; i++)
                {
                    strColNames += String.Format("{0} | ", Rows[0].Cell.ElementAt(i).Key);
                }
            }

            foreach (var row in Rows)
            {
                for (int i = 0; i < row.Cell.Count; i++)
                {
                    var cKey = row.Cell.ElementAt(i).Key;
                    strParsedValues += String.Format("{0} | ", row.Cell[cKey]);
                }
                strParsedValues += "\n";
            }
            str += String.Format("{0}\n{1}", strColNames, strParsedValues);
            return str;
        }
    }

    public class ColumnPath
    {
        //public int ColumnNumber;
        public string ColumnName;
        public Type ColumnType;
    }

    public class Row
    {
        public Dictionary<string, object> Cell = new Dictionary<string, object>();
    }
}
