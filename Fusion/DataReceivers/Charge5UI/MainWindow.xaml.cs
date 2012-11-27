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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Charge5Classes;
using ConnectionProvider;
using Converter;
using Implements;


namespace Charge5UI
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public InData ModelInData = new InData();
        public OutData ModelOutData = new OutData();
        public static Client MainGate;
        public MainWindow()
        {
            InitializeComponent();
            Pointer.PMainWindow = this;

            Init();
         }

        public List<string> GetSteelTypesData()
        {
            var descriptions = new List<string>();
            descriptions.Add("1 группа «Рядовые марки стали». Температура металла 1640 – 1660 0С.");
            descriptions.Add("2 группа «Автолистовая сталь». Температура металла 1640 – 1660 0С.");
            descriptions.Add("3 группа Р≤0,030%, «Штрипсовый и судостроительный металл с обработкой на УПК или УДМ. Температура металла 1660 – 1680 0С.");
            descriptions.Add("3 группа Р≤0,015%, «Штрипсовый и судостроительный металл с обработкой на УПК или УДМ. Температура металла 1660 – 1680 0С.");
            descriptions.Add("4 группа Р≤0,015%, «Штрипсовый и судостроительный металл с вакуумированием. Температура металла 1660 – 1680 0С.");
            descriptions.Add("5 группа «IF стали» с последующей обработкой на УВС. Температура металла 1700 – 1720 0С.");
            descriptions.Add("5 группа «IF стали» с обработкой на УПК и последующей обработкой на УВС. Температура металла 1650 – 1670 0С.");
            return descriptions;
        }

        private void LBSteeTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lbSteelType.SelectedIndex >= 0)
            {
                ModelInData.SteelType = lbSteelType.SelectedIndex;
                lblSteelType.Content = String.Format("Выбрана {0} группа стали",
                                                     GetSteelTypesData()[ModelInData.SteelType].Split(' ')[0]);
                lblSteelType.Background = new SolidColorBrush(Color.FromArgb(0,0,0,0));
            }
            else
            {
                DoChangeSteelGroup();
            }
        }

        private void cbPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPreset.SelectedIndex >= 0)
            {
                lblPreset.Content = "Загрузка пресета ...";
                lblPreset.Background = new SolidColorBrush(Color.FromArgb(127, 0, 100, 50));
                ReqPresetLoad(MainGate, (string)cbPreset.SelectedValue);
            }
            else
            {
                DoChangePreset();
            }
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            
            try
            {
                if (tbMHi.Text != "0"  && tbMHi.Text != "")
                {
                    ModelInData.MHi = Int32.Parse(tbMHi.Text);
                }
                else
                {
                    ModelInData.MHi = 0;
                }

                if (tbMSc.Text != "0" && tbMSc.Text != "")
                {
                    ModelInData.MSc = Int32.Parse(tbMSc.Text);
                }
                else
                {
                    ModelInData.MSc = 0;
                }

                ModelInData.SiHi = Double.Parse(tbSiHi.Text = tbSiHi.Text.Replace('.', ','));
                ModelInData.THi = Int32.Parse(tbTHi.Text);
                if ((ModelInData.SiHi < 0.3) || (ModelInData.SiHi > 1.5)) // потом сделать по нормальному 
                    throw new Exception("Не корректное значение кремния");
                if ((ModelInData.THi < 1320) || (ModelInData.THi > 1460)) // потом сделать по нормальному 
                    throw new Exception("Не корректное значение температуры");
                if (lbSteelType.SelectedIndex >= 0)
                {
                    ModelInData.SteelType = lbSteelType.SelectedIndex;
                }
                else
                {
                    throw new Exception("Не выбрана группа стали");
                }
                if (cbPreset.SelectedIndex >= 0)
                {
                    ModelInData.SteelType = lbSteelType.SelectedIndex;
                }
                else
                {
                    throw new Exception("Не выбран пресет");
                }

                ModelInData.IsProcessingUVS = (bool)chbUVS.IsChecked;

                ReqCalc(MainGate, ModelInData);
                lblStatus.Content = "Запрос расчета ...";
                lblStatus.Background = new SolidColorBrush(Color.FromArgb(127, 0, 0, 255));
            }
            catch (Exception ex)
            {
                lblStatus.Content = String.Format("{0}", ex.Message);
                lblStatus.Background = new SolidColorBrush(Color.FromArgb(127, 255, 0, 0));

            }
            
        }

        private void ResetStatus()
        {
            lblStatus.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            lblStatus.Content = "";
        }
        private void DoChangeSteelGroup()
        {
            lblSteelType.Content = "Группа стали не выбрана";
            lblSteelType.Background = new SolidColorBrush(Color.FromArgb(127, 255, 0, 0));
        }

        private void DoChangePreset()
        {
            lblSteelType.Content = "Пресет не выбран";
            lblSteelType.Background = new SolidColorBrush(Color.FromArgb(127, 255, 0, 0));
        }

        public void Init()
        {
            lbSteelType.ItemsSource = GetSteelTypesData();

            var o = new HeatChangeEvent();
            MainGate = new Client(new Listener());
            MainGate.Subscribe();

            ReqPatternNames(MainGate);
        }

        public void ReqPatternNames(Client CoreGate)
        {
            var fex = new FlexHelper("UI.GetPatternNames");
            fex.Fire(CoreGate);
        }

        public void ReqPresetLoad(Client CoreGate, string presetName)
        {
            var fex = new FlexHelper("UI.LoadPreset");
            fex.AddArg("Name",presetName);
            fex.Fire(CoreGate);
        }

        public void ReqCalc(Client CoreGate, InData modelInData)
        {
            var fex = new FlexHelper("UI.Calc");
            fex.AddArg("SteelType", modelInData.SteelType);
            fex.AddArg("MHi", modelInData.MHi);
            fex.AddArg("MSc", modelInData.MSc);
            fex.AddArg("SiHi", modelInData.SiHi);
            fex.AddArg("THi", modelInData.THi);
            fex.AddArg("IsProcessingUVS", modelInData.IsProcessingUVS);
            fex.Fire(CoreGate);
        }
        
    }
}
