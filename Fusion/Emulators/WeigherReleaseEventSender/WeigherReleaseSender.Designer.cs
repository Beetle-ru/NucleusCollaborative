namespace WeigherReleaseEventSender
{
    partial class WeigherReleaseSender
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
            this.btnReleaseW3 = new System.Windows.Forms.Button();
            this.btnReleaseW4 = new System.Windows.Forms.Button();
            this.btnReleaseW5 = new System.Windows.Forms.Button();
            this.btnReleaseW6 = new System.Windows.Forms.Button();
            this.btnReleaseW7 = new System.Windows.Forms.Button();
            this.cbEmul = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnReleaseW3
            // 
            this.btnReleaseW3.Location = new System.Drawing.Point(2, 2);
            this.btnReleaseW3.Name = "btnReleaseW3";
            this.btnReleaseW3.Size = new System.Drawing.Size(86, 50);
            this.btnReleaseW3.TabIndex = 0;
            this.btnReleaseW3.Text = "Освободить весы 3";
            this.btnReleaseW3.UseVisualStyleBackColor = true;
            this.btnReleaseW3.Click += new System.EventHandler(this.btnReleaseW3_Click);
            // 
            // btnReleaseW4
            // 
            this.btnReleaseW4.Location = new System.Drawing.Point(94, 2);
            this.btnReleaseW4.Name = "btnReleaseW4";
            this.btnReleaseW4.Size = new System.Drawing.Size(86, 50);
            this.btnReleaseW4.TabIndex = 0;
            this.btnReleaseW4.Text = "Освободить весы 4";
            this.btnReleaseW4.UseVisualStyleBackColor = true;
            this.btnReleaseW4.Click += new System.EventHandler(this.btnReleaseW4_Click);
            // 
            // btnReleaseW5
            // 
            this.btnReleaseW5.Location = new System.Drawing.Point(186, 2);
            this.btnReleaseW5.Name = "btnReleaseW5";
            this.btnReleaseW5.Size = new System.Drawing.Size(86, 50);
            this.btnReleaseW5.TabIndex = 0;
            this.btnReleaseW5.Text = "Освободить весы 5";
            this.btnReleaseW5.UseVisualStyleBackColor = true;
            this.btnReleaseW5.Click += new System.EventHandler(this.btnReleaseW5_Click);
            // 
            // btnReleaseW6
            // 
            this.btnReleaseW6.Location = new System.Drawing.Point(278, 2);
            this.btnReleaseW6.Name = "btnReleaseW6";
            this.btnReleaseW6.Size = new System.Drawing.Size(86, 50);
            this.btnReleaseW6.TabIndex = 0;
            this.btnReleaseW6.Text = "Освободить весы 6";
            this.btnReleaseW6.UseVisualStyleBackColor = true;
            this.btnReleaseW6.Click += new System.EventHandler(this.btnReleaseW6_Click);
            // 
            // btnReleaseW7
            // 
            this.btnReleaseW7.Location = new System.Drawing.Point(370, 2);
            this.btnReleaseW7.Name = "btnReleaseW7";
            this.btnReleaseW7.Size = new System.Drawing.Size(86, 50);
            this.btnReleaseW7.TabIndex = 0;
            this.btnReleaseW7.Text = "Освободить весы 7";
            this.btnReleaseW7.UseVisualStyleBackColor = true;
            this.btnReleaseW7.Click += new System.EventHandler(this.btnReleaseW7_Click);
            // 
            // cbEmul
            // 
            this.cbEmul.AutoSize = true;
            this.cbEmul.Location = new System.Drawing.Point(2, 58);
            this.cbEmul.Name = "cbEmul";
            this.cbEmul.Size = new System.Drawing.Size(203, 17);
            this.cbEmul.TabIndex = 1;
            this.cbEmul.Text = "Эмулировать освобождение весов";
            this.cbEmul.UseVisualStyleBackColor = true;
            // 
            // WeigherReleaseSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 75);
            this.Controls.Add(this.cbEmul);
            this.Controls.Add(this.btnReleaseW7);
            this.Controls.Add(this.btnReleaseW6);
            this.Controls.Add(this.btnReleaseW5);
            this.Controls.Add(this.btnReleaseW4);
            this.Controls.Add(this.btnReleaseW3);
            this.Name = "WeigherReleaseSender";
            this.Text = "Послать сигнал освободить весы";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReleaseW3;
        private System.Windows.Forms.Button btnReleaseW4;
        private System.Windows.Forms.Button btnReleaseW5;
        private System.Windows.Forms.Button btnReleaseW6;
        private System.Windows.Forms.Button btnReleaseW7;
        private System.Windows.Forms.CheckBox cbEmul;
    }
}

