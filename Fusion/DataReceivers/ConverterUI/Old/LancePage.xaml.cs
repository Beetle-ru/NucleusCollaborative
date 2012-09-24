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
    /// Interaction logic for LancePage.xaml
    /// </summary>
    public partial class LancePage : Page
    {
        public LancePage()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UpdateScreen;
            this.DataContext = Helper.HeatInfo;     
            
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            TimeSpan totaltime = DateTime.Now - Helper.HeatInfo.StartDate; 
            StringBuilder sb = new StringBuilder();
            if(totaltime.Hours > 0)
            {
                sb.Append(totaltime.Hours);
                sb.Append(":");
            }
                sb.Append(totaltime.Minutes);
                sb.Append(":");
                sb.Append(totaltime.Seconds.ToString("00"));
                tbTotalTime.Text = sb.ToString();
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
