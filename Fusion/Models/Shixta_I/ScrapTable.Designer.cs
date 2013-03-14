namespace AlgorithmsUI
{
    partial class ScrapTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panSelector = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnAddScrap = new System.Windows.Forms.Button();
            this.lblScrapSelector = new System.Windows.Forms.Label();
            this.cmbScrap = new System.Windows.Forms.ComboBox();
            this.scrapBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.scrapDataSet = new AlgorithmsUI.ScrapDataSet();
            this.btnSave = new System.Windows.Forms.Button();
            this.scrapTableAdapter = new AlgorithmsUI.ScrapDataSetTableAdapters.ScrapTableAdapter();
            this.gridScrap = new System.Windows.Forms.DataGridView();
            this.unitCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scrapCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pp = new System.Windows.Forms.DataGridViewButtonColumn();
            this.mm = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrapBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrapDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridScrap)).BeginInit();
            this.SuspendLayout();
            // 
            // panSelector
            // 
            this.panSelector.Controls.Add(this.btnClear);
            this.panSelector.Controls.Add(this.btnAddScrap);
            this.panSelector.Controls.Add(this.lblScrapSelector);
            this.panSelector.Controls.Add(this.cmbScrap);
            this.panSelector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panSelector.Location = new System.Drawing.Point(0, 375);
            this.panSelector.Name = "panSelector";
            this.panSelector.Size = new System.Drawing.Size(834, 100);
            this.panSelector.TabIndex = 0;
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClear.Location = new System.Drawing.Point(608, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(213, 68);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAddScrap
            // 
            this.btnAddScrap.Location = new System.Drawing.Point(200, 23);
            this.btnAddScrap.Name = "btnAddScrap";
            this.btnAddScrap.Size = new System.Drawing.Size(75, 25);
            this.btnAddScrap.TabIndex = 4;
            this.btnAddScrap.Text = "добавить";
            this.btnAddScrap.UseVisualStyleBackColor = true;
            this.btnAddScrap.Click += new System.EventHandler(this.btnAddScrap_Click);
            // 
            // lblScrapSelector
            // 
            this.lblScrapSelector.AutoSize = true;
            this.lblScrapSelector.Location = new System.Drawing.Point(33, 29);
            this.lblScrapSelector.Name = "lblScrapSelector";
            this.lblScrapSelector.Size = new System.Drawing.Size(161, 13);
            this.lblScrapSelector.TabIndex = 3;
            this.lblScrapSelector.Text = "Металлолом указанного типа:";
            // 
            // cmbScrap
            // 
            this.cmbScrap.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.scrapBindingSource, "Description", true));
            this.cmbScrap.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.scrapBindingSource, "Description", true));
            this.cmbScrap.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.scrapBindingSource, "Code", true));
            this.cmbScrap.DataSource = this.scrapBindingSource;
            this.cmbScrap.DisplayMember = "Description";
            this.cmbScrap.FormattingEnabled = true;
            this.cmbScrap.Location = new System.Drawing.Point(34, 54);
            this.cmbScrap.Name = "cmbScrap";
            this.cmbScrap.Size = new System.Drawing.Size(373, 21);
            this.cmbScrap.TabIndex = 2;
            this.cmbScrap.ValueMember = "Id";
            // 
            // scrapBindingSource
            // 
            this.scrapBindingSource.DataMember = "Scrap";
            this.scrapBindingSource.DataSource = this.scrapDataSet;
            // 
            // scrapDataSet
            // 
            this.scrapDataSet.DataSetName = "ScrapDataSet";
            this.scrapDataSet.EnforceConstraints = false;
            this.scrapDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(123, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(583, 49);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // scrapTableAdapter
            // 
            this.scrapTableAdapter.ClearBeforeFill = true;
            // 
            // gridScrap
            // 
            this.gridScrap.AllowUserToAddRows = false;
            this.gridScrap.AllowUserToDeleteRows = false;
            this.gridScrap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridScrap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.unitCount,
            this.scrapCode,
            this.text,
            this.pp,
            this.mm});
            this.gridScrap.Location = new System.Drawing.Point(12, 80);
            this.gridScrap.Name = "gridScrap";
            this.gridScrap.Size = new System.Drawing.Size(810, 289);
            this.gridScrap.TabIndex = 3;
            this.gridScrap.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridScrap_CellContentClick);
            // 
            // unitCount
            // 
            this.unitCount.HeaderText = "Единиц";
            this.unitCount.Name = "unitCount";
            this.unitCount.ReadOnly = true;
            // 
            // scrapCode
            // 
            this.scrapCode.HeaderText = "Код";
            this.scrapCode.Name = "scrapCode";
            // 
            // text
            // 
            this.text.HeaderText = "Описание";
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.Width = 400;
            // 
            // pp
            // 
            this.pp.HeaderText = "Добавить";
            this.pp.Name = "pp";
            this.pp.Text = "+";
            this.pp.UseColumnTextForButtonValue = true;
            this.pp.Width = 70;
            // 
            // mm
            // 
            this.mm.HeaderText = "Уменьшить";
            this.mm.Name = "mm";
            this.mm.Text = "-";
            this.mm.UseColumnTextForButtonValue = true;
            this.mm.Width = 70;
            // 
            // ScrapTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 475);
            this.Controls.Add(this.gridScrap);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panSelector);
            this.Name = "ScrapTable";
            this.Text = "Таблица металлолома";
            this.Load += new System.EventHandler(this.ScrapTable_Load);
            this.panSelector.ResumeLayout(false);
            this.panSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scrapBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scrapDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridScrap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panSelector;
        private System.Windows.Forms.Label lblScrapSelector;
        private System.Windows.Forms.ComboBox cmbScrap;
        private System.Windows.Forms.Button btnAddScrap;
        private System.Windows.Forms.Button btnSave;
        private ScrapDataSet scrapDataSet;
        private System.Windows.Forms.BindingSource scrapBindingSource;
        private ScrapDataSetTableAdapters.ScrapTableAdapter scrapTableAdapter;
        private System.Windows.Forms.DataGridView gridScrap;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn scrapCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
        private System.Windows.Forms.DataGridViewButtonColumn pp;
        private System.Windows.Forms.DataGridViewButtonColumn mm;
        private System.Windows.Forms.Button btnClear;
    }
}