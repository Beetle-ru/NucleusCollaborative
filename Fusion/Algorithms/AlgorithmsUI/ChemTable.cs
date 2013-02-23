using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlgorithmsUI.ChemistryDataSetTableAdapters;
using Implements;

namespace AlgorithmsUI
{
    public partial class ChemTable : Form
    {
        private ElementTableAdapter ada = new ElementTableAdapter();
        private ChemistryDataSet.AdditionDataTable idt = new ChemistryDataSet.AdditionDataTable();
        private ChemistryDataSet.ElementDataTable tbl = new ChemistryDataSet.ElementDataTable();
        private string m_configKey;
        private string m_path = "data";
        private char m_separator = ':';
        private Dictionary<string, double> m_inFP = new Dictionary<string, double>();
        private static string secretFP = ":Sn:Sb:Zn:Fe:Cu:Cr:Mo:Ni:N:O:H:TOTAL:Basiticy:Yield:Steel:T:eH:cp:TeH:ro:";
        public int m_propsStart = 0;
        public bool m_readOnLoad = true;
        private bool m_dataChanged, m_needComplete;
        private Guid m_sid = Guid.Empty;
        public ChemTable(string Name, string ConfigKey)
        {
            InitializeComponent();
            Text = Name;
            m_configKey = ConfigKey;
            Checker.cErr = Color.LightSalmon;
        }

        public void LoadCSVData()
        {
            additionTableAdapter.Fill(idt, m_configKey);
            if (idt.Rows.Count > 0)
            {
                m_sid = idt[0].Id;
            }
            ada.Fill(tbl, m_configKey);
            gridChem.Rows.Clear();
            gridChem.RowCount = tbl.Rows.Count;
            for (var rowCnt = 0; rowCnt < gridChem.RowCount; rowCnt++)
            {
                gridChem.Rows[rowCnt].Cells[0].Value = tbl[rowCnt].Name;
                gridChem.Rows[rowCnt].Cells[1].Value = tbl[rowCnt].Value;

            }
            m_propsStart = gridChem.RowCount;
            ada.Fill(tbl, m_configKey + ".props");
            if (tbl.Rows.Count > 0)
            {
                gridChem.Rows.Add(tbl.Rows.Count);
                for (var rowCnt = m_propsStart; rowCnt < gridChem.RowCount; rowCnt++)
                {
                    gridChem.Rows[rowCnt].Cells[0].Value = tbl[rowCnt - m_propsStart].Name;
                    gridChem.Rows[rowCnt].Cells[1].Value = tbl[rowCnt - m_propsStart].Value;

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
            for (var rowCnt = 0; rowCnt < m_propsStart; rowCnt++)
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
                    additionTableAdapter.UpdateQuery(v, m_sid, k.ToString());
                }
            }
        }

        private void ChemTable_Load(object sender, EventArgs e)
        {
            if (m_readOnLoad) LoadCSVData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCSVData();
            m_dataChanged = false;
        }

        private void gridChem_Enter(object sender, EventArgs e)
        {
            m_dataChanged = false;
            btnSave.Enabled = ValidateAll();
        }
      
        private string CellValue(int row, int col)
        {
            var cell = gridChem.Rows[row].Cells[col].Value;
            return cell == null ? "" : cell.ToString();
        }
        private Color ccolor = new Color();
        private readonly dMargin cmargin = new dMargin(0.0001, 7000.9999);
        private bool ValidateCell(int row, int col)
        {
            if (col == 0) return true;
            var res = Checker.isDoubleCorrect(CellValue(row, col), out ccolor, cmargin);
            gridChem.Rows[row].Cells[col].Style.BackColor = ccolor;
            return res;
        }
        private bool ValidateAll()
        {
            bool res = true;
            for (int i = 0; i < gridChem.RowCount; i++)
            {
                res &= ValidateCell(i, 1);
            }
            return res;
        }
        private void CompleteEdit(int row, int col)
        {
            m_dataChanged = CellValue(row, col) != m_beforeEdit;
            btnSave.Enabled = ValidateAll();
            m_needComplete = false;
        }
        private void gridChem_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled &= ValidateCell(e.RowIndex, e.ColumnIndex);
        }

        private string m_beforeEdit;
        private int m_br, m_bc;
        private void gridChem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_beforeEdit = CellValue(e.RowIndex, e.ColumnIndex);
            m_br = e.RowIndex;
            m_bc = e.ColumnIndex;
            m_needComplete = true;
        }

        private void gridChem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            CompleteEdit(e.RowIndex, e.ColumnIndex);
        }

        private void ChemTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_needComplete)
            {
                gridChem.CommitEdit(DataGridViewDataErrorContexts.LeaveControl);
                gridChem.RefreshEdit();
                CompleteEdit(m_br, m_bc);
            }
            if (m_dataChanged)
            {
                if (MessageBox.Show("Сохранить изменения?",
                    "Химия изменилась", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    m_dataChanged = false;
                    if (btnSave.Enabled)
                    {
                        SaveCSVData();
                    }
                    else
                    {
                        MessageBox.Show(Checker.Message, "Неверные данные", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        e.Cancel = true;
                        m_dataChanged = true;
                    }
                }
            }
        }

//=======
//        private void gridChem_Enter(object sender, EventArgs e)
//        {
//            m_dataChanged = false;
//            btnSave.Enabled = ValidateAll();
//        }

//        private Color ccolor = new Color();
//        private readonly dMargin cmargin = new dMargin(0.0001, 99.9999);
//        private bool ValidateCell(int row, int col)
//        {
//            if (col == 0) return true;
//            var cell = gridChem.Rows[row].Cells[col].Value;
//            var res = Checker.isDoubleCorrect(cell == null ? "" : cell.ToString(), out ccolor, cmargin);
//            gridChem.Rows[row].Cells[col].Style.BackColor = ccolor;
//            return res;
//        }
//        private bool ValidateAll()
//        {
//            bool res = true;
//            for (int i = 0; i < gridChem.RowCount; i++)
//            {
//                res &= ValidateCell(i, 1);
//            }
//            return res;
//        }
//        private void gridChem_CellValidated(object sender, DataGridViewCellEventArgs e)
//        {
//            btnSave.Enabled &= ValidateCell(e.RowIndex, e.ColumnIndex);
//        }

//        private void gridChem_CellEndEdit(object sender, DataGridViewCellEventArgs e)
//        {
//            m_dataChanged = true;
//            btnSave.Enabled = ValidateAll();
//        }

//        private void ChemTable_FormClosed(object sender, FormClosedEventArgs e)
//        {
//            for (int i = 0; i < gridChem.RowCount; i++)
//            {
//                gridChem.Rows[i].Cells[1].Value = "";
//            }
//            m_dataChanged = false;
//        }

//        private void gridChem_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
//        {
//            m_dataChanged = true;
//        }
//>>>>>>> 1c3514648f695c2672f63021e811c12342eecd15
    }
}
