using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using Converter;
using HeatControl;
using ConnectionProvider;
using HeatControl.HeatInfoDataSetTableAdapters;
using Implements;

namespace HeatControl {
    public partial class MixCalculator : UserControl {
        public HEATTARGETVALTableAdapter ada
            = new HEATTARGETVALTableAdapter();

        public HEATCALCPARAMTableAdapter adaC
            = new HEATCALCPARAMTableAdapter();

        public HEATFACTPARAMTableAdapter adaF
            = new HEATFACTPARAMTableAdapter();

        public HeatInfoDataSet.HEATTARGETVALDataTable tbl
            = new HeatInfoDataSet.HEATTARGETVALDataTable();

        public HeatInfoDataSet.HEATCALCPARAMDataTable tblC
            = new HeatInfoDataSet.HEATCALCPARAMDataTable();

        public HeatInfoDataSet.HEATFACTPARAMDataTable tblF
            = new HeatInfoDataSet.HEATFACTPARAMDataTable();

        public System.Timers.Timer dbTimer = new System.Timers.Timer();
        private void OnTimedEvent(object source, ElapsedEventArgs e) {
            switch (m_cn)
            {
                case "1":
                    adaC.FillByHN1(tblC, m_lbound_hn.ToString());
                    adaF.FillByHN1(tblF, m_lbound_hn.ToString());
                    break;
                case "2":
                    adaC.FillByHN2(tblC, m_lbound_hn.ToString());
                    adaF.FillByHN2(tblF, m_lbound_hn.ToString());
                    break;
                case "3":
                    adaC.FillByHN3(tblC, m_lbound_hn.ToString());
                    adaF.FillByHN3(tblF, m_lbound_hn.ToString());
                    break;
            }
            if (tblC.Count != tblF.Count) throw new Exception("рассогласование таблиц");
            Invoke(new MethodInvoker(delegate()
            {
                heats.RowCount = tblC.Count + tblF.Count;
                for (var i = 0; i < tblC.Count; i++)
                {
                    var calc = i << 1;
                    var fact = calc + 1;
                    heats.Rows[calc].Cells[0].Value = tblC[i].HEATNO;
                    heats.Rows[calc].Cells[1].Value = "расчет";
                    heats.Rows[calc].Cells[3].Value = tblC[i].HMWEIGHT;
                    heats.Rows[calc].Cells[4].Value = tblC[i].HMTEMP;
                    safeAssign(calc, 5, tblC[i].HMPSI);
                    heats.Rows[calc].Cells[6].Value = tblC[i].SCWEIGHT;
                    heats.Rows[calc].Cells[7].Value = tblC[i].STWEIGHT;
                    heats.Rows[calc].Cells[8].Value = tblC[i].STATUS;

                    heats.Rows[fact].Cells[0].Value = tblF[i].HEATNO;
                    heats.Rows[fact].Cells[1].Value = "факт";
                    heats.Rows[fact].Cells[3].Value = tblF[i].HMWEIGHT;
                    heats.Rows[fact].Cells[4].Value = tblF[i].HMTEMP;
                    heats.Rows[fact].Cells[5].Value = tblF[i].HMPSI;
                    heats.Rows[fact].Cells[6].Value = tblF[i].SCWEIGHT;
                    heats.Rows[fact].Cells[7].Value = tblF[i].STWEIGHT;
                    heats.Rows[fact].Cells[8].Value = tblF[i].STATUS;

                }
            })); 
            dbTimer.Interval = 5000;
        }

        private void safeAssign(int r, int c, object v) {
            try {
                heats.Rows[r].Cells[c].Value = v;
            }
            catch (Exception e) {
                heats.Rows[r].Cells[c].Value = "";
            };
        }

        public class WordPool<X> : Dictionary<string, X> {
            private readonly X nullValue;

            public WordPool(X nv) {
                nullValue = nv;
            }

            public X GetWord(string Key) {
                if (this.ContainsKey(Key)) return this[Key];
                return nullValue;
            }

