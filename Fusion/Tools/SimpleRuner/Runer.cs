using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleRuner
{
    public partial class Runer : Form
    {
        public const string DataFilePath = "order.csv";
        public const char Separator = ';';
        public List<string> Order = new List<string>();
        public Dictionary<int, string> ProgrammDic = new Dictionary<int, string>();
        public Runer()
        {
            InitializeComponent();
            cb_appNames.Items.Add("SimpleRunner log");
            cb_appNames.SelectedIndex = 0;
        }

        private void t_startApp_Tick(object sender, EventArgs e)
        {
            t_startApp.Enabled = false;
            LoadOrder();
        }

        private void LoadOrder()
        {
            string[] strings;
            try
            {
                strings = File.ReadAllLines(DataFilePath);
            }
            catch
            {
                strings = new string[0];
                MessageBox.Show("Cannot read the file: " + DataFilePath, "ERROR");
                return;
            }

            try
            {
                for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                {
                    Order.Add(strings[strCnt]);
                    //string[] values = strings[strCnt].Split(Separator);
                    //if (values.Count() == 2)
                    //{
                    //    Order.Add(values[0]);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Cannot read the file, bad string: " + strings[strCnt], "ERROR");
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Cannot read the file: {0}, bad format call exeption: {1}", DataFilePath, e.ToString()), "ERROR");
                //return;
                throw e;
            }

            int index = 0;
            for (int i = 0; i < Order.Count; i++)
            {
                if (!Order[i].StartsWith("_"))
                {
                    cb_appNames.Items.Add(Order[i].Replace(Separator, ' '));
                    ProgrammDic.Add(i,Order[i]);
                    tb_log.Text += String.Format("i = {0}, cb.item = {1}\n", i, cb_appNames.Items.Count -1);
                }
                else
                {
                    CmdExecutor(Order[i]);
                }
            }
        }
        public void OrderExecutor()
        {
            
        }
        public void CmdExecutor(string cmd)
        {
            
        }
    }
}
