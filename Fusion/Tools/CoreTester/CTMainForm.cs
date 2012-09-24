using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ConnectionProvider;
using Converter;
using Implements;

namespace CoreTester
{
    public partial class CTMainForm : Form
    {
        public static ConnectionProvider.Client PushGate;
        public static ConnectionProvider.Client ListenGate;
        private long m_pushEvent, m_receiveEvent, m_loss, m_dataLenght;
        private static bool isStarted;

        public CTMainForm()
        {
            InitializeComponent();

            PushGate = new Client();

            var o = new TestEvent();
            ListenGate = new Client(new Listener());
            ListenGate.Subscribe();

            m_pushEvent = 0;
            m_receiveEvent = 0;
            m_loss = 0;
            isStarted = false;
        }

        private void btnStartTest_Click(object sender, EventArgs e)
        {
            if (isStarted)
            {
                isStarted = false;
                btnStartTest.Text = "Старт";
                timer.Stop();
            }
            else if (GroupCheck())
            {
                m_pushEvent = 0;
                m_receiveEvent = 0;
                m_loss = 0;
                timer.Interval = Convertion.StrToInt32(tbDelay.Text);
                timer.Start();
                isStarted = true;
                btnStartTest.Text = "Стоп";
            }

        }

        private bool GroupCheck()
        {
            var isCorrect = true;
            Color color;
            var margin = new iMargin();
            
            margin.High = 99999;
            margin.Low = 0;
            
            if (!Checker.isIntCorrect(tbDimLength.Text, out color, margin))
            {
                isCorrect = false;
            }
            tbDimLength.BackColor = color;

            margin.Low = 1;

            if (!cbMonitoring.Checked)
            {
                if (!Checker.isIntCorrect(tbCount.Text, out color, margin))
                {
                    isCorrect = false;
                }
                tbCount.BackColor = color;
            }

            margin.High = 10000;
            margin.Low = 10;

            if (!Checker.isIntCorrect(tbDelay.Text, out color, margin))
            {
                isCorrect = false;
            }
            tbDelay.BackColor = color;

            return isCorrect;
        }

        private void cbMonitoring_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMonitoring.Checked)
            {
                tbCount.Enabled = false;
            }
            else
            {
                tbCount.Enabled = true;
            }
        }

        public void ListenerReceive(TestEvent te)
        {
            if (te.Dimm.Length == m_dataLenght)
            {
                m_receiveEvent++;
            }
            tbReceive.Text = m_receiveEvent.ToString();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (!cbMonitoring.Checked)
            {
                if (m_pushEvent >= Convertion.StrToInt32(tbCount.Text)-1)
                {
                    timer.Stop();
                }
            }
            m_loss = m_pushEvent - m_receiveEvent;
            tbLoss.Text = m_loss.ToString();
            var te = new TestEvent();
            te.Dimm = new double[Convertion.StrToInt32(tbDimLength.Text)];
            m_dataLenght = te.Dimm.Length;
            PushGate.PushEvent(te);
            //PushGate.PushEvent(new TestEvent());
            m_pushEvent++;
            tbSend.Text = m_pushEvent.ToString();
            
        }
    }
}
