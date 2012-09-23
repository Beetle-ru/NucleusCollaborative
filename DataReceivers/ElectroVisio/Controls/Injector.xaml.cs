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
    /// Логика взаимодействия для Injector.xaml
    /// </summary>

    public partial class Injector : UserControl
    {
        public enum InjectorState
        {
            Off = 0,
            Slow = 1,
            Full = 2,
            FullO2
        }

        private bool _enableO2 = true; 

        private InjectorState _state = InjectorState.Full;

        string _name = "";

        public Injector()
        {
            InitializeComponent();
        }

        public string InjectorName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public InjectorState State
        {
            get { return _state; }
            set
            {
                _state = value;
                switch (_state)
                {
                    case InjectorState.Off:
                        injectorImg.Source = new BitmapImage(new Uri(@"/Images/InjectOff.png", UriKind.Relative));
                        break;
                    case InjectorState.Slow:
                        injectorImg.Source = new BitmapImage(new Uri(@"/Images/InjectSlow.png", UriKind.Relative));
                        break;
                    case InjectorState.Full:
                        injectorImg.Source = new BitmapImage(new Uri(@"/Images/InjectFull.png", UriKind.Relative));
                        break;
                    case InjectorState.FullO2:
                        if (_enableO2)
                        {
                            injectorImg.Source = new BitmapImage(new Uri(@"/Images/InjectO2.png", UriKind.Relative));
                        }
                        else
                        {
                            _state = InjectorState.Off;
                            injectorImg.Source = new BitmapImage(new Uri(@"/Images/InjectOff.png", UriKind.Relative));
                            //injectorImg.Source = new BitmapImage(new Uri(@"/Images/InjectFull.png", UriKind.Relative));
                           
                        }
                        break;
                }
                //injectorImg.InvalidateVisual();
                //this.InvalidateVisual();
            }
        }

        public bool EnableO2
        {
            get { return _enableO2; }
            set
            {
                if (!value)
                {
                    _state = InjectorState.Off;
                }
                _enableO2 = value;
            }
        }
    }
}
