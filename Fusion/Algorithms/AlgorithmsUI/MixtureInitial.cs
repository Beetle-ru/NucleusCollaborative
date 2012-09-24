using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HeatCharge;
using Implements;

namespace AlgorithmsUI
{
    public partial class MixtureInitial : Form
    {
        private void LogStr(String str)
        {
            rtbReport.Text += String.Format("{0}\n", str);
            rtbReport.Select(rtbReport.TextLength, 0);
            rtbReport.ScrollToCaret();
        }
        private void LogClear()
        {
            rtbReport.Clear();
        }
        private ChemTable ch_Iron, ch_Scrap, ch_CaCO3, ch_Fom, ch_LimeStone, ch_Lime, ch_Coke, ch_Dust;
        private void GetValueByKey(string Key, TextBox Box)
        {
            if (mainConf.AppSettings.Settings.AllKeys.Contains(Key))
            {
                Box.Text = mainConf.AppSettings.Settings[Key].Value;
            }
        }
        private double SetDoubleByKey(string Key, TextBox Box)
        {
            if (mainConf.AppSettings.Settings.AllKeys.Contains(Key))
            {
                mainConf.AppSettings.Settings.Remove(Key);
            }
            mainConf.AppSettings.Settings.Add(Key, Box.Text);
            return Convert.ToDouble(Box.Text);
        }
        private System.Configuration.Configuration mainConf;
        public MixtureInitial()
        {
            InitializeComponent();
            Checker.cEmpty = Color.Yellow;
            //Checker.cOutOfRange = Color.MediumOrchid;
            Checker.cErr = Color.DeepPink;
            mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            GetValueByKey("IronTask", txbIronTask);
            GetValueByKey("ScrapTask", txbScrapTask);
            GetValueByKey("SteelTask", txbSteelTask);
            GetValueByKey("IronTemp", txbIronTemp);
            GetValueByKey("ScrapTemp", txbScrapTemp);
            GetValueByKey("SteelTemp", txbSteelTemp);
            GetValueByKey("Basiticy", txbBasiticy);
            GetValueByKey("PercentMgO", txbMgO);
            GetValueByKey("PercentFeO", txbFeO);
            GetValueByKey("FomTask", txbFomIn);
            GetValueByKey("LimeStoneTask", txbLimeStoneIn);
            GetValueByKey("CokeTask", txbCokeIn);
            ch_Iron = new ChemTable("Химия чугуна", "IronChemistry");
            ch_Iron.LoadCSVData();
            ch_Scrap = new ChemTable("Химия лома", "ScrapChemistry");
            ch_Scrap.LoadCSVData();
            ch_Fom = new ChemTable("Химия ФОМа", "FomChemistry");
            ch_Fom.LoadCSVData();
            ch_CaCO3 = new ChemTable("Химия известняка", "CaCO3Chemistry");
            ch_CaCO3.LoadCSVData();
            ch_LimeStone = new ChemTable("Химия доломита", "LimeStoneChemistry");
            ch_LimeStone.LoadCSVData();
            ch_Lime = new ChemTable("Химия извести", "LimeChemistry");
            ch_Lime.LoadCSVData();
            ch_Coke = new ChemTable("Химия кокса", "CokeChemistry");
            ch_Coke.LoadCSVData();
            ch_Dust = new ChemTable("Химия отходящих пылей", "DustChemistry");
            ch_Dust.LoadCSVData();
            btnCalculate.Select();
        }

        private void rbnIron_CheckedChanged(object sender, EventArgs e)
        {
            txbIronTask.Enabled = true;
            txbScrapTask.Enabled = false;
            txbSteelTask.Enabled = false;
        }

        private void rbnScrap_CheckedChanged(object sender, EventArgs e)
        {
            txbIronTask.Enabled = false;
            txbScrapTask.Enabled = true;
            txbSteelTask.Enabled = false;
        }

