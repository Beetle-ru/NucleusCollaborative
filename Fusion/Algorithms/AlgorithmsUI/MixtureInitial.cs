﻿using System;
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
            if (Box.Text == "") return 0.0;
            if (mainConf.AppSettings.Settings.AllKeys.Contains(Key))
            {
                mainConf.AppSettings.Settings.Remove(Key);
            }
            mainConf.AppSettings.Settings.Add(Key, Box.Text);
            mainConf.Save();
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
            GetValueByKey("IronTemp", txbIronTemp);
            GetValueByKey("ScrapTemp", txbScrapTemp);
            GetValueByKey("SteelTemp", txbSteelTemp);
            GetValueByKey("Basiticy", txbBasiticy);
            GetValueByKey("LimeTask", txbLimeIn);
            GetValueByKey("DolmaxTask", txbDolomIn);
            GetValueByKey("FomTask", txbFomIn);
            GetValueByKey("LimeStoneTask", txbLimeStoneIn);
            GetValueByKey("CokeTask", txbCokeIn);
            GetValueByKey("PercentMgO", txbMgO);
            GetValueByKey("PercentFeO", txbFeO);
            ch_Iron = new ChemTable("Химия чугуна", "IronChemistry");
            ch_Iron.LoadCSVData();
            ch_Scrap = new ChemTable("Химия лома", "ScrapChemistry");
            ch_Scrap.LoadCSVData();
            ch_Lime = new ChemTable("Химия извести", "LimeChemistry");
            ch_Lime.LoadCSVData();
            ch_LimeStone = new ChemTable("Химия доломита (ДОЛМИТ, МАХГ)", "LimeStoneChemistry");
            ch_LimeStone.LoadCSVData();
            ch_Fom = new ChemTable("Химия ФОМа", "FomChemistry");
            ch_Fom.LoadCSVData();
            ch_CaCO3 = new ChemTable("Химия известняка (СaCO3)", "CaCO3Chemistry");
            ch_CaCO3.LoadCSVData();
            ch_Coke = new ChemTable("Химия кокса", "CokeChemistry");
            ch_Coke.LoadCSVData();
            ch_Dust = new ChemTable("Химия отходящих пылей", "DustChemistry");
            ch_Dust.LoadCSVData();
            btnCalculate.Select();
        }

        private List<CheckBox> calcList = new List<CheckBox>(); 
        private void MixtureInitial_Load(object sender, EventArgs e)
        {
            calcVapno.Tag = txbLimeIn;
            calcDolmax.Tag = txbDolomIn;
            calcFom.Tag = txbFomIn;
            calcVapenec.Tag = txbLimeStoneIn;
            calcList.Add(calcVapno);
            calcList.Add(calcDolmax);
            calcList.Add(calcFom);
            calcList.Add(calcVapenec);
            calcVapno.CheckState = CheckState.Checked;
            calcDolmax.CheckState = CheckState.Checked;
        }

        private bool CheckItem(TextBox txb, dMargin mrg, out double rv, String cfgKey)
        {
            var color = new Color();
            bool result = Checker.isDoubleCorrect(txb.Text, out color, mrg);
            rv = SetDoubleByKey(cfgKey, txb);
            txb.BackColor = color;
            return result;
        }

        private bool isInputCorrect()
        {
            LogClear();

            bool result = CheckItem(txbIronTemp, new dMargin(1100, 1500), out Mixture1.t_Iron, "IronTemp");
            dMargin streetTemp = new dMargin(-50, 50);
            
            result &= CheckItem(txbScrapTemp, streetTemp, out Mixture1.t_Scrap, "ScrapTemp");
            result &= CheckItem(txbSteelTemp, new dMargin(1600, 1800), out Mixture1.t_Steel, "SteelTemp");
            result &= CheckItem(txbIronTask, new dMargin(200, 400), out Mixture1.m_IronTask, "IronTask");
            Mixture1.s_CalcTask = Mixture1.CalcTask.CalcTaskIron;
            result &= CheckItem(txbBasiticy, new dMargin(1, 4), out Mixture1.basiticy, "Basiticy");
            if (txbLimeIn.Visible)
            {
                result &= CheckItem(txbLimeIn, new dMargin(0, 20000), out Mixture1.m_lime, "LimeTask");
                Mixture1.m_lime *= 0.001;
            }
            if (txbDolomIn.Visible)
            {
                result &= CheckItem(txbDolomIn, new dMargin(0, 20000), out Mixture1.m_dolomite, "DolmaxTask");
                Mixture1.m_dolomite *= 0.001;
            }
            if (txbFomIn.Visible)
            {
                result &= CheckItem(txbFomIn, new dMargin(0, 20000), out Mixture1.m_Fom, "FomTask");
                Mixture1.m_Fom *= 0.001;
            }
            if (txbLimeStoneIn.Visible)
            {
                result &= CheckItem(txbLimeStoneIn, new dMargin(0, 20000), out Mixture1.m_CaCO3, "LimeStoneTask");
                Mixture1.m_CaCO3 *= 0.001;
            }
            Mixture1.calcPattern = 0;
            if (calcSelectedCount < 2)
            {
                foreach (var cle in calcList)
                {
                    cle.BackColor = (cle.CheckState == CheckState.Unchecked) ? Checker.cEmpty : SystemColors.Control;
                }
                result = false;
            }
            else
            {
                var cflag = 0x1000;
                for (int i = 0; i < calcList.Count; i++)
                {
                    var cle = calcList[i];
                    cle.BackColor = SystemColors.Control;
                    if (cle.CheckState == CheckState.Checked) Mixture1.calcPattern |= cflag;
                    cflag >>= 4;
                }
            }

            result &= CheckItem(txbCokeIn, new dMargin(0, 1000), out Mixture1.m_Coke, "CokeTask");
            Mixture1.m_Coke *= 0.001;
            result &= CheckItem(txbMgO, new dMargin(10, 40), out Mixture1.p_MgO, "PercentMgO");
            result &= CheckItem(txbFeO, new dMargin(10, 40), out Mixture1.p_FeO, "PercentFeO");
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
                Mixture1.m_IronTask = SetDoubleByKey("IronTask", txbIronTask);
                Mixture1.scaleFactor = Mixture1.m_IronTask / Mixture1.m_Iron;
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
                txbCokeOut.Text = txbCokeIn.Text;
                btnCalculate.Enabled = true;
            }
        }

        private void btnDolomiteChem_Click(object sender, EventArgs e)
        {
            ch_LimeStone.ShowDialog();
        }

        private void btnVapno_Click(object sender, EventArgs e)
        {
            ch_Lime.ShowDialog();
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
            ch_CaCO3.ShowDialog();
        }

        private CheckBox calcLastSelected = null;
        private int calcSelectedCount = 0;
        private void aCalcCheckedChanged(object sender, EventArgs e)
        {
            var cb = sender as CheckBox;
            var tb = cb.Tag as TextBox;
            if (cb.Checked)
            {
                tb.Visible = false;
                if (++calcSelectedCount > 2)
                {
                    calcLastSelected.CheckState = CheckState.Unchecked;
                }
                calcLastSelected = cb;
            }
            else
            {
                tb.Visible = true;
                calcSelectedCount -= 1;
                calcLastSelected = null;
            }
        }
    }
}
