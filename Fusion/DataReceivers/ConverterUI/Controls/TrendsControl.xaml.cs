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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace ConverterUI
{
    /// <summary>
    /// Interaction logic for TrendsControl.xaml
    /// </summary>
    public partial class TrendsControl : UserControl
    {

        public TrendsControl()
        {
            InitializeComponent();
           
            this.DataContext = Helper.HeatInfo;
            plotter.AddLineGraph(Helper.O2Points, Colors.Green, 2, "Кислород");
            plotter.AddLineGraph(Helper.LancePoints, Colors.Red, 2, "Фурма");
            plotter.AddLineGraph(Helper.COPoints, Colors.Cyan, 2, "CO");
            plotter.AddLineGraph(Helper.CO2Points, Colors.OliveDrab, 2, "CO2");
            plotter.AddLineGraph(Helper.H2Points, Colors.Brown, 2, "H2");
            plotter.AddLineGraph(Helper.N2Points, Colors.Black, 2, "N2");
            plotter.AddLineGraph(Helper.ArPoints, Colors.YellowGreen, 2, "Ar");

        }

        private void cbShowCO2Curve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowLanceCurve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowO2FlowCurve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowCOCurve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowN2Curve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowH2Curve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowO2Curve_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbShowArCurve_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
