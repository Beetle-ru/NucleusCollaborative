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
        public List<string> TableSchema;
        
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
            
        }

        public void LogWrite(object message)
        {
            tbLog.AppendText(message + "\n");
            tbLog.ScrollToVerticalOffset(tbLog.VerticalOffset + Double.MaxValue);
        }

        private void cbScheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbScheme.SelectedIndex >= 0) {
                //LogWrite(cbScheme.SelectedIndex);
                ReqScheme(cbScheme.SelectedIndex + 1);
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
