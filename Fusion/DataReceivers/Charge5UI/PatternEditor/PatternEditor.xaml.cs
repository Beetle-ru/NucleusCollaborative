using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Charge5Classes;
using ConnectionProvider;
using Implements;

namespace Charge5UI.PatternEditor
{
    /// <summary>
    /// Логика взаимодействия для PatternEditor.xaml
    /// </summary>
    public partial class PatternEditor : Window
    {
        public List<TableData> DGTables;
        public const int CountTables = 7;
        public CSVTableParser InitTbl;
        public List<CSVTableParser> Tables;
        public string PatternLoadedName = "";
        public PatternEditor()
        {
            InitializeComponent();
            Pointer.PPatternEditor = this;

            Init();
            StatusChange("Редактор паттернов загружен");
        }

        public void Init()
        {
            Requester.ReqPatternNames(Requester.MainGate);
            ResetTables();
            InitTbl = new CSVTableParser();
            Charge5Classes.Descriptions.SetDescriptionPI(ref InitTbl);
            Tables = new List<CSVTableParser>();
            for (int i = 0; i < CountTables; i++)
            {
                var t = new CSVTableParser();
                Charge5Classes.Descriptions.SetDescriptionTBL(ref t);
                Tables.Add(t);
            }
        }

        public void ResetTables()
        {
            DGTables = new List<TableData>();
            for (int i = 0; i < CountTables; i++)
            {
               DGTables.Add(new TableData());
            }
            SetGridsData();
        }

        public void SetGridsData()
        {
            dgTable1.ItemsSource = DGTables[0].Rows;
            dgTable2.ItemsSource = DGTables[1].Rows;
            dgTable3.ItemsSource = DGTables[2].Rows;
            dgTable4.ItemsSource = DGTables[3].Rows;
            dgTable5.ItemsSource = DGTables[4].Rows;
            dgTable6.ItemsSource = DGTables[5].Rows;
            dgTable7.ItemsSource = DGTables[6].Rows;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (lstPatterns.SelectedIndex >= 0)
            {
                Requester.ReqGetPattern(Requester.MainGate, (string)lstPatterns.SelectedValue);
                StatusChange("Запрошен паттерн " + (string)lstPatterns.SelectedValue);
            }
            else
            {
                StatusChange("Ошибка: \"не выбран паттерн\"");
            }
        }

        public void ConsolePush(string message)
        {
            tbConsole.AppendText(message + "\n");
            tbConsole.ScrollToVerticalOffset(tbConsole.VerticalOffset + Double.MaxValue);
        }

        public void StatusChange(string message)
        {
            lblStatus.Content = message;
            ConsolePush(message);
        }

        public void DisplayPattern()
        {
            for (int index = 0; index < Tables.Count; index++)
            {
                DGTables[index] = CsvtpToTD(Tables[index]);
            }
            SetGridsData();
        }

        private TableData CsvtpToTD(CSVTableParser table)
        {
            var tableOut = new TableData();
            foreach (var row in table.Rows)
            {
                tableOut.Rows.Add(new TableRow()
                {
                    MassDolom = (double)row.Cell["MassDolom"],
                    MassDolomS = (double)row.Cell["MassDolomS"],
                    MassFOM = (double)row.Cell["MassFOM"],
                    MassHotIron = (double)row.Cell["MassHotIron"],
                    MassLime = (double)row.Cell["MassLime"],
                    MassScrap = (double)row.Cell["MassScrap"],
                    MaxSiHotIron = (double)row.Cell["MaxSiHotIron"],
                    MaxTHotIron = (double)row.Cell["MaxTHotIron"],
                    MinSiHotIron = (double)row.Cell["MinSiHotIron"],
                    MinTHotIron = (double)row.Cell["MinTHotIron"],
                    UVSMassDolom = (double)row.Cell["UVSMassDolom"],
                    UVSMassFOM = (double)row.Cell["UVSMassFOM"]
                });
            }
            return tableOut;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string msg = String.Format("Сохранить изменения в паттерне \"{0}\"?", PatternLoadedName);
            var res = MessageBox.Show(msg, "Подтверждение сохранения", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                PatternSave();
            }
        }

        private void PatternSave()
        {
            for (int i = 0; i < CountTables; i++)
            {
                Tables[i] = UpdateCsvtpFromTd(DGTables[i], Tables[i]);
            }

            CSVTP_FlexEventConverter.AppName = "UI";
            var flex = CSVTP_FlexEventConverter.PackToFlex(PatternLoadedName, InitTbl, Tables);
            ConsolePush("Паттерн запакован");
            var fex = new FlexHelper(flex.Operation);
            fex.evt.Arguments = flex.Arguments;
            fex.Fire(Requester.MainGate);
            StatusChange("Паттерн отправлен на сохранение...");
        }

        private CSVTableParser UpdateCsvtpFromTd(TableData tableData, CSVTableParser tableParser)
        {
            tableParser.Rows.RemoveRange(0, tableParser.Rows.Count);
            for (int index = 0; index < tableData.Rows.Count; index++)
            {
                var row = tableData.Rows[index];
                tableParser.Rows.Add(new Row());
                tableParser.Rows[index].Cell["MassDolom"] = row.MassDolom;
                tableParser.Rows[index].Cell["MassDolomS"] = row.MassDolomS;
                tableParser.Rows[index].Cell["MassFOM"] = row.MassFOM;
                tableParser.Rows[index].Cell["MassHotIron"] = row.MassHotIron;
                tableParser.Rows[index].Cell["MassLime"] = row.MassLime;
                tableParser.Rows[index].Cell["MassScrap"] = row.MassScrap;
                tableParser.Rows[index].Cell["MaxSiHotIron"] = row.MaxSiHotIron;
                tableParser.Rows[index].Cell["MaxTHotIron"] = row.MaxTHotIron;
                tableParser.Rows[index].Cell["MinSiHotIron"] = row.MinSiHotIron;
                tableParser.Rows[index].Cell["MinTHotIron"] = row.MinTHotIron;
                tableParser.Rows[index].Cell["UVSMassDolom"] = row.UVSMassDolom;
                tableParser.Rows[index].Cell["UVSMassFOM"] = row.UVSMassFOM;
            }
            return tableParser;
        }

        private void btnCreatePattern_Click(object sender, RoutedEventArgs e)
        {
            if (PatternLoadedName == "")
            {
                MessageBox.Show("Необходимо загрузить паттерн на основе которого будет создан новый паттерн.",
                                "Предупреждение");
            }
            else
            {
                var createPatterDialog = new CreatePattern();
                createPatterDialog.ShowDialog();
            }
        }

        private void btnRemuvePattern_Click(object sender, RoutedEventArgs e)
        {
            if (lstPatterns.SelectedIndex >= 0)
            {
                var name = (string) lstPatterns.SelectedValue;
                var msg = String.Format("Удалить паттерн \"{0}\"?", name);
                var res = MessageBox.Show(msg, "Подтверждение удаления", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    Requester.ReqRemoovePattern(Requester.MainGate, name);
                    StatusChange("Удаление паттерна...");
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать паттерн для удаления.",
                                "Предупреждение");
            }
            
        }
    }
}
