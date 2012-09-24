using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Converter.Trends;
using System.IO;
using ZedGraph;

namespace DataGathering
{
    public partial class Form1 : Form
    {
        ConverterDBLayer m_Db;
        Fusion m_CurrentFussion;
        private string m_Path;
        int Zoom = 20;

        public string Path
        {
            get { return m_Path; }
            set { m_Path = value; }
        }
        List<Fusion> m_Fusions;

        public List<Fusion> Fusions
        {
            get { return m_Fusions; }
            set { m_Fusions = value; }
        }
        public Fusion CurrentFussion
        {
            get { return m_CurrentFussion; }
            set { m_CurrentFussion = value; }
        }
        private BindingSource m_BSFusion;

        public BindingSource BSFusion
        {
            get { return m_BSFusion; }
            set { m_BSFusion = value; }
        }

        private BindingSource m_BSOffGas;

        public BindingSource BSOffGas
        {
            get { return m_BSOffGas; }
            set { m_BSOffGas = value; }
        }

        private BindingSource m_BSLance;

        public BindingSource BSLance
        {
            get { return m_BSLance; }
            set { m_BSLance = value; }
        }

        private BindingSource m_BSAddition;

        public BindingSource BSAddition
        {
            get { return m_BSAddition; }
            set { m_BSAddition = value; }
        }
        private BindingNavigator m_BNFusion;

        public BindingNavigator BNFusion
        {
            get { return m_BNFusion; }
            set { m_BNFusion = value; }
        }

        private BindingNavigator m_BNOffGas;

        public BindingNavigator BNOffGas
        {
            get { return m_BNOffGas; }
            set { m_BNOffGas = value; }
        }

        private void InitializeBindings()
        {
            BSFusion = new BindingSource();
            BSFusion.DataSource = Fusions;
            BSFusion.AllowNew = true;

            BSOffGas = new BindingSource();
            BSOffGas.AllowNew = true;

            BSLance = new BindingSource();
            BSAddition = new BindingSource();
            /*
            // BindingNavigator
            BNFusion = new BindingNavigator(BSFusion);
            BNFusion.AutoSize = true;
            BNFusion.Top = 180;
            BNFusion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.Controls.Add(BNFusion);
             * */
            // Binding


            //dataGridViewFusion.DataBindings.Add("Value",BSFusion,"Id");
            /*
             OSName.DataBindings.Add("Text", bs, "Name");
            tbOSPopularity.DataBindings.Add("Text", bs, "Popularity");
            dpOSReleaseDate.DataBindings.Add("Value", bs, "ReleaseDate");
            tbOSReleaseYear.DataBindings.Add("Text", bs, "ReleaseYear");
            tbOSReleaseMonth.DataBindings.Add("Text", bs, "ReleaseMonth");
            tbOSReleaseDay.DataBindings.Add("Text", bs, "ReleaseDay");
             */
        }

        public Form1()
        {
            InitializeComponent();
            m_Db = new ConverterDBLayer();
            InitializeBindings();
            textBox1.Text = @"D:";
            comboBoxMark.Items.AddRange((m_Db.GetMarks((int)numericUpDownConverterNumber.Value,dateTimePickerBegin.Value,dateTimePickerEnd.Value)).ToArray());
            dateTimePickerBegin.Value = DateTime.Now.Date; //DateTime.Now.abso
            dateTimePickerEnd.Value = DateTime.Today.AddSeconds(86400-1);
        }
        private Converter.Trends.TrendsFusion m_TrendsFusion;


        private void открытьФайлToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_TrendsFusion = new Converter.Trends.TrendsFusion(openDialog.FileName);
                listBox1.Items.Add(DateTime.Now.ToString() + ": Файл загружен. Кол-во плавок:" + m_TrendsFusion.Fusions.Count);
                buttonConvert_Click(null, null);
                button1_Click(null, null);
                открытьФайлToolStripMenuItem1_Click(null, null);
                //
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            foreach (Fusion fusion in m_TrendsFusion.Fusions)
            {
                FindFusionMath(fusion);
            }
        }

