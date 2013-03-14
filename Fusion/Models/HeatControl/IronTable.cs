using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HeatControl
{
    public partial class IronTable : Form
    {
        public int m_selRow = -1;
        private MixCalculator ownerFace;
        public IronTable(MixCalculator _owner)
        {
            InitializeComponent();
            ownerFace = _owner;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_selRow == -1)
            {
                MessageBox.Show("Выберите чугун из имеющихся миксеров");
                return;
            }
            MixCalculator.WordPool<double> wpIron = new MixCalculator.WordPool<double>(0.0);
            MixCalculator.WordPool<double> wpProps = new MixCalculator.WordPool<double>(0.0);
            int cntMix = 0;
            for (var i = 2; i < dgw.Rows[m_selRow].Cells.Count; i++)
            {
                string key = dgw.Columns[i].HeaderText;
                wpIron.SetWord(key, (double)dgw.Rows[m_selRow].Cells[i].Value);
            }
            for (int i = ownerFace.ch_Iron.m_propsStart; i < ownerFace.ch_Iron.gridChem.RowCount; i++)
            {
                wpProps.SetWord((string)ownerFace.ch_Iron.gridChem.Rows[i].Cells[0].Value, (double)ownerFace.ch_Iron.gridChem.Rows[i].Cells[1].Value);
            }
            ownerFace.ch_Iron.m_inFP.Clear();
            ownerFace.ch_Iron.gridChem.Rows.Clear();
            ownerFace.ch_Iron.gridChem.RowCount = wpIron.Count;
            for (var rowCnt = 0;
                rowCnt < ownerFace.ch_Iron.gridChem.RowCount;
                rowCnt++)
            {
                ownerFace.ch_Iron.gridChem.Rows[rowCnt].Cells[0].Value
                    = wpIron.ElementAt(rowCnt).Key;
                ownerFace.ch_Iron.gridChem.Rows[rowCnt].Cells[1].Value
                    = wpIron.ElementAt(rowCnt).Value;
                ownerFace.ch_Iron.m_inFP.SetWord(wpIron.ElementAt(rowCnt).Key,
                    wpIron.ElementAt(rowCnt).Value);

            }
            ownerFace.ch_Iron.m_propsStart =
                ownerFace.ch_Iron.gridChem.RowCount;
            if (wpProps.Count > 0)
            {
                ownerFace.ch_Iron.gridChem.Rows.Add(wpProps.Count);
                for (var rowCnt = ownerFace.ch_Iron.m_propsStart; rowCnt < ownerFace.ch_Iron.gridChem.RowCount; rowCnt++)
                {
                    ownerFace.ch_Iron.gridChem.Rows[rowCnt].Cells[0].Value
                        = wpProps.ElementAt(rowCnt - ownerFace.ch_Iron.m_propsStart).Key;
                    ownerFace.ch_Iron.gridChem.Rows[rowCnt].Cells[1].Value
                        = wpProps.ElementAt(rowCnt - ownerFace.ch_Iron.m_propsStart).Value;
                    ownerFace.ch_Iron.m_inFP.SetWord(wpProps.ElementAt(rowCnt - ownerFace.ch_Iron.m_propsStart).Key,
                        wpProps.ElementAt(rowCnt - ownerFace.ch_Iron.m_propsStart).Value);

                }
            }
            ownerFace.ch_Iron.m_readOnLoad = false;
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
