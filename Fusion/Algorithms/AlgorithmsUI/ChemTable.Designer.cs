namespace AlgorithmsUI
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
            this.gridChem = new System.Windows.Forms.DataGridView();
            this.key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridChem)).BeginInit();
            this.SuspendLayout();
            // 
            // gridChem
            // 
            this.gridChem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridChem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.key,
            this.value});
            this.gridChem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChem.Location = new System.Drawing.Point(0, 0);
            this.gridChem.Name = "gridChem";
            this.gridChem.Size = new System.Drawing.Size(293, 595);
            this.gridChem.TabIndex = 0;
            // 
            // key
            // 
            this.key.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.key.HeaderText = "Элемент";
            this.key.Name = "key";
            this.key.Width = 76;
            // 
            // value
            // 
            this.value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.value.HeaderText = "Значение";
            this.value.Name = "value";
            // 
            // ChemTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(293, 595);
            this.Controls.Add(this.gridChem);
            this.Name = "ChemTable";
            this.Text = "chem";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChemTable_FormClosing);
            this.Load += new System.EventHandler(this.ChemTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridChem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridChem;
        private System.Windows.Forms.DataGridViewTextBoxColumn key;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;

    }
}