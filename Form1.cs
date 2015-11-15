using System;
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
        private PointF oldPosition;
        public Form1()
        {
            InitializeComponent();
            interfaceManager = new InterfaceManager(zedGraph);
            textBoxRRLLength.Text = interfaceManager.R.ToString();
            textBoxHMin.Text = interfaceManager.hMin.ToString();
            textBoxHMax.Text = interfaceManager.hMax.ToString();
            textBoxAntennaH.Text = interfaceManager.antennaH.ToString();
            textBoxLamda.Text = interfaceManager.lamda.ToString();
        }

        private void textBoxRRLLength_Leave(object sender, EventArgs e)
        {
            try
            {
                interfaceManager.R = Convert.ToDouble(textBoxRRLLength.Text.Replace('.', ','));
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
                interfaceManager.hMin = Convert.ToDouble(textBoxHMin.Text.Replace('.', ','));
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
                interfaceManager.hMax = Convert.ToDouble(textBoxHMax.Text.Replace('.', ','));
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
                interfaceManager.antennaH = Convert.ToDouble(textBoxAntennaH.Text.Replace('.', ','));
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введена высота антенн.\nПоле должно содержать вещественное число.", "Ошибка");
            }
        }

        private void textBoxLamda_Leave(object sender, EventArgs e)
        {
            try
            {
                interfaceManager.lamda = Convert.ToDouble(textBoxLamda.Text.Replace('.', ','));
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверно введена длина волны.\nПоле должно содержать вещественное число.", "Ошибка");
            }
        }

        private void zedGraph_MouseMove(object sender, MouseEventArgs e)
        {
            // Для того чтобы отлавливать реальные перемещения
            // Событие генерируется даже без перемещения курсора
            if (e.Location == oldPosition)
                return;

            interfaceManager.showHint(e.Location, coord);
           
            oldPosition = e.Location;
        }

        private void zedGraph_MouseLeave(object sender, EventArgs e)
        {
            coord.Hide(zedGraph);
        }

        private string zedGraph_PointEditEvent(ZedGraph.ZedGraphControl sender, ZedGraph.GraphPane pane, ZedGraph.CurveItem curve, int iPt)
        {
            RRL.PointD p = new RRL.PointD(curve[iPt].X, curve[iPt].Y);
            interfaceManager.editPointOnProfile(iPt, p);
            return default(string);
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
            interfaceManager.removeSelectedPoint();
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
    }
   
}
