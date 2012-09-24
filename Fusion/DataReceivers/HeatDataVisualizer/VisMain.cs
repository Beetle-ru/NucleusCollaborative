using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonTypes;
using ConnectionProvider;
using Converter;

namespace HeatDataVisualizer
{
    public partial class VisMain : Form
    {
        public static ConnectionProvider.Client PushGate;
        public static ConnectionProvider.Client ListenGate;

        private const int LanceStringsCount = 3;
        private const int MaterialsCount = 8;

        private List<int> m_cntJobReadyList;
        private const int WeigherNum = 5;

        private comJobW3Event comJobW3Old;
        private comJobW4Event comJobW4Old;
        private comJobW5Event comJobW5Old;
        private comJobW6Event comJobW6Old;
        private comJobW7Event comJobW7Old;

        public VisMain()
        {
            InitializeComponent();

            dgHaderLance.RowCount = LanceStringsCount;
            dgPatternLance.RowCount = LanceStringsCount;

            dgHaderAdditions.RowCount = MaterialsCount;
            dgPatternAdditions.RowCount = MaterialsCount;

            m_cntJobReadyList = new List<int>();
            for (int i = 0; i < WeigherNum; i++)
            {
                m_cntJobReadyList.Add(0);
            }

            PushGate = new Client();

            var o = new TestEvent();
            ListenGate = new Client(new Listener());
            ListenGate.Subscribe();

            initTimer.Interval = 2000;
            initTimer.Start();

            LanceHaderFill();

            comJobW3Old = new comJobW3Event();
            comJobW4Old = new comJobW4Event();
            comJobW5Old = new comJobW5Event();
            comJobW6Old = new comJobW6Event();
            comJobW7Old = new comJobW7Event();

            //gbW3.BackColor = Color.FromArgb(0,0,50);
            //gbW4.BackColor = Color.FromArgb(255,100,0);

        }

        public void MaterialsFill(BoundNameMaterialsEvent bnme)
        {
            int bunkerID = 0;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker5MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB5";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W3";

            bunkerID = 1;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker6MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB6";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W3";

            bunkerID = 2;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker7MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB7";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W4";

            bunkerID = 3;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker8MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB8";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W5";

            bunkerID = 4;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker9MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB9";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W6";

            bunkerID = 5;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker10MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB10";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W7";

            bunkerID = 6;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker11MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB11";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W7";

            bunkerID = 7;
            dgHaderAdditions.Rows[bunkerID].Cells[0].Value = Encoder(bnme.Bunker12MaterialName);
            dgHaderAdditions.Rows[bunkerID].Cells[1].Value = "RB12";
            dgHaderAdditions.Rows[bunkerID].Cells[2].Value = "W7";


        }

        public void DataFill(SteelMakingPatternEvent smpe)
        {
            const int maxBukerId = 8;
            const int widthCells = 40;
            dgPatternLance.ColumnCount = smpe.steps.Count;
            dgPatternAdditions.ColumnCount = smpe.steps.Count;

            for (int step = 0; step < smpe.steps.Count; step++)
            {
                var defBackColor = Color.FromArgb(100,20,0);
                var defForeColor = Color.White;
                var notToGiveBackColor = Color.Indigo;
                var notToGiveForeColor = Color.Chartreuse;
                var allowToAddBackColor = Color.DarkCyan;
                var allowToAddForeColor = Color.Chartreuse;

                dgPatternLance.Rows[0].Cells[step].Value = smpe.steps[step].lance.LancePositin;
                dgPatternLance.Rows[1].Cells[step].Value = smpe.steps[step].lance.O2Flow;
                dgPatternLance.Rows[2].Cells[step].Value = smpe.steps[step].O2Volume;
                dgPatternLance.Columns[step].Width = widthCells;
                for (int row = 0; row < dgPatternLance.Rows.Count; row++)
                {
                    dgPatternLance.Rows[row].Cells[step].Style.BackColor = defBackColor;
                    dgPatternLance.Rows[row].Cells[step].Style.ForeColor = defForeColor;
                }

                for (int row = 0; row < dgPatternAdditions.Rows.Count; row++)
                {
                    dgPatternAdditions.Rows[row].Cells[step].Style.BackColor = defBackColor;
                    dgPatternAdditions.Rows[row].Cells[step].Style.ForeColor = defForeColor;
                    dgPatternAdditions.Rows[row].Cells[step].Value = null;
                }

                for (int bunkerId = 0; bunkerId < maxBukerId; bunkerId++)
                {
                    for (int weigher = 0; weigher < smpe.steps[step].weigherLines.Count; weigher++)
                    {
                        if (smpe.steps[step].weigherLines[weigher].BunkerId == bunkerId)
                        {
                            dgPatternAdditions.Rows[bunkerId].Cells[step].Value =
                                smpe.steps[step].weigherLines[weigher].PortionWeight;

                            if (smpe.steps[step].weigherLines[weigher].NotToGive)
                            {
                                dgPatternAdditions.Rows[bunkerId].Cells[step].Style.BackColor = notToGiveBackColor;
                                dgPatternAdditions.Rows[bunkerId].Cells[step].Style.ForeColor = notToGiveForeColor;
                            }
                            if (smpe.steps[step].weigherLines[weigher].AllowToAdd)
                            {
                                dgPatternAdditions.Rows[bunkerId].Cells[step].Style.BackColor = allowToAddBackColor;
                                dgPatternAdditions.Rows[bunkerId].Cells[step].Style.ForeColor = allowToAddForeColor;
                            }
                        }
                    }
                }

                dgPatternAdditions.Columns[step].Width = widthCells;
            }
        }

