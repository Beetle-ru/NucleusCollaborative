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
using ElectroVisio.ViewModels;
using ElectroVisio.Views;

namespace ElectroVisio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Navigate(string page)
        {
            MainPageViewModel vm = new MainPageViewModel();
            MainView view = new MainView();
            view.DataContext = vm;
            MainFrame.Navigate(view);
        }

        private void aSUStatus2_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