            public void SetWord(string Key, X Value) {
                if (this.ContainsKey(Key)) {
                    this[Key] = Value;
                }
                else if (Value.Equals(nullValue)) {}
                else {
                    this.Add(Key, Value);
                }
            }
        }

        public string m_cn = "4";
        public int m_lbound_hn;
        public CoreListener listener;
        public FlexHelper fex = new FlexHelper("Model.Shixta-I.Result");

        private void LogStr(String str) {
            rtbConvState.Text += String.Format("{0}\n", str);
            rtbConvState.Select(rtbConvState.TextLength, 0);
            rtbConvState.ScrollToCaret();
        }

        private void LogClear() {
            rtbConvState.Clear();
        }

        public ChemTable ch_Iron, ch_Scrap, ch_Doloms, ch_Fom, ch_Dolmax, ch_Lime, ch_Coke, ch_Dust;
        private ScrapTable scrapTable;
        public IronTable ironTable;

        private void GetValueByKey(string Key, TextBox Box) {
            //if (mainConf.AppSettings.Settings.AllKeys.Contains(Key))
            //{
            //    Box.Text = mainConf.AppSettings.Settings[Key].Value;
            //}
        }

        private double SetDoubleByKey(string Key, TextBox Box) {
            if (Box.Text == "") return 0.0;
            //if (mainConf.AppSettings.Settings.AllKeys.Contains(Key))
            //{
            //    mainConf.AppSettings.Settings.Remove(Key);
            //}
            //mainConf.AppSettings.Settings.Add(Key, Box.Text);
            //mainConf.Save();
            return Convert.ToDouble(Box.Text);
        }

        //private System.Configuration.Configuration mainConf;
        public MixCalculator() {
            InitializeComponent();
        }

        public void Init() {
            if (listener == null) listener = new CoreListener(Tag.ToString(), this);
            dbTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //listener.Init();
            Checker.cEmpty = Color.Yellow;
            //Checker.cOutOfRange = Color.MediumOrchid;
            Checker.cErr = Color.DeepPink;
            //mainConf = System.Configuration.ConfigurationManager.OpenExeConfiguration("");
            GetValueByKey("IronTask", txbIronTask);
            GetValueByKey("IronTemp", txbIronTemp);
            GetValueByKey("ScrapTemp", txbScrapTemp);
            GetValueByKey("SteelTemp", txbSteelTemp);
            GetValueByKey("Basiticy", txbBasiticy);
            //GetValueByKey("LimeTask", txbLimeIn);
            //GetValueByKey("DolomSTask", txbLimeStoneIn);
            //GetValueByKey("FomTask", txbFomIn);
            //GetValueByKey("DolmaxTask", txbDolomIn);
            //GetValueByKey("CokeTask", txbCokeIn);
            GetValueByKey("PercentMgO", txbMgO);
            GetValueByKey("PercentFeO", txbFeO);
            ch_Iron = new ChemTable("Химия чугуна", "IRON");
            ch_Iron.LoadCSVData();
            ch_Scrap = new ChemTable("Химия лома", "SCRAP");
            ch_Scrap.LoadCSVData();
            ch_Lime = new ChemTable("Химия извести", "LIME");
            ch_Lime.LoadCSVData();
            ch_Dolmax = new ChemTable("Химия доломита сушеного (ДОЛОМС)", "DOLOMS");
            ch_Dolmax.LoadCSVData();
            ch_Fom = new ChemTable("Химия ФОМа", "FOM");
            ch_Fom.LoadCSVData();
            ch_Doloms = new ChemTable("Химия магнезита (МАХГ)", "MAXG");
            ch_Doloms.LoadCSVData();
            ch_Coke = new ChemTable("Химия кокса", "COKE");
            ch_Coke.LoadCSVData();
            ch_Dust = new ChemTable("Химия отходящих пылей", "OFFDUST");
            ch_Dust.LoadCSVData();
            scrapTable = new ScrapTable(this);
            ironTable = new IronTable(this);
            btnCalculate.Select();
        }

