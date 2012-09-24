using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using HeatCharge;
using Implements;
using Kurkin.Properties;

namespace Kurkin
{
   public partial class KurkinForm : Form
    {
        private Timer m_gasEmulTimer;
        private bool TimerStart = false;
        private string m_startStopBtnName;
        private double m_remainCarbon;
        private double TotalCarbonMass;
        private int m_secondsElapsed;
        private int DeltaTimeSec = 5;
        //private List<MFCMData> matrixStateData = new List<MFCMData>();
        //private const int dataLenght = 30; //размер выборки данных выборка данных
        private bool m_dataLoaded = false;
        private bool m_matrixDataCorrect = false;


        public KurkinForm()
        {
            InitializeComponent();
            pBarCarbonPercent.Maximum = 10000;
            pBarCarbonPercent.Minimum = 0;
            //pBarCarbonPercent.Style = Orientation.Vertical;
        }

        private void btnCalcC_Click(object sender, EventArgs e)
        {


            if (BacklightCarbon())
            {
                double ironMass = Convertion.StrToDouble(txbIronMass.Text);
                double ironCarbonPercent = Convertion.StrToDouble(txbIronCarbon.Text);
                double scrapMass = Convertion.StrToDouble(txbScrapMass.Text);
                double scrapCarbonPercent = Convertion.StrToDouble(txbScrapCarbon.Text);
                double steelCarbonPercent = Convertion.StrToDouble(txbSteelCarbon.Text);

                TotalCarbonMass = Decarbonater.HeatCarbonMass(ironMass, ironCarbonPercent, scrapMass, scrapCarbonPercent,
                                                              steelCarbonPercent);

                txbHeatCarbonMass.Text = TotalCarbonMass.ToString();
                double CarbonPercent = GetCarbonPercent(
                        TotalCarbonMass,
                        Convertion.StrToDouble(txbIronMass.Text),
                        Convertion.StrToDouble(txbIronCarbon.Text),
                        Convertion.StrToDouble(txbScrapMass.Text),
                        Convertion.StrToDouble(txbScrapCarbon.Text)
                        );
                txbCarbonPercent.Text = CarbonPercent.ToString();
                pBarCarbonPercent.Value = (int)(CarbonPercent * 100);
            }
            
        }
        private void  _UpdateLabel(int secondsElapsed)
        {
            string[] res = lblTime.Text.Split(new [] {'='}, 2);
            lblTime.Text = res[0] + "=" + Convert.ToString(secondsElapsed);
        }

        private void btnCGas_Click(object sender, EventArgs e)
        {
 
            
            if (!TimerStart)
            {


                if (BacklightGasCarbon())
                {
                    m_remainCarbon = Convertion.StrToDouble(txbHeatCarbonMass.Text);
                    pBarGasanCarbonMass.Minimum = 0;
                    if (m_remainCarbon >= 0)
                    {
                        pBarGasanCarbonMass.Maximum = (int) m_remainCarbon;
                    }
                    else
                    {
                        pBarGasanCarbonMass.Maximum = 0;
                    }
                    m_gasEmulTimer = new Timer();
                    m_gasEmulTimer.Tick += new EventHandler(GasTimerEvent);
                    m_gasEmulTimer.Interval = DeltaTimeSec * 200;
                    m_gasEmulTimer.Start();
                    TimerStart = true;
                    m_secondsElapsed = 0;
                    _UpdateLabel(m_secondsElapsed);
                    txbGasanCarbonMass.Text = txbHeatCarbonMass.Text;
                    m_startStopBtnName = btnCGas.Text;
                    btnCGas.Text = Resources.KurkinForm_btnCGas_Click_StopCalcCarbon;
                }
            }
            else
            {
                m_gasEmulTimer.Stop();
                TimerStart = false;
                btnCGas.Text = m_startStopBtnName;
            }

        }

