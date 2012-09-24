namespace AlgorithmsUI
{
    partial class MixtureFinal
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
            this.header = new System.Windows.Forms.GroupBox();
            this.txbSteelTemp = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txbSteelNum = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txbSteelID = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbSteelBrand = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.txbSteelTask = new System.Windows.Forms.TextBox();
            this.rbnScrap = new System.Windows.Forms.RadioButton();
            this.txbScrapTask = new System.Windows.Forms.TextBox();
            this.rbnSteel = new System.Windows.Forms.RadioButton();
            this.txbIronTask = new System.Windows.Forms.TextBox();
            this.rbnIron = new System.Windows.Forms.RadioButton();
            this.header.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.header.Controls.Add(this.txbSteelTemp);
            this.header.Controls.Add(this.textBox1);
            this.header.Controls.Add(this.label22);
            this.header.Controls.Add(this.label23);
            this.header.Controls.Add(this.label24);
            this.header.Controls.Add(this.txbSteelNum);
            this.header.Controls.Add(this.label25);
            this.header.Controls.Add(this.txbSteelID);
            this.header.Controls.Add(this.label26);
            this.header.Controls.Add(this.cmbSteelBrand);
            this.header.Location = new System.Drawing.Point(71, 46);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(824, 76);
            this.header.TabIndex = 14;
            this.header.TabStop = false;
            this.header.Text = "Паспорт плавки";
            // 
            // txbSteelTemp
            // 
            this.txbSteelTemp.Location = new System.Drawing.Point(567, 50);
            this.txbSteelTemp.Name = "txbSteelTemp";
            this.txbSteelTemp.Size = new System.Drawing.Size(100, 20);
            this.txbSteelTemp.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(686, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(296, 32);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(80, 13);
            this.label22.TabIndex = 5;
            this.label22.Text = "Номер плавки";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(686, 33);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(98, 13);
            this.label23.TabIndex = 10;
            this.label23.Text = "Конц. углерода, %";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(564, 33);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(87, 13);
            this.label24.TabIndex = 12;
            this.label24.Text = "Температура, С";
            // 
            // txbSteelNum
            // 
            this.txbSteelNum.Location = new System.Drawing.Point(299, 50);
            this.txbSteelNum.Name = "txbSteelNum";
            this.txbSteelNum.Size = new System.Drawing.Size(165, 20);
            this.txbSteelNum.TabIndex = 4;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(113, 33);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(87, 13);
            this.label25.TabIndex = 3;
            this.label25.Text = "Идентификатор";
            // 
            // txbSteelID
            // 
            this.txbSteelID.Location = new System.Drawing.Point(116, 50);
            this.txbSteelID.Name = "txbSteelID";
            this.txbSteelID.Size = new System.Drawing.Size(165, 20);
            this.txbSteelID.TabIndex = 2;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 33);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(72, 13);
            this.label26.TabIndex = 1;
            this.label26.Text = "Марка стали";
            // 
            // cmbSteelBrand
            // 
            this.cmbSteelBrand.FormattingEnabled = true;
            this.cmbSteelBrand.Location = new System.Drawing.Point(6, 49);
            this.cmbSteelBrand.Name = "cmbSteelBrand";
            this.cmbSteelBrand.Size = new System.Drawing.Size(92, 21);
            this.cmbSteelBrand.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnCalculate);
            this.panel1.Controls.Add(this.txbSteelTask);
            this.panel1.Controls.Add(this.rbnScrap);
            this.panel1.Controls.Add(this.txbScrapTask);
            this.panel1.Controls.Add(this.rbnSteel);
            this.panel1.Controls.Add(this.txbIronTask);
            this.panel1.Controls.Add(this.rbnIron);
            this.panel1.Location = new System.Drawing.Point(106, 199);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 115);
            this.panel1.TabIndex = 15;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalculate.Location = new System.Drawing.Point(563, 21);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnCalculate.Size = new System.Drawing.Size(253, 67);
            this.btnCalculate.TabIndex = 7;
            this.btnCalculate.Text = "Выполнить рассчет";
            this.btnCalculate.UseVisualStyleBackColor = true;
            // 
            // txbSteelTask
            // 
            this.txbSteelTask.Location = new System.Drawing.Point(86, 76);
            this.txbSteelTask.Name = "txbSteelTask";
            this.txbSteelTask.Size = new System.Drawing.Size(80, 20);
            this.txbSteelTask.TabIndex = 6;
            // 
            // rbnScrap
            // 
            this.rbnScrap.AutoSize = true;
            this.rbnScrap.Location = new System.Drawing.Point(9, 53);
            this.rbnScrap.Name = "rbnScrap";
            this.rbnScrap.Size = new System.Drawing.Size(47, 17);
            this.rbnScrap.TabIndex = 2;
            this.rbnScrap.TabStop = true;
            this.rbnScrap.Text = "Лом";
            this.rbnScrap.UseVisualStyleBackColor = true;
            // 
            // txbScrapTask
            // 
            this.txbScrapTask.Location = new System.Drawing.Point(86, 51);
            this.txbScrapTask.Name = "txbScrapTask";
            this.txbScrapTask.Size = new System.Drawing.Size(80, 20);
            this.txbScrapTask.TabIndex = 5;
            // 
            // rbnSteel
            // 
            this.rbnSteel.AutoSize = true;
            this.rbnSteel.Location = new System.Drawing.Point(9, 78);
            this.rbnSteel.Name = "rbnSteel";
            this.rbnSteel.Size = new System.Drawing.Size(55, 17);
            this.rbnSteel.TabIndex = 3;
            this.rbnSteel.TabStop = true;
            this.rbnSteel.Text = "Сталь";
            this.rbnSteel.UseVisualStyleBackColor = true;
            // 
            // txbIronTask
            // 
            this.txbIronTask.Location = new System.Drawing.Point(86, 25);
            this.txbIronTask.Name = "txbIronTask";
            this.txbIronTask.Size = new System.Drawing.Size(80, 20);
            this.txbIronTask.TabIndex = 4;
            // 
            // rbnIron
            // 
            this.rbnIron.AutoSize = true;
            this.rbnIron.Location = new System.Drawing.Point(9, 26);
            this.rbnIron.Name = "rbnIron";
            this.rbnIron.Size = new System.Drawing.Size(54, 17);
            this.rbnIron.TabIndex = 1;
            this.rbnIron.TabStop = true;
            this.rbnIron.Text = "Чугун";
            this.rbnIron.UseVisualStyleBackColor = true;
            // 
            // MixtureFinal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 513);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.header);
            this.Name = "MixtureFinal";
            this.Text = "Form1";
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox header;
        private System.Windows.Forms.TextBox txbSteelTemp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txbSteelNum;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txbSteelID;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbSteelBrand;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.TextBox txbSteelTask;
        private System.Windows.Forms.RadioButton rbnScrap;
        private System.Windows.Forms.TextBox txbScrapTask;
        private System.Windows.Forms.RadioButton rbnSteel;
        private System.Windows.Forms.TextBox txbIronTask;
        private System.Windows.Forms.RadioButton rbnIron;
    }
}