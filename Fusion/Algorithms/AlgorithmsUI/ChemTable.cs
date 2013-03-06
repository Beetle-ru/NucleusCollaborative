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
using Oracle.DataAccess.Client;

namespace AlgorithmsUI
{
    public partial class ChemTable : Form
    {
#if (!DB_IS_ORACLE)
        private ElementTableAdapter ada = new ElementTableAdapter();
        private ChemistryDataSet.AdditionDataTable idt = new ChemistryDataSet.AdditionDataTable();
        private ChemistryDataSet.ElementDataTable tbl = new ChemistryDataSet.ElementDataTable();
#endif
        private string m_configKey;
        private string m_path = "data";
        private char m_separator = ':';
        public WordPool<double> m_inFP = new WordPool<double>(0.0);
        private static string secretFP = ":Sn:Sb:Zn:Fe:Cu:Cr:Mo:Ni:N:O:H:TOTAL:Basiticy:Yield:Steel:T:eH:cp:TeH:ro:";
        public int m_propsStart = 0;
        public bool m_readOnLoad = true;
        private bool m_dataChanged, m_needComplete;
#if DB_IS_ORACLE
        private Int64 m_sid = -1;
#else
        private Guid m_sid = Guid.Empty;
#endif
        public ChemTable(string Name, string ConfigKey)
        {
            InitializeComponent();
            Text = Name;
            m_configKey = ConfigKey;
            Checker.cErr = Color.LightSalmon;
        }

