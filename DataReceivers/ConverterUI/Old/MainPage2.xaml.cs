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

namespace ConverterUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage2 : Page
    {

        public MainPage2()
        {
            InitializeComponent();
            this.DataContext = Helper.HeatInfo;
        }

        private void trendsControl1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