        private bool isInputCorrect() {
            LogClear();

            bool result = CheckItem(txbIronTemp, new dMargin(1100, 1500), out MixCalc.t_Iron, "IronTemp");
            dMargin streetTemp = new dMargin(-50, 50);

            result &= CheckItem(txbScrapTemp, streetTemp, out MixCalc.t_Scrap, "ScrapTemp");
            result &= CheckItem(txbSteelTemp, new dMargin(1600, 1800), out MixCalc.t_Steel, "SteelTemp");
            if (panIronTask.Visible) {
                result &= CheckItem(txbIronTask, new dMargin(200, 400), out MixCalc.m_IronTask, "IronTask");
                MixCalc.s_CalcTask = MixCalc.CalcTask.CalcTaskIron;
            }
            else {
                result &= CheckItem(txbSteelTask, new dMargin(300, 600), out MixCalc.m_SteelTask, "SteelTask");
                MixCalc.s_CalcTask = MixCalc.CalcTask.CalcTaskSteel;
            }
            result &= CheckItem(txbBasiticy, new dMargin(1, 4), out MixCalc.basiticy, "Basiticy");
            MixCalc.calcPattern = 0;
            result &= CheckItem(txbMgO, new dMargin(3, 40), out MixCalc.p_MgO, "PercentMgO");
            result &= CheckItem(txbFeO, new dMargin(3, 40), out MixCalc.p_FeO, "PercentFeO");
            if (!result) {
                LogStr(Checker.Message);
            }
            return result;
        }

        private FPCarrier _fp = null;

        private void rp(String Key, Double Val) {
            LogStr(string.Format("{0} = {1}", Key, Val));
            _fp.fpSet(Key, Val);
        }

        private void initChemistry(FPCarrier fp, ChemTable tbl) {
            LogStr(tbl.Text);
            _fp = fp;
            tbl.Enumerate(rp);
        }

        private void NextStep() {
            LogStr(string.Format("============= m_Scrap:{2} e_Common:{0} e_Curr:{1}",
                                 Math.Round(MixCalc.e_Common, 5),
                                 Math.Round(MixCalc.e_Curr, 5),
                                 Math.Round(MixCalc.m_Scrap, 3)));
        }

        private bool CheckItem(TextBox txb, dMargin mrg, out double rv, String cfgKey) {
            var color = new Color();
            bool result = Checker.isDoubleCorrect(txb.Text, out color, mrg);
            rv = SetDoubleByKey(cfgKey, txb);
            txb.BackColor = color;
            return result;
        }

        private void btnIronSel_Click(object sender, EventArgs e) {
            ironTable.ShowDialog();
        }

        private void btnIronChem_Click(object sender, EventArgs e) {
            ch_Iron.ShowDialog();
        }

        public void fexFillIn() {
            if ((MixCalc.calcPattern & 0x1000) == 0x0000) {
                fex.AddDbl("LimeTask", MixCalc.m_Lime);
            }
            if ((MixCalc.calcPattern & 0x0100) == 0x0000) {
                fex.AddDbl("DolomSTask", MixCalc.m_DolomS);
            }
            if ((MixCalc.calcPattern & 0x0010) == 0x0000) {
                fex.AddDbl("FomTask", MixCalc.m_Fom);
            }
            if ((MixCalc.calcPattern & 0x0001) == 0x0000) {
                fex.AddDbl("MaxGTask", MixCalc.m_DolMax);
            }
        }

        public void fexFillCalc() {
            if ((MixCalc.calcPattern & 0x1000) != 0x0000) {
                fex.AddDbl("LimeCalc", MixCalc.m_Lime);
            }
            if ((MixCalc.calcPattern & 0x0100) != 0x0000) {
                fex.AddDbl("DolomSCalc", MixCalc.m_DolomS);
            }
            if ((MixCalc.calcPattern & 0x0010) != 0x0000) {
                fex.AddDbl("FomCalc", MixCalc.m_Fom);
            }
            if ((MixCalc.calcPattern & 0x0001) != 0x0000) {
                fex.AddDbl("MaxGCalc", MixCalc.m_DolMax);
            }
        }

