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

    public partial class Mine : UserControl
    {
        public enum MineState
        {
            EmptyClosed = 0,
            EmptyClosedUp = 1,
            EmptyOpened = 2,
            FullClosed = 3,
            FullClosedOut = 4,
            FullClosedUp = 5
        }

        private MineState _state = MineState.EmptyClosedUp;

        string _name = "";

        public Mine()
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

        public MineState State
        {
            get { return _state; }
            set
            {
                //BitmapImage a;
                //RotateTransform rotate;
                //Rotation

                _state = value;
                switch (_state)
                {
                    case MineState.EmptyClosed:
                        mineImg.Source = new BitmapImage(new Uri(@"/Images/MineEmptyClosedUp.png", UriKind.Relative));
                        break;
                    case MineState.EmptyOpened:
                        mineImg.Source = new BitmapImage(new Uri(@"/Images/MineEmptyOpened.png", UriKind.Relative));
                        break;
                    case MineState.FullClosed:
                        mineImg.Source = new BitmapImage(new Uri(@"/Images/MineFullClosed.png", UriKind.Relative));
                        break;
                    case MineState.FullClosedOut:
                        mineImg.Source = new BitmapImage(new Uri(@"/Images/MineFullClosedOut.png", UriKind.Relative));
                        break;
                    case MineState.FullClosedUp:
                        mineImg.Source = new BitmapImage(new Uri(@"/Images/MineFullClosedUp.png", UriKind.Relative));
                        break;
                }
                mineImg.InvalidateVisual();

            }
        }

    }
}