        public void LoadCSVData()
        {
#if DB_IS_ORACLE
            Program.OraCmd.CommandText = "SELECT ID FROM ADDITION WHERE NAME = :N";
            Program.OraCmd.Parameters.Clear();
            Program.OraCmd.Parameters.Add(new OracleParameter("N", OracleDbType.NVarchar2, System.Data.ParameterDirection.Input));
            Program.OraCmd.Parameters["N"].Value = m_configKey;
            if (Program.OraCmd.Connection.State != System.Data.ConnectionState.Closed)
            {
                Program.OraCmd.Connection.Close();
            }
            Program.OraCmd.Connection.Open();
            Program.OraReader = Program.OraCmd.ExecuteReader();
            if (Program.OraReader.HasRows)
            {
                Program.OraReader.Read();
                m_sid = Convert.ToInt64(Program.OraReader[0]);
            }
            else m_sid = -1;
            Program.OraCmd.CommandText = "SELECT "
            + "ELEMENT.NAME AS N, ELEMENT.\"VALUE\" AS V "
            + "FROM ADDITION, ELEMENT "
            + "WHERE ADDITION.ID = ELEMENT.SID AND (ADDITION.NAME = :N)";
            if (Program.OraCmd.Connection.State != System.Data.ConnectionState.Closed)
            {
                Program.OraCmd.Connection.Close();
            }
            Program.OraCmd.Connection.Open();
            Program.OraReader = Program.OraCmd.ExecuteReader();
            gridChem.Rows.Clear();
            if (Program.OraReader.HasRows)
            {
                while (Program.OraReader.Read())
                {
                    gridChem.Rows.Add();
                    string key = Convert.ToString(Program.OraReader[0]);
                    double val = Convert.ToDouble(Program.OraReader[1]);
                    gridChem.Rows[gridChem.RowCount - 1].Cells[0].Value = key;
                    gridChem.Rows[gridChem.RowCount - 1].Cells[1].Value = val;
                    m_inFP.SetWord(key, val);
                }
            }
            m_propsStart = gridChem.RowCount;
            Program.OraCmd.Parameters["N"].Value += ".props";
            if (Program.OraCmd.Connection.State != System.Data.ConnectionState.Closed)
            {
                Program.OraCmd.Connection.Close();
            }
            Program.OraCmd.Connection.Open();
            Program.OraReader = Program.OraCmd.ExecuteReader();
            if (Program.OraReader.HasRows)
            {
                while (Program.OraReader.Read())
                {
                    gridChem.Rows.Add();
                    string key = Convert.ToString(Program.OraReader[0]);
                    double val = Convert.ToDouble(Program.OraReader[1]);
                    gridChem.Rows[gridChem.RowCount - 1].Cells[0].Value = key;
                    gridChem.Rows[gridChem.RowCount - 1].Cells[1].Value = val;
                    m_inFP.SetWord(key, val);
                }
            }
            m_sid = m_sid;
#else
            additionTableAdapter.Connection.ConnectionString = "Data Source=Chemistry.sdf";
            additionTableAdapter.Fill(idt, m_configKey);
            if (idt.Rows.Count > 0)
            {
                m_sid = idt[0].Id;
            }
            ada.Connection.ConnectionString = "Data Source=Chemistry.sdf";
            ada.Fill(tbl, m_configKey);
            gridChem.Rows.Clear();
            gridChem.RowCount = tbl.Rows.Count;
            for (var rowCnt = 0; rowCnt < gridChem.RowCount; rowCnt++)
            {
                gridChem.Rows[rowCnt].Cells[0].Value = tbl[rowCnt].Name;
                gridChem.Rows[rowCnt].Cells[1].Value = tbl[rowCnt].Value;
                m_inFP.SetWord(tbl[rowCnt].Name, tbl[rowCnt].Value);
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
                    m_inFP.SetWord(tbl[rowCnt - m_propsStart].Name, 
                        tbl[rowCnt - m_propsStart].Value);
                }
            }
#endif
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
#if DB_IS_ORACLE
                    Program.OraCmd.CommandText = "UPDATE"
                    + " ELEMENT SET \"VALUE\" = :V"
                    + " WHERE (NAME = :N)"
                    //+ " AND"
                    //+ " (SID = (SELECT ID FROM ADDITION WHERE NAME = :AN))"
                    + " AND (SID = :SID)"
                    ;
                    Program.OraCmd.Parameters.Clear();
                    Program.OraCmd.Parameters.Add(new OracleParameter("V", OracleDbType.Double, System.Data.ParameterDirection.Input));
                    Program.OraCmd.Parameters.Add(new OracleParameter("N", OracleDbType.NVarchar2, System.Data.ParameterDirection.Input));
                    //Program.OraCmdX.Parameters.Add(new OracleParameter("AN", OracleDbType.NVarchar2, System.Data.ParameterDirection.Input));
                    Program.OraCmd.Parameters.Add(new OracleParameter("SID", OracleDbType.Long, System.Data.ParameterDirection.Input));
                    Program.OraCmd.Parameters["V"].Value = v;
                    Program.OraCmd.Parameters["N"].Value = k.ToString();
                    //Program.OraCmdX.Parameters["AN"].Value = m_configKey;
                    Program.OraCmd.Parameters["SID"].Value = m_sid;
                    if (Program.OraCmd.Connection.State != System.Data.ConnectionState.Closed)
                    {
                        Program.OraCmd.Connection.Close();
                    }
                    Program.OraCmd.Connection.Open();
                    int rc = Program.OraCmd.ExecuteNonQuery();
                    if (rc != 1) // Update fail -- trying insert instead
                    {
                        Program.OraCmd.CommandText = "INSERT"
                        + " INTO ELEMENT(ID, SID, NAME, VALUE)"
                        + " VALUES (:ID, :SID, :N, :V)";
                        Program.OraCmd.Parameters.Add(new OracleParameter("ID", OracleDbType.Long, System.Data.ParameterDirection.Input));
                        Program.OraCmd.Parameters["ID"].Value = Program.makeKey();
                        if (Program.OraCmd.Connection.State != System.Data.ConnectionState.Closed)
                        {
                            Program.OraCmd.Connection.Close();
                        }
                        Program.OraCmd.Connection.Open();
                        rc = Program.OraCmd.ExecuteNonQuery();
                        Program.OraCmd.Parameters.RemoveAt("ID");
                    }
#else
                    //additionTableAdapter.Connection.ConnectionString = "Data Source=Chemisty.sdf";
                    int rc = additionTableAdapter.UpdateQuery(v, m_sid, k.ToString());
#endif
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
        private readonly dMargin cmargin = new dMargin(-7001, 7001);
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
            if (m_dataChanged && (col == 1)) m_inFP.SetWord(CellValue(row, 0), Convert.ToDouble(CellValue(row, 1)));
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

        private void ChemTable_DoubleClick(object sender, EventArgs e)
        {
            var f = new KeyGen();
            f.textBox1.Text = Program.makeKey().ToString();
            f.ShowDialog();
        }

        private void ChemTable_Click(object sender, EventArgs e)
        {

        }
    }
}
