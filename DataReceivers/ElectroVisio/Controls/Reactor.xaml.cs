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
    /// Логика взаимодействия для Reactor.xaml
    /// </summary>
    public partial class Reactor : UserControl
    {
        public Reactor()
        {
            InitializeComponent();
            //                        mineImg.RenderTransform = new RotateTransform() { Angle = 45.0 };

            //
            //
        }

        private double _angle = 0.0;


        public double Angle
        {
            get { return _angle; }
            set 
            {
                reactorImg.RenderTransform = new RotateTransform() { Angle = _angle, CenterX = reactorImg.Width / 2, CenterY = reactorImg.Height / 2};
                _angle = value;
            }
        }
    }
}
