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
using System.Threading;
using ConverterVisio.MainGate;
using System.ServiceModel;
using Converter;
using ZedGraph;

namespace ConverterVisio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int converterAngle = 0;
        public int lanceHeight;
        Heat heat = new Heat();
        double Zoom = 0.1;
        public int EventsAcceleration = 20;
        public DateTime? StartTime;
        public DateTime? StartBlowingTime;
        bool NeedToUpdateGraph = false;
        SolidColorBrush coBrush = new SolidColorBrush();

        Trends LanceTrend = new Trends("Фурма", System.Drawing.Color.Lime);
        Trends OFlowTrend = new Trends("Кислород", System.Drawing.Color.Magenta);
        Trends COTrend = new Trends("CO", System.Drawing.Color.Red);
        Trends CO2Trend = new Trends("CO2", System.Drawing.Color.Orange);
        Trends H2Trend = new Trends("H2", System.Drawing.Color.Green);
        Trends N2Trend = new Trends("N2", System.Drawing.Color.Black);
        Trends O2Trend = new Trends("O2", System.Drawing.Color.Blue);
        Trends ArTrend = new Trends("Ar", System.Drawing.Color.Turquoise);
        List<Trends> ListOfTrends = new List<Trends>();


        ZedGraph.ZedGraphControl zGraph = new ZedGraph.ZedGraphControl();

        public void InitZedGraph()
        {
            zGraph.IsEnableHZoom = false;
            zGraph.IsEnableVZoom = false;
            zGraph.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.XAxis.Title.IsVisible = false;
            zGraph.GraphPane.YAxis.Title.IsVisible = false;
            zGraph.GraphPane.Title.IsVisible = false;
            zGraph.GraphPane.XAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.YAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.XAxis.Scale.MajorStep = 300;
            zGraph.GraphPane.XAxis.Scale.MinorStep = 60;
            zGraph.GraphPane.Legend.IsVisible = false;
        }


        public System.Windows.Media.Color ConvertColor(System.Drawing.Color dColor)
        {
            System.Windows.Media.Color color = new Color();
            color.R = dColor.R;
            color.G = dColor.G;
            color.B = dColor.B;
            return color;
        }

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += UpdateScreen;
            InitZedGraph();
            wfhZedGraph.Child = zGraph;

            COTrend.NameLabel = lCOName;
            COTrend.ValueLabel = lCOValue;
            COTrend.XZoom = EventsAcceleration;
            //SolidColorBrush brush = new SolidColorBrush();
            //brush.Freeze();
            //coBrush = new SolidColorBrush(ConvertColor(COTrend.Color));
            //rCOColor.Fill = coBrush;

            //rCOColor.UpdateLayout();

            CO2Trend.NameLabel = lCO2Name;
            CO2Trend.ValueLabel = lCO2Value;
            CO2Trend.XZoom = EventsAcceleration;
           // rCO2Color.Fill = new SolidColorBrush(ConvertColor(CO2Trend.Color));


            H2Trend.NameLabel = lH2Name;
            H2Trend.ValueLabel = lH2Value;
            H2Trend.XZoom = EventsAcceleration;
          //  rH2Color.Fill = new SolidColorBrush(ConvertColor(H2Trend.Color));

            N2Trend.NameLabel = lN2Name;
            N2Trend.ValueLabel = lN2Value;
            N2Trend.XZoom = EventsAcceleration;
          //  rN2Color.Fill = new SolidColorBrush(ConvertColor(N2Trend.Color));

            O2Trend.NameLabel = lO2Name;
            O2Trend.ValueLabel = lO2Value;
            O2Trend.XZoom = EventsAcceleration;
         //   rO2Color.Fill = new SolidColorBrush(ConvertColor(O2Trend.Color));

            ArTrend.NameLabel = lArName;
            ArTrend.ValueLabel = lArValue;
            ArTrend.XZoom = EventsAcceleration;
         //   rArColor.Fill = new SolidColorBrush(ConvertColor(ArTrend.Color));

            LanceTrend.NameLabel = lLanceName;
            LanceTrend.ValueLabel = lLanceValue;
            LanceTrend.XZoom = EventsAcceleration;
            LanceTrend.YZoom = Zoom;
            //      rLanceColor.Fill = new SolidColorBrush(ConvertColor(LanceTrend.Color));

            OFlowTrend.NameLabel = lOFlowName;
            OFlowTrend.ValueLabel = lOFlowValue;
            OFlowTrend.XZoom = EventsAcceleration;
            OFlowTrend.YZoom = Zoom;
            //    rO2FlowColor.Fill = new SolidColorBrush(ConvertColor(OFlowTrend.Color));

            ListOfTrends.Add(COTrend);
            ListOfTrends.Add(CO2Trend);
            ListOfTrends.Add(H2Trend);
            ListOfTrends.Add(N2Trend);
            ListOfTrends.Add(O2Trend);
            ListOfTrends.Add(ArTrend);
            ListOfTrends.Add(LanceTrend);
            ListOfTrends.Add(OFlowTrend);
        }

        public void AddGasPoints(OffGasAnalysisEvent ogaEvent)
        {
            double time;
            time = (DateTime.Now - StartBlowingTime.Value).TotalSeconds;
            COTrend.Update(time, (float)ogaEvent.CO);
            CO2Trend.Update(time, (float)ogaEvent.CO2);
            H2Trend.Update(time, (float)ogaEvent.H2);
            N2Trend.Update(time, (float)ogaEvent.N2);
            O2Trend.Update(time, (float)ogaEvent.O2);
            ArTrend.Update(time, (float)ogaEvent.Ar);
            NeedToUpdateGraph = true;
        }

        public void AddLancePoints(LanceEvent lEvent)
        {
            LanceTrend.Update((DateTime.Now - StartBlowingTime.Value).TotalSeconds, lEvent.LanceHeight);
            OFlowTrend.Update((DateTime.Now - StartBlowingTime.Value).TotalSeconds, (float)lEvent.O2Flow);
            lanceHeight = lEvent.LanceHeight;
            lbLanceHeight.Content = lEvent.LanceHeight;
            lbO2Flow.Content = lEvent.O2Flow;
        }

        public void UpdateConverterAngle(int angle) 
        {
            converterAngle = angle;
            lConverterAngle.Content = angle;
        }

        public void UpdateScreen(object sender, EventArgs e)
        {
            if (NeedToUpdateGraph)
            {
                UpdateGraph();
            }
            NeedToUpdateGraph = false;
            lbDate.Content = DateTime.Now.Date.ToShortDateString();
            lbTime.Content = DateTime.Now.ToLongTimeString();
            ConverterImage.RenderTransform = new RotateTransform(converterAngle, ConverterImage.Width / 2, ConverterImage.Height / 2);
            if (converterAngle >= 0 && converterAngle < 10 || converterAngle > 350 && converterAngle < 360)
            {
                if (lanceHeight > 0)
                {
                    Lance.Height = 270 - (int)((float)lanceHeight / 1200 * 80);
                }
            }
            else
            {
                Lance.Height = 170;
            }
            #region анимация железа и шлака
            /*if (converterAngle <= -90)
            {
                imageFe.Visibility = System.Windows.Visibility.Visible;
                imageFe1.Visibility = System.Windows.Visibility.Visible;
                imageFe2.Visibility = System.Windows.Visibility.Visible;
                imageFe3.Visibility = System.Windows.Visibility.Visible;
                if (Canvas.GetTop(imageFe3) > 250)
                {
                    Canvas.SetTop(imageFe, 137);
                    Canvas.SetTop(imageFe1, 137);
                    Canvas.SetTop(imageFe2, 137);
                    Canvas.SetTop(imageFe3, 137);
                    scaleIndex = 0;
                }
                Canvas.SetTop(imageFe, Canvas.GetTop(imageFe) + 2);
                imageFe.RenderTransform = new ScaleTransform(1 - 0.01 * scaleIndex, 1 - 0.01 * scaleIndex);
                Canvas.SetTop(imageFe1, Canvas.GetTop(imageFe1) + 3);
                imageFe1.RenderTransform = new ScaleTransform(1 - 0.02 * scaleIndex, 1 - 0.02 * scaleIndex);
                Canvas.SetTop(imageFe2, Canvas.GetTop(imageFe2) + 5);
                imageFe2.RenderTransform = new ScaleTransform(1 - 0.03 * scaleIndex, 1 - 0.03 * scaleIndex);
                Canvas.SetTop(imageFe3, Canvas.GetTop(imageFe3) + 7);
                imageFe3.RenderTransform = new ScaleTransform(1 - 0.04 * scaleIndex, 1 - 0.04 * scaleIndex);
                scaleIndex++;
            }
            else
            {
                imageFe.Visibility = System.Windows.Visibility.Hidden;
                imageFe1.Visibility = System.Windows.Visibility.Hidden;
                imageFe2.Visibility = System.Windows.Visibility.Hidden;
                imageFe3.Visibility = System.Windows.Visibility.Hidden;
            }
            if (converterAngle >= 105)
            {
                slag.Visibility = System.Windows.Visibility.Visible;
                slag1.Visibility = System.Windows.Visibility.Visible;
                slag2.Visibility = System.Windows.Visibility.Visible;
                slag3.Visibility = System.Windows.Visibility.Visible;
                if (Canvas.GetTop(slag) > 170)
                {
                    Canvas.SetTop(slag, 130);
                    Canvas.SetTop(slag1, 130);
                    Canvas.SetTop(slag2, 130);
                    Canvas.SetTop(slag3, 130);
                }
                Canvas.SetTop(slag, Canvas.GetTop(slag) + 2);
                Canvas.SetTop(slag1, Canvas.GetTop(slag1) + 4);
                Canvas.SetTop(slag2, Canvas.GetTop(slag2) + 6);
                Canvas.SetTop(slag3, Canvas.GetTop(slag3) + 8);
            }
            else
            {
                slag.Visibility = System.Windows.Visibility.Hidden;
                slag1.Visibility = System.Windows.Visibility.Hidden;
                slag2.Visibility = System.Windows.Visibility.Hidden;
                slag3.Visibility = System.Windows.Visibility.Hidden;
            }
            if (SlagOut)
            {
                if (converterAngle < 105)
                {
                    converterAngle += 2;
                }
                else
                {
                    SlagOut = false;
                }
            }
            if (FeOut)
            {
                if (converterAngle > -90)
                {
                    converterAngle -= 2;
                }
                else
                {
                    FeOut = false;
                }
            }
             */
            #endregion
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainGateClient mainGate = new MainGateClient(new InstanceContext(new EventsListener(this)));
            mainGate.Subscribe();
        }

        public void UpdateGraph()
        {

            zGraph.GraphPane.CurveList.Clear();

            foreach (Trends trends in ListOfTrends)
            {
                if (trends.IsVisible)
                {
                    zGraph.GraphPane.AddCurve(trends.Name, trends.Points, trends.Color, SymbolType.None);
                }
            }


            zGraph.GraphPane.Title.IsVisible = false;
            zGraph.AxisChange();
            zGraph.Invalidate();
        }

        private void rLanceColor_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //string t = (rLanceColor.Fill as SolidColorBrush).Color.ToString();
        }

        private void cbShowLanceCurve_Checked(object sender, RoutedEventArgs e)
        {
            LanceTrend.IsVisible = cbShowLanceCurve.IsChecked.Value;
        }

        private void cbShowO2FlowCurve_Checked(object sender, RoutedEventArgs e)
        {
            OFlowTrend.IsVisible = cbShowO2FlowCurve.IsChecked.Value;
        }

        private void cbShowCOCurve_Checked(object sender, RoutedEventArgs e)
        {
            COTrend.IsVisible = cbShowCOCurve.IsChecked.Value;
        }

        private void cbShowCO2Curve_Checked(object sender, RoutedEventArgs e)
        {
            CO2Trend.IsVisible = cbShowCO2Curve.IsChecked.Value;
        }

        private void cbShowN2Curve_Checked(object sender, RoutedEventArgs e)
        {
            N2Trend.IsVisible = cbShowN2Curve.IsChecked.Value;
        }

        private void cbShowH2Curve_Checked(object sender, RoutedEventArgs e)
        {
            H2Trend.IsVisible = cbShowH2Curve.IsChecked.Value;
        }

        private void cbShowO2Curve_Checked(object sender, RoutedEventArgs e)
        {
            O2Trend.IsVisible = cbShowO2Curve.IsChecked.Value;
        }

        private void cbShowArCurve_Checked(object sender, RoutedEventArgs e)
        {
            ArTrend.IsVisible = cbShowArCurve.IsChecked.Value;
        }
    }
}
