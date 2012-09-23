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

namespace ElectroVisio
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        String[] menuText = { "F1-Помощь1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10-Предыдущий", "F11", "F12-Главный" };
        String[] menuUris = { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "MainPage.xaml" };


        public MainPage()
        {
            InitializeComponent();
            for (int i = 0; i < 12; i++)
            {
                MainMenu.AddItem(i, menuText[i], menuUris[i]);
            }
            MainMenu.parentPage = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
