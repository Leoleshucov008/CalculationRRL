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
            this.coord = new System.Windows.Forms.ToolTip(this.components);
            this.zedGraph = new ZedGraph.ZedGraphControl();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRRLLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHMin = new System.Windows.Forms.TextBox();
            this.textBoxAntennaH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // coord
            // 
            this.coord.AutomaticDelay = 0;
            this.coord.ShowAlways = true;
            this.coord.UseAnimation = false;
            this.coord.UseFading = false;
            // 
            // zedGraph
            // 
            this.zedGraph.EditButtons = System.Windows.Forms.MouseButtons.Left;
            this.zedGraph.EditModifierKeys = System.Windows.Forms.Keys.None;
            this.zedGraph.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.zedGraph.IsEnableHEdit = true;
            this.zedGraph.IsEnableHZoom = false;
            this.zedGraph.IsEnableVEdit = true;
            this.zedGraph.IsEnableVZoom = false;
            this.zedGraph.IsShowContextMenu = false;
            this.zedGraph.Location = new System.Drawing.Point(244, 12);
            this.zedGraph.Name = "zedGraph";
            this.zedGraph.ScrollGrace = 0D;
            this.zedGraph.ScrollMaxX = 0D;
            this.zedGraph.ScrollMaxY = 0D;
            this.zedGraph.ScrollMaxY2 = 0D;
            this.zedGraph.ScrollMinX = 0D;
            this.zedGraph.ScrollMinY = 0D;
            this.zedGraph.ScrollMinY2 = 0D;
            this.zedGraph.Size = new System.Drawing.Size(563, 383);
            this.zedGraph.TabIndex = 0;
            this.zedGraph.ZoomButtons = System.Windows.Forms.MouseButtons.Middle;
            this.zedGraph.ContextMenuBuilder += new ZedGraph.ZedGraphControl.ContextMenuBuilderEventHandler(this.zedGraph_ContextMenuBuilder);
            this.zedGraph.PointEditEvent += new ZedGraph.ZedGraphControl.PointEditHandler(this.zedGraph_PointEditEvent);
            this.zedGraph.MouseDownEvent += new ZedGraph.ZedGraphControl.ZedMouseEventHandler(this.zedGraph_MouseDownEvent);
            this.zedGraph.MouseLeave += new System.EventHandler(this.zedGraph_MouseLeave);
            this.zedGraph.MouseMove += new System.Windows.Forms.MouseEventHandler(this.zedGraph_MouseMove);
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
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 382);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры расчета";
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
            this.earthCurveCheckBox.Location = new System.Drawing.Point(244, 401);
            this.earthCurveCheckBox.Name = "earthCurveCheckBox";
            this.earthCurveCheckBox.Size = new System.Drawing.Size(243, 17);
            this.earthCurveCheckBox.TabIndex = 9;
            this.earthCurveCheckBox.Text = "Отобразить кривизну земной поверхности";
            this.earthCurveCheckBox.UseVisualStyleBackColor = true;
            this.earthCurveCheckBox.CheckedChanged += new System.EventHandler(this.earthCurve_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(831, 371);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Расчет";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 459);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.earthCurveCheckBox);
            this.Controls.Add(this.zedGraph);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Расчет РРЛ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveNumber)).EndInit();
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
        private System.Windows.Forms.ToolTip coord;
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
    }
}

