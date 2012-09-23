using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Tools.PldParser;
using Tools.Emulator;
using ZedGraph;
using Bestcode.MathParser;
using TrendsViewer.MainGate;
using System.ServiceModel;
using Client;

namespace TrendsViewer
{
    public partial class Trends : Form
    {
        PldParser trendFusion;
        int currentFusionIndex = 0;
        int currentVarsPosition = 10;
        int currentVarsIndex = 0;

        public Trends()
        {
            InitializeComponent();
        }

        private void Trends_Load(object sender, EventArgs e)
        {
            zgMain.IsEnableHZoom = false;
            zgMain.IsEnableVZoom = false;
            zgMain.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zgMain.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zgMain.GraphPane.XAxis.Scale.Min = 0;
            zgMain.GraphPane.YAxis.Scale.Max = 100;
            zgMain.GraphPane.XAxis.Scale.Min = 0;
            zgMain.GraphPane.XAxis.Scale.Max = 1800;
            zgMain.GraphPane.Legend.IsVisible = false;
            zgMain.GraphPane.Title.FontSpec.Size = 12;
            zgMain.GraphPane.XAxis.Title.Text = "";
            zgMain.GraphPane.YAxis.Title.Text = "";
            zgMain.GraphPane.XAxis.Scale.FontSpec.Size = 10;
            zgMain.GraphPane.YAxis.Scale.FontSpec.Size = 10;
            zgMain.GraphPane.XAxis.Scale.MajorStep = 300;
            zgMain.GraphPane.XAxis.Scale.MinorStep = 60;
            zgMain.GraphPane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(XAxis_ScaleFormatEvent);
            List<PointPairList> pointPairLists = new List<PointPairList>();
            for (int i = 0; i < 6; i++ )
                pointPairLists.Add(new PointPairList());

            MainGateClient mainGate = new MainGateClient(new InstanceContext(new TrendsListener(zgMain, pointPairLists)));
            mainGate.Subscribe();
        }