        public void ChangeBunker(BaseEvent be)
        {
            var backColor = Color.FromArgb(0, 70, 50);

            if (be is comJobW3Event)
            {
                var jobW3 = be as comJobW3Event;
                
                lblRB5Mass.Text = jobW3.RB5Weight.ToString();
                lblRB5Oxy.Text = jobW3.RB5Oxygen.ToString();
                
                lblRB6Mass.Text = jobW3.RB6Weight.ToString();
                lblRB6Oxy.Text = jobW3.RB6Oxygen.ToString();

                if (
                    (comJobW3Old.RB5Oxygen != jobW3.RB5Oxygen) &&
                    (comJobW3Old.RB5Weight != jobW3.RB5Weight) &&
                    (comJobW3Old.RB6Oxygen != jobW3.RB6Oxygen) &&
                    (comJobW3Old.RB6Weight != jobW3.RB6Weight)
                    )
                {
                    gbW3.BackColor = backColor;
                }

                comJobW3Old.RB5Oxygen = jobW3.RB5Oxygen;
                comJobW3Old.RB5Weight = jobW3.RB5Weight;
                comJobW3Old.RB6Oxygen = jobW3.RB6Oxygen;
                comJobW3Old.RB6Weight = jobW3.RB6Weight;

            }
            if (be is comJobW4Event)
            {
                var jobW4 = be as comJobW4Event;

                lblRB7Mass.Text = jobW4.RB7Weight.ToString();
                lblRB7Oxy.Text = jobW4.RB7Oxygen.ToString();

                if (
                    (comJobW4Old.RB7Oxygen != jobW4.RB7Oxygen) &&
                    (comJobW4Old.RB7Weight != jobW4.RB7Weight)
                    )
                {
                    gbW4.BackColor = backColor;
                }
                comJobW4Old.RB7Oxygen = jobW4.RB7Oxygen;
                comJobW4Old.RB7Weight = jobW4.RB7Weight;
            }
            if (be is comJobW5Event)
            {
                var jobW5 = be as comJobW5Event;
                
                lblRB8Mass.Text = jobW5.RB8Weight.ToString();
                lblRB8Oxy.Text = jobW5.RB8Oxygen.ToString();

                if (
                    (comJobW5Old.RB8Oxygen != jobW5.RB8Oxygen) &&
                    (comJobW5Old.RB8Weight != jobW5.RB8Weight)
                    )
                {
                    gbW5.BackColor = backColor;
                }
                comJobW5Old.RB8Oxygen = jobW5.RB8Oxygen;
                comJobW5Old.RB8Weight = jobW5.RB8Weight;
            }
            if (be is comJobW6Event)
            {
                var jobW6 = be as comJobW6Event;

                lblRB9Mass.Text = jobW6.RB9Weight.ToString();
                lblRB9Oxy.Text = jobW6.RB9Oxygen.ToString();

                if (
                    (comJobW6Old.RB9Oxygen != jobW6.RB9Oxygen) &&
                    (comJobW6Old.RB9Weight != jobW6.RB9Weight)
                    )
                {
                    gbW6.BackColor = backColor;
                }
                comJobW6Old.RB9Oxygen = jobW6.RB9Oxygen;
                comJobW6Old.RB9Weight = jobW6.RB9Weight;
            }
            if (be is comJobW7Event)
            {
                var jobW7 = be as comJobW7Event;

                lblRB10Mass.Text = jobW7.RB10Weight.ToString();
                lblRB10Oxy.Text = jobW7.RB10Oxygen.ToString();

                lblRB11Mass.Text = jobW7.RB11Weight.ToString();
                lblRB11Oxy.Text = jobW7.RB11Oxygen.ToString();

                lblRB12Mass.Text = jobW7.RB12Weight.ToString();
                lblRB12Oxy.Text = jobW7.RB12Oxygen.ToString();

                if (
                    (comJobW7Old.RB10Oxygen != jobW7.RB10Oxygen) &&
                    (comJobW7Old.RB10Weight != jobW7.RB10Weight) &&
                    (comJobW7Old.RB11Oxygen != jobW7.RB11Oxygen) &&
                    (comJobW7Old.RB11Weight != jobW7.RB11Weight) &&
                    (comJobW7Old.RB12Oxygen != jobW7.RB12Oxygen) &&
                    (comJobW7Old.RB12Weight != jobW7.RB12Weight)
                    )
                {
                    gbW7.BackColor = backColor;
                }
                comJobW7Old.RB10Oxygen = jobW7.RB10Oxygen;
                comJobW7Old.RB10Weight = jobW7.RB10Weight;
                comJobW7Old.RB11Oxygen = jobW7.RB11Oxygen;
                comJobW7Old.RB11Weight = jobW7.RB11Weight;
                comJobW7Old.RB12Oxygen = jobW7.RB12Oxygen;
                comJobW7Old.RB12Weight = jobW7.RB12Weight;
            }
        }

