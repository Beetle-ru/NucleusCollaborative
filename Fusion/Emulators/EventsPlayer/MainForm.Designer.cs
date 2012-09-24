namespace EventsPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbHeatsList = new System.Windows.Forms.ListBox();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tsslMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbMessages = new System.Windows.Forms.TextBox();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.pButtonsPanel = new System.Windows.Forms.Panel();
            this.btSpeedX1 = new System.Windows.Forms.Button();
            this.btSpeedx5 = new System.Windows.Forms.Button();
            this.btSpeedX10 = new System.Windows.Forms.Button();
            this.btSpeedX2 = new System.Windows.Forms.Button();
            this.btRecord = new System.Windows.Forms.Button();
            this.btPlay = new System.Windows.Forms.Button();
            this.btRefresh = new System.Windows.Forms.Button();
            this.ssStatus.SuspendLayout();
            this.pButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbHeatsList
            // 
            this.lbHeatsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbHeatsList.FormattingEnabled = true;
            this.lbHeatsList.Location = new System.Drawing.Point(3, 12);
            this.lbHeatsList.Name = "lbHeatsList";
            this.lbHeatsList.Size = new System.Drawing.Size(120, 394);
            this.lbHeatsList.TabIndex = 2;
            this.lbHeatsList.DoubleClick += new System.EventHandler(this.btPlay_Click);
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMessage,
            this.StatusText});
            this.ssStatus.Location = new System.Drawing.Point(0, 457);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Size = new System.Drawing.Size(561, 22);
            this.ssStatus.TabIndex = 3;
            this.ssStatus.Text = "statusStrip1";
            // 
            // tsslMessage
            // 
            this.tsslMessage.AccessibleDescription = "";
            this.tsslMessage.Name = "tsslMessage";
            this.tsslMessage.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(0, 17);
            // 
            // tbMessages
            // 
            this.tbMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessages.Location = new System.Drawing.Point(129, 12);
            this.tbMessages.Multiline = true;
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbMessages.Size = new System.Drawing.Size(420, 395);
            this.tbMessages.TabIndex = 4;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(299, 413);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(250, 34);
            this.pbProgress.TabIndex = 5;
            // 
            // pButtonsPanel
            // 
            this.pButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pButtonsPanel.Controls.Add(this.btSpeedX1);
            this.pButtonsPanel.Controls.Add(this.btSpeedx5);
            this.pButtonsPanel.Controls.Add(this.btSpeedX10);
            this.pButtonsPanel.Controls.Add(this.btSpeedX2);
            this.pButtonsPanel.Controls.Add(this.btRecord);
            this.pButtonsPanel.Controls.Add(this.btPlay);
            this.pButtonsPanel.Controls.Add(this.btRefresh);
            this.pButtonsPanel.Location = new System.Drawing.Point(3, 410);
            this.pButtonsPanel.Name = "pButtonsPanel";
            this.pButtonsPanel.Size = new System.Drawing.Size(290, 44);
            this.pButtonsPanel.TabIndex = 7;
            // 
            // btSpeedX1
            // 
            this.btSpeedX1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btSpeedX1.FlatAppearance.BorderSize = 0;
            this.btSpeedX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSpeedX1.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.btSpeedX1.Location = new System.Drawing.Point(128, 2);
            this.btSpeedX1.Name = "btSpeedX1";
            this.btSpeedX1.Size = new System.Drawing.Size(40, 40);
            this.btSpeedX1.TabIndex = 10;
            this.btSpeedX1.Text = "x1";
            this.btSpeedX1.UseVisualStyleBackColor = false;
            this.btSpeedX1.Click += new System.EventHandler(this.btSpeedX1_Click);
            // 
            // btSpeedx5
            // 
            this.btSpeedx5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btSpeedx5.FlatAppearance.BorderSize = 0;
            this.btSpeedx5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSpeedx5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btSpeedx5.Location = new System.Drawing.Point(210, 2);
            this.btSpeedx5.Name = "btSpeedx5";
            this.btSpeedx5.Size = new System.Drawing.Size(40, 40);
            this.btSpeedx5.TabIndex = 7;
            this.btSpeedx5.Text = "x5";
            this.btSpeedx5.UseVisualStyleBackColor = false;
            this.btSpeedx5.Click += new System.EventHandler(this.btSpeedx5_Click);
            // 
            // btSpeedX10
            // 
            this.btSpeedX10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btSpeedX10.FlatAppearance.BorderSize = 0;
            this.btSpeedX10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSpeedX10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btSpeedX10.Location = new System.Drawing.Point(250, 2);
            this.btSpeedX10.Name = "btSpeedX10";
            this.btSpeedX10.Size = new System.Drawing.Size(40, 40);
            this.btSpeedX10.TabIndex = 8;
            this.btSpeedX10.Text = "x10";
            this.btSpeedX10.UseVisualStyleBackColor = true;
            this.btSpeedX10.Click += new System.EventHandler(this.btSpeedX10_Click);
            // 
            // btSpeedX2
            // 
            this.btSpeedX2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btSpeedX2.FlatAppearance.BorderSize = 0;
            this.btSpeedX2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btSpeedX2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btSpeedX2.Location = new System.Drawing.Point(169, 2);
            this.btSpeedX2.Name = "btSpeedX2";
            this.btSpeedX2.Size = new System.Drawing.Size(40, 40);
            this.btSpeedX2.TabIndex = 9;
            this.btSpeedX2.Text = "x2";
            this.btSpeedX2.UseVisualStyleBackColor = false;
            this.btSpeedX2.Click += new System.EventHandler(this.btSpeedX2_Click);
            // 
            // btRecord
            // 
            this.btRecord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRecord.FlatAppearance.BorderSize = 0;
            this.btRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRecord.Image = global::EventsPlayer.Properties.Resources.record_mini;
            this.btRecord.Location = new System.Drawing.Point(42, 2);
            this.btRecord.Name = "btRecord";
            this.btRecord.Size = new System.Drawing.Size(40, 40);
            this.btRecord.TabIndex = 0;
            this.btRecord.UseVisualStyleBackColor = false;
            this.btRecord.Click += new System.EventHandler(this.btRecord_Click);
            // 
            // btPlay
            // 
            this.btPlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btPlay.FlatAppearance.BorderSize = 0;
            this.btPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPlay.Image = global::EventsPlayer.Properties.Resources.play_mini;
            this.btPlay.Location = new System.Drawing.Point(82, 2);
            this.btPlay.Name = "btPlay";
            this.btPlay.Size = new System.Drawing.Size(40, 40);
            this.btPlay.TabIndex = 1;
            this.btPlay.UseVisualStyleBackColor = true;
            this.btPlay.Click += new System.EventHandler(this.btPlay_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRefresh.FlatAppearance.BorderSize = 0;
            this.btRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btRefresh.Image")));
            this.btRefresh.Location = new System.Drawing.Point(1, 2);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(40, 40);
            this.btRefresh.TabIndex = 6;
            this.btRefresh.UseVisualStyleBackColor = false;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 479);
            this.Controls.Add(this.pButtonsPanel);
            this.Controls.Add(this.tbMessages);
            this.Controls.Add(this.pbProgress);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.lbHeatsList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Плеер событий";
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.pButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btRecord;
        private System.Windows.Forms.Button btPlay;
        private System.Windows.Forms.ListBox lbHeatsList;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsslMessage;
        private System.Windows.Forms.TextBox tbMessages;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.Panel pButtonsPanel;
        private System.Windows.Forms.Button btSpeedX1;
        private System.Windows.Forms.Button btSpeedx5;
        private System.Windows.Forms.Button btSpeedX10;
        private System.Windows.Forms.Button btSpeedX2;
    }
}

