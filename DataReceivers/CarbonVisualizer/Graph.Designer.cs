namespace CarbonVisualizer
{
    partial class Graph
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
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lblLancePosition = new System.Windows.Forms.Label();
            this.lblCarbon = new System.Windows.Forms.Label();
            this.lblFixDataMFactorMText = new System.Windows.Forms.Label();
            this.lblSubLanceStartText = new System.Windows.Forms.Label();
            this.lblLancePositionText = new System.Windows.Forms.Label();
            this.lblCarbonText = new System.Windows.Forms.Label();
            this.lblCOText = new System.Windows.Forms.Label();
            this.lblCO = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbGraph
            // 
            this.pbGraph.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbGraph.Location = new System.Drawing.Point(0, 0);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(908, 444);
            this.pbGraph.TabIndex = 0;
            this.pbGraph.TabStop = false;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pbGraph);
            this.splitMain.Panel1.Resize += new System.EventHandler(this.splitMain_Panel1_Resize);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.BackColor = System.Drawing.Color.Maroon;
            this.splitMain.Panel2.Controls.Add(this.lblCO);
            this.splitMain.Panel2.Controls.Add(this.lblLancePosition);
            this.splitMain.Panel2.Controls.Add(this.lblCarbon);
            this.splitMain.Panel2.Controls.Add(this.lblFixDataMFactorMText);
            this.splitMain.Panel2.Controls.Add(this.lblSubLanceStartText);
            this.splitMain.Panel2.Controls.Add(this.lblCOText);
            this.splitMain.Panel2.Controls.Add(this.lblLancePositionText);
            this.splitMain.Panel2.Controls.Add(this.lblCarbonText);
            this.splitMain.Size = new System.Drawing.Size(911, 491);
            this.splitMain.SplitterDistance = 447;
            this.splitMain.TabIndex = 1;
            // 
            // lblLancePosition
            // 
            this.lblLancePosition.AutoSize = true;
            this.lblLancePosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLancePosition.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLancePosition.Location = new System.Drawing.Point(360, 13);
            this.lblLancePosition.Name = "lblLancePosition";
            this.lblLancePosition.Size = new System.Drawing.Size(58, 18);
            this.lblLancePosition.TabIndex = 0;
            this.lblLancePosition.Text = "0,0000";
            // 
            // lblCarbon
            // 
            this.lblCarbon.AutoSize = true;
            this.lblCarbon.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCarbon.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCarbon.Location = new System.Drawing.Point(114, 13);
            this.lblCarbon.Name = "lblCarbon";
            this.lblCarbon.Size = new System.Drawing.Size(58, 18);
            this.lblCarbon.TabIndex = 0;
            this.lblCarbon.Text = "0,0000";
            // 
            // lblFixDataMFactorMText
            // 
            this.lblFixDataMFactorMText.AutoSize = true;
            this.lblFixDataMFactorMText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFixDataMFactorMText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFixDataMFactorMText.Location = new System.Drawing.Point(722, 13);
            this.lblFixDataMFactorMText.Name = "lblFixDataMFactorMText";
            this.lblFixDataMFactorMText.Size = new System.Drawing.Size(186, 18);
            this.lblFixDataMFactorMText.TabIndex = 0;
            this.lblFixDataMFactorMText.Text = "Fix data M factor model";
            // 
            // lblSubLanceStartText
            // 
            this.lblSubLanceStartText.AutoSize = true;
            this.lblSubLanceStartText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSubLanceStartText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblSubLanceStartText.Location = new System.Drawing.Point(589, 13);
            this.lblSubLanceStartText.Name = "lblSubLanceStartText";
            this.lblSubLanceStartText.Size = new System.Drawing.Size(116, 18);
            this.lblSubLanceStartText.TabIndex = 0;
            this.lblSubLanceStartText.Text = "Sublance start";
            // 
            // lblLancePositionText
            // 
            this.lblLancePositionText.AutoSize = true;
            this.lblLancePositionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLancePositionText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLancePositionText.Location = new System.Drawing.Point(198, 13);
            this.lblLancePositionText.Name = "lblLancePositionText";
            this.lblLancePositionText.Size = new System.Drawing.Size(156, 18);
            this.lblLancePositionText.TabIndex = 0;
            this.lblLancePositionText.Text = "Lance position cm :";
            // 
            // lblCarbonText
            // 
            this.lblCarbonText.AutoSize = true;
            this.lblCarbonText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCarbonText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCarbonText.Location = new System.Drawing.Point(12, 13);
            this.lblCarbonText.Name = "lblCarbonText";
            this.lblCarbonText.Size = new System.Drawing.Size(96, 18);
            this.lblCarbonText.TabIndex = 0;
            this.lblCarbonText.Text = "Carbone %:";
            // 
            // lblCOText
            // 
            this.lblCOText.AutoSize = true;
            this.lblCOText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCOText.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCOText.Location = new System.Drawing.Point(440, 13);
            this.lblCOText.Name = "lblCOText";
            this.lblCOText.Size = new System.Drawing.Size(57, 18);
            this.lblCOText.TabIndex = 0;
            this.lblCOText.Text = "CO% :";
            // 
            // lblCO
            // 
            this.lblCO.AutoSize = true;
            this.lblCO.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCO.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblCO.Location = new System.Drawing.Point(503, 13);
            this.lblCO.Name = "lblCO";
            this.lblCO.Size = new System.Drawing.Size(58, 18);
            this.lblCO.TabIndex = 0;
            this.lblCO.Text = "0,0000";
            // 
            // Graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 491);
            this.Controls.Add(this.splitMain);
            this.Name = "Graph";
            this.Text = "визуализация";
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Label lblCarbon;
        private System.Windows.Forms.Label lblCarbonText;
        private System.Windows.Forms.Label lblLancePosition;
        private System.Windows.Forms.Label lblSubLanceStartText;
        private System.Windows.Forms.Label lblLancePositionText;
        private System.Windows.Forms.Label lblFixDataMFactorMText;
        private System.Windows.Forms.Label lblCO;
        private System.Windows.Forms.Label lblCOText;
    }
}

