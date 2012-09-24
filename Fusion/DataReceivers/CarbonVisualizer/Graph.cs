using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Converter;
using Implements;

namespace CarbonVisualizer
{
    public partial class Graph : Form
    {
        private System.Threading.Timer m_onLoad;
        public List<Implements.Curve> Curves;
        public double CarbonCurrent, LancePos, CarbonMonoxideVolumePercent;
        public SimpleGrp PaintGraphs;
        private bool bpIsCreated ;
        private ConnectionProvider.Client m_listenGate;
        public void Init()
        {
            Curves = new List<Implements.Curve>();
            Curves.Add(new Implements.Curve());
            Curves.Add(new Implements.Curve());
            Curves.Add(new Implements.Curve());
            Curves.Add(new Implements.Curve());
            Curves.Add(new Implements.Curve());
            Curves[0].ColorCurve = Color.Yellow;
            Curves[1].ColorCurve = Color.Magenta;
            Curves[2].ColorCurve = Color.DodgerBlue;
            Curves[3].ColorCurve = Color.LimeGreen;
            Curves[4].ColorCurve = Color.Blue;
            //Curves[0].AddPoint(10, 10);
            //for (int i = 0; i < 100; i++)
            //{

            //    //Curves[0].AddPoint((float)(i * 0.0000004), (float)(i));
            //    //Curves[0].AddPoint(10, 10);
            //    //Curves[1].AddPoint(i, 50);
            //    //Curves[2].AddPoint(i, 60);
            //}
            //for (int i = 0; i < 100; i++)
            //{
            //    Curves[0].AddPoint(i, 100-i);
            //}
        }
        public Graph()
        {
            InitializeComponent();
            bpIsCreated = false;
            Init();
            m_onLoad = new System.Threading.Timer(new TimerCallback(TimerOnload));
            m_onLoad.Change(500, 0);
            PaintGraphs = new SimpleGrp(pbGraph.Font);
            
            var o = new HeatChangeEvent();
            m_listenGate = new ConnectionProvider.Client(new Listener());
            m_listenGate.Subscribe();
        }


        private void TimerOnload(object obj)
        {
            Size size = splitMain.Panel1.Size;
            pbGraph.Image = PaintGraphs.Redraw(size.Width, size.Height, Curves);
            bpIsCreated = true;
        }


        private void splitMain_Panel1_Resize(object sender, EventArgs e)
        {
            if (bpIsCreated)
            {
                Size size = splitMain.Panel1.Size;
                pbGraph.Image = PaintGraphs.Redraw(size.Width, size.Height, Curves);
                pbGraph.Size = size;
            }
        }

        public void Redraw()
        {
            if (bpIsCreated)
            {
                Size size = splitMain.Panel1.Size;
                pbGraph.Image = PaintGraphs.Redraw(size.Width, size.Height, Curves);
                pbGraph.Size = size;
                
                lblCarbon.Text = Math.Round(CarbonCurrent, 5).ToString();
                lblCarbon.ForeColor = Curves[0].ColorCurve;
                lblCarbonText.ForeColor = Curves[0].ColorCurve;
                
                lblLancePosition.Text = Math.Round(LancePos, 5).ToString();
                lblLancePosition.ForeColor = Curves[1].ColorCurve;
                lblLancePositionText.ForeColor = Curves[1].ColorCurve;

                lblCO.Text = Math.Round(CarbonMonoxideVolumePercent, 5).ToString();
                lblCO.ForeColor = Curves[4].ColorCurve;
                lblCOText.ForeColor = Curves[4].ColorCurve;
                
                lblSubLanceStartText.ForeColor = Curves[2].ColorCurve;
                
                lblFixDataMFactorMText.ForeColor = Curves[3].ColorCurve;
            }
        }
    }
}
