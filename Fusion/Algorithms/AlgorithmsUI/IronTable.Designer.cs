namespace AlgorithmsUI
{
    partial class IronTable
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
            this.dgw = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mixer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hmC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hmSi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hmMn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hmP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hmS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).BeginInit();
            this.SuspendLayout();
            // 
            // dgw
            // 
            this.dgw.AllowUserToAddRows = false;
            this.dgw.AllowUserToDeleteRows = false;
            this.dgw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgw.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.mixer,
            this.hmC,
            this.hmSi,
            this.hmMn,
            this.hmP,
            this.hmS});
            this.dgw.Location = new System.Drawing.Point(-3, 96);
            this.dgw.Name = "dgw";
            this.dgw.Size = new System.Drawing.Size(839, 324);
            this.dgw.TabIndex = 0;
            this.dgw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgw_CellContentClick);
            // 
            // select
            // 
            this.select.HeaderText = "Выбрать";
            this.select.Name = "select";
            // 
            // mixer
            // 
            this.mixer.HeaderText = "Миксер";
            this.mixer.Name = "mixer";
            this.mixer.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.mixer.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // hmC
            // 
            this.hmC.HeaderText = "C";
            this.hmC.Name = "hmC";
            this.hmC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.hmC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // hmSi
            // 
            this.hmSi.HeaderText = "Si";
            this.hmSi.Name = "hmSi";
            // 
            // hmMn
            // 
            this.hmMn.HeaderText = "Mn";
            this.hmMn.Name = "hmMn";
            // 
            // hmP
            // 
            this.hmP.HeaderText = "P";
            this.hmP.Name = "hmP";
            // 
            // hmS
            // 
            this.hmS.HeaderText = "S";
            this.hmS.Name = "hmS";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(89, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(616, 52);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // IronTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 419);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgw);
            this.Name = "IronTable";
            this.Text = "IronTable";
            ((System.ComponentModel.ISupportInitialize)(this.dgw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgw;
        public System.Windows.Forms.DataGridViewCheckBoxColumn select;
        public System.Windows.Forms.DataGridViewTextBoxColumn mixer;
        public System.Windows.Forms.DataGridViewTextBoxColumn hmC;
        public System.Windows.Forms.DataGridViewTextBoxColumn hmSi;
        public System.Windows.Forms.DataGridViewTextBoxColumn hmMn;
        public System.Windows.Forms.DataGridViewTextBoxColumn hmP;
        public System.Windows.Forms.DataGridViewTextBoxColumn hmS;
        private System.Windows.Forms.Button btnSave;
    }
}