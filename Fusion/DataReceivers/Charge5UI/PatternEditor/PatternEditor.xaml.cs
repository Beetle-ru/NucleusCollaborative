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
        public CSVTableParser Inittbl;
        public List<CSVTableParser> Tables;
        public string PatternLoadedName;
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
            Inittbl = new CSVTableParser();
            Charge5Classes.Descriptions.SetDescriptionPI(ref Inittbl);
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

        }
    
    }
}
