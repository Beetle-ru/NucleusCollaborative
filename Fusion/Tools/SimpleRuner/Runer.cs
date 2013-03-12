using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleRuner {
    public partial class Runer : Form {
        public const string DataFilePath = "order.csv";
        public const char Separator = ';';
        public List<string> Order = new List<string>();
        public Dictionary<int, string> ProgrammDic = new Dictionary<int, string>();
        public Dictionary<int, MsgLoop> MsgDic = new Dictionary<int, MsgLoop>();
        public const int MainLog = 0;
        public int CurrentLog = MainLog;

        public Runer() {
            InitializeComponent();
            cb_appNames.Items.Add("SimpleRunner log");
            cb_appNames.SelectedIndex = MainLog;
            MsgDic.Add(MainLog, new MsgLoop());
        }

        private void t_startApp_Tick(object sender, EventArgs e) {
            t_startApp.Enabled = false;
            LoadOrder();
        }

        private void LoadOrder() {
            string[] strings;
            try {
                strings = File.ReadAllLines(DataFilePath);
            }
            catch {
                strings = new string[0];
                MessageBox.Show("Cannot read the file: " + DataFilePath, "ERROR");
                return;
            }

            try {
                for (int strCnt = 0; strCnt < strings.Count(); strCnt++) {
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
            catch (Exception e) {
                MessageBox.Show(
                    String.Format("Cannot read the file: {0}, bad format call exeption: {1}", DataFilePath, e.ToString()),
                    "ERROR");
                //return;
                throw e;
            }

            int index = 0;
            for (int i = 0; i < Order.Count; i++) {
                if (!Order[i].StartsWith("_")) {
                    cb_appNames.Items.Add(Order[i].Replace(Separator, ' '));
                    ProgrammDic.Add(i, Order[i]);
                    MsgDic.Add(cb_appNames.Items.Count - 1, new MsgLoop());

                    Log(String.Format("i = {0}, cb.item = {1}", i, cb_appNames.Items.Count - 1));
                    ShowLog();
                }
                else
                    CmdExecutor(Order[i]);
            }
        }

        public void OrderExecutor() {}

        public void CmdExecutor(string cmd) {
            string[] values = cmd.Split(Separator);
            if (values.Count() == 2) {
                if (values[0] == "_sleep") {
                    try {
                        System.Threading.Thread.Sleep(Int32.Parse(values[1]));
                        Log(String.Format("Sleeping {0} ms", values[1]));
                        ShowLog();
                    }
                    catch (Exception) {
                        Log(String.Format("ERROR: bad parameter - {0}", values[1]));
                        ShowLog();
                    }
                }
            }
            else {
                Log(String.Format("ERROR: bad command - {0}", cmd));
                ShowLog();
            }
        }

        public void Log(string msg, int logNumber = MainLog) {
            if (logNumber < MsgDic.Count)
                MsgDic[logNumber].Add(msg);
            else {
                if (MsgDic.Count > MainLog)
                    MsgDic[MainLog].Add(String.Format("ERROR: logNumber({0}) > MsgDic.Count({2})", logNumber,
                                                      MsgDic.Count));
            }
        }

        public void ShowLog(int logNumber) {
            if (logNumber < MsgDic.Count) {
                tb_log.Text = MsgDic[logNumber].ToString();
                tb_log.Select(tb_log.TextLength, 0);
                tb_log.ScrollToCaret();
            }
            else {
                if (MsgDic.Count > MainLog)
                    MsgDic[MainLog].Add(String.Format("ERROR: logNumber({0}) > MsgDic.Count({2})", logNumber,
                                                      MsgDic.Count));
            }
        }

        public void ShowLog() {
            ShowLog(CurrentLog);
        }

        private void cb_appNames_SelectedIndexChanged(object sender, EventArgs e) {
            CurrentLog = ((System.Windows.Forms.ComboBox) sender).SelectedIndex;
            Log(String.Format("Selected: {0}", CurrentLog));
            ShowLog();
        }
    }
}