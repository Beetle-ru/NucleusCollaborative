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
    public partial class IronTable : Form
    {
        public int m_selRow = -1;
        public IronTable()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_selRow == -1)
            {
                MessageBox.Show("Выберите чугун из имеющихся миксеров");
                return;
            }
            WordPool<double> wpIron = new WordPool<double>(0.0);
            WordPool<double> wpProps = new WordPool<double>(0.0);
            int cntMix = 0;
            for (var i = 2; i < dgw.Rows[m_selRow].Cells.Count; i++)
            {
                string key = dgw.Columns[i].HeaderText;
                wpIron.SetWord(key, (double)dgw.Rows[m_selRow].Cells[i].Value);
            }
            for (int i = Program.face.ch_Iron.m_propsStart; i < Program.face.ch_Iron.gridChem.RowCount; i++)
            {
                wpProps.SetWord((string)Program.face.ch_Iron.gridChem.Rows[i].Cells[0].Value, (double)Program.face.ch_Iron.gridChem.Rows[i].Cells[1].Value);
            }
            Program.face.ch_Iron.m_inFP.Clear();
            Program.face.ch_Iron.gridChem.Rows.Clear();
            Program.face.ch_Iron.gridChem.RowCount = wpIron.Count;
            for (var rowCnt = 0;
                rowCnt < Program.face.ch_Iron.gridChem.RowCount;
                rowCnt++)
            {
                Program.face.ch_Iron.gridChem.Rows[rowCnt].Cells[0].Value
                    = wpIron.ElementAt(rowCnt).Key;
                Program.face.ch_Iron.gridChem.Rows[rowCnt].Cells[1].Value
                    = wpIron.ElementAt(rowCnt).Value;
                Program.face.ch_Iron.m_inFP.SetWord(wpIron.ElementAt(rowCnt).Key,
                    wpIron.ElementAt(rowCnt).Value);

            }
            Program.face.ch_Iron.m_propsStart =
                Program.face.ch_Iron.gridChem.RowCount;
            if (wpProps.Count > 0)
            {
                Program.face.ch_Iron.gridChem.Rows.Add(wpProps.Count);
                for (var rowCnt = Program.face.ch_Iron.m_propsStart; rowCnt < Program.face.ch_Iron.gridChem.RowCount; rowCnt++)
                {
                    Program.face.ch_Iron.gridChem.Rows[rowCnt].Cells[0].Value
                        = wpProps.ElementAt(rowCnt - Program.face.ch_Iron.m_propsStart).Key;
                    Program.face.ch_Iron.gridChem.Rows[rowCnt].Cells[1].Value
                        = wpProps.ElementAt(rowCnt - Program.face.ch_Iron.m_propsStart).Value;
                    Program.face.ch_Iron.m_inFP.SetWord(wpProps.ElementAt(rowCnt - Program.face.ch_Iron.m_propsStart).Key,
                        wpProps.ElementAt(rowCnt - Program.face.ch_Iron.m_propsStart).Value);

                }
            }
            Program.face.ch_Iron.m_readOnLoad = false;
            Close();
        }

        private void dgw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(String.Format("Selected {0} {1}", e.RowIndex, e.ColumnIndex));
            if (dgw.Rows[e.RowIndex].Cells[1].Value == null) return;
            if (m_selRow != -1)
            {
                dgw.Rows[m_selRow].Cells[0].Value = false;
            }
            m_selRow = e.RowIndex;
        }
    }
}