        public void ShowFusion(Fusion fusion)
        {
            lbH2Value.Text = "";
            lbO2Value.Text = "";
            lbCOValue.Text = "";
            lbCO2Value.Text = "";
            lbN2Value.Text = "";
            lbArValue.Text = "";
            zgMain.GraphPane.CurveList.Clear();
            PointPairList pointsH2 = new PointPairList();
            PointPairList pointsO2 = new PointPairList();
            PointPairList pointsCO = new PointPairList();
            PointPairList pointsCO2 = new PointPairList();
            PointPairList pointsN2 = new PointPairList();
            PointPairList pointsAr = new PointPairList();
            foreach (TrendPoint tp in fusion.Points)
            {
                pointsH2.Add(tp.Time.TotalSeconds, tp.H2);
                pointsO2.Add(tp.Time.TotalSeconds, tp.O2);
                pointsCO.Add(tp.Time.TotalSeconds, tp.CO);
                pointsCO2.Add(tp.Time.TotalSeconds, tp.CO2);
                pointsN2.Add(tp.Time.TotalSeconds, tp.N2);
                pointsAr.Add(tp.Time.TotalSeconds, tp.Ar);
            }
            if (cbH2.Checked)
            {
                zgMain.GraphPane.AddCurve("H2", pointsH2, Color.Green, SymbolType.None);
            }
            if (cbO2.Checked)
            {
                zgMain.GraphPane.AddCurve("O2", pointsO2, Color.Blue, SymbolType.None);
            }
            if (cbCO.Checked)
            {
                zgMain.GraphPane.AddCurve("CO", pointsCO, Color.Red, SymbolType.None);
            }
            if (cbCO2.Checked)
            {
                zgMain.GraphPane.AddCurve("CO2", pointsCO2, Color.Orange, SymbolType.None);
            }
            if (cbN2.Checked)
            {
                zgMain.GraphPane.AddCurve("N2", pointsN2, Color.Black, SymbolType.None);
            }
            if (cbAr.Checked)
            {
                zgMain.GraphPane.AddCurve("Ar", pointsAr, Color.Turquoise, SymbolType.None);
            }
            zgMain.GraphPane.Title.FontSpec.Size = 6;
            zgMain.GraphPane.Title.Text = string.Format("Плавка: {0}  Марка: {1}  Начало: {2}({3})  Бригада: {4}  Конвертер:{5}  Т зад={6}  Т факт={7}  С зад={8}  С факт={9}",
                                                         fusion.Number, fusion.Grade, fusion.StartDate, fusion.StartDateDB,
                                                         fusion.TeamNumber, fusion.ConverterNumber, fusion.PlannedTempereture,
                                                         fusion.FactTemperature, fusion.PlannedC, fusion.FactC);

            // Отображаем график для введенных формул
            if (gbVars.Controls.Count > 0)
            {
                MathParser mathParser = new MathParser();
                int varsCount = gbVars.Controls.Count / 3;
                for (int i = 0; i < varsCount; i++)
                {
                    PointPairList points = new PointPairList();
                    foreach (TrendPoint tp in fusion.Points)
                    {
                        mathParser.CreateVar("H2", tp.H2, null);
                        mathParser.CreateVar("O2", tp.O2, null);
                        mathParser.CreateVar("CO", tp.CO, null);
                        mathParser.CreateVar("CO2", tp.CO2, null);
                        mathParser.CreateVar("N2", tp.N2, null);
                        mathParser.CreateVar("Ar", tp.Ar, null);
                        mathParser.Expression = (gbVars.Controls.Find(string.Format("tb{0}", i), true)[0] as System.Windows.Forms.TextBox).Text;
                        try
                        {
                            points.Add(tp.Time.TotalSeconds, mathParser.ValueAsDouble);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(string.Format("Переменная {0} задана неверно, ошибка в формуле", (gbVars.Controls.Find(string.Format("lb{0}", i), true)[0] as System.Windows.Forms.Label).Text));
                            return;
                        }
                    }
                    mathParser.CreateVar((gbVars.Controls.Find(string.Format("lb{0}", i), true)[0] as System.Windows.Forms.Label).Text,
                                          mathParser.ValueAsString,
                                          null);
                    if ((gbVars.Controls.Find(string.Format("cb{0}", i), true)[0] as System.Windows.Forms.CheckBox).Checked)
                    {
                        zgMain.GraphPane.AddCurve((gbVars.Controls.Find(string.Format("lb{0}", i), true)[0] as System.Windows.Forms.Label).Text, points, Color.DeepPink, SymbolType.None);
                    }
                }
            }
            zgMain.GraphPane.Legend.IsVisible = false;
            zgMain.AxisChange();
            zgMain.Invalidate();
            zgMain.Visible = true;
            pAddVar.Visible = true;
            pVars.Visible = true;
            gbValues.Visible = true;
        }

        string XAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            return (string.Format("{0}:{1}", (int)(val / 60), (val % 60).ToString("00")));
        }