        private void rbnSteel_CheckedChanged(object sender, EventArgs e)
        {
            txbIronTask.Enabled = false;
            txbScrapTask.Enabled = false;
            txbSteelTask.Enabled = true;
        }

        private void MixtureInitial_Load(object sender, EventArgs e)
        {
            rbnIron.Checked = true;

        }

        private void txbSteelTemp_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isInputCorrect()
        {
            LogClear();
            Color color = new Color();
            bool result = Checker.isDoubleCorrect(txbIronTemp.Text, out color, new dMargin(1100, 1500));
            txbIronTemp.BackColor = color;
            dMargin streetTemp = new dMargin(-50, 50);
            result &= Checker.isDoubleCorrect(txbScrapTemp.Text, out color, streetTemp);
            txbScrapTemp.BackColor = color;
            result &= Checker.isDoubleCorrect(txbSteelTemp.Text, out color, new dMargin(1600, 1800));
            txbSteelTemp.BackColor = color;
            if (rbnIron.Checked)
            {
                result &= Checker.isDoubleCorrect(txbIronTask.Text, out color, new dMargin(200, 400));
                txbIronTask.BackColor = color;
                Mixture1.s_CalcTask = Mixture1.CalcTask.CalcTaskIron;
            }
            else if (rbnScrap.Checked)
            {
                result &= Checker.isDoubleCorrect(txbScrapTask.Text, out color, new dMargin(100, 200));
                txbScrapTask.BackColor = color;
                Mixture1.s_CalcTask = Mixture1.CalcTask.CalcTaskScrap;
            }
            else if (rbnSteel.Checked)
            {
                result &= Checker.isDoubleCorrect(txbSteelTask.Text, out color, new dMargin(300, 600));
                txbSteelTask.BackColor = color;
                Mixture1.s_CalcTask = Mixture1.CalcTask.CalcTaskSteel;
            }
            result &= Checker.isDoubleCorrect(txbBasiticy.Text, out color, new dMargin(1, 4));
            txbBasiticy.BackColor = color;
            result &= Checker.isDoubleCorrect(txbMgO.Text, out color, new dMargin(10, 40));
            txbMgO.BackColor = color;
            result &= Checker.isDoubleCorrect(txbFeO.Text, out color, new dMargin(10, 40));
            txbFeO.BackColor = color;
            result &= Checker.isDoubleCorrect(txbFeO.Text, out color, new dMargin(10, 40));
            txbFeO.BackColor = color;
            result &= Checker.isDoubleCorrect(txbFomIn.Text, out color, new dMargin(0, 20000));
            txbFomIn.BackColor = color;
            result &= Checker.isDoubleCorrect(txbLimeStoneIn.Text, out color, new dMargin(0, 20000));
            txbLimeStoneIn.BackColor = color;
            result &= Checker.isDoubleCorrect(txbCokeIn.Text, out color, new dMargin(0, 1000));
            txbCokeIn.BackColor = color;
            if (!result)
            {
                LogStr("Заполните значения в желтых секторах, исправьте в красных и фиолетовых");
            }
            return result;
        }

        private FPCarrier _fp = null;
        private void rp(String Key, Double Val)
        {
            LogStr(string.Format("{0} = {1}", Key, Val));
            _fp.fpSet(Key, Val);
        }
        private void initChemistry(FPCarrier fp, ChemTable tbl)
        {
            LogStr(tbl.Text);
            _fp = fp;
            tbl.Enumerate(rp);
        }

