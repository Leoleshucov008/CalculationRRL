namespace CalculationRRL
{
    partial class Form1
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
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRRLLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHMin = new System.Windows.Forms.TextBox();
            this.textBoxAntennaH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.acceptIntervalParametersBtn = new System.Windows.Forms.Button();
            this.waveNumInfo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.waveNumber = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.SubRange = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.StationType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.earthCurveCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.acceptProfileInputBtn = new System.Windows.Forms.Button();
            this.addBarierBtn = new System.Windows.Forms.Button();
            this.acceptBariersBtn = new System.Windows.Forms.Button();
            this.coordsLabel = new System.Windows.Forms.Label();
            this.bariersListBox = new System.Windows.Forms.ListBox();
            this.bariersListLabel = new System.Windows.Forms.Label();
            this.deleteBarierBtn = new System.Windows.Forms.Button();
            this.surfaceTypeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.bariersComboBox = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveNumber)).BeginInit();
            this.bariersComboBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // zedGraph
            // 
            this.zedGraph.Cursor = System.Windows.Forms.Cursors.Default;
            this.zedGraph.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraph.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.zedGraph.IsEnableHEdit = true;
            this.zedGraph.IsEnableHZoom = false;
            this.zedGraph.IsEnableVEdit = true;
            this.zedGraph.IsEnableVZoom = false;
            this.zedGraph.IsShowContextMenu = false;
            this.zedGraph.Location = new System.Drawing.Point(204, 12);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(655, 452);
            this.zedGraph.TabIndex = 0;
            this.zedGraph.Visible = false;
            this.zedGraph.ZoomButtons = System.Windows.Forms.MouseButtons.Middle;
            this.zedGraph.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zedGraph_ContextMenuBuilder);
            this.zedGraph.PointEditEvent += new ZedGraph.ZedGraphControl.PointEditHandler(this.zedGraph_PointEditEvent);
            this.zedGraph.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraph_MouseDownEvent);
            this.zedGraph.MouseMoveEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraph_MouseMoveEvent);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Длина интревала (км)";
            // 
            // textBoxRRLLength
            // 
            this.textBoxRRLLength.Location = new System.Drawing.Point(9, 50);
            this.textBoxRRLLength.Name = "textBoxRRLLength";
            this.textBoxRRLLength.Size = new System.Drawing.Size(126, 21);
            this.textBoxRRLLength.TabIndex = 1;
            this.textBoxRRLLength.Text = "20";
            this.textBoxRRLLength.Leave += new System.EventHandler(this.textBoxRRLLength_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Перепад высот на интервале (м)";
            // 
            // textBoxHMin
            // 
            this.textBoxHMin.Location = new System.Drawing.Point(35, 103);
            this.textBoxHMin.Name = "textBoxHMin";
            this.textBoxHMin.Size = new System.Drawing.Size(100, 21);
            this.textBoxHMin.TabIndex = 3;
            this.textBoxHMin.Text = "0";
            this.textBoxHMin.Leave += new System.EventHandler(this.textBoxHMin_Leave);
            // 
            // textBoxAntennaH
            // 
            this.textBoxAntennaH.Location = new System.Drawing.Point(9, 189);
            this.textBoxAntennaH.Name = "textBoxAntennaH";
            this.textBoxAntennaH.Size = new System.Drawing.Size(126, 21);
            this.textBoxAntennaH.TabIndex = 5;
            this.textBoxAntennaH.Text = "20";
            this.textBoxAntennaH.Leave += new System.EventHandler(this.textBoxAntennaH_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Высота антенн (м)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.acceptIntervalParametersBtn);
            this.groupBox1.Controls.Add(this.waveNumInfo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.waveNumber);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.SubRange);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.StationType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxHMax);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxRRLLength);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxAntennaH);
            this.groupBox1.Controls.Add(this.textBoxHMin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 405);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры расчета";
            // 
            // acceptIntervalParametersBtn
            // 
            this.acceptIntervalParametersBtn.Location = new System.Drawing.Point(6, 359);
            this.acceptIntervalParametersBtn.Name = "acceptIntervalParametersBtn";
            this.acceptIntervalParametersBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptIntervalParametersBtn.TabIndex = 18;
            this.acceptIntervalParametersBtn.Text = "Применить";
            this.acceptIntervalParametersBtn.UseVisualStyleBackColor = true;
            this.acceptIntervalParametersBtn.Click += new System.EventHandler(this.acceptIntervalParametersBtn_Click);
            // 
            // waveNumInfo
            // 
            this.waveNumInfo.AutoSize = true;
            this.waveNumInfo.Location = new System.Drawing.Point(132, 330);
            this.waveNumInfo.Name = "waveNumInfo";
            this.waveNumInfo.Size = new System.Drawing.Size(0, 13);
            this.waveNumInfo.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 307);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Номер волны";
            // 
            // waveNumber
            // 
            this.waveNumber.Location = new System.Drawing.Point(5, 323);
            this.waveNumber.Name = "waveNumber";
            this.waveNumber.Size = new System.Drawing.Size(120, 21);
            this.waveNumber.TabIndex = 15;
            this.waveNumber.ValueChanged += new System.EventHandler(this.waveNumber_ValueChanged);
            this.waveNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.waveNumber_KeyUp);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 263);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Поддиапазон";
            // 
            // SubRange
            // 
            this.SubRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SubRange.FormattingEnabled = true;
            this.SubRange.Items.AddRange(new object[] {
            "Б",
            "В"});
            this.SubRange.Location = new System.Drawing.Point(6, 279);
            this.SubRange.Name = "SubRange";
            this.SubRange.Size = new System.Drawing.Size(69, 21);
            this.SubRange.TabIndex = 13;
            this.SubRange.SelectedValueChanged += new System.EventHandler(this.SubRange_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Тип станции";
            // 
            // StationType
            // 
            this.StationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StationType.FormattingEnabled = true;
            this.StationType.Items.AddRange(new object[] {
            "Р-409",
            "Р-419"});
            this.StationType.Location = new System.Drawing.Point(6, 234);
            this.StationType.Name = "StationType";
            this.StationType.Size = new System.Drawing.Size(69, 21);
            this.StationType.TabIndex = 11;
            this.StationType.SelectedIndexChanged += new System.EventHandler(this.StationType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Max";
            // 
            // textBoxHMax
            // 
            this.textBoxHMax.Location = new System.Drawing.Point(35, 135);
            this.textBoxHMax.Name = "textBoxHMax";
            this.textBoxHMax.Size = new System.Drawing.Size(100, 21);
            this.textBoxHMax.TabIndex = 9;
            this.textBoxHMax.Text = "200";
            this.textBoxHMax.Leave += new System.EventHandler(this.textBoxHMax_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Min\r\n";
            // 
            // earthCurveCheckBox
            // 
            this.earthCurveCheckBox.AutoSize = true;
            this.earthCurveCheckBox.Location = new System.Drawing.Point(204, 470);
            this.earthCurveCheckBox.Name = "earthCurveCheckBox";
            this.earthCurveCheckBox.Size = new System.Drawing.Size(243, 17);
            this.earthCurveCheckBox.TabIndex = 9;
            this.earthCurveCheckBox.Text = "Отобразить кривизну земной поверхности";
            this.earthCurveCheckBox.UseVisualStyleBackColor = true;
            this.earthCurveCheckBox.Visible = false;
            this.earthCurveCheckBox.CheckedChanged += new System.EventHandler(this.earthCurve_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(784, 470);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Расчет";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // acceptProfileInputBtn
            // 
            this.acceptProfileInputBtn.Location = new System.Drawing.Point(739, 470);
            this.acceptProfileInputBtn.Name = "acceptProfileInputBtn";
            this.acceptProfileInputBtn.Size = new System.Drawing.Size(119, 23);
            this.acceptProfileInputBtn.TabIndex = 11;
            this.acceptProfileInputBtn.Text = "Применить";
            this.acceptProfileInputBtn.UseVisualStyleBackColor = true;
            this.acceptProfileInputBtn.Visible = false;
            this.acceptProfileInputBtn.Click += new System.EventHandler(this.acceptProfileInputBtn_Click);
            // 
            // addBarierBtn
            // 
            this.addBarierBtn.Location = new System.Drawing.Point(546, 470);
            this.addBarierBtn.Name = "addBarierBtn";
            this.addBarierBtn.Size = new System.Drawing.Size(135, 23);
            this.addBarierBtn.TabIndex = 12;
            this.addBarierBtn.Text = "Добавить препятствие";
            this.addBarierBtn.UseVisualStyleBackColor = true;
            this.addBarierBtn.Visible = false;
            this.addBarierBtn.Click += new System.EventHandler(this.addBarierBtn_Click);
            // 
            // acceptBariersBtn
            // 
            this.acceptBariersBtn.Location = new System.Drawing.Point(687, 470);
            this.acceptBariersBtn.Name = "acceptBariersBtn";
            this.acceptBariersBtn.Size = new System.Drawing.Size(173, 23);
            this.acceptBariersBtn.TabIndex = 13;
            this.acceptBariersBtn.Text = "Закончить ввод препятствий";
            this.acceptBariersBtn.UseVisualStyleBackColor = true;
            this.acceptBariersBtn.Visible = false;
            this.acceptBariersBtn.Click += new System.EventHandler(this.acceptBariersBtn_Click);
            // 
            // coordsLabel
            // 
            this.coordsLabel.AutoSize = true;
            this.coordsLabel.Location = new System.Drawing.Point(284, 436);
            this.coordsLabel.Name = "coordsLabel";
            this.coordsLabel.Size = new System.Drawing.Size(0, 13);
            this.coordsLabel.TabIndex = 14;
            // 
            // bariersListBox
            // 
            this.bariersListBox.FormattingEnabled = true;
            this.bariersListBox.Location = new System.Drawing.Point(6, 34);
            this.bariersListBox.Name = "bariersListBox";
            this.bariersListBox.Size = new System.Drawing.Size(127, 95);
            this.bariersListBox.TabIndex = 15;
            this.bariersListBox.SelectedIndexChanged += new System.EventHandler(this.bariersListBox_SelectedIndexChanged);
            // 
            // bariersListLabel
            // 
            this.bariersListLabel.AutoSize = true;
            this.bariersListLabel.Location = new System.Drawing.Point(3, 16);
            this.bariersListLabel.Name = "bariersListLabel";
            this.bariersListLabel.Size = new System.Drawing.Size(115, 13);
            this.bariersListLabel.TabIndex = 16;
            this.bariersListLabel.Text = "Список препятствий:";
            // 
            // deleteBarierBtn
            // 
            this.deleteBarierBtn.Font = new System.Drawing.Font("Tahoma", 8.2F);
            this.deleteBarierBtn.Location = new System.Drawing.Point(6, 135);
            this.deleteBarierBtn.Name = "deleteBarierBtn";
            this.deleteBarierBtn.Size = new System.Drawing.Size(127, 23);
            this.deleteBarierBtn.TabIndex = 17;
            this.deleteBarierBtn.Text = "Удалить препятствие";
            this.deleteBarierBtn.UseVisualStyleBackColor = true;
            this.deleteBarierBtn.Click += new System.EventHandler(this.deleteBarierBtn_Click);
            // 
            // surfaceTypeComboBox
            // 
            this.surfaceTypeComboBox.FormattingEnabled = true;
            this.surfaceTypeComboBox.Items.AddRange(new object[] {
            "Равнины, луга, соланчаки",
            "Ровная лесистая поверхность",
            "Среднепересеченная местность"});
            this.surfaceTypeComboBox.Location = new System.Drawing.Point(139, 34);
            this.surfaceTypeComboBox.Name = "surfaceTypeComboBox";
            this.surfaceTypeComboBox.Size = new System.Drawing.Size(172, 21);
            this.surfaceTypeComboBox.TabIndex = 18;
            this.surfaceTypeComboBox.SelectedValueChanged += new System.EventHandler(this.surfaceTypeComboBox_SelectedValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Тип поверхности";
            // 
            // bariersComboBox
            // 
            this.bariersComboBox.Controls.Add(this.bariersListBox);
            this.bariersComboBox.Controls.Add(this.label4);
            this.bariersComboBox.Controls.Add(this.bariersListLabel);
            this.bariersComboBox.Controls.Add(this.surfaceTypeComboBox);
            this.bariersComboBox.Controls.Add(this.deleteBarierBtn);
            this.bariersComboBox.Location = new System.Drawing.Point(874, 12);
            this.bariersComboBox.Name = "bariersComboBox";
            this.bariersComboBox.Size = new System.Drawing.Size(317, 165);
            this.bariersComboBox.TabIndex = 20;
            this.bariersComboBox.TabStop = false;
            this.bariersComboBox.Text = "Препятствия";
            this.bariersComboBox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 498);
            this.Controls.Add(this.bariersComboBox);
            this.Controls.Add(this.coordsLabel);
            this.Controls.Add(this.acceptBariersBtn);
            this.Controls.Add(this.addBarierBtn);
            this.Controls.Add(this.acceptProfileInputBtn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.earthCurveCheckBox);
            this.Controls.Add(this.zedGraph);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Расчет РРЛ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveNumber)).EndInit();
            this.bariersComboBox.ResumeLayout(false);
            this.bariersComboBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRRLLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxHMin;
        private System.Windows.Forms.TextBox textBoxAntennaH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHMax;
        private System.Windows.Forms.Label label5;
        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.CheckBox earthCurveCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown waveNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox SubRange;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox StationType;
        private System.Windows.Forms.Label waveNumInfo;
        private System.Windows.Forms.Button acceptIntervalParametersBtn;
        private System.Windows.Forms.Button acceptProfileInputBtn;
        private System.Windows.Forms.Button addBarierBtn;
        private System.Windows.Forms.Button acceptBariersBtn;
        private System.Windows.Forms.Label coordsLabel;
        private System.Windows.Forms.ListBox bariersListBox;
        private System.Windows.Forms.Label bariersListLabel;
        private System.Windows.Forms.Button deleteBarierBtn;
        private System.Windows.Forms.ComboBox surfaceTypeComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox bariersComboBox;
    }
}

