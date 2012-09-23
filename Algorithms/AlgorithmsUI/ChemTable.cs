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
        public ChemTable(string Name, string ConfigKey)
        {
            InitializeComponent();
            this.Text = Name;
            m_configKey = ConfigKey;
        }
        public void LoadCSVData()
        {
            //MessageBox.Show("load");
            string filePath = String.Format("{0}\\{1}.csv", m_path, m_configKey);
            string[] strings;
            try
            {
                strings = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                strings = new string[0];
                MessageBox.Show(String.Format("Cannot read the file: {0}, call: {1}", filePath, e.ToString()));
                return;
            }
            try
            {
                gridChem.RowCount = strings.Count() + 1; //на 1 больше для того чтоб можно нормально было добавлять данные
                for (int strCnt = 0; strCnt < strings.Count(); strCnt++)
                {
                    string[] values = strings[strCnt].Split(m_separator);
                    if (values.Count() == 2)
                    {
                        gridChem.Rows[strCnt].Cells[0].Value = values[0];
                        gridChem.Rows[strCnt].Cells[1].Value = values[1];
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Cannot read the file: {0}, bad format call exeption: {1}", filePath, e.ToString()));
                //return;
                throw e;
            }
        }

        public delegate void RowProcessor(String Key, Double Value);

        public void Enumerate(RowProcessor rp)
        {
            foreach (DataGridViewRow r in gridChem.Rows)
            {
                if (
                        ((string)r.Cells[0].Value != "") && ((string)r.Cells[0].Value != "") &&
                        (r.Cells[0].Value != null) && (r.Cells[0].Value != null)
                    )
                {
                    rp.Invoke(r.Cells[0].Value.ToString(), Convert.ToDouble((r.Cells[1].Value)));
                }
            }
        }

        public void SaveCSVData()
        {
            //MessageBox.Show("save");
            string filePath = String.Format("{0}\\{1}.csv", m_path, m_configKey);
            //string[] strings = new string[gridChem.Rows.Count];
            var strings = new List<string>();
            Directory.CreateDirectory(m_path);
            for (int row = 0; row < gridChem.Rows.Count; row++)
            {
                if (
                        ((string) gridChem.Rows[row].Cells[0].Value != "") && ((string) gridChem.Rows[row].Cells[0].Value != "") &&
                        (gridChem.Rows[row].Cells[0].Value != null) && (gridChem.Rows[row].Cells[0].Value != null)
                    )
                {
                    
                    var str = String.Format("{1}{0}{2}",
                                                 m_separator,
                                                 gridChem.Rows[row].Cells[0].Value,
                                                 gridChem.Rows[row].Cells[1].Value
                    );
                    strings.Add(str);
                }
            }
            try
            {
                File.WriteAllLines(filePath, strings);
            }
            catch (Exception e)
            {
                MessageBox.Show(String.Format("Cannot write the file: {0}, call: {1}", filePath, e.ToString()));
                return;
            }
        }

        private void ChemTable_Load(object sender, EventArgs e)
        {
            LoadCSVData();
        }

        private void ChemTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCSVData();
        }
    }
}
