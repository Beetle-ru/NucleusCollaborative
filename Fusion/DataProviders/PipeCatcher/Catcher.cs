using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PipeCatcher
{
    public partial class Catcher : Form
    {
        private List<DBReader> m_dbrList;
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

        public Catcher(List<DBReader> dbrList)
        {
            if (dbrList.Count == 0) throw new Exception("No DBReader specified!!!");
            m_dbrList = dbrList;
            InitializeComponent();
        }

        private void Catcher_Load(object sender, EventArgs e)
        {
            LogClear();
            LogStr("Начало работы: " + DateTime.Now);
            txbProcName.Text = "PCK_DATA.PGET_XIMSLAG";
            txbRecId.Text = "3434633";
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text == "Start")
            {
                tmrPipeCheck.Start();
                btnStartStop.Text = "Stop";
                panCaller.Enabled = false;
            }
            else
            {
                tmrPipeCheck.Stop();
                btnStartStop.Text = "Start";
                panCaller.Enabled = true;
            }

        }

        private void tmrPipeCheck_Tick(object sender, EventArgs e)
        {
            String[] lblParts = lblInfo.Text.Split('=');
            Int32 i = Convert.ToInt32(lblParts[1]);
            lblInfo.Text = String.Format("{0}={1}", lblParts[0], ++i);
            foreach(DBReader dbr in m_dbrList)
            {
                if (dbr.HaveNews())
                {
                    LogStr(dbr.GetNews());
                    LogStr(dbr.ProcessNews());
                }
            }

        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            m_dbrList[0].ProcName = txbProcName.Text;
            m_dbrList[0].RecId = Convert.ToDecimal(txbRecId.Text);
            LogStr(m_dbrList[0].ProcessNews());
        }
    }
}
