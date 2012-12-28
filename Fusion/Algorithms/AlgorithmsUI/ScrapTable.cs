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
            var txt = cmbScrap.Items[cmbScrap.SelectedIndex].ToString();
            txt = cmbScrap.SelectedItem.ToString();
            txt = cmbScrap.Tag.ToString() + " | " + cmbScrap.Text;
            //MessageBox.Show(txt);
            gridScrap.Rows[m_crow].Cells[0].Value = 1;
            gridScrap.Rows[m_crow].Cells[1].Value = txt;
            //(gridScrap.Rows[m_crow].Cells[2].Value as Button).Text = "++";
            //(gridScrap.Rows[m_crow].Cells[3].Value as Button).Text = "--";
            if (++m_crow == gridScrap.RowCount) btnAddScrap.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void gridScrap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(string.Format("{0} {1}", e.ColumnIndex, e.RowIndex));
        }
    }
}