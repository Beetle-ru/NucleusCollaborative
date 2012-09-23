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
using ElectroVisio.Controls;

namespace ElectroVisio.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Page
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void Injector_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Injector i = sender as ElectroVisio.Controls.Injector;
            if  ( (!i.EnableO2 && (i.State == Injector.InjectorState.Full)) || (i.EnableO2 && (i.State == Injector.InjectorState.FullO2)) )
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

        private void Reactor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Reactor r = sender as Reactor;
                r.Angle++;
            }
            else
            {
                Reactor r = sender as Reactor;
                r.Angle--;
                
            }
        }

        private void Reactor_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Reactor r = sender as Reactor;
            r.Angle--;
        }

        private void Reactor_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void reactorStatus1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        

       
    }
}
