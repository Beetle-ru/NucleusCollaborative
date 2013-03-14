namespace Shixta_I_Selector
{
    partial class ConvSelector
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
            System.Windows.Forms.TabPage tabPage1;
            System.Windows.Forms.TabPage tabPage2;
            System.Windows.Forms.TabPage tabPage3;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConvSelector));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mixCalculator1 = new HeatControl.MixCalculator();
            this.mixCalculator2 = new HeatControl.MixCalculator();
            this.mixCalculator3 = new HeatControl.MixCalculator();
            tabPage1 = new System.Windows.Forms.TabPage();
            tabPage2 = new System.Windows.Forms.TabPage();
            tabPage3 = new System.Windows.Forms.TabPage();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(this.mixCalculator1);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(738, 608);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Конвертер 1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(this.mixCalculator2);
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(738, 608);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Конвертер 2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(this.mixCalculator3);
            tabPage3.Location = new System.Drawing.Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(738, 608);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Конвертер 3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(tabPage1);
            this.tabControl1.Controls.Add(tabPage2);
            this.tabControl1.Controls.Add(tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(746, 634);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // mixCalculator1
            // 
            this.mixCalculator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mixCalculator1.Location = new System.Drawing.Point(3, 3);
            this.mixCalculator1.Name = "mixCalculator1";
            this.mixCalculator1.Size = new System.Drawing.Size(732, 602);
            this.mixCalculator1.TabIndex = 1;
            this.mixCalculator1.Tag = "Converter1";
            // 
            // mixCalculator2
            // 
            this.mixCalculator2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mixCalculator2.Location = new System.Drawing.Point(3, 3);
            this.mixCalculator2.Name = "mixCalculator2";
            this.mixCalculator2.Size = new System.Drawing.Size(732, 602);
            this.mixCalculator2.TabIndex = 1;
            this.mixCalculator2.Tag = "Converter2";
            // 
            // mixCalculator3
            // 
            this.mixCalculator3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mixCalculator3.Location = new System.Drawing.Point(0, 0);
            this.mixCalculator3.Name = "mixCalculator3";
            this.mixCalculator3.Size = new System.Drawing.Size(738, 608);
            this.mixCalculator3.TabIndex = 1;
            this.mixCalculator3.Tag = "Converter3";
            this.mixCalculator3.Load += new System.EventHandler(this.mixCalculator3_Load);
            // 
            // ConvSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 634);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConvSelector";
            this.Text = "Расчет шихты";
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public HeatControl.MixCalculator mixCalculator1;
        public HeatControl.MixCalculator mixCalculator2;
        public HeatControl.MixCalculator mixCalculator3;

        private System.Windows.Forms.TabControl tabControl1;
    }
}

