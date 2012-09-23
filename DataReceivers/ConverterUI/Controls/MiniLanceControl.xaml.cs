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

namespace ConverterUI.Controls
{
    /// <summary>
    /// Interaction logic for MiniLanceControl.xaml
    /// </summary>
    public partial class MiniLanceControl : UserControl
    {
        System.Timers.Timer timer = new System.Timers.Timer(1000);

        public MiniLanceControl()
        {
            InitializeComponent();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Enabled = true;  
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                if(Helper.HeatInfo.BlowingStartTime != null)
                {
                    TimeSpan blowingTime = DateTime.Now - Helper.HeatInfo.BlowingStartTime.Value;
                    tbBlowingTime.Text = string.Format("{0}:{1}", (int)blowingTime.TotalMinutes, blowingTime.Seconds);
                }
                else 
                {
                    tbBlowingTime.Text = "";
                }
            }));
        }
    }
}