        private bool zgMain_MouseMoveEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zgMain.GraphPane.ReverseTransform(e.Location, out x, out y);
            if (zgMain.GraphPane.CurveList.Count > 0)
            {
                for (int curveCount = 0; curveCount < zgMain.GraphPane.CurveList.Count; curveCount++)
                {
                    for (int i = 0; i < zgMain.GraphPane.CurveList[curveCount].Points.Count; i++)
                    {
                        if (zgMain.GraphPane.CurveList[curveCount].Points[i].X == (int)x)
                        {
                            double time = zgMain.GraphPane.CurveList[curveCount].Points[i].X;
                            lbTimeValue.Text = string.Format("{0}:{1}", (int)(time / 60), (time % 60).ToString("00"));
                            switch (zgMain.GraphPane.CurveList[curveCount].Label.Text)
                            {
                                case "H2":
                                    lbH2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "O2":
                                    lbO2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "CO":
                                    lbCOValue.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "CO2":
                                    lbCO2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "N2":
                                    lbN2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "Ar":
                                    lbArValue.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                            }
                        }
                    }
                }
            }
            return default(bool);
        }

        private void cbH2_CheckedChanged(object sender, EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
            lbH2Value.Text = "";
        }

        private void cbO2_CheckedChanged(object sender, EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
            lbO2Value.Text = "";
        }

        private void cbCO_CheckedChanged(object sender, EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
            lbCOValue.Text = "";
        }

        private void cbCO2_CheckedChanged(object sender, EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
            lbCO2Value.Text = "";
        }

        private void cbN2_CheckedChanged(object sender, EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
            lbN2Value.Text = "";
        }

        private void cbAr_CheckedChanged(object sender, EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
            lbArValue.Text = "";
        }

        private string zgMain_CursorValueEvent(ZedGraphControl sender, GraphPane pane, Point mousePt)
        {
            return default(string);
        }

        private string zgMain_PointValueEvent(ZedGraphControl sender, GraphPane pane, CurveItem curve, int iPt)
        {
            return default(string);
        }

        private void zgMain_Click(object sender, EventArgs e)
        {
            double x, y;
            zgMain.GraphPane.ReverseTransform((e as MouseEventArgs).Location, out x, out y);
            if (zgMain.GraphPane.CurveList.Count > 0)
            {
                for (int curveCount = 0; curveCount < zgMain.GraphPane.CurveList.Count; curveCount++)
                {
                    for (int i = 0; i < zgMain.GraphPane.CurveList[curveCount].Points.Count; i++)
                    {
                        if (zgMain.GraphPane.CurveList[curveCount].Points[i].X == (int)x)
                        {
                            switch (zgMain.GraphPane.CurveList[curveCount].Label.Text)
                            {
                                case "H2":
                                    lbH2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "O2":
                                    lbO2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "CO":
                                    lbCOValue.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "CO2":
                                    lbCO2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "N2":
                                    lbN2Value.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                                case "Ar":
                                    lbArValue.Text = zgMain.GraphPane.CurveList[curveCount].Points[i].Y.ToString();
                                    break;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void var_Click(Object sender, System.EventArgs e)
        {
            string prefix;
            if (sender is TextBox)
            {
                prefix = "tb";
            }
            else
            {
                prefix = "lb";
            }
            Control control = (sender as Control);
            tbVarName.Text = (control.Parent.Controls.Find("lb" + control.Name.Replace(prefix, ""), true)[0] as System.Windows.Forms.Label).Text;
            tbVarExpression.Text = (control.Parent.Controls.Find("tb" + control.Name.Replace(prefix, ""), true)[0] as TextBox).Text;
        }

        private void varCheckBox_Click(Object sender, System.EventArgs e)
        {
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
        }


        private void btAddVar_Click(object sender, EventArgs e)
        {
            foreach (Control control in gbVars.Controls)
            {
                if (control is System.Windows.Forms.Label)
                {
                    if (control.Text == tbVarName.Text)
                    {
                        (control as System.Windows.Forms.Label).Text = tbVarName.Text;
                        (control.Parent.Controls.Find("tb" + control.Name.Replace("lb", ""), true)[0] as TextBox).Text = tbVarExpression.Text;
                        ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
                        return;
                    }
                }
            }

            currentVarsPosition += 25;

            CheckBox checkBox = new CheckBox();
            checkBox.Left = 5;
            checkBox.Top = currentVarsPosition;
            checkBox.Name = string.Format("cb{0}", currentVarsIndex);
            checkBox.Checked = false;
            checkBox.Width = 15;
            checkBox.Click += new System.EventHandler(varCheckBox_Click);

            gbVars.Controls.Add(checkBox);

            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            label.Text = tbVarName.Text;
            label.Top = currentVarsPosition;
            label.Width = 50;
            label.Left = 20;
            label.TextAlign = ContentAlignment.MiddleRight;
            label.Name = string.Format("lb{0}", currentVarsIndex);
            label.Click += new System.EventHandler(var_Click);
            gbVars.Controls.Add(label);

            TextBox textBox = new TextBox();
            textBox.Name = string.Format("tb{0}", currentVarsIndex);
            textBox.Click += new System.EventHandler(var_Click);
            textBox.Top = currentVarsPosition;
            textBox.Left = label.Left + label.Width + 5;
            textBox.Text = tbVarExpression.Text;
            textBox.Width = 125;
            textBox.Enabled = false;
            gbVars.Controls.Add(textBox);
            currentVarsIndex++;
            tbVarExpression.Text = "";
            tbVarName.Text = "";
            ShowFusion(trendFusion.Fusions.ElementAt<Fusion>(currentFusionIndex));
        }

        private void zgMain_Load(object sender, EventArgs e)
        {

        }
    }
}
