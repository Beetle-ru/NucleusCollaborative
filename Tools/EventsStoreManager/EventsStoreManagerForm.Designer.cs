namespace EventsStoreManager
{
    partial class EventsStoreManagerForm
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
            this.DateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.DateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.RadioButtonEventsForHeatInfo = new System.Windows.Forms.RadioButton();
            this.RadioButtonAllEvents = new System.Windows.Forms.RadioButton();
            this.CheckBoxAllHeat = new System.Windows.Forms.CheckBox();
            this.CheckedListBoxHeatNumber = new System.Windows.Forms.CheckedListBox();
            this.DownLoadHeats = new System.Windows.Forms.Button();
            this.CheckBoxCN3 = new System.Windows.Forms.CheckBox();
            this.CheckBoxCN2 = new System.Windows.Forms.CheckBox();
            this.CheckBoxCN1 = new System.Windows.Forms.CheckBox();
            this.TextBoxDownLoadPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ButtonDownLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBoxModel = new System.Windows.Forms.GroupBox();
            this.ButtonGetModel = new System.Windows.Forms.Button();
            this.TextBoxModel = new System.Windows.Forms.TextBox();
            this.GroupBoxLoad = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.GroupBoxDownLoad.SuspendLayout();
            this.GroupBoxModel.SuspendLayout();
            this.GroupBoxLoad.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxDownLoad
            // 
            this.GroupBoxDownLoad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBoxDownLoad.Controls.Add(this.DateTimePickerEnd);
            this.GroupBoxDownLoad.Controls.Add(this.DateTimePickerStart);
            this.GroupBoxDownLoad.Controls.Add(this.RadioButtonEventsForHeatInfo);
            this.GroupBoxDownLoad.Controls.Add(this.RadioButtonAllEvents);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxAllHeat);
            this.GroupBoxDownLoad.Controls.Add(this.CheckedListBoxHeatNumber);
            this.GroupBoxDownLoad.Controls.Add(this.DownLoadHeats);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxCN3);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxCN2);
            this.GroupBoxDownLoad.Controls.Add(this.CheckBoxCN1);
            this.GroupBoxDownLoad.Controls.Add(this.TextBoxDownLoadPath);
            this.GroupBoxDownLoad.Controls.Add(this.label4);
            this.GroupBoxDownLoad.Controls.Add(this.ButtonDownLoad);
            this.GroupBoxDownLoad.Controls.Add(this.label2);
            this.GroupBoxDownLoad.Controls.Add(this.label1);
            this.GroupBoxDownLoad.Location = new System.Drawing.Point(12, 62);
            this.GroupBoxDownLoad.Name = "GroupBoxDownLoad";
            this.GroupBoxDownLoad.Size = new System.Drawing.Size(434, 197);
            this.GroupBoxDownLoad.TabIndex = 0;
            this.GroupBoxDownLoad.TabStop = false;
            this.GroupBoxDownLoad.Text = "Загрузка из БД";
            // 
            // DateTimePickerEnd
            // 
            this.DateTimePickerEnd.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePickerEnd.Location = new System.Drawing.Point(203, 16);
            this.DateTimePickerEnd.Name = "DateTimePickerEnd";
            this.DateTimePickerEnd.Size = new System.Drawing.Size(147, 20);
            this.DateTimePickerEnd.TabIndex = 18;
            // 
            // DateTimePickerStart
            // 
            this.DateTimePickerStart.CustomFormat = "dd.MM.yyyy HH:mm:ss";
            this.DateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePickerStart.Location = new System.Drawing.Point(30, 16);
            this.DateTimePickerStart.Name = "DateTimePickerStart";
            this.DateTimePickerStart.Size = new System.Drawing.Size(140, 20);
            this.DateTimePickerStart.TabIndex = 15;
            // 
            // RadioButtonEventsForHeatInfo
            // 
            this.RadioButtonEventsForHeatInfo.AutoSize = true;
            this.RadioButtonEventsForHeatInfo.Checked = true;
            this.RadioButtonEventsForHeatInfo.Location = new System.Drawing.Point(234, 85);
            this.RadioButtonEventsForHeatInfo.Name = "RadioButtonEventsForHeatInfo";
            this.RadioButtonEventsForHeatInfo.Size = new System.Drawing.Size(127, 17);
            this.RadioButtonEventsForHeatInfo.TabIndex = 17;
            this.RadioButtonEventsForHeatInfo.TabStop = true;
            this.RadioButtonEventsForHeatInfo.Text = "Только для HeatInfo";
            this.RadioButtonEventsForHeatInfo.UseVisualStyleBackColor = true;
            // 
            // RadioButtonAllEvents
            // 
            this.RadioButtonAllEvents.AutoSize = true;
            this.RadioButtonAllEvents.Location = new System.Drawing.Point(234, 63);
            this.RadioButtonAllEvents.Name = "RadioButtonAllEvents";
            this.RadioButtonAllEvents.Size = new System.Drawing.Size(90, 17);
            this.RadioButtonAllEvents.TabIndex = 16;
            this.RadioButtonAllEvents.Text = "Все события";
            this.RadioButtonAllEvents.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAllHeat
            // 
            this.CheckBoxAllHeat.AutoSize = true;
            this.CheckBoxAllHeat.Location = new System.Drawing.Point(33, 40);
            this.CheckBoxAllHeat.Name = "CheckBoxAllHeat";
            this.CheckBoxAllHeat.Size = new System.Drawing.Size(67, 17);
            this.CheckBoxAllHeat.TabIndex = 15;
            this.CheckBoxAllHeat.Text = "Плавки:";
            this.CheckBoxAllHeat.UseVisualStyleBackColor = true;
            this.CheckBoxAllHeat.CheckedChanged += new System.EventHandler(this.CheckBoxAllHeatCheckedChanged);
            // 
            // CheckedListBoxHeatNumber
            // 
            this.CheckedListBoxHeatNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CheckedListBoxHeatNumber.FormattingEnabled = true;
            this.CheckedListBoxHeatNumber.Location = new System.Drawing.Point(30, 63);
            this.CheckedListBoxHeatNumber.Name = "CheckedListBoxHeatNumber";
            this.CheckedListBoxHeatNumber.Size = new System.Drawing.Size(111, 109);
            this.CheckedListBoxHeatNumber.TabIndex = 14;
            // 
            // DownLoadHeats
            // 
            this.DownLoadHeats.Location = new System.Drawing.Point(356, 14);
            this.DownLoadHeats.Name = "DownLoadHeats";
            this.DownLoadHeats.Size = new System.Drawing.Size(67, 23);
            this.DownLoadHeats.TabIndex = 13;
            this.DownLoadHeats.Text = "Взять";
            this.DownLoadHeats.UseVisualStyleBackColor = true;
            this.DownLoadHeats.Click += new System.EventHandler(this.DownLoadHeatsClick);
            // 
            // CheckBoxCN3
            // 
            this.CheckBoxCN3.AutoSize = true;
            this.CheckBoxCN3.Checked = true;
            this.CheckBoxCN3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxCN3.Location = new System.Drawing.Point(153, 109);
            this.CheckBoxCN3.Name = "CheckBoxCN3";
            this.CheckBoxCN3.Size = new System.Drawing.Size(75, 17);
            this.CheckBoxCN3.TabIndex = 12;
            this.CheckBoxCN3.Text = "Агрегат 3";
            this.CheckBoxCN3.UseVisualStyleBackColor = true;
            // 
            // CheckBoxCN2
            // 
            this.CheckBoxCN2.AutoSize = true;
            this.CheckBoxCN2.Checked = true;
            this.CheckBoxCN2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxCN2.Location = new System.Drawing.Point(153, 86);
            this.CheckBoxCN2.Name = "CheckBoxCN2";
            this.CheckBoxCN2.Size = new System.Drawing.Size(75, 17);
            this.CheckBoxCN2.TabIndex = 11;
            this.CheckBoxCN2.Text = "Агрегат 2";
            this.CheckBoxCN2.UseVisualStyleBackColor = true;
            // 
            // CheckBoxCN1
            // 
            this.CheckBoxCN1.AutoSize = true;
            this.CheckBoxCN1.Checked = true;
            this.CheckBoxCN1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxCN1.Location = new System.Drawing.Point(153, 63);
            this.CheckBoxCN1.Name = "CheckBoxCN1";
            this.CheckBoxCN1.Size = new System.Drawing.Size(75, 17);
            this.CheckBoxCN1.TabIndex = 10;
            this.CheckBoxCN1.Text = "Агрегат 1";
            this.CheckBoxCN1.UseVisualStyleBackColor = true;
            // 
            // TextBoxDownLoadPath
            // 
            this.TextBoxDownLoadPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TextBoxDownLoadPath.Location = new System.Drawing.Point(195, 168);
            this.TextBoxDownLoadPath.Name = "TextBoxDownLoadPath";
            this.TextBoxDownLoadPath.ReadOnly = true;
            this.TextBoxDownLoadPath.Size = new System.Drawing.Size(148, 20);
            this.TextBoxDownLoadPath.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Путь:";
            // 
            // ButtonDownLoad
            // 
            this.ButtonDownLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonDownLoad.Location = new System.Drawing.Point(349, 168);
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
            // GroupBoxModel
            // 
            this.GroupBoxModel.Controls.Add(this.ButtonGetModel);
            this.GroupBoxModel.Controls.Add(this.TextBoxModel);
            this.GroupBoxModel.Location = new System.Drawing.Point(12, 12);
            this.GroupBoxModel.Name = "GroupBoxModel";
            this.GroupBoxModel.Size = new System.Drawing.Size(434, 44);
            this.GroupBoxModel.TabIndex = 1;
            this.GroupBoxModel.TabStop = false;
            this.GroupBoxModel.Text = "Модуль ";
            // 
            // ButtonGetModel
            // 
            this.ButtonGetModel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ButtonGetModel.Location = new System.Drawing.Point(401, 15);
            this.ButtonGetModel.Name = "ButtonGetModel";
            this.ButtonGetModel.Size = new System.Drawing.Size(27, 23);
            this.ButtonGetModel.TabIndex = 1;
            this.ButtonGetModel.Text = "...";
            this.ButtonGetModel.UseVisualStyleBackColor = true;
            this.ButtonGetModel.Click += new System.EventHandler(this.ButtonGetModelClick);
            // 
            // TextBoxModel
            // 
            this.TextBoxModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxModel.Location = new System.Drawing.Point(13, 19);
            this.TextBoxModel.Name = "TextBoxModel";
            this.TextBoxModel.ReadOnly = true;
            this.TextBoxModel.Size = new System.Drawing.Size(382, 20);
            this.TextBoxModel.TabIndex = 0;
            // 
            // GroupBoxLoad
            // 
            this.GroupBoxLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBoxLoad.Controls.Add(this.button6);
            this.GroupBoxLoad.Controls.Add(this.button5);
            this.GroupBoxLoad.Controls.Add(this.button4);
            this.GroupBoxLoad.Controls.Add(this.textBox3);
            this.GroupBoxLoad.Controls.Add(this.label6);
            this.GroupBoxLoad.Controls.Add(this.label5);
            this.GroupBoxLoad.Controls.Add(this.listBox2);
            this.GroupBoxLoad.Location = new System.Drawing.Point(12, 265);
            this.GroupBoxLoad.Name = "GroupBoxLoad";
            this.GroupBoxLoad.Size = new System.Drawing.Size(434, 140);
            this.GroupBoxLoad.TabIndex = 2;
            this.GroupBoxLoad.TabStop = false;
            this.GroupBoxLoad.Text = "Загрузка из файла";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(356, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(64, 23);
            this.button6.TabIndex = 14;
            this.button6.Text = "Папка";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(165, 107);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(105, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "Записать в БД";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(323, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(27, 23);
            this.button4.TabIndex = 12;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(85, 23);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(232, 20);
            this.textBox3.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Путь:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Плавки:";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(39, 74);
            this.listBox2.Name = "listBox2";
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox2.Size = new System.Drawing.Size(120, 56);
            this.listBox2.TabIndex = 6;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 411);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(457, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // EventsStoreManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 433);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.GroupBoxLoad);
            this.Controls.Add(this.GroupBoxModel);
            this.Controls.Add(this.GroupBoxDownLoad);
            this.Name = "EventsStoreManagerForm";
            this.Text = "Менеджер хранения событий";
            this.GroupBoxDownLoad.ResumeLayout(false);
            this.GroupBoxDownLoad.PerformLayout();
            this.GroupBoxModel.ResumeLayout(false);
            this.GroupBoxModel.PerformLayout();
            this.GroupBoxLoad.ResumeLayout(false);
            this.GroupBoxLoad.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBoxDownLoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GroupBoxModel;
        private System.Windows.Forms.Button ButtonGetModel;
        private System.Windows.Forms.TextBox TextBoxModel;
        private System.Windows.Forms.TextBox TextBoxDownLoadPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ButtonDownLoad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckBoxCN3;
        private System.Windows.Forms.CheckBox CheckBoxCN2;
        private System.Windows.Forms.CheckBox CheckBoxCN1;
        private System.Windows.Forms.GroupBox GroupBoxLoad;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button DownLoadHeats;
        private System.Windows.Forms.CheckedListBox CheckedListBoxHeatNumber;
        private System.Windows.Forms.CheckBox CheckBoxAllHeat;
        private System.Windows.Forms.RadioButton RadioButtonEventsForHeatInfo;
        private System.Windows.Forms.RadioButton RadioButtonAllEvents;
        private System.Windows.Forms.DateTimePicker DateTimePickerStart;
        private System.Windows.Forms.DateTimePicker DateTimePickerEnd;
    }
}

