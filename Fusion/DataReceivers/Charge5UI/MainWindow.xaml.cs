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


namespace Charge5UI
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public InData ModelInData = new InData();
        public OutData ModelOutData = new OutData();
        public MainWindow()
        {
            InitializeComponent();
            Pointer.PMainWindow = this;

            lbSteeType.ItemsSource = GetSteelTypesData();
            
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
            ModelInData.SteelType = lbSteeType.SelectedIndex;
        }
        
    }
}