        private void RenewEventAndDB(String StatusString) {
            fex.ClearArgs();
            fex.AddDbl("IronCalc", txbIronCalc.Text);
            fex.AddDbl("IronTemp", txbIronTemp.Text);
            fex.AddDbl("ScrapTemp", txbScrapTemp.Text);
            fex.AddDbl("SteelTemp", txbSteelTemp.Text);
            fex.AddDbl("Basiticy", txbBasiticy.Text);
            fexFillCalc();
            fex.AddDbl("PercentMgO", txbMgO.Text);
            fex.AddDbl("PercentFeO", txbFeO.Text);
            fexFillIn();
            fex.AddDbl("CokeTask", MixCalc.m_Coke);
            fex.AddDbl("ScrapCalc", MixCalc.m_Scrap);
            fex.AddDbl("SteelCalc", MixCalc.m_Steel);
            if (StatusString.Contains("твержд")) {
                fex.Fire(listener.MainGate);
                ch_Iron.FireChemistry(listener.MainGate);
                ch_Scrap.FireChemistry(listener.MainGate);
                ch_Lime.FireChemistry(listener.MainGate);
                ch_Doloms.FireChemistry(listener.MainGate);
                ch_Fom.FireChemistry(listener.MainGate);
                ch_Dolmax.FireChemistry(listener.MainGate);
                ch_Coke.FireChemistry(listener.MainGate);
                ch_Dust.FireChemistry(listener.MainGate);
            }
            int rc = ada.UpdateFromV(
                (float?) fex.GetDbl("SteelTemp"),
                (float?) fex.GetDbl("PercentFeO"),
                (float?) fex.GetDbl("PercentMgO"),
                (float?) fex.GetDbl("Basiticy"),
                txbHeatNum.Text
                );
            if (rc != 0) {
                rc = adaC.UpdateHeatCalc(
                    (float?) fex.GetDbl("IronTemp"),
                    (float?)fex.GetDbl("IronCalc"),
                    (float?) fex.GetDbl("ScrapCalc"),
                    (float?) fex.GetDbl("SteelCalc"),
                    (float?) ch_Iron.m_inFP["Si"],
                    StatusString,
                    txbHeatNum.Text
                    );
            }
            else {
                rc = ada.InsertFromV(
                    txbHeatNum.Text,
                    (float?) fex.GetDbl("SteelTemp"),
                    (float?) fex.GetDbl("PercentFeO"),
                    (float?) fex.GetDbl("PercentMgO"),
                    (float?) fex.GetDbl("Basiticy")
                    );
                rc = adaC.InsertHeatCalc(
                    txbHeatNum.Text,
                    (float?) fex.GetDbl("IronTemp"),
                    (float?)fex.GetDbl("IronCalc"),
                    (float?) fex.GetDbl("ScrapCalc"),
                    (float?) fex.GetDbl("SteelCalc"),
                    (float?) ch_Iron.m_inFP["Si"],
                    StatusString
                    );
                rc = adaF.InsertQuery(
                    txbHeatNum.Text,
                    0, 0, 0, 0, 0,
                    "не зашихтована"
                    );
            }
        }