        public void cntJobReady(BaseEvent be)
        {
            var backColor = Color.FromArgb(255, 100, 0);
            var backErrColor = Color.FromArgb(0, 0, 170);

            //m_cntJobReadyList;
            
            if (be is cntWeigher3JobReadyEvent)
            {
                var cw3jre = be as cntWeigher3JobReadyEvent;

                if (m_cntJobReadyList[0] != cw3jre.Counter)
                {
                    if (gbW3.BackColor == backColor)
                    {
                        gbW3.BackColor = backErrColor;
                    }
                    else
                    {
                        gbW3.BackColor = backColor;
                    }
                }
                else
                {
                    gbW3.BackColor = backColor;
                }
                m_cntJobReadyList[0] = cw3jre.Counter;
            }
            
            if (be is cntWeigher4JobReadyEvent)
            {
                var cw4jre = be as cntWeigher4JobReadyEvent;

                if (m_cntJobReadyList[1] != cw4jre.Counter)
                {
                    if (gbW4.BackColor == backColor)
                    {
                        gbW4.BackColor = backErrColor;
                    }
                    else
                    {
                        gbW4.BackColor = backColor;
                    }
                }
                else
                {
                    gbW4.BackColor = backColor;
                }
                m_cntJobReadyList[1] = cw4jre.Counter;
            }
            
            if (be is cntWeigher5JobReadyEvent)
            {
                var cw5jre = be as cntWeigher5JobReadyEvent;

                if (m_cntJobReadyList[2] != cw5jre.Counter)
                {
                    if (gbW5.BackColor == backColor)
                    {
                        gbW5.BackColor = backErrColor;
                    }
                    else
                    {
                        gbW5.BackColor = backColor;
                    }
                }
                else
                {
                    gbW5.BackColor = backColor;
                }
                m_cntJobReadyList[2] = cw5jre.Counter;
            }
            
            if (be is cntWeigher6JobReadyEvent)
            {
                var cw6jre = be as cntWeigher6JobReadyEvent;

                if (m_cntJobReadyList[3] != cw6jre.Counter)
                {
                    if (gbW6.BackColor == backColor)
                    {
                        gbW6.BackColor = backErrColor;
                    }
                    else
                    {
                        gbW6.BackColor = backColor;
                    }
                }
                else
                {
                    gbW6.BackColor = backColor;
                }
                m_cntJobReadyList[3] = cw6jre.Counter;
            }
            
            if (be is cntWeigher7JobReadyEvent)
            {
                var cw7jre = be as cntWeigher7JobReadyEvent;

                if (m_cntJobReadyList[4] != cw7jre.Counter)
                {
                    if (gbW7.BackColor == backColor)
                    {
                        gbW7.BackColor = backErrColor;
                    }
                    else
                    {
                        gbW7.BackColor = backColor;
                    }
                }
                else
                {
                    gbW7.BackColor = backColor;
                }
                m_cntJobReadyList[4] = cw7jre.Counter;
            }
            
        }

