namespace EsmsFusionProtocol
{
    partial class MainForm
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
            this.GroupBoxDownLoad = new System.Windows.Forms.GroupBox();
            this.СomboBoxStepTime = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ButtonGetTemplate = new System.Windows.Forms.Button();
            this.TextBoxTemplate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonStopProcess = new System.Windows.Forms.Button();
            this.DateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.DateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.CheckBoxAllHeat = new System.Windows.Forms.CheckBox();
            this.CheckedListBoxHeatNumber = new System.Windows.Forms.CheckedListBox();
            this.DownLoadHeats = new System.Windows.Forms.Button();
            this.CheckBoxCN2 = new System.Windows.Forms.CheckBox();
            this.CheckBoxCN1 = new System.Windows.Forms.CheckBox();
            this.ButtonDownLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.GroupBoxDownLoad.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxDownLoad
            // 
            this.GroupBoxDownLoad.Controls.Add(this.label7);
            this.GroupBoxDownLoad.Controls.Add(this.label6);
            this.GroupBoxDownLoad.Controls.Add(this.СomboBoxStepTime);
            this.GroupBoxDownLoad.Controls.Add(this.label5);
            this.GroupBoxDownLoad.Controls.Add(this.ButtonGetTemplate);
            this.GroupBoxDownLoad.Controls.Add(this.TextBoxTemplate);
            this.GroupBoxDownLoad.Controls.Add(this.label4);
            this.GroupBoxDownLoad.Controls.Add(this.label3);
            this.GroupBoxDownLoad.Controls.Add(this.ButtonStopProcess);
            this.GroupBoxDownLoad.Controls.Add(this.DateTimePickerEnd);
            this.GroupBoxDownLoad.Controls.Add(this.DateTimePickerStart);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxAllHeat);
            this.GroupBoxDownLoad.Controls.Add(this.CheckedListBoxHeatNumber);
            this.GroupBoxDownLoad.Controls.Add(this.DownLoadHeats);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxCN2);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxCN1);
            this.GroupBoxDownLoad.Controls.Add(this.ButtonDownLoad);
            this.GroupBoxDownLoad.Controls.Add(this.label2);
            this.GroupBoxDownLoad.Controls.Add(this.label1);
            this.GroupBoxDownLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GroupBoxDownLoad.Location = new System.Drawing.Point(0, 0);
            this.GroupBoxDownLoad.Name = "GroupBoxDownLoad";
            this.GroupBoxDownLoad.Size = new System.Drawing.Size(350, 305);
            this.GroupBoxDownLoad.TabIndex = 1;
            this.GroupBoxDownLoad.TabStop = false;
            this.GroupBoxDownLoad.Text = "Загрузка из БД";
            // 
            // СomboBoxStepTime
            // 
            this.СomboBoxStepTime.FormattingEnabled = true;
            this.СomboBoxStepTime.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55",
            "60"});
            this.СomboBoxStepTime.Location = new System.Drawing.Point(254, 121);
            this.СomboBoxStepTime.Name = "СomboBoxStepTime";
            this.СomboBoxStepTime.Size = new System.Drawing.Size(82, 21);
            this.СomboBoxStepTime.TabIndex = 25;
            this.СomboBoxStepTime.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(251, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Шаг в секундах";
            // 
            // ButtonGetTemplate
            // 
            this.ButtonGetTemplate.Location = new System.Drawing.Point(316, 67);
            this.ButtonGetTemplate.Name = "ButtonGetTemplate";
            this.ButtonGetTemplate.Size = new System.Drawing.Size(25, 23);
            this.ButtonGetTemplate.TabIndex = 23;
            this.ButtonGetTemplate.Text = "...";
            this.ButtonGetTemplate.UseVisualStyleBackColor = true;
            this.ButtonGetTemplate.Click += new System.EventHandler(this.GetTemplateClick);
            // 
            // TextBoxTemplate
            // 
            this.TextBoxTemplate.Location = new System.Drawing.Point(65, 70);
            this.TextBoxTemplate.Name = "TextBoxTemplate";
            this.TextBoxTemplate.Size = new System.Drawing.Size(245, 20);
            this.TextBoxTemplate.TabIndex = 22;
            this.TextBoxTemplate.TextChanged += new System.EventHandler(this.TextBoxTemplateTextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Шаблон";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Для";
            // 
            // ButtonStopProcess
            // 
            this.ButtonStopProcess.Location = new System.Drawing.Point(254, 235);
            this.ButtonStopProcess.Name = "ButtonStopProcess";
            this.ButtonStopProcess.Size = new System.Drawing.Size(75, 23);
            this.ButtonStopProcess.TabIndex = 19;
            this.ButtonStopProcess.Text = "Остановить";
            this.ButtonStopProcess.UseVisualStyleBackColor = true;
            this.ButtonStopProcess.Click += new System.EventHandler(this.ButtonStopProcessClick);
            // 
            // DateTimePickerEnd
            // 
            this.DateTimePickerEnd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePickerEnd.Location = new System.Drawing.Point(203, 16);
            this.DateTimePickerEnd.Name = "DateTimePickerEnd";
            this.DateTimePickerEnd.Size = new System.Drawing.Size(138, 20);
            this.DateTimePickerEnd.TabIndex = 18;
            // 
            // DateTimePickerStart
            // 
            this.DateTimePickerStart.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.DateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePickerStart.Location = new System.Drawing.Point(30, 16);
            this.DateTimePickerStart.Name = "DateTimePickerStart";
            this.DateTimePickerStart.Size = new System.Drawing.Size(138, 20);
            this.DateTimePickerStart.TabIndex = 15;
            // 
            // CheckBoxAllHeat
            // 
            this.CheckBoxAllHeat.AutoSize = true;
            this.CheckBoxAllHeat.Location = new System.Drawing.Point(254, 148);
            this.CheckBoxAllHeat.Name = "CheckBoxAllHeat";
            this.CheckBoxAllHeat.Size = new System.Drawing.Size(87, 17);
            this.CheckBoxAllHeat.TabIndex = 15;
            this.CheckBoxAllHeat.Text = "Все плавки:";
            this.CheckBoxAllHeat.UseVisualStyleBackColor = true;
            this.CheckBoxAllHeat.CheckedChanged += new System.EventHandler(this.CheckBoxAllHeatCheckedChanged);
            // 
            // CheckedListBoxHeatNumber
            // 
            this.CheckedListBoxHeatNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckedListBoxHeatNumber.FormattingEnabled = true;
            this.CheckedListBoxHeatNumber.Location = new System.Drawing.Point(13, 105);
            this.CheckedListBoxHeatNumber.Name = "CheckedListBoxHeatNumber";
            this.CheckedListBoxHeatNumber.Size = new System.Drawing.Size(224, 154);
            this.CheckedListBoxHeatNumber.TabIndex = 14;
            // 
            // DownLoadHeats
            // 
            this.DownLoadHeats.Location = new System.Drawing.Point(274, 41);
            this.DownLoadHeats.Name = "DownLoadHeats";
            this.DownLoadHeats.Size = new System.Drawing.Size(67, 23);
            this.DownLoadHeats.TabIndex = 13;
            this.DownLoadHeats.Text = "Взять";
            this.DownLoadHeats.UseVisualStyleBackColor = true;
            this.DownLoadHeats.Click += new System.EventHandler(this.DownLoadHeatsClick);
            // 
            // CheckBoxCN2
            // 
            this.CheckBoxCN2.AutoSize = true;
            this.CheckBoxCN2.Checked = true;
            this.CheckBoxCN2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxCN2.Location = new System.Drawing.Point(162, 45);
            this.CheckBoxCN2.Name = "CheckBoxCN2";
            this.CheckBoxCN2.Size = new System.Drawing.Size(75, 17);
            this.CheckBoxCN2.TabIndex = 11;
            this.CheckBoxCN2.Text = "Агрегат 2";
            this.CheckBoxCN2.UseVisualStyleBackColor = true;
            // 
            // CheckBoxCN1
            // 
            this.CheckBoxCN1.AutoSize = true;
            this.CheckBoxCN1.Enabled = false;
            this.CheckBoxCN1.Location = new System.Drawing.Point(69, 45);
            this.CheckBoxCN1.Name = "CheckBoxCN1";
            this.CheckBoxCN1.Size = new System.Drawing.Size(75, 17);
            this.CheckBoxCN1.TabIndex = 10;
            this.CheckBoxCN1.Text = "Агрегат 1";
            this.CheckBoxCN1.UseVisualStyleBackColor = true;
            // 
            // ButtonDownLoad
            // 
            this.ButtonDownLoad.Location = new System.Drawing.Point(254, 206);
            this.ButtonDownLoad.Name = "ButtonDownLoad";
            this.ButtonDownLoad.Size = new System.Drawing.Size(74, 23);
            this.ButtonDownLoad.TabIndex = 6;
            this.ButtonDownLoad.Text = "Сохранить";
            this.ButtonDownLoad.UseVisualStyleBackColor = true;
            this.ButtonDownLoad.Click += new System.EventHandler(this.ButtonDownLoadClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "По";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "С";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 283);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(350, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Step = 1;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(251, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Выбрано:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(251, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "Выгружено :";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 305);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.GroupBoxDownLoad);
            this.Name = "MainForm";
            this.Text = "Протокол плавки";
            this.GroupBoxDownLoad.ResumeLayout(false);
            this.GroupBoxDownLoad.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxDownLoad;
        private System.Windows.Forms.DateTimePicker DateTimePickerEnd;
        private System.Windows.Forms.DateTimePicker DateTimePickerStart;
        private System.Windows.Forms.CheckBox CheckBoxAllHeat;
        private System.Windows.Forms.CheckedListBox CheckedListBoxHeatNumber;
        private System.Windows.Forms.Button DownLoadHeats;
        private System.Windows.Forms.Button ButtonDownLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.Button ButtonStopProcess;
        private System.Windows.Forms.Button ButtonGetTemplate;
        private System.Windows.Forms.TextBox TextBoxTemplate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox CheckBoxCN2;
        private System.Windows.Forms.CheckBox CheckBoxCN1;
        private System.Windows.Forms.ComboBox СomboBoxStepTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}