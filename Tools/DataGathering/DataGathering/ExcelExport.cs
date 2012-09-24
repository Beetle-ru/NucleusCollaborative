using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
namespace DataGathering
{
    class ExcelExport
    {  
        private string m_FileName;
        int m_Count; 
        public void Save(string directoryName)
        {
            m_FileName = string.Format("{0}\\{1}", directoryName, m_FileName);
            Save();
        }
        public void Save()
        {
            ExcelWorkBook.SaveAs(m_FileName + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ExcelWorkBook.Close(0);
            ExcelApp.Quit();
        }
        public void Do(DataGridView dataGrid, string sheatName)
        {
            Microsoft.Office.Interop.Excel.Worksheet newWorksheet=ExcelWorkBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            newWorksheet.Name = sheatName;

            int col = 0; int row = 0;
            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                ExcelApp.Cells[row + 1, col + 1 + i] = dataGrid.Columns[i].HeaderText;
            }

            row = 1;
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                for (int j = 0; j < dataGrid.ColumnCount; j++)
                {
                    ExcelApp.Cells[row + i + 1, j + 1] = dataGrid.Rows[i].Cells[j].Value;
                }
            }
            

            //Вызываем нашу созданную эксельку.
            //ExcelApp.Visible = true;
            //ExcelApp.UserControl = true;
            
        }
        public ExcelExport(string fileName)
        {
            m_Count=1;
            m_ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            m_FileName = fileName;

            //Книга.
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);

            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            //ExcelWorkSheet.Name = "test1";

        }
        Microsoft.Office.Interop.Excel.Application m_ExcelApp;
        Microsoft.Office.Interop.Excel.Workbook m_ExcelWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet m_ExcelWorkSheet;

        public Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet
        {
            get { return m_ExcelWorkSheet; }
            set { m_ExcelWorkSheet = value; }
        }
        public Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook
        {
            get { return m_ExcelWorkBook; }
            set { m_ExcelWorkBook = value; }
        }
        public Microsoft.Office.Interop.Excel.Application ExcelApp
        {
            get { return m_ExcelApp; }
            set { m_ExcelApp = value; }
        }

        /* 
 
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    ExcelApp.Cells[i + 1, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
                }
            }
            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;  
         * */
    }
}
