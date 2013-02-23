using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlgorithmsUI.ScrapDataSetTableAdapters;

namespace AlgorithmsUI
{
    public partial class ScrapTable : Form
    {
        private int m_crow;

        public ScrapTable()
        {
            InitializeComponent();
            gridScrap.RowCount = 12;
        }

        private void ScrapTable_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'scrapDataSet.Scrap' table. You can move, or remove it, as needed.
            this.scrapTableAdapter.Fill(this.scrapDataSet.Scrap);

        }

        private void btnAddScrap_Click(object sender, EventArgs e)
        {
            //var txt = cmbScrap.Items[cmbScrap.SelectedIndex].ToString();
            //txt = cmbScrap.SelectedItem.ToString();
            //txt = cmbScrap.Tag.ToString() + " | " + cmbScrap.Text;
            //MessageBox.Show(txt);
            gridScrap.Rows[m_crow].Cells[0].Value = 1;
            gridScrap.Rows[m_crow].Cells[1].Value = cmbScrap.Tag.ToString();
            gridScrap.Rows[m_crow].Cells[2].Value = cmbScrap.Text;
            //(gridScrap.Rows[m_crow].Cells[2].Value as Button).Text = "++";
            //(gridScrap.Rows[m_crow].Cells[3].Value as Button).Text = "--";
            if (++m_crow == gridScrap.RowCount) btnAddScrap.Enabled = false;
            btnSave.Enabled = true;
        }

        private int countScrapShares()
        {
            int shareSum = 0;
            for (int i = 0; i < gridScrap.RowCount; i++)
            {
                if (gridScrap.Rows[i].Cells[0].Value == null) break;
                shareSum += (int) gridScrap.Rows[i].Cells[0].Value;
            }
            return shareSum;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("Sum {0}", countScrapShares()));
            ScrapDataSetTableAdapters.ScrapMixerTableAdapter ada = new ScrapMixerTableAdapter();
            ScrapDataSet.ScrapMixerDataTable tbl = new ScrapDataSet.ScrapMixerDataTable();
            WordPool<double> wpProps = new WordPool<double>(0.0);
            WordPool<double> wpTotal = new WordPool<double>(0.0);
            for (int i = 0; i < gridScrap.RowCount; i++)
            {
                if (gridScrap.Rows[i].Cells[1].Value == null) break;
                short code = Convert.ToInt16(gridScrap.Rows[i].Cells[1].Value);
                short shares = Convert.ToInt16(gridScrap.Rows[i].Cells[0].Value);
                try
                {
                    ada.FillByCode(tbl, code);
                }
                catch (Exception) { }
                ;
                for (int j = 0; j < tbl.Count; j++)
                {
                    string key = tbl[j].Name;
                    wpTotal.SetWord(key, wpTotal.GetWord(key) + shares * tbl[j].Value);
                }
            }
            double scaleFactor = 1.0 / countScrapShares();
            for (int k = 0; k < wpTotal.Count; k++)
            {
                wpTotal.SetWord(wpTotal.ElementAt(k).Key, scaleFactor * wpTotal.ElementAt(k).Value);
            }
            for (int i = Program.face.ch_Scrap.m_propsStart; i < Program.face.ch_Scrap.gridChem.RowCount; i++)
            {
                wpProps.SetWord((string)Program.face.ch_Scrap.gridChem.Rows[i].Cells[0].Value, (double)Program.face.ch_Scrap.gridChem.Rows[i].Cells[1].Value);
            }
            Program.face.ch_Scrap.gridChem.Rows.Clear();
            Program.face.ch_Scrap.gridChem.RowCount = wpTotal.Count;
            for (var rowCnt = 0;
                rowCnt < Program.face.ch_Scrap.gridChem.RowCount;
                rowCnt++)
            {
                Program.face.ch_Scrap.gridChem.Rows[rowCnt].Cells[0].Value
                    = wpTotal.ElementAt(rowCnt).Key;
                Program.face.ch_Scrap.gridChem.Rows[rowCnt].Cells[1].Value
                    = wpTotal.ElementAt(rowCnt).Value;

            }
            Program.face.ch_Scrap.m_propsStart =
                Program.face.ch_Scrap.gridChem.RowCount;
            if (wpProps.Count > 0)
            {
                Program.face.ch_Scrap.gridChem.Rows.Add(wpProps.Count);
                for (var rowCnt = Program.face.ch_Scrap.m_propsStart; rowCnt < Program.face.ch_Scrap.gridChem.RowCount; rowCnt++)
                {
                    Program.face.ch_Scrap.gridChem.Rows[rowCnt].Cells[0].Value
                        = wpProps.ElementAt(rowCnt - Program.face.ch_Scrap.m_propsStart).Key;
                    Program.face.ch_Scrap.gridChem.Rows[rowCnt].Cells[1].Value
                        = wpProps.ElementAt(rowCnt - Program.face.ch_Scrap.m_propsStart).Value;

                }
            }
            Program.face.ch_Scrap.m_readOnLoad = false;
        }

        private void gridScrap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridScrap.Rows[e.RowIndex].Cells[0].Value == null) return;
            int val = (int)gridScrap.Rows[e.RowIndex].Cells[0].Value;
            if (e.ColumnIndex == 3)
            {
                gridScrap.Rows[e.RowIndex].Cells[0].Value = ++val;
            }
            else if (e.ColumnIndex == 4)
            {
                if (val > 0) gridScrap.Rows[e.RowIndex].Cells[0].Value = --val;
            }
            //MessageBox.Show(string.Format("{0} {1} {2}", e.ColumnIndex, e.RowIndex, val));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            gridScrap.RowCount = 0;
            m_crow = 0;
            gridScrap.RowCount = 12;
            btnSave.Enabled = false;
            Program.face.ch_Scrap.m_readOnLoad = true;
        }
    }
}