        private void NextStep()
        {
            LogStr(string.Format("============= m_Scrap:{2} e_Common:{0} e_Curr:{1}", 
                Math.Round(Mixture1.e_Common, 5), 
                Math.Round(Mixture1.e_Curr, 5),
                Math.Round(Mixture1.m_Scrap, 3)));
        }
        private void ShowResults()
        {
            LogStr(string.Format("m_Iron = {0}", Mixture1.m_Iron));
            LogStr(string.Format("m_Scrap = {0}", Mixture1.m_Scrap));
            LogStr(string.Format("m_Steel = {0}", Mixture1.m_Steel));
            LogStr(string.Format("m_Fom = {0}", Mixture1.m_Fom));
            LogStr(string.Format("m_CaCO3 = {0}", Mixture1.m_CaCO3));
            LogStr(string.Format("m_dolomite = {0}", Mixture1.m_dolomite));
            LogStr(string.Format("m_lime = {0}", Mixture1.m_lime));
            LogStr(string.Format("m_slag = {0}", Mixture1.m_slag));
        }
        private void ClearOutputs()
        {
            Color color = Implements.Checker.cNormal;
            txbIronOut.Text = "";
            txbIronOut.BackColor = color;
            txbScrapOut.Text = "";
            txbScrapOut.BackColor = color;
            txbSteelOut.Text = "";
            txbSteelOut.BackColor = color;
            txbLimeOut.Text = "";
            txbLimeOut.BackColor = color;
            txbDolomitOut.Text = "";
            txbDolomitOut.BackColor = color;
            txbFomOut.Text = "";
            txbFomOut.BackColor = color;
            txbLimeStoneOut.Text = "";
            txbLimeStoneOut.BackColor = color;

            txtCu.Text = "";
            txtMo.Text = "";
            txtNi.Text = "";
            txtCo.Text = "";
            txtW.Text = "";
            txtAs.Text = "";
            txtSn.Text = "";
            txtSb.Text = "";
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (isInputCorrect())
            {
                btnCalculate.Enabled = false;
                Mixture1.t_Iron = SetDoubleByKey("IronTemp", txbIronTemp);
                Mixture1.t_Scrap = SetDoubleByKey("ScrapTemp", txbScrapTemp);
                Mixture1.t_Steel = SetDoubleByKey("SteelTemp", txbSteelTemp);
                Mixture1.basiticy = SetDoubleByKey("Basiticy", txbBasiticy);
                Mixture1.p_MgO = SetDoubleByKey("PercentMgO", txbMgO);
                Mixture1.p_FeO = SetDoubleByKey("PercentFeO", txbFeO);
                Mixture1.m_Fom = SetDoubleByKey("FomTask", txbFomIn) * 0.001;
                Mixture1.m_CaCO3 = SetDoubleByKey("LimeStoneTask", txbLimeStoneIn) * 0.001;
                Mixture1.m_Coke = SetDoubleByKey("CokeTask", txbCokeIn) * 0.001;
                mainConf.Save();
                LogStr("Рассчет запущен " + DateTime.Now);
                initChemistry(Mixture1.s_Iron, ch_Iron);
                initChemistry(Mixture1.s_Scrap, ch_Scrap);
                initChemistry(Mixture1.s_Fom, ch_Fom);
                initChemistry(Mixture1.s_CaCO3, ch_CaCO3);
                initChemistry(Mixture1.s_LimeStone, ch_LimeStone);
                initChemistry(Mixture1.s_Lime, ch_Lime);
                initChemistry(Mixture1.s_Dust, ch_Dust);
                initChemistry(Mixture1.s_Coke, ch_Coke);
                Mixture1.Initialize();
                ClearOutputs();
                while (true)
                {
                    Mixture1.Calculate();
                    System.Threading.Thread.Sleep(0);
                    Mixture1.e_Curr = Mixture1.e_Curr;
                    NextStep();
                    if (Mixture1.Ready()) break;
                }
                switch (Mixture1.s_CalcTask)
                {
                    case Mixture1.CalcTask.CalcTaskIron:
                        Mixture1.m_IronTask = SetDoubleByKey("IronTask", txbIronTask);
                        Mixture1.scaleFactor = Mixture1.m_IronTask / Mixture1.m_Iron;
                        break;
                    case Mixture1.CalcTask.CalcTaskScrap:
                        Mixture1.m_ScrapTask = SetDoubleByKey("ScrapTask", txbScrapTask);
                        Mixture1.scaleFactor = Mixture1.m_ScrapTask / Mixture1.m_Scrap;
                        break;
                    case Mixture1.CalcTask.CalcTaskSteel:
                        Mixture1.m_SteelTask = SetDoubleByKey("SteelTask", txbSteelTask);
                        Mixture1.scaleFactor = Mixture1.m_SteelTask / Mixture1.m_Steel;
                        break;
                }
                Mixture1.PostCalc();
                ShowResults();
                LogStr("Рассчет окончен " + DateTime.Now);
                Color color;
                txbIronOut.Text = Mixture1.m_Iron.ToString();
                Checker.isDoubleCorrect(txbIronOut.Text, out color);
                txbIronOut.BackColor = color;
                txbScrapOut.Text = Math.Round(Mixture1.m_Scrap, 3).ToString();
                Checker.isDoubleCorrect(txbScrapOut.Text, out color);
                txbScrapOut.BackColor = color;
                txbSteelOut.Text = Math.Round(Mixture1.m_Steel, 3).ToString();
                Checker.isDoubleCorrect(txbSteelOut.Text, out color, new dMargin(Mixture1.m_Iron));
                txbSteelOut.BackColor = color;
                Double m_lime_t = Math.Round(Mixture1.m_lime * 1000, 0);
                txbLimeOut.Text = m_lime_t.ToString();
                Checker.isDoubleCorrect(txbLimeOut.Text, out color, new dMargin(0, 20000));
                txbLimeOut.BackColor = color;
                Double m_dolomit_t = Math.Round(Mixture1.m_dolomite * 1000, 0);
                txbDolomitOut.Text = m_dolomit_t.ToString();
                Checker.isDoubleCorrect(txbDolomitOut.Text, out color, new dMargin(0, 20000));
                txbDolomitOut.BackColor = color;
                Double m_Fom_t = Math.Round(Mixture1.m_Fom * 1000, 0);
                txbFomOut.Text = m_Fom_t.ToString();
                Checker.isDoubleCorrect(txbFomOut.Text, out color, new dMargin(0, 10000));
                txbFomOut.BackColor = color;
                Double m_limeStone_t = Math.Round(Mixture1.m_CaCO3 * 1000, 0);
                txbLimeStoneOut.Text = m_limeStone_t.ToString();
                Checker.isDoubleCorrect(txbLimeStoneOut.Text, out color, new dMargin(0, 10000));
                txbLimeStoneOut.BackColor = color;

                txtCu.Text = Math.Round(Mixture1.p_SteelAdd[0], 5).ToString();
                txtMo.Text = Math.Round(Mixture1.p_SteelAdd[1], 5).ToString();
                txtNi.Text = Math.Round(Mixture1.p_SteelAdd[2], 5).ToString();
                txtCo.Text = Math.Round(Mixture1.p_SteelAdd[3], 5).ToString();
                txtW.Text = Math.Round(Mixture1.p_SteelAdd[4], 5).ToString();
                txtAs.Text = Math.Round(Mixture1.p_SteelAdd[5], 5).ToString();
                txtSn.Text = Math.Round(Mixture1.p_SteelAdd[6], 5).ToString();
                txtSb.Text = Math.Round(Mixture1.p_SteelAdd[7], 5).ToString();
                btnCalculate.Enabled = true;
            }
        }

        private void panSlagInput_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnIronControl_Click(object sender, EventArgs e)
        {
            ch_Scrap.ShowDialog();
        }

        private void btnIronChem_Click(object sender, EventArgs e)
        {
            ch_Iron.ShowDialog();
        }

        private void btnFomChem_Click(object sender, EventArgs e)
        {
            ch_Fom.ShowDialog();
        }

        private void btnLimeStoneChem_Click(object sender, EventArgs e)
        {
            ch_LimeStone.ShowDialog();
        }

        private void btnCalculate_Resize(object sender, EventArgs e)
        {
            if (btnCalculate.Size.Width < 200) btnCalculate.Text = "Счет";
            else btnCalculate.Text = "Выполнить рассчет";
        }

    }
}
