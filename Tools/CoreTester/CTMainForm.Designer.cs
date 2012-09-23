namespace CoreTester
{
    partial class CTMainForm
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
            this.components = new System.ComponentModel.Container();
            this.grpSingleTest = new System.Windows.Forms.GroupBox();
            this.cbMonitoring = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbReceive = new System.Windows.Forms.TextBox();
            this.tbSend = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.tbCount = new System.Windows.Forms.TextBox();
            this.tbDimLength = new System.Windows.Forms.TextBox();
            this.btnStartTest = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tbLoss = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpSingleTest.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSingleTest
            // 
            this.grpSingleTest.Controls.Add(this.cbMonitoring);
            this.grpSingleTest.Controls.Add(this.panel1);
            this.grpSingleTest.Controls.Add(this.label3);
            this.grpSingleTest.Controls.Add(this.label2);
            this.grpSingleTest.Controls.Add(this.label1);
            this.grpSingleTest.Controls.Add(this.tbDelay);
            this.grpSingleTest.Controls.Add(this.tbCount);
            this.grpSingleTest.Controls.Add(this.tbDimLength);
            this.grpSingleTest.Controls.Add(this.btnStartTest);
            this.grpSingleTest.ForeColor = System.Drawing.Color.White;
            this.grpSingleTest.Location = new System.Drawing.Point(13, 13);
            this.grpSingleTest.Name = "grpSingleTest";
            this.grpSingleTest.Size = new System.Drawing.Size(452, 179);
            this.grpSingleTest.TabIndex = 0;
            this.grpSingleTest.TabStop = false;
            this.grpSingleTest.Text = "Испытание отправкой тестового события";
            // 
            // cbMonitoring
            // 
            this.cbMonitoring.AutoSize = true;
            this.cbMonitoring.Location = new System.Drawing.Point(359, 71);
            this.cbMonitoring.Name = "cbMonitoring";
            this.cbMonitoring.Size = new System.Drawing.Size(87, 17);
            this.cbMonitoring.TabIndex = 4;
            this.cbMonitoring.Text = "Мониторинг";
            this.cbMonitoring.UseVisualStyleBackColor = true;
            this.cbMonitoring.CheckedChanged += new System.EventHandler(this.cbMonitoring_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbLoss);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tbReceive);
            this.panel1.Controls.Add(this.tbSend);
            this.panel1.Location = new System.Drawing.Point(168, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 154);
            this.panel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 61);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Принято";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Отправлено";
            // 
            // tbReceive
            // 
            this.tbReceive.Location = new System.Drawing.Point(15, 77);
            this.tbReceive.Name = "tbReceive";
            this.tbReceive.Size = new System.Drawing.Size(136, 20);
            this.tbReceive.TabIndex = 1;
            // 
            // tbSend
            // 
            this.tbSend.Location = new System.Drawing.Point(15, 29);
            this.tbSend.Name = "tbSend";
            this.tbSend.Size = new System.Drawing.Size(136, 20);
            this.tbSend.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Задержка мс";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Число событий";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Размер массива события";
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(10, 140);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(136, 20);
            this.tbDelay.TabIndex = 1;
            // 
            // tbCount
            // 
            this.tbCount.Location = new System.Drawing.Point(10, 96);
            this.tbCount.Name = "tbCount";
            this.tbCount.Size = new System.Drawing.Size(136, 20);
            this.tbCount.TabIndex = 1;
            // 
            // tbDimLength
            // 
            this.tbDimLength.Location = new System.Drawing.Point(10, 48);
            this.tbDimLength.Name = "tbDimLength";
            this.tbDimLength.Size = new System.Drawing.Size(136, 20);
            this.tbDimLength.TabIndex = 1;
            // 
            // btnStartTest
            // 
            this.btnStartTest.ForeColor = System.Drawing.Color.Black;
            this.btnStartTest.Location = new System.Drawing.Point(359, 106);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(75, 48);
            this.btnStartTest.TabIndex = 0;
            this.btnStartTest.Text = "Старт";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tbLoss
            // 
            this.tbLoss.Location = new System.Drawing.Point(15, 121);
            this.tbLoss.Name = "tbLoss";
            this.tbLoss.Size = new System.Drawing.Size(136, 20);
            this.tbLoss.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Потеряно";
            // 
            // CTMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(480, 207);
            this.Controls.Add(this.grpSingleTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "CTMainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CoreTester";
            this.grpSingleTest.ResumeLayout(false);
            this.grpSingleTest.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSingleTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDimLength;
        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbReceive;
        private System.Windows.Forms.TextBox tbSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDelay;
        private System.Windows.Forms.TextBox tbCount;
        private System.Windows.Forms.CheckBox cbMonitoring;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbLoss;
    }
}

