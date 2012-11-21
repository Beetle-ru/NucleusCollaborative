using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlgorithmsUI
{
    public partial class ChemTable : Form
    {
        private string m_configKey;
        private string m_path = "data";
        private char m_separator = ':';
        private Dictionary<string, double> m_inFP = new Dictionary<string, double>();
        private static string secretFP = ":Sn:Sb:Zn:Fe:Cu:Cr:Mo:Ni:N:O:H:TOTAL:Basiticy:Yield:Steel:T:eH:cp:TeH:ro:";
        private int m_secFPcnt = 0;
        private bool m_dataChanged = false;
        public ChemTable(string Name, string ConfigKey)
        {
            InitializeComponent();
            Text = Name;
            m_configKey = ConfigKey;
        }

        public void LoadCSVData()
        {
            m_inFP.Clear();
            m_secFPcnt = 0;
            string filePath = String.Format("{0}\\{1}.csv", m_path, m_configKey);
            string[] strings;
            try
            {
                strings = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Cannot read the file: {0}, call: {1}", filePath, e));
                return;
            }
            try
            {
                for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                {
                    string[] values = strings[strCnt].Split(m_separator);
                    if (values.Count() == 2)
                    {
                        if (secretFP.Contains(':' + values[0] + ':'))
                        {
                            m_secFPcnt++;
                            values[0] = '-' + values[0];
                        }
                        m_inFP.Add(values[0], Convert.ToDouble(values[1]));
                    }
                    else throw new Exception("Invalid CSV format");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Cannot read the file: {0}, bad format call exeption: {1}", filePath, e.ToString()));
                //return;
                throw e;
            }
            gridChem.RowCount = m_inFP.Count() - m_secFPcnt;
            var rowCnt = 0;
            foreach (var fp in m_inFP)
            {
                if (!fp.Key.StartsWith("-"))
                {
                    gridChem.Rows[rowCnt].Cells[0].Value = fp.Key;
                    gridChem.Rows[rowCnt].Cells[1].Value = fp.Value.ToString();
                    rowCnt++;
                }
            }
        }

        public delegate void RowProcessor(String Key, Double Value);

        public void Enumerate(RowProcessor rp)
        {
            foreach (var fp in m_inFP) rp.Invoke(fp.Key.StartsWith("-") ? fp.Key.Substring(1) : fp.Key, fp.Value);
        }

        private static System.Globalization.NumberFormatInfo nfi;
        public void SaveCSVData()
        {
            string filePath = String.Format("{0}\\{1}.csv", m_path, m_configKey);
            var strings = new List<string>();
            Directory.CreateDirectory(m_path);
            for (var rowCnt = 0; rowCnt < gridChem.RowCount; rowCnt++)
            {
                var k = gridChem.Rows[rowCnt].Cells[0].Value;
                if ((k != null) && (k.ToString() != ""))
                {
                    double v = 0.0;
                    string s = gridChem.Rows[rowCnt].Cells[1].Value.ToString();
                    try
                    {
                        v = Convert.ToDouble(s, nfi);
                    }
                    catch (System.FormatException e)
                    {
                        MessageBox.Show(String.Format("Неверный формат {0} = {1}", k, s));
                    }
                    m_inFP[k.ToString()] = Convert.ToDouble(v, nfi);
                }
            }
            foreach (var fp in m_inFP) strings.Add(String.Format("{1}{0}{2}", m_separator,
                fp.Key.StartsWith("-") ? fp.Key.Substring(1) : fp.Key, fp.Value));
            try
            {
                File.WriteAllLines(filePath, strings);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Cannot write the file: {0}, call: {1}", filePath, e));
                return;
            }
        }

        private void ChemTable_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("Loading");
            LoadCSVData();
        }

        private void ChemTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_dataChanged)
            {
                if (MessageBox.Show("Сохранить изменения?",
                    "Химия изменилась", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveCSVData();
                }
                m_dataChanged = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCSVData();
            m_dataChanged = false;
        }

        private void gridChem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            m_dataChanged = true;
        }

        private void gridChem_Enter(object sender, EventArgs e)
        {
            m_dataChanged = false;
        }
    }
}
