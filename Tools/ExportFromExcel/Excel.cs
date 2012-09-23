using System;
using Microsoft.Office.Interop.Excel;

namespace Emulator
{
    class Excel
    {
        private readonly Application xlApp;
        private Workbook _workbook;
        private Worksheet _worksheet;

        private string _connectionString;

        public Excel()
        {
            xlApp = new Application();
        }

        public bool SaveAs(string fileName, System.Data.DataTable dataTable)
        {
            if (dataTable == null || dataTable.Rows.Count == 0) return false;
            if (xlApp == null) return false;
            _workbook = xlApp.Workbooks.Add(Type.Missing);
            _worksheet = (Worksheet)_workbook.Worksheets[1];
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                _worksheet.Cells[1, i + 1] = dataTable.Columns[i].ColumnName;
                //range = (Range)worksheet.Cells[1, i + 1];
                //range.Interior.ColorIndex = 15;
                //range.Font.Bold = true;
            }
            _workbook.SaveAs(fileName,XlFileFormat.xlWorkbookNormal, "", "", false, false, XlSaveAsAccessMode.xlExclusive,XlPlatform.xlWindows, false, false, false, false);
            _workbook.Close(true, fileName, false);
            xlApp.Quit();
            _connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + fileName + "';Extended Properties=Excel 8.0;"; // строка подключения
            using (var excelConn = new System.Data.OleDb.OleDbConnection(_connectionString)) // используем OleDb
            {
                var queryValues = String.Empty;
                excelConn.Open();
                for (var i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (var c = 0; c < dataTable.Columns.Count; c++)
                    {
                        queryValues += dataTable.Rows[i][c] + "','";
                    }
                    queryValues = queryValues.Substring(0, queryValues.Length - 3);
                    var writeCmd = new System.Data.OleDb.OleDbCommand("INSERT INTO [Лист1$] VALUES ('" + queryValues + "')", excelConn);
                    writeCmd.ExecuteNonQuery(); // вставляем данные в лист1 файла - filename
                    writeCmd.Dispose();
                    queryValues = String.Empty;
                }
                excelConn.Close();
            }
            return System.IO.File.Exists(fileName);
        }

        public System.Data.DataTable Read(string filename)
        {
            var excelDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
            _connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + filename + "; Extended Properties = Excel 8.0;";
            var excelConn = new System.Data.OleDb.OleDbConnection(_connectionString);
            excelConn.Open();
            var dtProtocol = new System.Data.DataTable { TableName = "Protocol" };
            var excelCommand = new System.Data.OleDb.OleDbCommand("SELECT * FROM [протокол плавки$]", excelConn);
            excelDataAdapter.SelectCommand = excelCommand;
            excelDataAdapter.Fill(dtProtocol);
            excelConn.Close();
            return dtProtocol;
        }
    }
}