        private void btnApprove_Click(object sender, EventArgs e) {
            btnApprove.Enabled = false;
            RenewEventAndDB("расчет подтвержден");
            listener.MainGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(HeatChangeEvent).Name });
            btnApprove.Enabled = true;
        }

        private void btnScrapChem_Click(object sender, EventArgs e) {
            ch_Scrap.ShowDialog();
        }

        private void btnScrapSel_Click(object sender, EventArgs e) {
            scrapTable.ShowDialog();
        }

        private void ShowResults() {
            LogStr(string.Format("m_Iron = {0}", MixCalc.m_Iron));
            LogStr(string.Format("m_Scrap = {0}", MixCalc.m_Scrap));
            LogStr(string.Format("m_Steel = {0}", MixCalc.m_Steel));
            LogStr(string.Format("m_Fom = {0}", MixCalc.m_Fom));
            LogStr(string.Format("m_DolMax = {0}", MixCalc.m_DolMax));
            LogStr(string.Format("m_DolomS = {0}", MixCalc.m_DolomS));
            LogStr(string.Format("m_Lime = {0}", MixCalc.m_Lime));
            LogStr(string.Format("m_slag = {0}", MixCalc.m_slag));
        }

        private void ClearOutputs() {
            Color color = Implements.Checker.cNormal;
            txbIronCalc.Text = "";
            txbIronCalc.BackColor = color;
            txbScrapOut.Text = "";
            txbScrapOut.BackColor = color;
            txbSteelOut.Text = "";
            txbSteelOut.BackColor = color;
            //txbLimeOut.Text = "";
            //txbLimeOut.BackColor = color;
            //txbDolomitOut.Text = "";
            //txbDolomitOut.BackColor = color;
            //txbFomOut.Text = "";
            //txbFomOut.BackColor = color;
            //txbLimeStoneOut.Text = "";
            //txbLimeStoneOut.BackColor = color;
        }

        public const int DIGS = 2;
        private void btnCalculate_Click(object sender, EventArgs e) {
            if (isInputCorrect()) {
                btnCalculate.Enabled = false;
                LogStr("Рассчет запущен " + DateTime.Now);
                initChemistry(MixCalc.s_Iron, ch_Iron);
                initChemistry(MixCalc.s_Scrap, ch_Scrap);
                initChemistry(MixCalc.s_Fom, ch_Fom);
                initChemistry(MixCalc.s_DolMax, ch_Doloms);
                initChemistry(MixCalc.s_DolomS, ch_Dolmax);
                initChemistry(MixCalc.s_Lime, ch_Lime);
                initChemistry(MixCalc.s_Dust, ch_Dust);
                initChemistry(MixCalc.s_Coke, ch_Coke);
                MixCalc.Initialize();
                MixCalc.calcPattern = 0x1001;
                MixCalc.m_Fom = 2;
                ClearOutputs();
                while (true) {
                    MixCalc.Calculate();
                    System.Threading.Thread.Sleep(0);
                    MixCalc.e_Curr = MixCalc.e_Curr;
                    NextStep();
                    if (MixCalc.Ready()) break;
                }
                MixCalc.m_IronTask = SetDoubleByKey("IronTask", txbIronTask);
                switch (MixCalc.s_CalcTask) {
                    case MixCalc.CalcTask.CalcTaskIron:
                        MixCalc.scaleFactor = MixCalc.m_IronTask / MixCalc.m_Iron;
                        break;
                    case MixCalc.CalcTask.CalcTaskSteel:
                        MixCalc.scaleFactor = MixCalc.m_SteelTask / MixCalc.m_Steel;
                        break;
                    default:
                        MixCalc.scaleFactor = 1;
                        break;
                }
                MixCalc.PostCalc();
                ShowResults();
                LogStr("Рассчет окончен " + DateTime.Now);
                Color color;
                txbIronCalc.Text = Math.Round(MixCalc.m_Iron, DIGS).ToString();
                Checker.isDoubleCorrect(txbIronCalc.Text, out color);
                txbIronCalc.BackColor = color;
                txbScrapOut.Text = Math.Round(MixCalc.m_Scrap, DIGS).ToString();
                Checker.isDoubleCorrect(txbScrapOut.Text, out color);
                txbScrapOut.BackColor = color;
                txbSteelOut.Text = Math.Round(MixCalc.m_Steel, DIGS).ToString();
                Checker.isDoubleCorrect(txbSteelOut.Text, out color, new dMargin(MixCalc.m_Iron));
                txbSteelOut.BackColor = color;
                //Double m_lime_t = Math.Round(MixCalc.m_Lime * 1000, 0);
                //txbLimeOut.Text = m_lime_t.ToString();
                //Checker.isDoubleCorrect(txbLimeOut.Text, out color, new dMargin(0, 20000));
                //txbLimeOut.BackColor = color;
                //Double m_dolomit_t = Math.Round(MixCalc.m_DolomS * 1000, 0);
                //txbDolomitOut.Text = m_dolomit_t.ToString();
                //Checker.isDoubleCorrect(txbDolomitOut.Text, out color, new dMargin(0, 20000));
                //txbDolomitOut.BackColor = color;
                //Double m_Fom_t = Math.Round(MixCalc.m_Fom * 1000, 0);
                //txbFomOut.Text = m_Fom_t.ToString();
                //Checker.isDoubleCorrect(txbFomOut.Text, out color, new dMargin(0, 10000));
                //txbFomOut.BackColor = color;
                //Double m_limeStone_t = Math.Round(MixCalc.m_DolMax * 1000, 0);
                //txbLimeStoneOut.Text = m_limeStone_t.ToString();
                //Checker.isDoubleCorrect(txbLimeStoneOut.Text, out color, new dMargin(0, 10000));
                //txbLimeStoneOut.BackColor = color;
                RenewEventAndDB("расчет выполнен");
                btnCalculate.Enabled = true;
            }
        }

        private void heats_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex < 0) return;
            var strix = e.RowIndex >> 1;
            if (Convert.ToString(heats.Rows[strix << 1].Cells[8].Value).Contains("твержд")) {
                MessageBox.Show(String.Format(
                    "Расчет по плавке №{0} утвержден\rи не может быть пересчитан",
                    m_lbound_hn + strix));
            }
            else {
                txbHeatNum.Text = Convert.ToString(m_lbound_hn + strix);
            }
        }

        private void MixCalculator_Load(object sender, EventArgs e) {
            listener.Init();
            dbTimer.Interval = 500;
            dbTimer.AutoReset = false;
            dbTimer.Enabled = true;
        }

        private void txbHeatNum_TextChanged(object sender, EventArgs e) {
            m_lbound_hn = Convert.ToInt32(txbHeatNum.Text) - 3;
            for (var i = 0; i < 10; i++) {
                if (1 != (int)ada.SelectCount(Convert.ToString(m_lbound_hn + i))) {
                    ada.InsertFromV(
                        Convert.ToString(m_lbound_hn + i),
                        1650, 27, 10, 2.7f);
                }
                if (1 != (int)adaC.SelectCount(Convert.ToString(m_lbound_hn + i))) {
                    adaC.InsertHeatCalc(
                        Convert.ToString(m_lbound_hn + i),
                        1380, 300, 0, 0, 0,
                        "не расчитана");
                }
                if (1 != (int)adaF.SelectCount(Convert.ToString(m_lbound_hn + i))) {
                    adaF.InsertQuery(
                        Convert.ToString(m_lbound_hn + i),
                        0, 0, 0, 0, 0,
                        "не зашихтована");
                }
            }
            ada.FillTargets(tbl, txbHeatNum.Text);
            txbSteelTemp.Text = tbl[0].STTEMP.ToString();
            txbMgO.Text = tbl[0].PMGO.ToString();
            txbFeO.Text = tbl[0].PFEO.ToString();
            txbBasiticy.Text = tbl[0].BASITICY.ToString();
            txbIronTask.Text = "300";
            txbIronTemp.Text = "1380";
            txbScrapTemp.Text = "0";
            cmbSteelGroup.SelectedIndex = 0;
            dbTimer.Interval = 500;
        }

        private void heats_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpd_Click(object sender, EventArgs e)
        {
        }

        private void txbSteelTask_TextChanged(object sender, EventArgs e) {
            panIronTask.Visible = (txbSteelTask.Text == "");
        }
    }
}