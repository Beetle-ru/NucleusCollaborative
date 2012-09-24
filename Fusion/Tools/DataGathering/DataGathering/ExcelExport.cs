using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Converter.Trends;
using DataGathering;
using Converter;

namespace DataGathering
{
    class ExcelExport
    {
        private string m_FileName;
        private bool m_FileExist=false;
        public void Save(string directoryName)
        {
            if (!m_FileExist)
            {
                m_FileName = string.Format("{0}\\{1}", directoryName, m_FileName);
            }
            Save();
        }

        public void Save()
        {
            ExcelWorkBook.SaveAs(m_FileName + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, false, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, 2, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            ExcelWorkBook.Close(0);
            ExcelApp.Quit();
        }

        public void Do(string[] lines, string sheatName)
        {
            Microsoft.Office.Interop.Excel.Worksheet newWorksheet = ExcelWorkBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            newWorksheet.Name = sheatName;
            ExcelApp.Cells.Font.Name = "Courier New";
            int col = 1; int row = 1;
            
            if (lines.Length == 0)
            {
                ExcelApp.Cells[1, 1] = "Нет данных";
                return;
            }

            foreach (string line in lines)
            {
                ExcelApp.Cells[row++, col] = line;
            }
        }

        public void Do(DataGridView dataGrid, string sheatName)
        {
            Microsoft.Office.Interop.Excel.Worksheet newWorksheet = ExcelWorkBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            newWorksheet.Name = sheatName;

            int col = 0; int row = 0;

            if (dataGrid.Rows.Count == 0)
            {
                ExcelApp.Cells[1, 1] = "Нет данных";
                return;
            }

            var colCount = 0;
            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                if (!dataGrid.Columns[i].Visible) continue;
                ExcelApp.Cells[row + 1, col + 1 + colCount] = dataGrid.Columns[i].HeaderText;
                colCount++;
            }

            row = 1;
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                colCount = 0;
                for (int j = 0; j < dataGrid.ColumnCount; j++)
                {
                    if (!dataGrid.Columns[j].Visible) continue;
                    ExcelApp.Cells[row + i + 1, colCount + 1] = dataGrid.Rows[i].Cells[j].Value;
                    colCount++;
                }
            }
        }

        public ExcelExport(string fileName)
        {
            m_ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            m_FileName = fileName;
            if (!System.IO.File.Exists(fileName+".xls"))
            {
                //Книга.
                ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            }
            else
            {
                ExcelWorkBook = ExcelApp.Workbooks.Open(fileName, 1, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", false, false, 0, true, false, Microsoft.Office.Interop.Excel.XlCorruptLoad.xlNormalLoad);
                m_FileExist = true;
            }
            //Таблица.
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);
            //ExcelWorkSheet.Name = "test1";
        }

