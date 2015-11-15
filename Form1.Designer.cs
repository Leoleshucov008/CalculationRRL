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
            this.textBoxLamda = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.earthCurveCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
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
            // textBoxLamda
            // 
            this.textBoxLamda.Location = new System.Drawing.Point(9, 246);
            this.textBoxLamda.Name = "textBoxLamda";
            this.textBoxLamda.Size = new System.Drawing.Size(126, 21);
            this.textBoxLamda.TabIndex = 7;
            this.textBoxLamda.Text = "2.5";
            this.textBoxLamda.Leave += new System.EventHandler(this.textBoxLamda_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Длина волны (м)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxHMax);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxLamda);
            this.groupBox1.Controls.Add(this.textBoxRRLLength);
            this.groupBox1.Controls.Add(this.label4);
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
        private System.Windows.Forms.TextBox textBoxLamda;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip coord;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHMax;
        private System.Windows.Forms.Label label5;
        private ZedGraph.ZedGraphControl zedGraph;
        private System.Windows.Forms.CheckBox earthCurveCheckBox;
        private System.Windows.Forms.Button button1;
    }
}

