namespace PipeCatcher
{
    partial class Catcher
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Catcher));
            this.tmrPipeCheck = new System.Windows.Forms.Timer(this.components);
            this.btnStartStop = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.txbProcName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txbRecId = new System.Windows.Forms.TextBox();
            this.btnCall = new System.Windows.Forms.Button();
            this.panCaller = new System.Windows.Forms.Panel();
            this.panCaller.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrPipeCheck
            // 
            this.tmrPipeCheck.Interval = 500;
            this.tmrPipeCheck.Tick += new System.EventHandler(this.tmrPipeCheck_Tick);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(12, 12);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(142, 40);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblInfo.Location = new System.Drawing.Point(195, 12);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(72, 24);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Tick=0";
            // 
            // rtbReport
            // 
            this.rtbReport.BackColor = System.Drawing.Color.DimGray;
            this.rtbReport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbReport.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbReport.ForeColor = System.Drawing.Color.GreenYellow;
            this.rtbReport.Location = new System.Drawing.Point(0, 135);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.Size = new System.Drawing.Size(698, 289);
            this.rtbReport.TabIndex = 3;
            this.rtbReport.Text = "";
            // 
            // txbProcName
            // 
            this.txbProcName.Location = new System.Drawing.Point(21, 37);
            this.txbProcName.Name = "txbProcName";
            this.txbProcName.Size = new System.Drawing.Size(155, 20);
            this.txbProcName.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "ProcName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "NRECID";
            // 
            // txbRecId
            // 
            this.txbRecId.Location = new System.Drawing.Point(24, 89);
            this.txbRecId.Name = "txbRecId";
            this.txbRecId.Size = new System.Drawing.Size(155, 20);
            this.txbRecId.TabIndex = 6;
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(213, 37);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(113, 72);
            this.btnCall.TabIndex = 8;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // panCaller
            // 
            this.panCaller.Controls.Add(this.txbRecId);
            this.panCaller.Controls.Add(this.btnCall);
            this.panCaller.Controls.Add(this.txbProcName);
            this.panCaller.Controls.Add(this.label2);
            this.panCaller.Controls.Add(this.label1);
            this.panCaller.Dock = System.Windows.Forms.DockStyle.Right;
            this.panCaller.Location = new System.Drawing.Point(339, 0);
            this.panCaller.Name = "panCaller";
            this.panCaller.Size = new System.Drawing.Size(359, 135);
            this.panCaller.TabIndex = 9;
            // 
            // Catcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 424);
            this.Controls.Add(this.panCaller);
            this.Controls.Add(this.rtbReport);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnStartStop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Catcher";
            this.Text = "Chemistry Catcher";
            this.Load += new System.EventHandler(this.Catcher_Load);
            this.panCaller.ResumeLayout(false);
            this.panCaller.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrPipeCheck;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.RichTextBox rtbReport;
        private System.Windows.Forms.TextBox txbProcName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbRecId;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.Panel panCaller;
    }
}