        public bool DoCommon(Fusion fusion, List<OffGas> offgases, List<Lance> lances, List<Addition> additions, List<Sublance> sublances, List<SlagOutburstEvent> slagOutburst, List<IgnitionEvent> ignition)
        {
            if (!m_FileExist)
            {
                Microsoft.Office.Interop.Excel.Worksheet newWorksheet = ExcelWorkBook.Sheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                newWorksheet.Name = "Сводные данные";
            }
            int col = 2; int row = ExcelApp.Rows.Worksheet.UsedRange.Rows.Count + 2;

            ExcelApp.Cells[++row, col++] = "Плавка :";
            ExcelApp.Cells[row, col++] = "Марка: ";
            ExcelApp.Cells[row, col++] = "T задан.: ";
            ExcelApp.Cells[row, col++] = "T факт.: ";
            ExcelApp.Cells[row, col++] = "С задан.: ";
            ExcelApp.Cells[row, col++] = "С факт.: ";
            ExcelApp.Cells[row, col++] = "Дата начала: ";
            ExcelApp.Cells[row, col] = "Дата окончания: ";
            col = 2;
            ExcelApp.Cells[++row, col++] = fusion.Number.ToString();
            ExcelApp.Cells[row, col++] = fusion.Grade;
            ExcelApp.Cells[row, col++] = fusion.PlannedTemperature;
            ExcelApp.Cells[row, col++] = fusion.FactTemperature;
            ExcelApp.Cells[row, col++] = fusion.PlannedCarbon;
            ExcelApp.Cells[row, col++] = fusion.FactCarbon;
            ExcelApp.Cells[row, col++] = fusion.StartDate;
            ExcelApp.Cells[row, col] = fusion.EndDate;
            ExcelApp.Cells[++row, 1] = "Время";
            ExcelApp.Cells[row, 2] = "Положение фурмы";
            ExcelApp.Cells[row, 3] = "Интенсивность О2";
            ExcelApp.Cells[row, 4] = "H2";
            ExcelApp.Cells[row, 5] = "О2";
            ExcelApp.Cells[row, 6] = "CО";
            ExcelApp.Cells[row, 7] = "CО2";
            ExcelApp.Cells[row, 8] = "N2";
            ExcelApp.Cells[row, 9] = "Ar";
            ExcelApp.Cells[row, 10] = "Материал название";
            ExcelApp.Cells[row, 11] = "Материал вес";
            ExcelApp.Cells[row, 12] = "Зонд [T]";
            ExcelApp.Cells[row, 13] = "Зонд [C]";
            ExcelApp.Cells[row, 14] = "Зонд [Оксислен.]";
            ExcelApp.Cells[row, 15] = "Лев.фурма Q воды вход";
            ExcelApp.Cells[row, 16] = "Лев.фурма T воды вход";
            ExcelApp.Cells[row, 17] = "Лев.фурма Q воды слив";
            ExcelApp.Cells[row, 18] = "Лев.фурма T воды слив";
            ExcelApp.Cells[row, 19] = "Прав.фурма Q воды вход";
            ExcelApp.Cells[row, 20] = "Прав.фурма T воды вход";
            ExcelApp.Cells[row, 21] = "Прав.фурма Q воды слив";
            ExcelApp.Cells[row, 22] = "Прав.фурма T воды слив";
            ExcelApp.Cells[row, 23] = "V отх. газов";
            ExcelApp.Cells[row, 24] = "Т отх. газов";
            ExcelApp.Cells[row, 25] = "Т отх. газов на выходе";
            ExcelApp.Cells[row, 26] = "Т отх. газов охлаждение";
            ExcelApp.Cells[row, 27] = "Т отх. газов 1 ступень";
            ExcelApp.Cells[row, 28] = "Т отх. газов 2 ступень";
            ExcelApp.Cells[row, 29] = "Зажигание плавки";
            ExcelApp.Cells[row, 30] = "Выбросы шлака";

            row++;

            var minDate = DateTime.Now;
            var maxDate = new DateTime();

            if (offgases.Count != 0)
            {
                minDate = new[] { minDate, offgases.Min(p => p.Date) }.Min();
                maxDate = new[] { maxDate, offgases.Max(p => p.Date) }.Max();
            }

            if (lances.Count != 0)
            {
                minDate = new[] { minDate, lances.Min(p => p.Date) }.Min();
                maxDate = new[] { maxDate, lances.Max(p => p.Date) }.Max();
            }

            if (additions.Count != 0)
            {
                minDate = new[] { minDate, additions.Min(p => p.Date) }.Min();
                maxDate = new[] { maxDate, additions.Max(p => p.Date) }.Max();
            }

            DateTime curDate = minDate;
            Lance lastLanceInfo = null;
            OffGas lastOffgas = null;
            while (curDate <= maxDate)
            {
                bool findSomeThing = false;
                Lance lanceInfo = null;
                if (lances != null)
                {
                    lanceInfo = lances.Find(p =>p.Date.Date == curDate.Date && p.Date.Hour == curDate.Hour && p.Date.Minute == curDate.Minute && p.Date.Second == curDate.Second);
                    if (lanceInfo != null)
                    {
                        ExcelApp.Cells[row, 2] = lanceInfo.Height;
                        ExcelApp.Cells[row, 3] = lanceInfo.O2Flow;
                        ExcelApp.Cells[row, 15] = lanceInfo.O2LeftLanceWaterInput;
                        ExcelApp.Cells[row, 16] = lanceInfo.O2LeftLanceWaterTempInput;
                        ExcelApp.Cells[row, 17] = lanceInfo.O2LeftLanceWaterOutput;
                        ExcelApp.Cells[row, 18] = lanceInfo.O2LeftLanceWaterTempOutput;
                        ExcelApp.Cells[row, 19] = lanceInfo.O2RightLanceWaterInput;
                        ExcelApp.Cells[row, 20] = lanceInfo.O2RightLanceWaterTempInput;
                        ExcelApp.Cells[row, 21] = lanceInfo.O2RightLanceWaterOutput;
                        ExcelApp.Cells[row, 22] = lanceInfo.O2RightLanceWaterTempOutput;
                        lastLanceInfo = lanceInfo;
                        findSomeThing = true;
                    }
                }

                OffGas offgas = null;
                if (offgases != null)
                {

                    offgas = offgases.Find( p =>p.Date.Date == curDate.Date && p.Date.Hour == curDate.Hour && p.Date.Minute == curDate.Minute && p.Date.Second == curDate.Second);
                    if (offgas != null)
                    {
                        ExcelApp.Cells[row, 4] = offgas.H2;
                        ExcelApp.Cells[row, 5] = offgas.O2;
                        ExcelApp.Cells[row, 6] = offgas.CO;
                        ExcelApp.Cells[row, 7] = offgas.CO2;
                        ExcelApp.Cells[row, 8] = offgas.N2;
                        ExcelApp.Cells[row, 9] = offgas.Ar;
                        ExcelApp.Cells[row, 23] = offgas.Temperature;
                        ExcelApp.Cells[row, 24] = offgas.Flow;
                        ExcelApp.Cells[row, 25] = offgas.TemperatureOnExit;
                        ExcelApp.Cells[row, 26] = offgas.PrecollingTemperature;
                        ExcelApp.Cells[row, 27] = offgas.TemperatureAfter1Step;
                        ExcelApp.Cells[row, 28] = offgas.TemperatureAfter2Step;
                        lastOffgas = offgas;
                        findSomeThing = true;
                    }
                }

                if (additions != null)
                    {
                        Addition addition = additions.Find(p => p.Date.Date == curDate.Date && p.Date.Hour == curDate.Hour && p.Date.Minute == curDate.Minute 
                                                                && p.Date.Second == curDate.Second);
                        if (addition != null)
                        {
                            ExcelApp.Cells[row, 10] = addition.MaterialName;
                            ExcelApp.Cells[row, 11] = addition.PortionWeight;
                            findSomeThing = true;
                        }
                    }

                    if (sublances != null)
                    {
                        Sublance sublance = sublances.Find(p => p.StartDate.Date == curDate.Date && p.StartDate.Hour == curDate.Hour && p.StartDate.Minute == curDate.Minute
                                                                && p.StartDate.Second == curDate.Second);
                        if (sublance != null)
                        {
                            ExcelApp.Cells[row, 12] = sublance.Temperature;
                            ExcelApp.Cells[row, 13] = sublance.C;
                            ExcelApp.Cells[row, 14] = sublance.Oxigen;
                            findSomeThing = true;
                        }
                    }

                    if (ignition != null)
                    {
                        IgnitionEvent ign = ignition.Find(p => p.Time.Date == curDate.Date && p.Time.Hour == curDate.Hour && p.Time.Minute == curDate.Minute
                                                               && p.Time.Second == curDate.Second);
                        if (ign != null)
                        {
                            ExcelApp.Cells[row, 29] = ign.FusionIgnition;
                            findSomeThing = true;
                        }
                    }

                    if (slagOutburst != null)
                    {
                        SlagOutburstEvent outburst = slagOutburst.Find(p => p.Time.Date == curDate.Date && p.Time.Hour == curDate.Hour && p.Time.Minute == curDate.Minute
                                                                            && p.Time.Second == curDate.Second);
                        if (outburst != null)
                        {
                            ExcelApp.Cells[row, 30] = outburst.Counter;
                            findSomeThing = true;
                        }
                    }


                    if (findSomeThing)
                    {
                        if (lanceInfo == null && lastLanceInfo != null)
                        {
                            ExcelApp.Cells[row, 2] = lastLanceInfo.Height;
                            ExcelApp.Cells[row, 3] = lastLanceInfo.O2Flow;
                        }

                        if (offgas == null && lastOffgas != null)
                        {
                            ExcelApp.Cells[row, 4] = lastOffgas.H2;
                            ExcelApp.Cells[row, 5] = lastOffgas.O2;
                            ExcelApp.Cells[row, 6] = lastOffgas.CO;
                            ExcelApp.Cells[row, 7] = lastOffgas.CO2;
                            ExcelApp.Cells[row, 8] = lastOffgas.N2;
                            ExcelApp.Cells[row, 9] = lastOffgas.Ar;
                        }
                        ExcelApp.Cells[row++, 1] = curDate;
                    }
                    
                curDate = curDate.AddSeconds(1);
            }
            return true;
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

    }
}