        private void initTimer_Tick(object sender, EventArgs e)
        {
            //PushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(BoundNameMaterialsEvent).Name});
            PushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(ModeLanceEvent).Name });
            PushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(ModeVerticalPathEvent).Name });

            initTimer.Stop();
        }

        public void DirectRead()
        {
            PushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(ModeLanceEvent).Name });
            PushGate.PushEvent(new OPCDirectReadEvent() { EventName = typeof(ModeVerticalPathEvent).Name });
        }

        private void LanceHaderFill()
        {
            dgHaderLance.Rows[0].Cells[0].Value = "Lance pos";
            dgHaderLance.Rows[0].Cells[1].Value = "cm";
            
            dgHaderLance.Rows[1].Cells[0].Value = "O2 flow";
            dgHaderLance.Rows[1].Cells[1].Value = "Nm3/m";
            
            dgHaderLance.Rows[2].Cells[0].Value = "O2 rate";
            dgHaderLance.Rows[2].Cells[1].Value = "Nm3";
        }

        public void BacklightStep(int step)
        {
            Color backNow = Color.Gold;
            Color foreNow = Color.Red;
            Color back = Color.Black;
            Color fore = Color.CornflowerBlue;
            
            if ((step <= dgPatternLance.Columns.Count) && (step <= dgPatternAdditions.Columns.Count))
            {
               
                for (int row = 0; row < dgPatternLance.Rows.Count; row++)
                {
                    dgPatternLance.Rows[row].Cells[step].Style.BackColor = backNow;
                    dgPatternLance.Rows[row].Cells[step].Style.ForeColor = foreNow;
                }

                for (int row = 0; row < dgPatternAdditions.Rows.Count; row++)
                {
                    dgPatternAdditions.Rows[row].Cells[step].Style.BackColor = backNow;
                    dgPatternAdditions.Rows[row].Cells[step].Style.ForeColor = foreNow;
                }

                if (step > 0)
                {
                    for (int row = 0; row < dgPatternLance.Rows.Count; row++)
                    {
                        dgPatternLance.Rows[row].Cells[step-1].Style.BackColor = back;
                        dgPatternLance.Rows[row].Cells[step-1].Style.ForeColor = fore;
                    }

                    for (int row = 0; row < dgPatternAdditions.Rows.Count; row++)
                    {
                        dgPatternAdditions.Rows[row].Cells[step-1].Style.BackColor = back;
                        dgPatternAdditions.Rows[row].Cells[step-1].Style.ForeColor = fore;
                    }
                }

                //for (int s = 0; s < step; s++)
                //{
                //    for (int row = 0; row < dgPatternLance.Rows.Count; row++)
                //    {
                //        dgPatternLance.Rows[row].Cells[s].Style.BackColor = back;
                //    }

                //    for (int row = 0; row < dgPatternAdditions.Rows.Count; row++)
                //    {
                //        dgPatternAdditions.Rows[row].Cells[s].Style.BackColor = back;
                //    }
                //}
            }
        }
        public string Encoder(string str)
        {
            var charArray = str.ToCharArray();
            str = "";
            foreach (char c in charArray)
            {
                if (c > 190)
                {
                    str += (char)(c + 848);
                }
                else
                {
                    str += c;
                }


            }
            return str;
        }

        private void dgPatternAdditions_Scroll(object sender, ScrollEventArgs e)
        {
            dgPatternLance.HorizontalScrollingOffset = e.NewValue;
        }

        public void SetLanceMode(int mode)
        {
            lblLanceMode.Text = GetTexMode(mode);
        }
        public void SetOxygenMode(int mode)
        {
            lblOxygenMode.Text = GetTexMode(mode);
        }
        public void SetVerticalPathMode(int mode)
        {
            lblVerticalPathMode.Text = GetTexMode(mode);
        }

        private string GetTexMode(int mode)
        {
            var str = "";
            //str = mode.ToString();
            switch (mode)
            {
                case 1:
                    str = "РУЧ";
                    break;
                case 2:
                    str = "АВТ";
                    break;
                case 3:
                    str = "УВМ";
                    break;
                default:
                    str = "###";
                    break;
            }
            return str;
        }
    }
}
