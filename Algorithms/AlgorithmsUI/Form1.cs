using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlgorithmsUI
{
    public partial class Form1 : Form
    {
        private int num = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HeatCharge.Mixture1.Initialize();
            label1.Text = "?????????????????????";
            button1.Text = "n = 0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s;
            try
            {
                s = string.Format("s_Iron.fp[{0}].Mm == {1}; Val={2}", num, HeatCharge.Mixture1.s_Iron.fp[num].Mm, HeatCharge.Mixture1.s_Iron.fpNorm(num));

            }
            catch (Exception)
            {
                s = string.Format("Uininitialized s_Iron.fp[{0}].Mm", num);
            }
            label1.Text = s;
            num++;
            if (num > 74) num = 0;
            button1.Text = string.Format("n = {0}", num);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
