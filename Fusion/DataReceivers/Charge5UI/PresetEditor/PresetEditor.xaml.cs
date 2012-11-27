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

namespace Charge5UI.PatternEditor
{
    /// <summary>
    /// Логика взаимодействия для PatternEditor.xaml
    /// </summary>
    public partial class PatternEditor : Window
    {
        public List<TableData> DGTables;
        public const int CountTables = 7;
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
    
    }
}
