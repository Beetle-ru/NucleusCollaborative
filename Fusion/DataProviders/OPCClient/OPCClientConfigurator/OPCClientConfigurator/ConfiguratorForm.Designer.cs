namespace OPCClientConfigurator
{
    partial class ConfiguratorForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxEvents = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEvents = new System.Windows.Forms.ComboBox();
            this.buttonGetModule = new System.Windows.Forms.Button();
            this.textBoxModule = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxGroups = new System.Windows.Forms.GroupBox();
            this.checkBoxIsWriteble = new System.Windows.Forms.CheckBox();
            this.groupBoxPoints = new System.Windows.Forms.GroupBox();
            this.textBoxBitNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBoxIsBoolean = new System.Windows.Forms.CheckBox();
            this.buttonPointRemove = new System.Windows.Forms.Button();
            this.buttonPointAdd = new System.Windows.Forms.Button();
            this.listBoxProperties = new System.Windows.Forms.ListBox();
            this.listBoxPoints = new System.Windows.Forms.ListBox();
            this.comboBoxPointEncoding = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxPointLocation = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxPointName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxPointType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxGroupFilterPropertyValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxGroupFilterPropertyName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxGroupDestination = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxGroupLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxGroups = new System.Windows.Forms.ComboBox();
            this.buttonRemoveGroup = new System.Windows.Forms.Button();
            this.buttonAddGroup = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.groupBoxEvents.SuspendLayout();
            this.groupBoxGroups.SuspendLayout();
            this.groupBoxPoints.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.новыйToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(937, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuMain";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem1.Text = " ";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem1,
            this.открытьToolStripMenuItem,
            this.toolStripSeparator1,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.новыйToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem1
            // 
            this.новыйToolStripMenuItem1.Name = "новыйToolStripMenuItem1";
            this.новыйToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.новыйToolStripMenuItem1.Text = "Новый";
            this.новыйToolStripMenuItem1.Click += new System.EventHandler(this.новыйToolStripMenuItem1_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.открытьToolStripMenuItem.Text = "Открыть ...";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как ...";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(162, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // groupBoxEvents
            // 
            this.groupBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxEvents.Controls.Add(this.label2);
            this.groupBoxEvents.Controls.Add(this.comboBoxEvents);
            this.groupBoxEvents.Controls.Add(this.buttonGetModule);
            this.groupBoxEvents.Controls.Add(this.textBoxModule);
            this.groupBoxEvents.Controls.Add(this.label1);
            this.groupBoxEvents.Location = new System.Drawing.Point(12, 28);
            this.groupBoxEvents.Name = "groupBoxEvents";
            this.groupBoxEvents.Size = new System.Drawing.Size(242, 278);
            this.groupBoxEvents.TabIndex = 1;
            this.groupBoxEvents.TabStop = false;
            this.groupBoxEvents.Text = "События";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Список событий:";
            // 
            // comboBoxEvents
            // 
            this.comboBoxEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxEvents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.comboBoxEvents.Enabled = false;
            this.comboBoxEvents.FormattingEnabled = true;
            this.comboBoxEvents.Location = new System.Drawing.Point(9, 73);
            this.comboBoxEvents.Name = "comboBoxEvents";
            this.comboBoxEvents.Size = new System.Drawing.Size(227, 197);
            this.comboBoxEvents.TabIndex = 6;
            this.comboBoxEvents.SelectedIndexChanged += new System.EventHandler(this.comboBoxEvents_SelectedIndexChanged);
            // 
            // buttonGetModule
            // 
            this.buttonGetModule.Location = new System.Drawing.Point(207, 29);
            this.buttonGetModule.Name = "buttonGetModule";
            this.buttonGetModule.Size = new System.Drawing.Size(29, 23);
            this.buttonGetModule.TabIndex = 5;
            this.buttonGetModule.Text = "...";
            this.buttonGetModule.UseVisualStyleBackColor = true;
            this.buttonGetModule.Click += new System.EventHandler(this.buttonGetModule_Click);
            // 
            // textBoxModule
            // 
            this.textBoxModule.Location = new System.Drawing.Point(9, 32);
            this.textBoxModule.Name = "textBoxModule";
            this.textBoxModule.ReadOnly = true;
            this.textBoxModule.Size = new System.Drawing.Size(192, 20);
            this.textBoxModule.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Сборка:";
            // 
            // groupBoxGroups
            // 
            this.groupBoxGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxGroups.Controls.Add(this.checkBoxIsWriteble);
            this.groupBoxGroups.Controls.Add(this.groupBoxPoints);
            this.groupBoxGroups.Controls.Add(this.textBoxGroupFilterPropertyValue);
            this.groupBoxGroups.Controls.Add(this.label8);
            this.groupBoxGroups.Controls.Add(this.comboBoxGroupFilterPropertyName);
            this.groupBoxGroups.Controls.Add(this.label7);
            this.groupBoxGroups.Controls.Add(this.textBoxGroupDestination);
            this.groupBoxGroups.Controls.Add(this.label6);
            this.groupBoxGroups.Controls.Add(this.textBoxGroupLocation);
            this.groupBoxGroups.Controls.Add(this.label5);
            this.groupBoxGroups.Controls.Add(this.label4);
            this.groupBoxGroups.Controls.Add(this.comboBoxGroups);
            this.groupBoxGroups.Controls.Add(this.buttonRemoveGroup);
            this.groupBoxGroups.Controls.Add(this.buttonAddGroup);
            this.groupBoxGroups.Enabled = false;
            this.groupBoxGroups.Location = new System.Drawing.Point(271, 28);
            this.groupBoxGroups.Name = "groupBoxGroups";
            this.groupBoxGroups.Size = new System.Drawing.Size(658, 278);
            this.groupBoxGroups.TabIndex = 2;
            this.groupBoxGroups.TabStop = false;
            this.groupBoxGroups.Text = "Группы";
            // 
            // checkBoxIsWriteble
            // 
            this.checkBoxIsWriteble.AutoSize = true;
            this.checkBoxIsWriteble.Location = new System.Drawing.Point(92, 11);
            this.checkBoxIsWriteble.Name = "checkBoxIsWriteble";
            this.checkBoxIsWriteble.Size = new System.Drawing.Size(77, 17);
            this.checkBoxIsWriteble.TabIndex = 20;
            this.checkBoxIsWriteble.Text = "на запись";
            this.checkBoxIsWriteble.UseVisualStyleBackColor = true;
            this.checkBoxIsWriteble.CheckedChanged += new System.EventHandler(this.checkBoxIsWriteble_CheckedChanged);
            // 
            // groupBoxPoints
            // 
            this.groupBoxPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPoints.Controls.Add(this.textBoxBitNumber);
            this.groupBoxPoints.Controls.Add(this.label12);
            this.groupBoxPoints.Controls.Add(this.checkBoxIsBoolean);
            this.groupBoxPoints.Controls.Add(this.buttonPointRemove);
            this.groupBoxPoints.Controls.Add(this.buttonPointAdd);
            this.groupBoxPoints.Controls.Add(this.listBoxProperties);
            this.groupBoxPoints.Controls.Add(this.listBoxPoints);
            this.groupBoxPoints.Controls.Add(this.comboBoxPointEncoding);
            this.groupBoxPoints.Controls.Add(this.label11);
            this.groupBoxPoints.Controls.Add(this.textBoxPointLocation);
            this.groupBoxPoints.Controls.Add(this.label10);
            this.groupBoxPoints.Controls.Add(this.textBoxPointName);
            this.groupBoxPoints.Controls.Add(this.label9);
            this.groupBoxPoints.Controls.Add(this.textBoxPointType);
            this.groupBoxPoints.Controls.Add(this.label3);
            this.groupBoxPoints.Enabled = false;
            this.groupBoxPoints.Location = new System.Drawing.Point(6, 52);
            this.groupBoxPoints.Name = "groupBoxPoints";
            this.groupBoxPoints.Size = new System.Drawing.Size(640, 220);
            this.groupBoxPoints.TabIndex = 19;
            this.groupBoxPoints.TabStop = false;
            this.groupBoxPoints.Text = "Точки";
            // 
            // textBoxBitNumber
            // 
            this.textBoxBitNumber.Location = new System.Drawing.Point(539, 31);
            this.textBoxBitNumber.Name = "textBoxBitNumber";
            this.textBoxBitNumber.Size = new System.Drawing.Size(95, 20);
            this.textBoxBitNumber.TabIndex = 26;
            this.textBoxBitNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBitNumber_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(537, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Номер бита:";
            // 
            // checkBoxIsBoolean
            // 
            this.checkBoxIsBoolean.AutoSize = true;
            this.checkBoxIsBoolean.Location = new System.Drawing.Point(476, 33);
            this.checkBoxIsBoolean.Name = "checkBoxIsBoolean";
            this.checkBoxIsBoolean.Size = new System.Drawing.Size(68, 17);
            this.checkBoxIsBoolean.TabIndex = 24;
            this.checkBoxIsBoolean.Text = "Булевое";
            this.checkBoxIsBoolean.UseVisualStyleBackColor = true;
            // 
            // buttonPointRemove
            // 
            this.buttonPointRemove.Enabled = false;
            this.buttonPointRemove.Location = new System.Drawing.Point(295, 148);
            this.buttonPointRemove.Name = "buttonPointRemove";
            this.buttonPointRemove.Size = new System.Drawing.Size(29, 23);
            this.buttonPointRemove.TabIndex = 23;
            this.buttonPointRemove.Text = "<<";
            this.buttonPointRemove.UseVisualStyleBackColor = true;
            this.buttonPointRemove.Click += new System.EventHandler(this.buttonPointRemove_Click);
            // 
            // buttonPointAdd
            // 
            this.buttonPointAdd.Enabled = false;
            this.buttonPointAdd.Location = new System.Drawing.Point(295, 119);
            this.buttonPointAdd.Name = "buttonPointAdd";
            this.buttonPointAdd.Size = new System.Drawing.Size(29, 23);
            this.buttonPointAdd.TabIndex = 22;
            this.buttonPointAdd.Text = ">>";
            this.buttonPointAdd.UseVisualStyleBackColor = true;
            this.buttonPointAdd.Click += new System.EventHandler(this.buttonPointAdd_Click);
            // 
            // listBoxProperties
            // 
            this.listBoxProperties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxProperties.DisplayMember = "Name";
            this.listBoxProperties.FormattingEnabled = true;
            this.listBoxProperties.Location = new System.Drawing.Point(9, 59);
            this.listBoxProperties.Name = "listBoxProperties";
            this.listBoxProperties.Size = new System.Drawing.Size(283, 147);
            this.listBoxProperties.TabIndex = 21;
            this.listBoxProperties.SelectedIndexChanged += new System.EventHandler(this.listBoxProperties_SelectedIndexChanged);
            // 
            // listBoxPoints
            // 
            this.listBoxPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPoints.Enabled = false;
            this.listBoxPoints.FormattingEnabled = true;
            this.listBoxPoints.Location = new System.Drawing.Point(327, 59);
            this.listBoxPoints.Name = "listBoxPoints";
            this.listBoxPoints.Size = new System.Drawing.Size(307, 147);
            this.listBoxPoints.TabIndex = 20;
            // 
            // comboBoxPointEncoding
            // 
            this.comboBoxPointEncoding.DisplayMember = "WebName";
            this.comboBoxPointEncoding.Enabled = false;
            this.comboBoxPointEncoding.FormattingEnabled = true;
            this.comboBoxPointEncoding.Location = new System.Drawing.Point(327, 32);
            this.comboBoxPointEncoding.Name = "comboBoxPointEncoding";
            this.comboBoxPointEncoding.Size = new System.Drawing.Size(142, 21);
            this.comboBoxPointEncoding.TabIndex = 19;
            this.comboBoxPointEncoding.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxPointEncoding_KeyPress);
            this.comboBoxPointEncoding.Leave += new System.EventHandler(this.textBoxPointLocation_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(325, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Кодировка:";
            // 
            // textBoxPointLocation
            // 
            this.textBoxPointLocation.Location = new System.Drawing.Point(231, 33);
            this.textBoxPointLocation.Name = "textBoxPointLocation";
            this.textBoxPointLocation.Size = new System.Drawing.Size(90, 20);
            this.textBoxPointLocation.TabIndex = 17;
            this.textBoxPointLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPointLocation_KeyPress);
            this.textBoxPointLocation.Leave += new System.EventHandler(this.textBoxPointLocation_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(229, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "PLC адрес:";
            // 
            // textBoxPointName
            // 
            this.textBoxPointName.Location = new System.Drawing.Point(105, 33);
            this.textBoxPointName.Name = "textBoxPointName";
            this.textBoxPointName.ReadOnly = true;
            this.textBoxPointName.Size = new System.Drawing.Size(120, 20);
            this.textBoxPointName.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(102, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Имя:";
            // 
            // textBoxPointType
            // 
            this.textBoxPointType.Location = new System.Drawing.Point(9, 33);
            this.textBoxPointType.Name = "textBoxPointType";
            this.textBoxPointType.ReadOnly = true;
            this.textBoxPointType.Size = new System.Drawing.Size(90, 20);
            this.textBoxPointType.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Тип:";
            // 
            // textBoxGroupFilterPropertyValue
            // 
            this.textBoxGroupFilterPropertyValue.Location = new System.Drawing.Point(519, 31);
            this.textBoxGroupFilterPropertyValue.Name = "textBoxGroupFilterPropertyValue";
            this.textBoxGroupFilterPropertyValue.Size = new System.Drawing.Size(65, 20);
            this.textBoxGroupFilterPropertyValue.TabIndex = 18;
            this.textBoxGroupFilterPropertyValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxGroups_KeyPress);
            this.textBoxGroupFilterPropertyValue.Leave += new System.EventHandler(this.comboBoxGroups_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(517, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Значение фильтр:";
            // 
            // comboBoxGroupFilterPropertyName
            // 
            this.comboBoxGroupFilterPropertyName.FormattingEnabled = true;
            this.comboBoxGroupFilterPropertyName.Location = new System.Drawing.Point(372, 31);
            this.comboBoxGroupFilterPropertyName.Name = "comboBoxGroupFilterPropertyName";
            this.comboBoxGroupFilterPropertyName.Size = new System.Drawing.Size(142, 21);
            this.comboBoxGroupFilterPropertyName.TabIndex = 16;
            this.comboBoxGroupFilterPropertyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxGroups_KeyPress);
            this.comboBoxGroupFilterPropertyName.Leave += new System.EventHandler(this.comboBoxGroups_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(369, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Свойство фильтр:";
            // 
            // textBoxGroupDestination
            // 
            this.textBoxGroupDestination.Location = new System.Drawing.Point(276, 31);
            this.textBoxGroupDestination.Name = "textBoxGroupDestination";
            this.textBoxGroupDestination.Size = new System.Drawing.Size(90, 20);
            this.textBoxGroupDestination.TabIndex = 14;
            this.textBoxGroupDestination.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxGroups_KeyPress);
            this.textBoxGroupDestination.Leave += new System.EventHandler(this.comboBoxGroups_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(274, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Имя сore:";
            // 
            // textBoxGroupLocation
            // 
            this.textBoxGroupLocation.Location = new System.Drawing.Point(180, 31);
            this.textBoxGroupLocation.Name = "textBoxGroupLocation";
            this.textBoxGroupLocation.Size = new System.Drawing.Size(90, 20);
            this.textBoxGroupLocation.TabIndex = 12;
            this.textBoxGroupLocation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxGroups_KeyPress);
            this.textBoxGroupLocation.Leave += new System.EventHandler(this.comboBoxGroups_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "PLC адрес:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Имя:";
            // 
            // comboBoxGroups
            // 
            this.comboBoxGroups.FormattingEnabled = true;
            this.comboBoxGroups.Location = new System.Drawing.Point(6, 31);
            this.comboBoxGroups.Name = "comboBoxGroups";
            this.comboBoxGroups.Size = new System.Drawing.Size(169, 21);
            this.comboBoxGroups.TabIndex = 8;
            this.comboBoxGroups.SelectedIndexChanged += new System.EventHandler(this.comboBoxGroups_SelectedIndexChanged);
            this.comboBoxGroups.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxGroups_KeyPress);
            this.comboBoxGroups.Leave += new System.EventHandler(this.comboBoxGroups_Leave);
            // 
            // buttonRemoveGroup
            // 
            this.buttonRemoveGroup.Location = new System.Drawing.Point(620, 30);
            this.buttonRemoveGroup.Name = "buttonRemoveGroup";
            this.buttonRemoveGroup.Size = new System.Drawing.Size(29, 23);
            this.buttonRemoveGroup.TabIndex = 7;
            this.buttonRemoveGroup.Text = "-";
            this.buttonRemoveGroup.UseVisualStyleBackColor = true;
            this.buttonRemoveGroup.Click += new System.EventHandler(this.buttonRemoveGroup_Click);
            // 
            // buttonAddGroup
            // 
            this.buttonAddGroup.Location = new System.Drawing.Point(589, 30);
            this.buttonAddGroup.Name = "buttonAddGroup";
            this.buttonAddGroup.Size = new System.Drawing.Size(29, 23);
            this.buttonAddGroup.TabIndex = 6;
            this.buttonAddGroup.Text = "+";
            this.buttonAddGroup.UseVisualStyleBackColor = true;
            this.buttonAddGroup.Click += new System.EventHandler(this.buttonAddGroup_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 311);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(937, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(148, 17);
            this.toolStripStatusLabel1.Text = "Загрузите файл сборки ...";
            // 
            // ConfiguratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 333);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxGroups);
            this.Controls.Add(this.groupBoxEvents);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConfiguratorForm";
            this.Text = "Настройка OPC клиента";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxEvents.ResumeLayout(false);
            this.groupBoxEvents.PerformLayout();
            this.groupBoxGroups.ResumeLayout(false);
            this.groupBoxGroups.PerformLayout();
            this.groupBoxPoints.ResumeLayout(false);
            this.groupBoxPoints.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxEvents;
        private System.Windows.Forms.TextBox textBoxModule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEvents;
        private System.Windows.Forms.Button buttonGetModule;
        private System.Windows.Forms.GroupBox groupBoxGroups;
        private System.Windows.Forms.GroupBox groupBoxPoints;
        private System.Windows.Forms.ListBox listBoxPoints;
        private System.Windows.Forms.ComboBox comboBoxPointEncoding;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxPointLocation;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxPointName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxPointType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxGroupFilterPropertyValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxGroupFilterPropertyName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxGroupDestination;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxGroupLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxGroups;
        private System.Windows.Forms.Button buttonRemoveGroup;
        private System.Windows.Forms.Button buttonAddGroup;
        private System.Windows.Forms.Button buttonPointRemove;
        private System.Windows.Forms.Button buttonPointAdd;
        private System.Windows.Forms.ListBox listBoxProperties;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBoxIsWriteble;
        private System.Windows.Forms.TextBox textBoxBitNumber;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox checkBoxIsBoolean;
    }
}