        private void btnCalcMultiFactor_Click(object sender, EventArgs e)
        {
            if (m_matrixDataCorrect && BacklightMatrixinputs())
            {
                List<MFCMData> matrixStateData = new List<MFCMData>();
                var currentStateData = new MFCMData();

                for (int row = 0; row < dGMatrixState.RowCount; row++)
                {
                    matrixStateData.Add(new MFCMData());
                    
                    matrixStateData[row].CarbonMonoxideVolumePercent =
                        Convertion.StrToDouble(dGMatrixState.Rows[row].Cells[2].Value.ToString());
                    
                    matrixStateData[row].HeightLanceCentimeters =
                        Convertion.StrToInt32(dGMatrixState.Rows[row].Cells[3].Value.ToString());
                    
                    matrixStateData[row].OxygenVolumeRate =
                        Convertion.StrToDouble(dGMatrixState.Rows[row].Cells[4].Value.ToString());
                    
                    matrixStateData[row].SteelCarbonPercent =
                        Convertion.StrToDouble(dGMatrixState.Rows[row].Cells[5].Value.ToString());
                }
                currentStateData.CarbonMonoxideVolumePercent = Convertion.StrToDouble(txbMatrixCO.Text);
                currentStateData.HeightLanceCentimeters = Convertion.StrToInt32(txbMatrixLance.Text);
                currentStateData.OxygenVolumeRate = Convertion.StrToDouble(txbMatrixOxigenVolumeRate.Text);
                lblMatrixCarbone.Text = Decarbonater.MultiFactorCarbonMass(matrixStateData, currentStateData).ToString().Substring(0,8);
            }
        }

