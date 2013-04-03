using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConnectionProvider;
using Converter;

namespace EndBlowingTCorrectionTableEditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public const string ArgEventName = "@EventName";
        public const string ArgCommandName = "@Command";
        public const string ArgCountName = "@Count";
        public const string ArgErrorCodeName = "@ErrorCode";
        public const string ArgErrorStringName = "@ErrorString";

        public static Configuration MainConf;
        public static Client MainGate;

        public List<TableRow> TableData;
        public List<TableRow> StandartTableData;
        public List<string> TableSchema;
        public int CurrentSchema;
        public int TableChangeCounter;
        
        public MainWindow() {
            InitializeComponent();
            Pointer.PMainWindow = this;
            Init();
        }

        public void Init() {
            MainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            //CfgMainDir = ConfigurationManager.OpenExeConfiguration("").AppSettings.Settings["CfgMainDir"].Value;

            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            TableData = new List<TableRow>();
            StandartTableData = new List<TableRow>();
            dgScheme.ItemsSource = TableData;
            dgScheme.Items.Refresh();

            TableSchema = new List<string>();
            TableSchema.Add("Схема 1");
            TableSchema.Add("Схема 2");
            TableSchema.Add("Схема 3");
            cbScheme.ItemsSource = TableSchema;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LogWrite("Save...");
            TableChangeCounter = 0;
            for (int i = 0; i < TableData.Count; i++) {
                TableData[i].Item = i;
                if (i < StandartTableData.Count) {
                    if (!RowIsCompare(TableData[i], StandartTableData[i])) {
                        //LogWrite(String.Format("Row {0} modified", i));
                        ReqUpdateRow(TableData[i]);
                        TableChangeCounter++;
                    }
                }
                else {
                    //LogWrite(String.Format("Row {0} created", i));
                    ReqInsertRow(TableData[i]);
                    TableChangeCounter++;
                }
            }
            for (int i = TableData.Count; i < StandartTableData.Count; i++)
            {
                //LogWrite(String.Format("Row {0} deleted", i));
                ReqDeleteRow(StandartTableData[i]);
                TableChangeCounter++;
            }

            if (TableChangeCounter != 0) {
                btnSave.IsEnabled = false;
            }
        }

        public void ReqInsertRow(TableRow tr) {
            var fex = new FlexHelper("DBFlex.Request");
            fex.AddArg(ArgEventName, "SQL.Corrections");
            fex.AddArg(ArgCommandName, "InsertSchemeRow");
            fex.AddArg("Schema", CurrentSchema);
            fex.AddArg("Item", tr.Item);
            fex.AddArg("Cmin", tr.CMin);
            fex.AddArg("Cmax", tr.CMax);
            fex.AddArg("Oxygen", tr.Oxygen);
            fex.AddArg("Heating", tr.Heating);
            fex.Fire(MainGate);
        }

        public void ReqUpdateRow(TableRow tr)
        {
            var fex = new FlexHelper("DBFlex.Request");
            fex.AddArg(ArgEventName, "SQL.Corrections");
            fex.AddArg(ArgCommandName, "UpdateSchemeRow");
            fex.AddArg("Schema", CurrentSchema);
            fex.AddArg("Item", tr.Item);
            fex.AddArg("Cmin", tr.CMin);
            fex.AddArg("Cmax", tr.CMax);
            fex.AddArg("Oxygen", tr.Oxygen);
            fex.AddArg("Heating", tr.Heating);
            fex.Fire(MainGate);
        }

        public void ReqDeleteRow(TableRow tr)
        {
            var fex = new FlexHelper("DBFlex.Request");
            fex.AddArg(ArgEventName, "SQL.Corrections");
            fex.AddArg(ArgCommandName, "DeleteSchemeRow");
            fex.AddArg("Schema", CurrentSchema);
            fex.AddArg("Item", tr.Item);
            fex.Fire(MainGate);
        }

        private bool RowIsCompare(TableRow tr1, TableRow  tr2) {
            return (tr1.Item == tr2.Item)
                && (tr1.CMin == tr2.CMin)
                && (tr1.CMax == tr2.CMax)
                && (tr1.Oxygen == tr2.Oxygen)
                && (tr1.Heating == tr2.Heating);
        }

        public void LogWrite(object message)
        {
            tbLog.AppendText(message + "\n");
            tbLog.ScrollToVerticalOffset(tbLog.VerticalOffset + Double.MaxValue);
        }

        private void cbScheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbScheme.SelectedIndex >= 0) {
                CurrentSchema = cbScheme.SelectedIndex + 1;
                ReqScheme(CurrentSchema);
            }
        }

        public void ReqScheme(int schemaN)
        {
            var fex = new FlexHelper("DBFlex.Request");
            fex.AddArg(ArgEventName, "SQL.Corrections");
            fex.AddArg(ArgCommandName, "GetScheme");
            fex.AddArg("Schema", schemaN);
            fex.Fire(MainGate);
            //LogWrite(fex.evt);
        }
    }
}
