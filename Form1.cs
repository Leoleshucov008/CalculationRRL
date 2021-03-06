﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculationRRL
{
    public partial class Form1 : Form
    {
        private CalculationRRL.InterfaceManager interfaceManager;
        public Form1()
        {
            InitializeComponent();
            interfaceManager = new InterfaceManager(zedGraph, bariersListBox, surfaceTypeComboBox);
            textBoxRRLLength.Text = interfaceManager.R.ToString();
            textBoxHMin.Text = interfaceManager.hMin.ToString();
            textBoxHMax.Text = interfaceManager.hMax.ToString();
            textBoxAntennaH.Text = interfaceManager.antennaH.ToString();
            StationType.SelectedIndex = 0;
            SubRange.SelectedIndex = 0;
        }

        private void textBoxRRLLength_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(textBoxRRLLength.Text.Replace('.', ','));
                interfaceManager.parametersChange();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введена длина интервала РРЛ.\nПоле должно содержать вещественное число.", "Ошибка");
            }
        }

        private void textBoxHMin_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(textBoxHMin.Text.Replace('.', ','));
                interfaceManager.parametersChange();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введена минимальная высота.\nПоле должно содержать вещественное число.", "Ошибка");
            }
        }
        private void textBoxHMax_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(textBoxHMax.Text.Replace('.', ','));
                interfaceManager.parametersChange();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введена максимальная высота.\nПоле должно содержать вещественное число.", "Ошибка");
            }

        }

        private void textBoxAntennaH_Leave(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDouble(textBoxHMax.Text.Replace('.', ','));
                interfaceManager.parametersChange();
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введена высота антенн.\nПоле должно содержать вещественное число.", "Ошибка");
            }
        }       
     
        private bool zedGraph_MouseDownEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left : interfaceManager.zedGraphLeftDown(e.Location); break;
                case MouseButtons.Right : interfaceManager.zedGraphRightDown(e.Location); break;
            }
            return default(bool);
        }

        private void zedGraph_ContextMenuBuilder(ZedGraph.ZedGraphControl sender, ContextMenuStrip menuStrip, Point mousePt, ZedGraph.ZedGraphControl.ContextMenuObjectState objState)
        {        
            menuStrip.Items.Clear();
            menuStrip.Items.Add("Удалить точку");
            menuStrip.Items[0].Click += new EventHandler(delItem_MouseClick);
        }
        private void delItem_MouseClick(object o, EventArgs e)
        {
            interfaceManager.removePoint();
        }
        private void menuStrip_VisibleChanged(object sender, EventArgs e)
        {
            zedGraph.ContextMenuStrip.Close();
        }

        private void earthCurve_CheckedChanged(object sender, EventArgs e)
        {
            interfaceManager.isShowEarthCurve = earthCurveCheckBox.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            interfaceManager.calculation();
        }

        private void StationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SubRange.Items.Clear();
            if (StationType.SelectedIndex == 0)
            {
                SubRange.Items.Add("Б");
                SubRange.Items.Add("В");
            }
            if (StationType.SelectedIndex == 1)
            {
                SubRange.Items.Add("2");
                SubRange.Items.Add("3");
                SubRange.Items.Add("4");
                SubRange.Items.Add("5");

            }
            SubRange.SelectedIndex = 0;

        }
        private void setMinMaxWaveNum(int min, int max)
        {
            waveNumber.Maximum = max;
            waveNumber.Minimum = min;
        }
        private void SubRange_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (SubRange.Items[SubRange.SelectedIndex].ToString())
            {
                case "Б": setMinMaxWaveNum(0, 600); break;
                case "В": setMinMaxWaveNum(0, 600); break;
                case "2": setMinMaxWaveNum(0, 800); break;
                case "3": setMinMaxWaveNum(0, 534); break;
                case "4": setMinMaxWaveNum(0, 800); break;
                case "5": setMinMaxWaveNum(0, 550); break;

            }
            waveNumInfo.Text = "0..." + waveNumber.Maximum.ToString();
        }

        private void waveNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (waveNumber.Value < waveNumber.Minimum)
                waveNumber.Value = waveNumber.Minimum;
            if (waveNumber.Value > waveNumber.Maximum)
                waveNumber.Value = waveNumber.Maximum;
        }

        private void waveNumber_ValueChanged(object sender, EventArgs e)
        {
            interfaceManager.lambda = getLambda(Convert.ToInt32(waveNumber.Value), SubRange.SelectedItem.ToString(), StationType.SelectedItem.ToString());
        }

        private double getLambda(int number, string podd, string stationtype)
        {
            double f;
            double lambda;
            double CC = 3 * Math.Pow(10.0, 8.0);
            if (stationtype == "Р-409")
            {
                switch (podd)
                {
                    case "А":
                        {
                            f = 60 + 0.1 * number;
                            break;
                        }
                    case "Б":
                        {
                            f = 120 + 0.1 * number;
                            break;
                        }
                    default:
                        {
                            f = 240 + 0.1 * number;
                            break;
                        }
                }
            }

            else
            {
                switch (podd)
                {
                    case "2":
                        {
                            f = 160 + 0.1 * number;
                            break;
                        }
                    case "3":
                        {
                            f = 240 + 0.1 * number;
                            break;
                        }
                    case "4":
                        {
                            f = 320 + 0.1 * number;
                            break;
                        }
                    default:
                        {
                            f = 480 + 0.1 * number;
                            break;
                        }
                }
                
            }
            lambda = CC / f / 1000000.0;
            return lambda;

        }

        private void acceptIntervalParametersBtn_Click(object sender, EventArgs e)
        {
            interfaceManager.antennaH = Convert.ToDouble(textBoxAntennaH.Text.Replace('.', ','));
            interfaceManager.R = Convert.ToDouble(textBoxRRLLength.Text.Replace('.', ','));
            interfaceManager.hMin = Convert.ToDouble(textBoxHMin.Text.Replace('.', ','));
            interfaceManager.hMax = Convert.ToDouble(textBoxHMax.Text.Replace('.', ','));
            interfaceManager.goNextState(new InputProfilePoints(null, interfaceManager));
            interfaceManager.stationType = StationType.SelectedItem.ToString();
            interfaceManager.subRange = SubRange.SelectedItem.ToString();
            interfaceManager.waveNumber = Convert.ToInt32(waveNumber.Value.ToString());
            earthCurveCheckBox.Visible = zedGraph.Visible;
            acceptProfileInputBtn.Visible = zedGraph.Visible;
            
        }

        private void acceptProfileInputBtn_Click(object sender, EventArgs e)
        {
            interfaceManager.goNextState(new InputBarier(null, interfaceManager));
            acceptProfileInputBtn.Visible = false;
            acceptBariersBtn.Visible = true; 
            addBarierBtn.Visible = true;
            bariersComboBox.Visible = true;
        }

        private void acceptBariersBtn_Click(object sender, EventArgs e)
        {
            interfaceManager.goNextState(new FinalState(interfaceManager));
        }

        private void addBarierBtn_Click(object sender, EventArgs e)
        {
            try
            {
                interfaceManager.goNextState(new InputBarier(null, interfaceManager));
            }
            catch (BarierIsUncompleted exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private bool zedGraph_MouseMoveEvent(ZedGraph.ZedGraphControl sender, MouseEventArgs e)
        {
            double x, y;
            sender.GraphPane.ReverseTransform(new PointF(e.X, e.Y), out x, out y);
            string s = "(" + x.ToString("f2") + "; " + y.ToString("f2") + ")";
            coordsLabel.Text = s;
            return default(bool);
        }

        private string zedGraph_PointEditEvent(ZedGraph.ZedGraphControl sender, ZedGraph.GraphPane pane, ZedGraph.CurveItem curve, int iPt)
        {
            interfaceManager.editPoint(curve, iPt);
            return default(string);

        }

        private void bariersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            interfaceManager.selectBarier(bariersListBox.SelectedIndex);
            surfaceTypeComboBox.SelectedItem = interfaceManager.interval.currentBarier.barierType;
        }

        private void deleteBarierBtn_Click(object sender, EventArgs e)
        {
            interfaceManager.removeCurrentBarier();
            zedGraph.Invalidate();
        }

        private void surfaceTypeComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (surfaceTypeComboBox.SelectedIndex != -1)
            {
                interfaceManager.interval.currentBarier.barierType = surfaceTypeComboBox.Items[surfaceTypeComboBox.SelectedIndex].ToString();
            }
        }
    }
   
}