        private void btnMatrixLoad_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            fd.Title = Resources.KurkinForm_btnMatrixLoad_Click_Load_matrix_state;
            fd.Filter = "All files (*.csv)|*.csv|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(fd.FileName);
                string[] strings;
                try
                {
                    strings = File.ReadAllLines(fd.FileName);
                }
                catch
                {
                    strings = new string[0];
                    MessageBox.Show("Cannot read the file: {0}", fd.FileName);
                    return;
                }
                dGMatrixState.RowCount = strings.Count();
                for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                {
                    string[] values = strings[strCnt].Split(':');
                    for (int collumnCnt = 0; collumnCnt < Math.Min(values.Count(), dGMatrixState.ColumnCount); collumnCnt++)
                    {
                        dGMatrixState.Rows[strCnt].Cells[collumnCnt].Value = values[collumnCnt];
                    }
                    
                }
                
            }
        }

        private void dGMatrixState_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Math.Min(dGMatrixState.ColumnCount, dGMatrixState.RowCount) > 0)
            {
                var color = new Color();
                m_matrixDataCorrect = true;


                if (dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if ((e.ColumnIndex == 0) || (e.ColumnIndex == 1) || (e.ColumnIndex == 3))
                    {
                        if (
                        !Checker.isIntCorrect(dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                                             out color))
                        {
                            m_matrixDataCorrect = false;
                        }
                        dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = color;
                    }
                    else if ((e.ColumnIndex == 2) || (e.ColumnIndex == 4) || (e.ColumnIndex == 5) || (e.ColumnIndex == 6))
                    {
                        if (!Checker.isDoubleCorrect(dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out color))
                        {
                            m_matrixDataCorrect = false;
                        }
                        dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = color;
                    }
                    else
                    {
                        dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Gray;
                    }

                    
                }
                else
                {
                    Checker.isIntCorrect("", out color);
                    dGMatrixState.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = color;
                }
            }
            
        }

        private void GasTimerEvent(Object myObject, EventArgs myEventArgs)
        {
            
            

            if (BacklightGasCarbon())
            {
                double carbonMonoxideVolumePercent = Convertion.StrToDouble(txbGasCO.Text);
                double offgasVolumeRate = Convertion.StrToDouble(txbGasQ.Text);
                double deltaT = DeltaTimeSec;

                double kgasan = Convertion.StrToDouble(btnKGasan.Text);

                m_remainCarbon -= Decarbonater.GasanCarbonMass(carbonMonoxideVolumePercent, offgasVolumeRate, deltaT,
                                                               kgasan);
                m_secondsElapsed += Convert.ToInt32(deltaT);
                _UpdateLabel(m_secondsElapsed);

                txbGasanCarbonMass.Text = m_remainCarbon.ToString();
                //GetCarbonPercent
                if (BacklightCarbon())
                {
                    double CarbonPercent = GetCarbonPercent(
                        m_remainCarbon,
                        Convertion.StrToDouble(txbIronMass.Text),
                        Convertion.StrToDouble(txbIronCarbon.Text),
                        Convertion.StrToDouble(txbScrapMass.Text),
                        Convertion.StrToDouble(txbScrapCarbon.Text)
                        );
                    txbCarbonPercent.Text = CarbonPercent.ToString();
                    pBarCarbonPercent.Value = (int)(CarbonPercent * 100);
                }
                if (m_remainCarbon > 0)
                {
                    pBarGasanCarbonMass.Value = (int)m_remainCarbon;
                }
                else
                {
                    pBarGasanCarbonMass.Value = 0;
                }

                //if (m_remainCarbon != 0) ///////////////////////////
                //{
                    //txbGasanCarbonMass.Text = (m_remainCarbon/TotalCarbonMass).ToString();
                    //double qq = m_remainCarbon/TotalCarbonMass;
                    //InstantLogger.log(qq.ToString());
                    if ((m_remainCarbon / TotalCarbonMass) <= 0.1)
                    {
                        tabSimple.SelectedIndex = 1;
                        m_gasEmulTimer.Stop();
                        return;
                    }
                }

            //}

        }

        private bool DoubleDataFormatCorrect(string str)
        {
            double dValue = 0.0;
            return System.Double.TryParse(str, out dValue);
        }
        private bool IntDataFormatCorrect(string str)
        {
            int iValue = 0;
            return System.Int32.TryParse(str, out iValue);
        }
        private bool BacklightMatrixinputs()
        {
            bool dataCorrect = true;
            Color color = new Color();

            if (!Checker.isDoubleCorrect(txbMatrixCO.Text, out color))
            {
                dataCorrect = false;
            }
            txbMatrixCO.BackColor = color;

            if (!Checker.isIntCorrect(txbMatrixLance.Text, out color))
            {
                dataCorrect = false;
            }
            txbMatrixLance.BackColor = color;

            if (!Checker.isDoubleCorrect(txbMatrixOxigenVolumeRate.Text, out color))
            {
                dataCorrect = false;
            }
            txbMatrixOxigenVolumeRate.BackColor = color;

            return dataCorrect;
        }

        private bool BacklightCarbon()
        {
            bool dataCorrect = true;
            Color color = new Color();
           
            if (!Checker.isDoubleCorrect(txbIronMass.Text, out color, new dMargin(1000, 5e+6)))
            {
                dataCorrect = false;
            }
            txbIronMass.BackColor = color;


            if (!Checker.isDoubleCorrect(txbIronCarbon.Text, out color))
            {
                dataCorrect = false;
            }
            txbIronCarbon.BackColor = color;


            if (!Checker.isDoubleCorrect(txbScrapMass.Text, out color))
            {
                dataCorrect = false;
            }
            txbScrapMass.BackColor = color;

            if (!Checker.isDoubleCorrect(txbScrapCarbon.Text, out color))
            {
                dataCorrect = false;
            }
            txbScrapCarbon.BackColor = color;

            if (!Checker.isDoubleCorrect(txbSteelCarbon.Text, out color))
            {
                dataCorrect = false;
            }
            txbSteelCarbon.BackColor = color;

            return dataCorrect;
        }

        private bool BacklightGasCarbon()
        {
            bool dataCorrect = true;
            Color color = new Color();

            if (!Checker.isDoubleCorrect(txbGasCO.Text, out color))
            {
                dataCorrect = false;
            }
            txbGasCO.BackColor = color;

            if (!Checker.isDoubleCorrect(txbGasQ.Text, out color))
            {
                dataCorrect = false;
            }
            txbGasQ.BackColor = color;

            if (!Checker.isDoubleCorrect(txbHeatCarbonMass.Text, out color))
            {
                dataCorrect = false;
            }
            txbHeatCarbonMass.BackColor = color;

            if (!Checker.isDoubleCorrect(btnKGasan.Text, out color))
            {
                dataCorrect = false;
            }
            btnKGasan.BackColor = color;
            

            return dataCorrect;
        }
        
        private Color InvertColor(Color color)
        {
            return Color.FromArgb(255 - color.A, 255 - color.R, 255 - color.G, 255 - color.B);
        }

        static private double GetCarbonPercent(
            double carbonMass,
            double ironMass,
            double ironCarbonPercent,
            double scrapMass,
            double scrapCarbonPercent
            )
        {
            double ferumMass = (ironMass - (ironMass * ironCarbonPercent * 0.01)) +
                           (scrapMass - (scrapMass * scrapCarbonPercent * 0.01));
            if (ferumMass > 0.0)
            {
                return carbonMass / ferumMass * 100;
            }
            else
            {
                return -1.0;
            }
        }
    }
}
