using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Emulator
{
    public delegate void Helper(TextBox textbox, int rCnt, int cCnt);

    public partial class MainForm : Form
    {
        private DataTable _dtProtocol;
        private List<int> _listHib;
        private List<int> _listHie;
        private List<int> _listStTapB;
        private List<int> _listStTapE;
        private List<int> _listSlAddB;
        private List<int> _listSlAddE;
        
        private Model _model;
        private readonly Excel _excel;
        private int _rowNum;
        private int _rowCount;

        public MainForm()
        {
            InitializeComponent();
            _excel = new Excel();
            _listHib = new List<int>();
            _listHie = new List<int>();
            _listStTapB = new List<int>();
            _listStTapE = new List<int>();
            _listSlAddB = new List<int>();
            _listSlAddE = new List<int>();
            SetEnabledMenuItems(1);
            ButtonNext.Enabled = false;
            DgvInitValuesInit();
            DgvProtocolsInit();
            DgvMainParamsInit();
            DgvMatBalanceInit();
            DgvHeatBalanceInit();
        }

        #region Подготовка данных

        private Model InitModel()
        {
            var CCH = CheckValuesToDouble(dgvInitValues.Rows[0].Cells[1].Value.ToString());
            var CFeH = CheckValuesToDouble(dgvInitValues.Rows[1].Cells[1].Value.ToString());
            var CMnH = CheckValuesToDouble(dgvInitValues.Rows[2].Cells[1].Value.ToString());
            var CSiH = CheckValuesToDouble(dgvInitValues.Rows[3].Cells[1].Value.ToString());
            var COH = CheckValuesToDouble(dgvInitValues.Rows[4].Cells[1].Value.ToString());
            var CFeOH = CheckValuesToDouble(dgvInitValues.Rows[5].Cells[1].Value.ToString());
            var CMnOH = CheckValuesToDouble(dgvInitValues.Rows[6].Cells[1].Value.ToString());
            var CSiO2H = CheckValuesToDouble(dgvInitValues.Rows[7].Cells[1].Value.ToString());
            var CCaOH = CheckValuesToDouble(dgvInitValues.Rows[8].Cells[1].Value.ToString());
            var prt = GetProtocol();
            return new Model(prt, CCH, CFeH, CMnH, CSiH, COH, CFeOH, CMnOH, CSiO2H, CCaOH);
        }

        private static string CheckValues(string val)
        {
            return val == "" || val == "NULL" ? null : val;
        }

        private static double CheckValuesToDouble(string val)
        {
            return Convert.ToDouble(CheckValues(val));
        }
        private static int CheckValuesToInt(string val)
        {
            return Convert.ToInt32(CheckValues(val));
        }

        private Protocol GetProtocol()
        {
            var prt = new Protocol();
            prt.dt = CheckValuesToInt(_dtProtocol.Rows[5][2].ToString()) - CheckValuesToInt(_dtProtocol.Rows[4][2].ToString());
            prt.i = CheckValuesToInt(_dtProtocol.Rows[_rowNum][1].ToString());
            prt.t = CheckValuesToInt(_dtProtocol.Rows[_rowNum][2].ToString());
            prt.We = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][3].ToString());
            prt.UCH4B = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][4].ToString());
            prt.UO2B = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][5].ToString());
            prt.UCH4RB = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][6].ToString());
            prt.UO2RB = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][7].ToString());
            prt.UO2RL = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][8].ToString());
            prt.UO2L = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][9].ToString());
            prt.mCP = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][10].ToString());
            prt.mCh1 = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][11].ToString());
            prt.mCh2S = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][12].ToString());
            prt.mHI = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][13].ToString());
            prt.mHISl = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][14].ToString());
            prt.mCh1SN = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][15].ToString());
            prt.THI = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][16].ToString());
            prt.mCkAdd = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][17].ToString());
            prt.mLmAdd = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][18].ToString());
            prt.mDlmtAdd = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][19].ToString());
            prt.mStTap = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][20].ToString());
            prt.mSlTap = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][21].ToString());
            prt.mSlTip = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][22].ToString());
            prt.Tenv = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][23].ToString());
            prt.TWout = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][24].ToString());
            prt.TWin = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][25].ToString());
            prt.UCWW = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][26].ToString());
            prt.TFout = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][27].ToString());
            prt.TFin = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][28].ToString());
            prt.UCWF = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][29].ToString());
            prt.TRout = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][30].ToString());
            prt.TRin = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][31].ToString());
            prt.UCWR = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][32].ToString());
            prt.TSout = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][33].ToString());
            prt.TSin = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][34].ToString());
            prt.UCWS = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][35].ToString());
            prt.TS = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][36].ToString());
            prt.mAir = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][37].ToString());

            prt.HIB = CheckValuesToInt(_dtProtocol.Rows[_rowNum][38].ToString());
            prt.HIE = CheckValuesToInt(_dtProtocol.Rows[_rowNum][38].ToString());
            if (_listHib.Count > 0 && _listHie.Count > 0)
            {
                if (prt.i >= _listHib[0] && prt.i <= _listHie[0])
                {
                    prt.HIB = _listHib[0];
                    prt.HIE = _listHie[0];
                    if (prt.i == _listHie[0])
                    {
                        _listHib.Remove(0);
                        _listHie.Remove(0);
                    }
                }
            }

            prt.Ch1 = CheckValuesToInt(_dtProtocol.Rows[_rowNum][40].ToString());
            prt.Ch2S = CheckValuesToInt(_dtProtocol.Rows[_rowNum][41].ToString());
            prt.Ch1SN = CheckValuesToInt(_dtProtocol.Rows[_rowNum][42].ToString());
            
            prt.StTapB = CheckValuesToInt(_dtProtocol.Rows[_rowNum][43].ToString());
            prt.StTapE = CheckValuesToInt(_dtProtocol.Rows[_rowNum][44].ToString());
            if (_listStTapB.Count > 0 && _listStTapE.Count > 0)
            {
                if (prt.i >= _listStTapB[0] && prt.i <= _listStTapE[0])
                {
                    prt.StTapB = _listStTapB[0];
                    prt.StTapE = _listStTapE[0];
                    if (prt.i == _listStTapE[0])
                    {
                        _listStTapB.Remove(0);
                        _listStTapE.Remove(0);
                    }
                }
                
            }
            prt.SlAddB = CheckValuesToInt(_dtProtocol.Rows[_rowNum][45].ToString());
            prt.SlAddE = CheckValuesToInt(_dtProtocol.Rows[_rowNum][46].ToString());
            if (_listSlAddB.Count > 0 && _listSlAddE.Count > 0)
            {
                if (prt.i >= _listSlAddB[0] && prt.i <= _listSlAddE[0])
                {
                    prt.SlAddB = _listSlAddB[0];
                    prt.SlAddE = _listSlAddE[0];
                    if (prt.i == _listSlAddE[0])
                    {
                        _listSlAddB.Remove(0);
                        _listSlAddE.Remove(0);
                    }
                }

            }
            prt.mStH = CheckValuesToDouble(_dtProtocol.Rows[4][47].ToString());
            prt.mSlH = CheckValuesToDouble(_dtProtocol.Rows[4][48].ToString());
            prt.TH = CheckValuesToDouble(_dtProtocol.Rows[4][49].ToString());
            prt.WCh1SP = CheckValuesToDouble(_dtProtocol.Rows[4][50].ToString());
            prt.OCSD = CheckValuesToDouble(_dtProtocol.Rows[_rowNum][51].ToString());
            return prt;
        }


        private void SetParamsLists()
        {
            _listHib.Clear();
            _listHie.Clear();
            _listStTapB.Clear();
            _listStTapE.Clear();
            _listSlAddB.Clear();
            _listSlAddE.Clear();
            _listHib = GetList(38);
            _listHie = GetList(39);
            _listStTapB = GetList(43);
            _listStTapE = GetList(44);
            _listSlAddB = GetList(45);
            _listSlAddE = GetList(46);
        }

        private List<int> GetList(int colNum)
        {
            var list = new List<int>();
            for (var i = 4; i <= _rowCount; i++)
            {
                var d = _dtProtocol.Rows;
                var s = d[i];
                var t = s[colNum];
                var e = t.ToString();
                var val = CheckValuesToInt(_dtProtocol.Rows[i][colNum].ToString());
                if (val != 0)
                {
                    list.Add(val);
                }
            }
            return list;

        }

        #endregion


        #region Заполение гридов

        private void FillProtocolDataGrid()
        {
            for (var i = 0; i < dgvProtocols.Rows.Count; i++)
            {
                var value = _dtProtocol.Rows[_rowNum][i + 1].ToString();
                dgvProtocols.Rows[i].Cells[1].Value = value;
            }
        }

        private void FilldvgMailParams(Currents res)
        {
            var row = _dtProtocol.Rows[_rowNum];
            dgvMainParams.Rows.Add(new[] { row[1].ToString(), row[2].ToString(), res.CnsEE.ToString(), res.CnsO2L.ToString(), res.CnsO2B.ToString(),
                res.CnsCH4.ToString(), res.CnsScr.ToString(), res.CnsHI.ToString(), res.CnsCP.ToString(), res.CnsLm.ToString(), res.CnsDlmt.ToString(), 
                res.CnsCk.ToString(), res.mSt.ToString(), res.CC.ToString(), res.CSi.ToString(), res.CMn.ToString(), res.CO.ToString(), res.mSl.ToString(),
                res.CCaO.ToString(), res.CSiO2.ToString(), res.CMnO.ToString(), res.CFeO.ToString(), res.Tav.ToString() });
        }

        #endregion
        
        #region Инициализация гридов

        private void DgvMainInit(DataGridView dataGridView, IList<string> columns)
        {
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dgvMainParams.Font, FontStyle.Bold);
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ColumnCount = columns.Count;
            for (var i = 0; i < dataGridView.ColumnCount; i++)
            {
                dataGridView.Columns[i].Name = columns[i];
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns[i].ReadOnly = true;
                dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void DgvMainInitExtended(DataGridView dataGridView, IList<string> columns, IList<string> inputParams, IList<string> outputParams)
        {
            DgvMainInit(dataGridView, columns);
            dataGridView.ScrollBars = ScrollBars.None;
            for (var i = 0; i < dataGridView.Columns.Count; i++)
            {
                if ( i == 0 || i == 3 ) continue;
                dataGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            for (var i = 0; i < inputParams.Count; i++)
            {
                if (i < outputParams.Count)
                {
                    dataGridView.Rows.Add(inputParams[i], "", "", outputParams[i], "", "");
                }
                else
                {
                    if (inputParams[i] == "Всего")
                    {
                        dataGridView.Rows.Add(inputParams[i], "", "100", inputParams[i], "", "100");
                        dataGridView.Rows[i].DefaultCellStyle.Font = new Font(dataGridView.Font, FontStyle.Bold);
                    }
                    else
                    {
                        dataGridView.Rows.Add(inputParams[i], "", "", "", "", "");
                    }
                }
            }

        }

        private void DgvInitValuesInit()
        {
            var cNames = new[] { "Параметр", "Значение" };
            var strInput = new[] { "CCH", "CFeH", "CMnH", "CSiH", "COH", "CFeOH", "CMnOH", "CSiO2H", "CCaOH"};
            DgvMainInitExtended(dgvInitValues, cNames, strInput, new List<string>());
            dgvInitValues.ScrollBars = ScrollBars.Both;
            dgvInitValues.Columns[1].ReadOnly = false;
            dgvInitValues.Rows[0].Cells[1].Value = Constants.CCH;
            dgvInitValues.Rows[1].Cells[1].Value = Constants.CFeH;
            dgvInitValues.Rows[2].Cells[1].Value = Constants.CMnH;
            dgvInitValues.Rows[3].Cells[1].Value = Constants.CSiH;
            dgvInitValues.Rows[4].Cells[1].Value = Constants.COH;
            dgvInitValues.Rows[5].Cells[1].Value = Constants.CFeOH;
            dgvInitValues.Rows[6].Cells[1].Value = Constants.CMnOH;
            dgvInitValues.Rows[7].Cells[1].Value = Constants.CSiO2H;
            dgvInitValues.Rows[8].Cells[1].Value = Constants.CCaOH;
        }
        
        private void DgvProtocolsInit()
        {
            var cNames = new[] {"Параметр", "Значение"};
            var strInput = new[] { "i", "t, c", "We, МВт", "UCH4B, м3/ч", "UO2B, м3/ч", "UCH4RB, м3/ч", "UO2RB, м3/ч", "UO2RL, м3/ч", "UO2L, кг/мин", "mCP, кг/мин",
                "mCh1, т", "mCh2S, т", "mHI, т", "mHISl, т", "mCh1SN, т", "THI, С", "mCkAdd, кг", "mLmAdd, кг", "mDlmtAdd, кг", "mStTap, кг/с", "mSlTap, кг/с", "mSlTip, кг/с",
                "Tenv, C", "TWout, С", "TWin, С", "UCWW, м3/ч", "TFout, С", "TFin, С", "UCWF, м3/ч", "TRout, С", "TRin, С", "UCWR, м3/ч", "TSout, С", "TSin, С", "UCWS, м3/ч", "TS, С", 
                "mAir, кг/с", "HIB", "HIE", "Ch2","Ch2S", "Ch1SN", "StTapB", "StTapE", "SlAddB", "SlAddE", "mStH, кг", "mSlH, кг", "TH, С", "WCh1SP, МДж", "OCSD" }; 
            DgvMainInitExtended(dgvProtocols, cNames, strInput, new List<string>());
            dgvProtocols.ScrollBars = ScrollBars.Both;
        }

        private void DgvMainParamsInit()
        {
            var cNames = new[] { "i", "t, c", "CnsEE, кВт·ч", "CnsO2L, м3", "CnsO2B, м3", "CnsCH4, м3", "CnsScr, т", "CnsHI, т", "CnsCP, кг", "CnsLm, кг", "CnsDlmt, кг", "CnsCk, кг", 
                "mSt, т", "CC, %", "CSi, %", "CMn, %", "CO, %", "mSl, т", "CCaO, %", "CSiO2, %", "CMnO, %", "CFeO, %", "Tav, С" };
            DgvMainInit(dgvMainParams, cNames);
        }
        
        private void DgvMatBalanceInit()
        {
            var cNames = new[] { "Приходные статьи", "кг", "%", "Расходные статьи", "кг", "%" };
            var strInput = new[] { "Лом (по группам)", "Болото (металл)", "Болото (шлак)", "Чугун жидкий", "Известь", "Доломит", "Кокс завалки", "Угольный порошок",
                "Заправочный материал", "Кислород", "Природный газ", "Электроды", "Воздух", "Всего", "Невязка абс., кг","Невязка отн., %","Выход годного" };
            var strOutput = new[] { "Металл (выпуск)", "Шлак (слив)", "Болото (металл)", "Болото (шлак)", "Технологические газы" };
            DgvMainInitExtended(dgvMatBalance, cNames, strInput, strOutput);
        }

        private void DgvHeatBalanceInit()
        {
            var cNames = new[] { "Приходные статьи", "МДж", "%", "Расходные статьи", "МДж", "%" };
            var strInput = new[] {"Физическое тепло материалов, в т.ч.:", "  лом завалки", "  лом подвалки", "  болото (металл)", "  болото (шлак)", "  чугун жидкий", 
                "  известь", "  доломит", "  кокс завалки", "  угольный порошок", "  магнезит", "  кислород", "  природный газ", "  электроды", "  воздух", 
                "Энергия экзотермических реакций, в т.ч.:", "  окисление элементов металла", "  горение природного газа", "  окисление электродов", 
                "Электрическая энергия", "Всего", "Невязка абс., МДж", "Невязка отн., %", "Тепловой КПД, %", "Общий КПД, %"};
            var strOutput = new[] { "Физическое тепло продуктов плавки, в т.ч.:", "  металл (выпуск)", "  шлак (слив)", "  болото (металл)", "  болото (шлак)", "  лом в шахте", 
                "Тепловые потери, в т.ч.:", "  корпус печи", "  свод", "  электроды", "  шахта", "  охлаждающая вода", "  технологические газы", "Электрические потери"};
            DgvMainInitExtended(dgvHeatBalance, cNames, strInput, strOutput);
        }

        #endregion
        
        #region Меню Иммитатор
        
        /// <summary>
        /// Открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemOpenFileClick(object sender, EventArgs e)
        {
            dgvMainParams.Rows.Clear();
            var openFile = new OpenFileDialog();
            if (openFile.ShowDialog() != DialogResult.OK) return;
            _dtProtocol = _excel.Read(openFile.FileName);
            _rowNum = 4;
            _rowCount = _dtProtocol.Rows.Count - 1;
            SetParamsLists();
            FillProtocolDataGrid();
            _model = InitModel();
            toolStripStatusLabel1.Text = String.Format("Загружен файл: {0}", openFile.FileName);
            EnabledButtons();
            dgvInitValues.Columns[1].ReadOnly = true;
        }

        /// <summary>
        /// подготовка данных для сохранения 
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private static DataTable DataGridToDataTable(DataGridView dataGridView)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                dt.Columns.Add(col.Name);
            }
            foreach (DataGridViewRow gridRow in dataGridView.Rows)
            {
                if (gridRow.IsNewRow) continue;
                var dtRow = dt.NewRow();
                for (var i = 0; i < dataGridView.Columns.Count; i++)
                {
                    dtRow[i] = (gridRow.Cells[i].Value ?? DBNull.Value);
                }
                dt.Rows.Add(dtRow);
            }
            return dt;
        }

        /// <summary>
        /// Сохранить основные параметры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemMainParamsClick(object sender, EventArgs e)
        {
            var saveFile = new SaveFileDialog
            {
                Filter = "Excel files (*.xls)|*.xls", 
                DefaultExt = "xls", 
                RestoreDirectory = true
            };

            if (saveFile.ShowDialog() != DialogResult.OK) return;
            if (_excel.SaveAs(saveFile.FileName, DataGridToDataTable(dgvMainParams)))
            {
                MessageBox.Show("Файл " + saveFile.FileName + " успешно сохранен!");
            }
            else
            {
                MessageBox.Show("Ошибка при сохранении файла!");
            }
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemExitClick(object sender, EventArgs e)
        {
            ToolStripMenuItemStopClick(sender, e);
            Close();
        }

        #endregion

        #region Меню модель

        private void SetEnabledMenuItems(int itemsIndex)
        {
            ToolStripMenuItemStart.Enabled = itemsIndex == 1 || itemsIndex == 2;
            ToolStripMenuItemPause.Enabled = itemsIndex == 3;
            ToolStripMenuItemStop.Enabled = itemsIndex == 3 || itemsIndex == 2;
        }

        private void ToolStripMenuItemStartClick(object sender, EventArgs e)
        {
            SetEnabledMenuItems(3);
        }

        private void ToolStripMenuItemPauseClick(object sender, EventArgs e)
        {
            SetEnabledMenuItems(2);
        }

        private void ToolStripMenuItemStopClick(object sender, EventArgs e)
        {
            SetEnabledMenuItems(1);
        }

        #endregion

        #region Другие элементы управления

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            ToolStripMenuItemStopClick(sender, e);
        }

        private void ButtonAllClick(object sender, EventArgs e)
        {
            while (ButtonNext.Enabled)
            {
                ButtonNextClick(sender, e); 
            }
        }

        private void ButtonNextClick(object sender, EventArgs e)
        {
            _rowNum++;
            if (_rowNum > _rowCount)
            {
                _rowNum--;
                return;
            }
            FillProtocolDataGrid();
            var prt = GetProtocol();
            var res = _model.GetMainParamsHeat(prt);
            FilldvgMailParams(res);
            EnabledButtons();
        }

      
        private void EnabledButtons()
        {
            ButtonNext.Enabled = _rowNum < _rowCount;
        }

        #endregion

    }
}
