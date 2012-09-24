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

namespace ElectroVisio.Controls
{
    /// <summary>
    /// Interaction logic for StatusControl.xaml
    /// </summary>
    public partial class IntensStatus : UserControl
    {
        public IntensStatus()
        {
            InitializeComponent();
        }

        private void Injector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Injector i = sender as ElectroVisio.Controls.Injector;
            if ((!i.EnableO2 && (i.State == Injector.InjectorState.Full)) || (i.EnableO2 && (i.State == Injector.InjectorState.FullO2)))
            {
                i.State = Injector.InjectorState.Off;
            }
            else
            {
                i.State++;
            }
        }

        private void Mine_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mine i = sender as ElectroVisio.Controls.Mine;
            if (i.State == Mine.MineState.FullClosedUp)
            {
                i.State = Mine.MineState.EmptyClosed;
            }
            else
            {
                i.State++;
            }
        }
    }
}
