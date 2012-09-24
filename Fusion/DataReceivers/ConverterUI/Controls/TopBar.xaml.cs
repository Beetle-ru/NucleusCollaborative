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
using System.Windows.Threading;

namespace ConverterUI
{
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        
        public TopBar()
        {
            InitializeComponent();
            //this.DataContext = Helper.HeatInfo;
           /* timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;*/
        }


        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                //lDate.Content = DateTime.Now.ToShortDateString();
                //lTime.Content = DateTime.Now.ToLongTimeString();
            }));
        }
    }
}