        private void FindFusionMath(Fusion fusion)
        {
            m_Db.FillFusionData(fusion);
            listBox1.Items.Add(DateTime.Now.ToString() + ": Плавка №" + fusion.Number.ToString() + "(" + fusion.Id.ToString() + ") загружена.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveToNewFile(m_TrendsFusion);
        }

        private void SaveToNewFile(TrendsFusion trendsFusion)
        {
            trendsFusion.Save();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void открытьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //MessageBox.Show(fb.SelectedPath);
                foreach (string file in Directory.GetFiles(fb.SelectedPath, "*.pld"))
                {

                    m_TrendsFusion = new TrendsFusion(file);
                    if (!new FileInfo(m_TrendsFusion.GetFileNameNew()).Exists)
                    {
                        buttonConvert_Click(null, null);
                        button1_Click(null, null);
                    }
                }
            }

        }
        void comboBoxFusions_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentFussion = Fusions[comboBoxFusions.SelectedIndex];
            BSFusion.DataSource = CurrentFussion;

            dataGridViewFusion.DataSource = BSFusion;
            dataGridViewFusion.Columns["EndDate"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";
            dataGridViewFusion.Columns["StartDateDB"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";

            BSOffGas.DataSource = m_Db.GetOffGases(CurrentFussion.Id);
            dataGridViewOffGas.DataSource = BSOffGas;
            dataGridViewOffGas.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";

            BSLance.DataSource = m_Db.GetLance(CurrentFussion.Id);
            dataGridViewLance.DataSource = BSLance;
            dataGridViewLance.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";

            BSAddition.DataSource = m_Db.GetAdditions(CurrentFussion.Id);
            dataGridViewTrakt.DataSource = BSAddition;
            dataGridViewTrakt.Columns["Date"].DefaultCellStyle.Format = "dd.MM.yy hh:mm:ss";

            BindingSource BSBathLevel = new BindingSource();
            BSBathLevel.DataSource = m_Db.GetBathLevel(CurrentFussion.Id);
            dataGridViewMetalMirror.DataSource = BSBathLevel;

            BindingSource BSScrapBuckets = new BindingSource();
            BSScrapBuckets.DataSource = m_Db.GetScrapBuckets(CurrentFussion.Id);
            dataGridViewScrapBuckets.DataSource = BSScrapBuckets;

            BindingSource BSHotMetal = new BindingSource();
            BSHotMetal.DataSource = m_Db.GetHotMetal(CurrentFussion.Id);
            dataGridViewHotMetal.DataSource = null;
            dataGridViewHotMetal.DataSource = BSHotMetal;
            dataGridViewHotMetal.Columns["Temperature"].HeaderText = "Температура";
            dataGridViewHotMetal.Columns["Weight"].HeaderText = "Вес";
            dataGridViewHotMetal.Update();

            UpdateGraph();
        }

        private void UpdateGraph()
        {
            zGraph.GraphPane.CurveList.Clear();
            PointPairList pointsH2 = new PointPairList();
            PointPairList pointsO2 = new PointPairList();
            PointPairList pointsCO = new PointPairList();
            PointPairList pointsCO2 = new PointPairList();
            PointPairList pointsN2 = new PointPairList();
            PointPairList pointsAr = new PointPairList();

            CurrentFussion = Fusions[comboBoxFusions.SelectedIndex];

            double time;
            foreach (OffGas og in m_Db.GetOffGases(CurrentFussion.Id))
            {
                time = (og.Date - CurrentFussion.StartDateDB).TotalSeconds;
                pointsH2.Add(time, og.H2 * Zoom);
                pointsO2.Add(time, og.O2 * Zoom);
                pointsCO.Add(time, og.CO * Zoom);
                pointsCO2.Add(time, og.CO2 * Zoom);
                pointsN2.Add(time, og.N2 * Zoom);
                pointsAr.Add(time, og.Ar * Zoom);
            }

            if (cbH2.Checked)
            {
                zGraph.GraphPane.AddCurve("H2", pointsH2, Color.Green, SymbolType.None);
            }
            if (cbO2.Checked)
            {
                zGraph.GraphPane.AddCurve("O2", pointsO2, Color.Blue, SymbolType.None);
            }
            if (cbCO.Checked)
            {
                zGraph.GraphPane.AddCurve("CO", pointsCO, Color.Red, SymbolType.None);
            }
            if (cbCO2.Checked)
            {
                zGraph.GraphPane.AddCurve("CO2", pointsCO2, Color.Orange, SymbolType.None);
            }
            if (cbN2.Checked)
            {
                zGraph.GraphPane.AddCurve("N2", pointsN2, Color.Black, SymbolType.None);
            }
            if (cbAr.Checked)
            {
                zGraph.GraphPane.AddCurve("Ar", pointsAr, Color.Turquoise, SymbolType.None);
            }

            PointPairList pointsLance = new PointPairList();
            PointPairList pointsOFlow = new PointPairList();

            List<Lance> lanceHeights = m_Db.GetLance(CurrentFussion.Id);
            int lanceHeight = lanceHeights.First().Height;

            for (double second = (lanceHeights.First().Date - CurrentFussion.StartDateDB).TotalSeconds; second < (lanceHeights.Last().Date - CurrentFussion.StartDateDB).TotalSeconds; second++)
            {
                foreach (Lance lance in lanceHeights)
                {
                    if (second > 0)
                    {
                        if ((lance.Date - CurrentFussion.StartDateDB).TotalSeconds == second)
                        {
                            pointsLance.Add(second, lance.Height);
                            pointsOFlow.Add(second, lance.O2Flow);
                            lanceHeight = lance.Height;
                            continue;
                        }
                        pointsLance.Add(second, lanceHeight);
                    }
                }
            }

            if (cbLance.Checked)
            {
                zGraph.GraphPane.AddCurve("Фурма", pointsLance, Color.Lime, SymbolType.None);
            }
            if (cbOFlow.Checked)
            {
                zGraph.GraphPane.AddCurve("OFlow", pointsOFlow, Color.Magenta, SymbolType.None);
            }

            PointPairList pointsAdditions = new PointPairList();


            /*foreach (Addition addition in m_Db.GetAdditions(CurrentFussion.Id))
            {
                time = (addition.Date - CurrentFussion.StartDateDB).TotalSeconds;
                pointsAdditions.Add(time, 1000);
                pointsAdditions.Add(time + 5, 1005);
                pointsAdditions.Add(time, 1005);
                pointsAdditions.Add(time + 5, 1000);
                zGraph.GraphPane.AddStick(addition.MaterialName, pointsAdditions, Color.Black);
                pointsAdditions.Clear();
            }*/


            zGraph.GraphPane.Title.FontSpec.Size = 6;
            zGraph.GraphPane.Title.Text = string.Format("Плавка: {0}  Марка: {1}  Начало: {2}({3})  Бригада: {4}  Конвертер:{5}  Т зад={6}  Т факт={7}  С зад={8}  С факт={9}",
                                                     CurrentFussion.Number, CurrentFussion.Grade, CurrentFussion.StartDate, CurrentFussion.StartDateDB,
                                                     CurrentFussion.TeamNumber, CurrentFussion.ConverterNumber, CurrentFussion.PlannedTempereture,
                                                     CurrentFussion.FactTemperature, CurrentFussion.PlannedC, CurrentFussion.FactC);
            zGraph.AxisChange();
            zGraph.Invalidate();
        }

        private void numericUpDownConverterNumber_ValueChanged(object sender, EventArgs e)
        {
            buttonGet_Click(null, null);
            comboBoxMark.Items.Clear();
            comboBoxMark.Text = "";
            comboBoxMark.Items.AddRange(m_Db.GetMarks((int)numericUpDownConverterNumber.Value,dateTimePickerBegin.Value, dateTimePickerEnd.Value).ToArray());
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            comboBoxFusions.Items.Clear();
            comboBoxFusions.Text = "";
            Fusions = m_Db.GetFusionsData(dateTimePickerBegin.Value, dateTimePickerEnd.Value, (int)numericUpDownConverterNumber.Value, !checkBoxMark.Checked || comboBoxMark.SelectedItem == null ? 0 : ((MarkSteel)comboBoxMark.SelectedItem).ID);
            comboBoxFusions.Items.AddRange(Fusions.Select(p => p.Number.ToString()).ToArray());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Fusion fusion in Fusions)
            {
                comboBoxFusions.SelectedIndex = Fusions.IndexOf(fusion);
                ExcelExport exc = new ExcelExport(CurrentFussion.Number.ToString());
                exc.Do(dataGridViewScrapBuckets, "Шихта");
                exc.Do(dataGridViewMetalMirror, "Зеркало металла");
                exc.Do(dataGridViewOffGas, "Выходящие газы");
                exc.Do(dataGridViewLance, "Фурма");
                exc.Do(dataGridViewTrakt, "Тракт сыпучих");
                exc.Do(dataGridViewFusion, "Плавка");
                exc.Save(textBox1.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = fb.SelectedPath;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            zGraph.IsEnableHZoom = false;
            zGraph.IsEnableVZoom = false;
            zGraph.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zGraph.GraphPane.XAxis.Title.IsVisible = false;
            zGraph.GraphPane.YAxis.Title.IsVisible = false;
            zGraph.GraphPane.Title.FontSpec.Size = 12;
            zGraph.GraphPane.XAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.YAxis.Scale.FontSpec.Size = 10;
            zGraph.GraphPane.XAxis.Scale.MajorStep = 300;
            zGraph.GraphPane.XAxis.Scale.MinorStep = 60;
            zGraph.GraphPane.Legend.IsVisible = false;
            zGraph.GraphPane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(XAxis_ScaleFormatEvent);
        }

        string XAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            return (string.Format("{0}:{1}", (int)(val / 60), (val % 60).ToString("00")));
        }

        private void cbH2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbO2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbCO_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbCO2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbN2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbAr_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private bool zGraph_MouseMoveEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            zGraph.GraphPane.ReverseTransform(e.Location, out x, out y);
            if (zGraph.GraphPane.CurveList.Count > 0)
            {
                for (int curveCount = 0; curveCount < zGraph.GraphPane.CurveList.Count; curveCount++)
                {
                    for (int i = 0; i < zGraph.GraphPane.CurveList[curveCount].Points.Count; i++)
                    {
                        if (zGraph.GraphPane.CurveList[curveCount].Points[i].X == (int)x)
                        {
                            double time = zGraph.GraphPane.CurveList[curveCount].Points[i].X;
                            lbTimeValue.Text = string.Format("{0}:{1}", (int)(time / 60), (time % 60).ToString("00"));
                            switch (zGraph.GraphPane.CurveList[curveCount].Label.Text)
                            {
                                case "H2":
                                    lbH2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "O2":
                                    lbO2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "CO":
                                    lbCOValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "CO2":
                                    lbCO2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "N2":
                                    lbN2Value.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "Ar":
                                    lbArValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y / Zoom).ToString();
                                    break;
                                case "Фурма":
                                    lbLanceValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y).ToString();
                                    break;
                                case "OFlow":
                                    lbOFlowValue.Text = (zGraph.GraphPane.CurveList[curveCount].Points[i].Y).ToString();
                                    break;
                            }
                        }
                    }
                }
            }
            return default(bool);

        }

        private void cbLance_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void cbOFlow_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void checkBoxMark_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxMark.Enabled = checkBoxMark.Checked;
        }

        private void dateTimePickerBegin_ValueChanged(object sender, EventArgs e)
        {
            comboBoxMark.Items.Clear();
            comboBoxMark.Text = "";
            comboBoxMark.Items.AddRange(m_Db.GetMarks((int)numericUpDownConverterNumber.Value, dateTimePickerBegin.Value, dateTimePickerEnd.Value).ToArray());
        }

        


    }
}
