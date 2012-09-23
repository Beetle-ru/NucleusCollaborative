using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ConverterUI.Controls
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        MainWindow mainWindow;

        public Menu()
        {
            InitializeComponent();
            mainWindow = Application.Current.MainWindow as MainWindow;
            //this.DataContext = Helper.HeatInfo;
            //lConverterNumber.Content = mainWindow.converterAPIClient.GetConverterNumber();
        }


        private void btF1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btF2_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate(@"\Views\MainView.xaml");
        }

        private void btF3_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate("LancePage.xaml");
        }

        private void btF4_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate("TractPage.xaml");
        }

        private void btF5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btF6_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btF7_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Navigate("HeatEditorPage.xaml");
        }

        private void btF8_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btF9_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btF10_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btF11_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btF12_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void btExit_Click(object sender, RoutedEventArgs e)
        {
	        mainWindow.Button_Click_2(sender, e);
            Application.Current.Shutdown();
        }


        public void ProcessKey(Key key)
        {
            switch (key)
            {
                case Key.F1:
                    btF1_Click(this, null);
                    break;
                case Key.F2:
                    btF2_Click(this, null);
                    break;
                case Key.F3:
                    btF3_Click(this, null);
                    break;
                case Key.F4:
                    btF4_Click(this, null);
                    break;
                case Key.F5:
                    btF5_Click(this, null);
                    break;
                case Key.F6:
                    btF6_Click(this, null);
                    break;
                case Key.F7:
                    btF7_Click(this, null);
                    break;
                case Key.F8:
                    btF8_Click(this, null);
                    break;
                case Key.F9:
                    btF9_Click(this, null);
                    break;
                case Key.F10:
                    btF10_Click(this, null);
                    break;
                case Key.F11:
                    btF11_Click(this, null);
                    break;
                case Key.F12:
                    btF12_Click(this, null);
                    break;
            }
        }
    }
}
