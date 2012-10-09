using System.Collections.Generic;

namespace SimpleRuner
{
    partial class Runer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Runer));
            this.tb_log = new System.Windows.Forms.RichTextBox();
            this.b_start = new System.Windows.Forms.Button();
            this.b_stop = new System.Windows.Forms.Button();
            this.cb_appNames = new System.Windows.Forms.ComboBox();
            this.t_startApp = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tb_log
            // 
            this.tb_log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.tb_log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tb_log.Location = new System.Drawing.Point(1, 26);
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(792, 579);
            this.tb_log.TabIndex = 0;
            this.tb_log.Text = "";
            // 
            // b_start
            // 
            this.b_start.Location = new System.Drawing.Point(240, -1);
            this.b_start.Name = "b_start";
            this.b_start.Size = new System.Drawing.Size(75, 23);
            this.b_start.TabIndex = 1;
            this.b_start.Text = "Start";
            this.b_start.UseVisualStyleBackColor = true;
            // 
            // b_stop
            // 
            this.b_stop.Location = new System.Drawing.Point(321, -1);
            this.b_stop.Name = "b_stop";
            this.b_stop.Size = new System.Drawing.Size(75, 23);
            this.b_stop.TabIndex = 2;
            this.b_stop.Text = "Stop";
            this.b_stop.UseVisualStyleBackColor = true;
            // 
            // cb_appNames
            // 
            this.cb_appNames.FormattingEnabled = true;
            this.cb_appNames.Location = new System.Drawing.Point(1, -1);
            this.cb_appNames.Name = "cb_appNames";
            this.cb_appNames.Size = new System.Drawing.Size(233, 21);
            this.cb_appNames.TabIndex = 3;
            // 
            // t_startApp
            // 
            this.t_startApp.Enabled = true;
            this.t_startApp.Interval = 500;
            this.t_startApp.Tick += new System.EventHandler(this.t_startApp_Tick);
            // 
            // Runer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 603);
            this.Controls.Add(this.cb_appNames);
            this.Controls.Add(this.b_stop);
            this.Controls.Add(this.b_start);
            this.Controls.Add(this.tb_log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Runer";
            this.Text = "Runer";
            this.ResumeLayout(false);

            /////////////////////////////////////////////////////////////////////////
            
            //this.ProgrammList = new List<string>();

        }

        #endregion

        private System.Windows.Forms.RichTextBox tb_log;
        private System.Windows.Forms.Button b_start;
        private System.Windows.Forms.Button b_stop;
        private System.Windows.Forms.ComboBox cb_appNames;
        private System.Windows.Forms.Timer t_startApp;
    }
}

