using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeatPassport
{
    public partial class FormProggress : Form
    {
        DataGathering.Form1 form;
        public FormProggress(DataGathering.Form1 parentForm)
        {
            InitializeComponent();
            progressBar1.Maximum = parentForm.m_datFiles.Length;
            progressBar1.Step = 1;
           // timer1.Enabled = true;
            form = parentForm;
        }

        public FormProggress(DataGathering.Form1 parentForm, int max)
        {
            InitializeComponent();
            progressBar1.Maximum = max;
            progressBar1.Step = 1;
            form = parentForm;
        }

        public void NewValue()
        {
            progressBar1.Value = form.m_currentFile;
            label2.Text = string.Format("{0}/{1}", form.m_currentFile, form.m_datFiles.Length);
        } 


        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Value = form.m_currentFile;
            label2.Text = string.Format("{0}/{1}", form.m_currentFile, form.m_datFiles.Length);
        }
    }
}
