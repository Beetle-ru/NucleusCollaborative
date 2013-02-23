﻿namespace AlgorithmsUI
{
    partial class ChemTable
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
            this.gridChem = new System.Windows.Forms.DataGridView();
            this.key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panCtrl = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.chemistryDataSet = new AlgorithmsUI.ChemistryDataSet();
            this.additionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.additionTableAdapter = new AlgorithmsUI.ChemistryDataSetTableAdapters.AdditionTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.gridChem)).BeginInit();
            this.panCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chemistryDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.additionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridChem
            // 
            this.gridChem.AllowUserToAddRows = false;
            this.gridChem.AllowUserToDeleteRows = false;
            this.gridChem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridChem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.key,
            this.value});
            this.gridChem.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.additionBindingSource, "Id", true));
            this.gridChem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChem.Location = new System.Drawing.Point(0, 0);
            this.gridChem.Name = "gridChem";
            this.gridChem.Size = new System.Drawing.Size(293, 364);
            this.gridChem.TabIndex = 0;
            this.gridChem.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.gridChem_CellBeginEdit);
            this.gridChem.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridChem_CellEndEdit);
            this.gridChem.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridChem_CellValidated);
            this.gridChem.Enter += new System.EventHandler(this.gridChem_Enter);
            // 
            // key
            // 
            this.key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.key.HeaderText = "Элемент";
            this.key.Name = "key";
            this.key.ReadOnly = true;
            this.key.Width = 76;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.HeaderText = "Значение";
            this.value.Name = "value";
            // 
            // panCtrl
            // 
            this.panCtrl.Controls.Add(this.btnSave);
            this.panCtrl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panCtrl.Location = new System.Drawing.Point(0, 283);
            this.panCtrl.Name = "panCtrl";
            this.panCtrl.Size = new System.Drawing.Size(293, 81);
            this.panCtrl.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(56, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(187, 42);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chemistryDataSet
            // 
            this.chemistryDataSet.DataSetName = "ChemistryDataSet";
            this.chemistryDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // additionBindingSource
            // 
            this.additionBindingSource.DataMember = "Addition";
            this.additionBindingSource.DataSource = this.chemistryDataSet;
            // 
            // additionTableAdapter
            // 
            this.additionTableAdapter.ClearBeforeFill = true;
            // 
            // ChemTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(293, 364);
            this.Controls.Add(this.panCtrl);
            this.Controls.Add(this.gridChem);
            this.Name = "ChemTable";
            this.Text = "chem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChemTable_FormClosing);
            this.Load += new System.EventHandler(this.ChemTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridChem)).EndInit();
            this.panCtrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chemistryDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.additionBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gridChem;
        public System.Windows.Forms.Panel panCtrl;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.DataGridViewTextBoxColumn key;
        public System.Windows.Forms.DataGridViewTextBoxColumn value;
        public ChemistryDataSet chemistryDataSet;
        private System.Windows.Forms.BindingSource additionBindingSource;
        private ChemistryDataSetTableAdapters.AdditionTableAdapter additionTableAdapter;

    }
}