namespace charts
{
    partial class Trends
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trends));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.предToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.следToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pVars = new System.Windows.Forms.Panel();
            this.gbVars = new System.Windows.Forms.GroupBox();
            this.gbValues = new System.Windows.Forms.GroupBox();
            this.lbTimeValue = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.cbO2 = new System.Windows.Forms.CheckBox();
            this.cbCO = new System.Windows.Forms.CheckBox();
            this.cbCO2 = new System.Windows.Forms.CheckBox();
            this.cbN2 = new System.Windows.Forms.CheckBox();
            this.cbAr = new System.Windows.Forms.CheckBox();
            this.cbH2 = new System.Windows.Forms.CheckBox();
            this.lbArValue = new System.Windows.Forms.Label();
            this.lbO2Value = new System.Windows.Forms.Label();
            this.lbCOValue = new System.Windows.Forms.Label();
            this.lbN2Value = new System.Windows.Forms.Label();
            this.lbCO2Value = new System.Windows.Forms.Label();
            this.lbH2Value = new System.Windows.Forms.Label();
            this.lbAr = new System.Windows.Forms.Label();
            this.lbO2 = new System.Windows.Forms.Label();
            this.lbCO = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCO2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbN2 = new System.Windows.Forms.Label();
            this.lbH2 = new System.Windows.Forms.Label();
            this.zgMain = new ZedGraph.ZedGraphControl();
            this.pAddVar = new System.Windows.Forms.Panel();
            this.btAddVar = new System.Windows.Forms.Button();
            this.tbVarExpression = new System.Windows.Forms.TextBox();
            this.tbVarName = new System.Windows.Forms.TextBox();
            this.lbVarExpression = new System.Windows.Forms.Label();
            this.lbVarName = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            this.pVars.SuspendLayout();
            this.gbValues.SuspendLayout();
            this.pAddVar.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.предToolStripMenuItem,
            this.следToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mainMenu.Size = new System.Drawing.Size(882, 24);
            this.mainMenu.TabIndex = 2;
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьФайлToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьФайлToolStripMenuItem
            // 
            this.открытьФайлToolStripMenuItem.Name = "открытьФайлToolStripMenuItem";
            this.открытьФайлToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.открытьФайлToolStripMenuItem.Text = "Открыть";
            this.открытьФайлToolStripMenuItem.Click += new System.EventHandler(this.открытьФайлToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // предToolStripMenuItem
            // 
            this.предToolStripMenuItem.Name = "предToolStripMenuItem";
            this.предToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.предToolStripMenuItem.Text = "<-- Пред ";
            this.предToolStripMenuItem.Click += new System.EventHandler(this.предToolStripMenuItem_Click);
            // 
            // следToolStripMenuItem
            // 
            this.следToolStripMenuItem.Name = "следToolStripMenuItem";
            this.следToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.следToolStripMenuItem.Text = "След -->";
            this.следToolStripMenuItem.Click += new System.EventHandler(this.следToolStripMenuItem_Click);
            // 
            // pVars
            // 
            this.pVars.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.pVars.Controls.Add(this.gbVars);
            this.pVars.Controls.Add(this.gbValues);
            this.pVars.Location = new System.Drawing.Point(0, 24);
            this.pVars.Name = "pVars";
            this.pVars.Size = new System.Drawing.Size(209, 385);
            this.pVars.TabIndex = 3;
            this.pVars.Visible = false;
            // 
            // gbVars
            // 
            this.gbVars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbVars.Location = new System.Drawing.Point(0, 158);
            this.gbVars.Name = "gbVars";
            this.gbVars.Size = new System.Drawing.Size(206, 227);
            this.gbVars.TabIndex = 1;
            this.gbVars.TabStop = false;
            this.gbVars.Text = "Функции";
            // 
            // gbValues
            // 
            this.gbValues.Controls.Add(this.lbTimeValue);
            this.gbValues.Controls.Add(this.lbTime);
            this.gbValues.Controls.Add(this.cbO2);
            this.gbValues.Controls.Add(this.cbCO);
            this.gbValues.Controls.Add(this.cbCO2);
            this.gbValues.Controls.Add(this.cbN2);
            this.gbValues.Controls.Add(this.cbAr);
            this.gbValues.Controls.Add(this.cbH2);
            this.gbValues.Controls.Add(this.lbArValue);
            this.gbValues.Controls.Add(this.lbO2Value);
            this.gbValues.Controls.Add(this.lbCOValue);
            this.gbValues.Controls.Add(this.lbN2Value);
            this.gbValues.Controls.Add(this.lbCO2Value);
            this.gbValues.Controls.Add(this.lbH2Value);
            this.gbValues.Controls.Add(this.lbAr);
            this.gbValues.Controls.Add(this.lbO2);
            this.gbValues.Controls.Add(this.lbCO);
            this.gbValues.Controls.Add(this.label1);
            this.gbValues.Controls.Add(this.lbCO2);
            this.gbValues.Controls.Add(this.label3);
            this.gbValues.Controls.Add(this.lbN2);
            this.gbValues.Controls.Add(this.lbH2);
            this.gbValues.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbValues.Location = new System.Drawing.Point(0, 0);
            this.gbValues.Name = "gbValues";
            this.gbValues.Size = new System.Drawing.Size(209, 158);
            this.gbValues.TabIndex = 0;
            this.gbValues.TabStop = false;
            this.gbValues.Text = "Значения";
            this.gbValues.Visible = false;
            // 
            // lbTimeValue
            // 
            this.lbTimeValue.AutoSize = true;
            this.lbTimeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTimeValue.Location = new System.Drawing.Point(81, 139);
            this.lbTimeValue.Name = "lbTimeValue";
            this.lbTimeValue.Size = new System.Drawing.Size(0, 16);
            this.lbTimeValue.TabIndex = 18;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbTime.Location = new System.Drawing.Point(21, 139);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(54, 16);
            this.lbTime.TabIndex = 17;
            this.lbTime.Text = "Время";
            // 
            // cbO2
            // 
            this.cbO2.AutoSize = true;
            this.cbO2.Checked = true;
            this.cbO2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbO2.Location = new System.Drawing.Point(24, 39);
            this.cbO2.Name = "cbO2";
            this.cbO2.Size = new System.Drawing.Size(15, 14);
            this.cbO2.TabIndex = 16;
            this.cbO2.UseVisualStyleBackColor = true;
            this.cbO2.CheckedChanged += new System.EventHandler(this.cbO2_CheckedChanged);
            // 
            // cbCO
            // 
            this.cbCO.AutoSize = true;
            this.cbCO.Checked = true;
            this.cbCO.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCO.Location = new System.Drawing.Point(24, 59);
            this.cbCO.Name = "cbCO";
            this.cbCO.Size = new System.Drawing.Size(15, 14);
            this.cbCO.TabIndex = 15;
            this.cbCO.UseVisualStyleBackColor = true;
            this.cbCO.CheckedChanged += new System.EventHandler(this.cbCO_CheckedChanged);
            // 
            // cbCO2
            // 
            this.cbCO2.AutoSize = true;
            this.cbCO2.Checked = true;
            this.cbCO2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCO2.Location = new System.Drawing.Point(24, 79);
            this.cbCO2.Name = "cbCO2";
            this.cbCO2.Size = new System.Drawing.Size(15, 14);
            this.cbCO2.TabIndex = 14;
            this.cbCO2.UseVisualStyleBackColor = true;
            this.cbCO2.CheckedChanged += new System.EventHandler(this.cbCO2_CheckedChanged);
            // 
            // cbN2
            // 
            this.cbN2.AutoSize = true;
            this.cbN2.Checked = true;
            this.cbN2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbN2.Location = new System.Drawing.Point(24, 98);
            this.cbN2.Name = "cbN2";
            this.cbN2.Size = new System.Drawing.Size(15, 14);
            this.cbN2.TabIndex = 13;
            this.cbN2.UseVisualStyleBackColor = true;
            this.cbN2.CheckedChanged += new System.EventHandler(this.cbN2_CheckedChanged);
            // 
            // cbAr
            // 
            this.cbAr.AutoSize = true;
            this.cbAr.Checked = true;
            this.cbAr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAr.Location = new System.Drawing.Point(24, 118);
            this.cbAr.Name = "cbAr";
            this.cbAr.Size = new System.Drawing.Size(15, 14);
            this.cbAr.TabIndex = 12;
            this.cbAr.UseVisualStyleBackColor = true;
            this.cbAr.CheckedChanged += new System.EventHandler(this.cbAr_CheckedChanged);
            // 
            // cbH2
            // 
            this.cbH2.AutoSize = true;
            this.cbH2.Checked = true;
            this.cbH2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbH2.Location = new System.Drawing.Point(24, 19);
            this.cbH2.Name = "cbH2";
            this.cbH2.Size = new System.Drawing.Size(15, 14);
            this.cbH2.TabIndex = 5;
            this.cbH2.UseVisualStyleBackColor = true;
            this.cbH2.CheckedChanged += new System.EventHandler(this.cbH2_CheckedChanged);
            // 
            // lbArValue
            // 
            this.lbArValue.AutoSize = true;
            this.lbArValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbArValue.Location = new System.Drawing.Point(75, 116);
            this.lbArValue.Name = "lbArValue";
            this.lbArValue.Size = new System.Drawing.Size(16, 16);
            this.lbArValue.TabIndex = 11;
            this.lbArValue.Text = "0";
            // 
            // lbO2Value
            // 
            this.lbO2Value.AutoSize = true;
            this.lbO2Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbO2Value.Location = new System.Drawing.Point(74, 37);
            this.lbO2Value.Name = "lbO2Value";
            this.lbO2Value.Size = new System.Drawing.Size(16, 16);
            this.lbO2Value.TabIndex = 10;
            this.lbO2Value.Text = "0";
            // 
            // lbCOValue
            // 
            this.lbCOValue.AutoSize = true;
            this.lbCOValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCOValue.Location = new System.Drawing.Point(75, 59);
            this.lbCOValue.Name = "lbCOValue";
            this.lbCOValue.Size = new System.Drawing.Size(16, 16);
            this.lbCOValue.TabIndex = 9;
            this.lbCOValue.Text = "0";
            // 
            // lbN2Value
            // 
            this.lbN2Value.AutoSize = true;
            this.lbN2Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbN2Value.Location = new System.Drawing.Point(75, 98);
            this.lbN2Value.Name = "lbN2Value";
            this.lbN2Value.Size = new System.Drawing.Size(16, 16);
            this.lbN2Value.TabIndex = 8;
            this.lbN2Value.Text = "0";
            // 
            // lbCO2Value
            // 
            this.lbCO2Value.AutoSize = true;
            this.lbCO2Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCO2Value.Location = new System.Drawing.Point(75, 79);
            this.lbCO2Value.Name = "lbCO2Value";
            this.lbCO2Value.Size = new System.Drawing.Size(16, 16);
            this.lbCO2Value.TabIndex = 7;
            this.lbCO2Value.Text = "0";
            // 
            // lbH2Value
            // 
            this.lbH2Value.AutoSize = true;
            this.lbH2Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbH2Value.Location = new System.Drawing.Point(75, 16);
            this.lbH2Value.Name = "lbH2Value";
            this.lbH2Value.Size = new System.Drawing.Size(16, 16);
            this.lbH2Value.TabIndex = 6;
            this.lbH2Value.Text = "0";
            // 
            // lbAr
            // 
            this.lbAr.AutoSize = true;
            this.lbAr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbAr.ForeColor = System.Drawing.Color.Turquoise;
            this.lbAr.Location = new System.Drawing.Point(42, 116);
            this.lbAr.Name = "lbAr";
            this.lbAr.Size = new System.Drawing.Size(23, 16);
            this.lbAr.TabIndex = 5;
            this.lbAr.Text = "Ar";
            // 
            // lbO2
            // 
            this.lbO2.AutoSize = true;
            this.lbO2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbO2.ForeColor = System.Drawing.Color.Blue;
            this.lbO2.Location = new System.Drawing.Point(41, 37);
            this.lbO2.Name = "lbO2";
            this.lbO2.Size = new System.Drawing.Size(27, 16);
            this.lbO2.TabIndex = 4;
            this.lbO2.Text = "O2";
            // 
            // lbCO
            // 
            this.lbCO.AutoSize = true;
            this.lbCO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCO.ForeColor = System.Drawing.Color.Red;
            this.lbCO.Location = new System.Drawing.Point(41, 58);
            this.lbCO.Name = "lbCO";
            this.lbCO.Size = new System.Drawing.Size(29, 16);
            this.lbCO.TabIndex = 3;
            this.lbCO.Text = "CO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(41, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "CO2";
            // 
            // lbCO2
            // 
            this.lbCO2.AutoSize = true;
            this.lbCO2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbCO2.Location = new System.Drawing.Point(41, 79);
            this.lbCO2.Name = "lbCO2";
            this.lbCO2.Size = new System.Drawing.Size(37, 16);
            this.lbCO2.TabIndex = 2;
            this.lbCO2.Text = "CO2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(42, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "N2";
            // 
            // lbN2
            // 
            this.lbN2.AutoSize = true;
            this.lbN2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbN2.Location = new System.Drawing.Point(42, 97);
            this.lbN2.Name = "lbN2";
            this.lbN2.Size = new System.Drawing.Size(27, 16);
            this.lbN2.TabIndex = 1;
            this.lbN2.Text = "N2";
            // 
            // lbH2
            // 
            this.lbH2.AutoSize = true;
            this.lbH2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbH2.ForeColor = System.Drawing.Color.Green;
            this.lbH2.Location = new System.Drawing.Point(42, 17);
            this.lbH2.Name = "lbH2";
            this.lbH2.Size = new System.Drawing.Size(27, 16);
            this.lbH2.TabIndex = 0;
            this.lbH2.Text = "H2";
            // 
            // zgMain
            // 
            this.zgMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.zgMain.IsEnableHZoom = false;
            this.zgMain.IsEnableVZoom = false;
            this.zgMain.Location = new System.Drawing.Point(215, 24);
            this.zgMain.Name = "zgMain";
            this.zgMain.ScrollGrace = 0D;
            this.zgMain.ScrollMaxX = 0D;
            this.zgMain.ScrollMaxY = 0D;
            this.zgMain.ScrollMaxY2 = 0D;
            this.zgMain.ScrollMinX = 0D;
            this.zgMain.ScrollMinY = 0D;
            this.zgMain.ScrollMinY2 = 0D;
            this.zgMain.Size = new System.Drawing.Size(667, 385);
            this.zgMain.TabIndex = 4;
            this.zgMain.Visible = false;
            this.zgMain.PointValueEvent += new ZedGraph.ZedGraphControl.PointValueHandler(this.zgMain_PointValueEvent);
            this.zgMain.CursorValueEvent += new ZedGraph.ZedGraphControl.CursorValueHandler(this.zgMain_CursorValueEvent);
            this.zgMain.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zgMain_MouseMoveEvent);
            this.zgMain.Click += new System.EventHandler(this.zgMain_Click);
            // 
            // pAddVar
            // 
            this.pAddVar.Controls.Add(this.btAddVar);
            this.pAddVar.Controls.Add(this.tbVarExpression);
            this.pAddVar.Controls.Add(this.tbVarName);
            this.pAddVar.Controls.Add(this.lbVarExpression);
            this.pAddVar.Controls.Add(this.lbVarName);
            this.pAddVar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pAddVar.Location = new System.Drawing.Point(0, 415);
            this.pAddVar.Name = "pAddVar";
            this.pAddVar.Size = new System.Drawing.Size(882, 33);
            this.pAddVar.TabIndex = 5;
            this.pAddVar.Visible = false;
            // 
            // btAddVar
            // 
            this.btAddVar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btAddVar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btAddVar.Location = new System.Drawing.Point(796, 0);
            this.btAddVar.Name = "btAddVar";
            this.btAddVar.Size = new System.Drawing.Size(86, 33);
            this.btAddVar.TabIndex = 4;
            this.btAddVar.Text = "Сохранить";
            this.btAddVar.UseVisualStyleBackColor = true;
            this.btAddVar.Click += new System.EventHandler(this.btAddVar_Click);
            // 
            // tbVarExpression
            // 
            this.tbVarExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVarExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbVarExpression.Location = new System.Drawing.Point(178, 6);
            this.tbVarExpression.Name = "tbVarExpression";
            this.tbVarExpression.Size = new System.Drawing.Size(611, 22);
            this.tbVarExpression.TabIndex = 3;
            // 
            // tbVarName
            // 
            this.tbVarName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbVarName.Location = new System.Drawing.Point(44, 7);
            this.tbVarName.Name = "tbVarName";
            this.tbVarName.Size = new System.Drawing.Size(60, 22);
            this.tbVarName.TabIndex = 2;
            // 
            // lbVarExpression
            // 
            this.lbVarExpression.AutoSize = true;
            this.lbVarExpression.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbVarExpression.Location = new System.Drawing.Point(110, 11);
            this.lbVarExpression.Name = "lbVarExpression";
            this.lbVarExpression.Size = new System.Drawing.Size(63, 13);
            this.lbVarExpression.TabIndex = 1;
            this.lbVarExpression.Text = "Значение";
            // 
            // lbVarName
            // 
            this.lbVarName.AutoSize = true;
            this.lbVarName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbVarName.Location = new System.Drawing.Point(9, 11);
            this.lbVarName.Name = "lbVarName";
            this.lbVarName.Size = new System.Drawing.Size(32, 13);
            this.lbVarName.TabIndex = 0;
            this.lbVarName.Text = "Имя";
            // 
            // Trends
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 448);
            this.Controls.Add(this.pAddVar);
            this.Controls.Add(this.zgMain);
            this.Controls.Add(this.pVars);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Trends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тренды";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.pVars.ResumeLayout(false);
            this.gbValues.ResumeLayout(false);
            this.gbValues.PerformLayout();
            this.pAddVar.ResumeLayout(false);
            this.pAddVar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem предToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem следToolStripMenuItem;
        private System.Windows.Forms.Panel pVars;
        private System.Windows.Forms.GroupBox gbValues;
        private System.Windows.Forms.Label lbAr;
        private System.Windows.Forms.Label lbO2;
        private System.Windows.Forms.Label lbCO;
        private System.Windows.Forms.Label lbCO2;
        private System.Windows.Forms.Label lbN2;
        private System.Windows.Forms.Label lbH2;
        private System.Windows.Forms.Label lbArValue;
        private System.Windows.Forms.Label lbO2Value;
        private System.Windows.Forms.Label lbN2Value;
        private System.Windows.Forms.Label lbCO2Value;
        private System.Windows.Forms.Label lbH2Value;
        private ZedGraph.ZedGraphControl zgMain;
        private System.Windows.Forms.CheckBox cbO2;
        private System.Windows.Forms.CheckBox cbCO;
        private System.Windows.Forms.CheckBox cbCO2;
        private System.Windows.Forms.CheckBox cbN2;
        private System.Windows.Forms.CheckBox cbAr;
        private System.Windows.Forms.CheckBox cbH2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbCOValue;
        private System.Windows.Forms.Panel pAddVar;
        private System.Windows.Forms.GroupBox gbVars;
        private System.Windows.Forms.TextBox tbVarExpression;
        private System.Windows.Forms.TextBox tbVarName;
        private System.Windows.Forms.Label lbVarExpression;
        private System.Windows.Forms.Label lbVarName;
        private System.Windows.Forms.Button btAddVar;
        private System.Windows.Forms.Label lbTimeValue;
        private System.Windows.Forms.Label lbTime;
    }
}

