namespace Emulator
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.grbInitValues = new System.Windows.Forms.GroupBox();
            this.dgvInitValues = new System.Windows.Forms.DataGridView();
            this.dgvProtocols = new System.Windows.Forms.DataGridView();
            this.grbResults = new System.Windows.Forms.GroupBox();
            this.tabControlResults = new System.Windows.Forms.TabControl();
            this.tabPageGraph = new System.Windows.Forms.TabPage();
            this.tabPageMainParams = new System.Windows.Forms.TabPage();
            this.dgvMainParams = new System.Windows.Forms.DataGridView();
            this.tabPageMatBalance = new System.Windows.Forms.TabPage();
            this.dgvMatBalance = new System.Windows.Forms.DataGridView();
            this.tabPageHeatBalance = new System.Windows.Forms.TabPage();
            this.dgvHeatBalance = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemEmulator = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemExcelExport = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMainsParams = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemMatBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHeatBalance = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemTotalParams = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemModel = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPause = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemStop = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonAll = new System.Windows.Forms.Button();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.grbInitValues.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInitValues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProtocols)).BeginInit();
            this.grbResults.SuspendLayout();
            this.tabControlResults.SuspendLayout();
            this.tabPageMainParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainParams)).BeginInit();
            this.tabPageMatBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatBalance)).BeginInit();
            this.tabPageHeatBalance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeatBalance)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 690);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 87;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // grbInitValues
            // 
            this.grbInitValues.Controls.Add(this.dgvInitValues);
            this.grbInitValues.Controls.Add(this.dgvProtocols);
            this.grbInitValues.Location = new System.Drawing.Point(12, 27);
            this.grbInitValues.Name = "grbInitValues";
            this.grbInitValues.Size = new System.Drawing.Size(263, 618);
            this.grbInitValues.TabIndex = 345;
            this.grbInitValues.TabStop = false;
            this.grbInitValues.Text = "Исходные данные";
            // 
            // dgvInitValues
            // 
            this.dgvInitValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInitValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvInitValues.Location = new System.Drawing.Point(3, 16);
            this.dgvInitValues.Name = "dgvInitValues";
            this.dgvInitValues.Size = new System.Drawing.Size(257, 228);
            this.dgvInitValues.TabIndex = 347;
            // 
            // dgvProtocols
            // 
            this.dgvProtocols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProtocols.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvProtocols.Location = new System.Drawing.Point(3, 250);
            this.dgvProtocols.Name = "dgvProtocols";
            this.dgvProtocols.RowHeadersVisible = false;
            this.dgvProtocols.Size = new System.Drawing.Size(257, 365);
            this.dgvProtocols.TabIndex = 346;
            // 
            // grbResults
            // 
            this.grbResults.Controls.Add(this.tabControlResults);
            this.grbResults.Location = new System.Drawing.Point(281, 27);
            this.grbResults.Name = "grbResults";
            this.grbResults.Size = new System.Drawing.Size(891, 618);
            this.grbResults.TabIndex = 346;
            this.grbResults.TabStop = false;
            this.grbResults.Text = "Результаты расчета";
            // 
            // tabControlResults
            // 
            this.tabControlResults.Controls.Add(this.tabPageGraph);
            this.tabControlResults.Controls.Add(this.tabPageMainParams);
            this.tabControlResults.Controls.Add(this.tabPageMatBalance);
            this.tabControlResults.Controls.Add(this.tabPageHeatBalance);
            this.tabControlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlResults.Location = new System.Drawing.Point(3, 16);
            this.tabControlResults.Name = "tabControlResults";
            this.tabControlResults.SelectedIndex = 0;
            this.tabControlResults.Size = new System.Drawing.Size(885, 599);
            this.tabControlResults.TabIndex = 0;
            // 
            // tabPageGraph
            // 
            this.tabPageGraph.Location = new System.Drawing.Point(4, 22);
            this.tabPageGraph.Name = "tabPageGraph";
            this.tabPageGraph.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGraph.Size = new System.Drawing.Size(877, 573);
            this.tabPageGraph.TabIndex = 0;
            this.tabPageGraph.Text = "Графики";
            this.tabPageGraph.UseVisualStyleBackColor = true;
            // 
            // tabPageMainParams
            // 
            this.tabPageMainParams.Controls.Add(this.dgvMainParams);
            this.tabPageMainParams.Location = new System.Drawing.Point(4, 22);
            this.tabPageMainParams.Name = "tabPageMainParams";
            this.tabPageMainParams.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMainParams.Size = new System.Drawing.Size(877, 573);
            this.tabPageMainParams.TabIndex = 1;
            this.tabPageMainParams.Text = "Основные показатели";
            this.tabPageMainParams.UseVisualStyleBackColor = true;
            // 
            // dgvMainParams
            // 
            this.dgvMainParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainParams.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMainParams.Location = new System.Drawing.Point(3, 3);
            this.dgvMainParams.Name = "dgvMainParams";
            this.dgvMainParams.RowHeadersVisible = false;
            this.dgvMainParams.Size = new System.Drawing.Size(871, 567);
            this.dgvMainParams.TabIndex = 1;
            // 
            // tabPageMatBalance
            // 
            this.tabPageMatBalance.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.tabPageMatBalance.Controls.Add(this.dgvMatBalance);
            this.tabPageMatBalance.Location = new System.Drawing.Point(4, 22);
            this.tabPageMatBalance.Name = "tabPageMatBalance";
            this.tabPageMatBalance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatBalance.Size = new System.Drawing.Size(877, 573);
            this.tabPageMatBalance.TabIndex = 2;
            this.tabPageMatBalance.Text = "Материальный баланс";
            // 
            // dgvMatBalance
            // 
            this.dgvMatBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatBalance.Location = new System.Drawing.Point(3, 3);
            this.dgvMatBalance.Name = "dgvMatBalance";
            this.dgvMatBalance.Size = new System.Drawing.Size(871, 567);
            this.dgvMatBalance.TabIndex = 0;
            // 
            // tabPageHeatBalance
            // 
            this.tabPageHeatBalance.Controls.Add(this.dgvHeatBalance);
            this.tabPageHeatBalance.Location = new System.Drawing.Point(4, 22);
            this.tabPageHeatBalance.Name = "tabPageHeatBalance";
            this.tabPageHeatBalance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHeatBalance.Size = new System.Drawing.Size(877, 573);
            this.tabPageHeatBalance.TabIndex = 3;
            this.tabPageHeatBalance.Text = "Тепловой баланс";
            this.tabPageHeatBalance.UseVisualStyleBackColor = true;
            // 
            // dgvHeatBalance
            // 
            this.dgvHeatBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHeatBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHeatBalance.Location = new System.Drawing.Point(3, 3);
            this.dgvHeatBalance.Name = "dgvHeatBalance";
            this.dgvHeatBalance.Size = new System.Drawing.Size(871, 567);
            this.dgvHeatBalance.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemEmulator,
            this.ToolStripMenuItemModel});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 347;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemEmulator
            // 
            this.ToolStripMenuItemEmulator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOpenFile,
            this.toolStripSeparator1,
            this.ToolStripMenuItemExcelExport,
            this.toolStripSeparator2,
            this.ToolStripMenuItemExit});
            this.ToolStripMenuItemEmulator.Name = "ToolStripMenuItemEmulator";
            this.ToolStripMenuItemEmulator.Size = new System.Drawing.Size(74, 20);
            this.ToolStripMenuItemEmulator.Text = "Имитатор";
            // 
            // ToolStripMenuItemOpenFile
            // 
            this.ToolStripMenuItemOpenFile.Name = "ToolStripMenuItemOpenFile";
            this.ToolStripMenuItemOpenFile.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItemOpenFile.Text = "Открыть файл...";
            this.ToolStripMenuItemOpenFile.Click += new System.EventHandler(this.ToolStripMenuItemOpenFileClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // ToolStripMenuItemExcelExport
            // 
            this.ToolStripMenuItemExcelExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemMainsParams,
            this.ToolStripMenuItemMatBalance,
            this.ToolStripMenuItemHeatBalance,
            this.ToolStripMenuItemTotalParams});
            this.ToolStripMenuItemExcelExport.Name = "ToolStripMenuItemExcelExport";
            this.ToolStripMenuItemExcelExport.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItemExcelExport.Text = "Сохранить в Excel";
            // 
            // ToolStripMenuItemMainsParams
            // 
            this.ToolStripMenuItemMainsParams.Name = "ToolStripMenuItemMainsParams";
            this.ToolStripMenuItemMainsParams.Size = new System.Drawing.Size(200, 22);
            this.ToolStripMenuItemMainsParams.Text = "Основные показатели";
            this.ToolStripMenuItemMainsParams.Click += new System.EventHandler(this.ToolStripMenuItemMainParamsClick);
            // 
            // ToolStripMenuItemMatBalance
            // 
            this.ToolStripMenuItemMatBalance.Name = "ToolStripMenuItemMatBalance";
            this.ToolStripMenuItemMatBalance.Size = new System.Drawing.Size(200, 22);
            this.ToolStripMenuItemMatBalance.Text = "Материальный баланс";
            // 
            // ToolStripMenuItemHeatBalance
            // 
            this.ToolStripMenuItemHeatBalance.Name = "ToolStripMenuItemHeatBalance";
            this.ToolStripMenuItemHeatBalance.Size = new System.Drawing.Size(200, 22);
            this.ToolStripMenuItemHeatBalance.Text = "Тепловой баланс";
            // 
            // ToolStripMenuItemTotalParams
            // 
            this.ToolStripMenuItemTotalParams.Name = "ToolStripMenuItemTotalParams";
            this.ToolStripMenuItemTotalParams.Size = new System.Drawing.Size(200, 22);
            this.ToolStripMenuItemTotalParams.Text = "Все параметры";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // ToolStripMenuItemExit
            // 
            this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            this.ToolStripMenuItemExit.Size = new System.Drawing.Size(170, 22);
            this.ToolStripMenuItemExit.Text = "Выход";
            this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExitClick);
            // 
            // ToolStripMenuItemModel
            // 
            this.ToolStripMenuItemModel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemStart,
            this.ToolStripMenuItemPause,
            this.ToolStripMenuItemStop});
            this.ToolStripMenuItemModel.Name = "ToolStripMenuItemModel";
            this.ToolStripMenuItemModel.Size = new System.Drawing.Size(62, 20);
            this.ToolStripMenuItemModel.Text = "Модель";
            // 
            // ToolStripMenuItemStart
            // 
            this.ToolStripMenuItemStart.Name = "ToolStripMenuItemStart";
            this.ToolStripMenuItemStart.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemStart.Text = "Запуск";
            this.ToolStripMenuItemStart.Click += new System.EventHandler(this.ToolStripMenuItemStartClick);
            // 
            // ToolStripMenuItemPause
            // 
            this.ToolStripMenuItemPause.Name = "ToolStripMenuItemPause";
            this.ToolStripMenuItemPause.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemPause.Text = "Пауза";
            this.ToolStripMenuItemPause.Click += new System.EventHandler(this.ToolStripMenuItemPauseClick);
            // 
            // ToolStripMenuItemStop
            // 
            this.ToolStripMenuItemStop.Name = "ToolStripMenuItemStop";
            this.ToolStripMenuItemStop.Size = new System.Drawing.Size(112, 22);
            this.ToolStripMenuItemStop.Text = "Стоп";
            this.ToolStripMenuItemStop.Click += new System.EventHandler(this.ToolStripMenuItemStopClick);
            // 
            // ButtonAll
            // 
            this.ButtonAll.Location = new System.Drawing.Point(210, 648);
            this.ButtonAll.Name = "ButtonAll";
            this.ButtonAll.Size = new System.Drawing.Size(62, 23);
            this.ButtonAll.TabIndex = 348;
            this.ButtonAll.Text = "Все";
            this.ButtonAll.UseVisualStyleBackColor = true;
            this.ButtonAll.Click += new System.EventHandler(this.ButtonAllClick);
            // 
            // ButtonNext
            // 
            this.ButtonNext.Location = new System.Drawing.Point(129, 648);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(75, 23);
            this.ButtonNext.TabIndex = 349;
            this.ButtonNext.Text = "Расчитать";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNextClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 712);
            this.Controls.Add(this.ButtonNext);
            this.Controls.Add(this.ButtonAll);
            this.Controls.Add(this.grbResults);
            this.Controls.Add(this.grbInitValues);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1200, 750);
            this.MinimumSize = new System.Drawing.Size(16, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Имитатор";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grbInitValues.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInitValues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProtocols)).EndInit();
            this.grbResults.ResumeLayout(false);
            this.tabControlResults.ResumeLayout(false);
            this.tabPageMainParams.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainParams)).EndInit();
            this.tabPageMatBalance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatBalance)).EndInit();
            this.tabPageHeatBalance.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHeatBalance)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.GroupBox grbInitValues;
        private System.Windows.Forms.DataGridView dgvProtocols;
        private System.Windows.Forms.GroupBox grbResults;
        private System.Windows.Forms.TabControl tabControlResults;
        private System.Windows.Forms.TabPage tabPageGraph;
        private System.Windows.Forms.TabPage tabPageMainParams;
        private System.Windows.Forms.TabPage tabPageMatBalance;
        private System.Windows.Forms.TabPage tabPageHeatBalance;
        private System.Windows.Forms.DataGridView dgvMainParams;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemEmulator;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExcelExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMainsParams;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemMatBalance;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHeatBalance;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemTotalParams;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemModel;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStart;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPause;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemStop;
        private System.Windows.Forms.DataGridView dgvMatBalance;
        private System.Windows.Forms.DataGridView dgvHeatBalance;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.Button ButtonAll;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.DataGridView dgvInitValues;
    }
}

