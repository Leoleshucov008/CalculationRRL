using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalculationRRL
{

    class InterfaceManager
    {
        // Выделенная точка, на которую нажали правой
        int selectedPointIndex;
        ZedGraph.PointPair oldPointPos;
        public ListBox bariersListBox {get; private set; }
        public ComboBox surfaceTypeComboBox { get; private set; }
        public Interval interval { get; set; }
        // Редактируемый график
        public IGraphic graphic { get; set; }
        // Текущее состояние
        private State currentState;

        public void goNextState()
        {
            currentState.doAfter();
            currentState = currentState.getNext();
            currentState.doBefore();
        }

        public void goNextState(State state)
        {
            currentState.doAfter();
            currentState = state;
            currentState.doBefore();
        }
       
        // Отображать кривизне земной поверхности
        private bool _isShowEarthCurve;
        public bool isShowEarthCurve 
        {
            get { return _isShowEarthCurve; } 
            set
            {
                if (_isShowEarthCurve == value)
                    return;
                _isShowEarthCurve = value;

                //interval.earthCurveShow = _isShowEarthCurve;

                updateGraph();
            }
        }

        // Длина интервала(км)
        private double _R;
        public double R 
        {
            get { return _R; } 
            set 
            {
                if (_R == value)
                    return;
                _R = value;

                zedGraphPane.XAxis.Scale.Max = _R;
                zedGraphPane.XAxis.Scale.MajorStep = _R / 10;
                zedGraphPane.XAxis.Scale.MinorStep = _R / 20;
            }
        }

        // Высота антенн(м)
        private double _antennaH;
        public double antennaH 
        { 
            get {return _antennaH; }  
            set
            {
                if (_antennaH == value)
                    return;
                _antennaH = value;

            }
        }
        // Максимальная и минимальная высоты на профиле интервала (м)
        private double _hMin;
        public double hMin
        {
            get { return _hMin; } 
            set
            {
                if (_hMin == value)
                    return;
                _hMin = value;

                zedGraphPane.YAxis.Scale.Min = _hMin;
                updateZedGraphSteps();

            }
        }
        private double _hMax;
        public double hMax
        { 
            get { return _hMax; } 
            set
            {
                if (_hMax == value)
                    return;
                _hMax = value;

                zedGraphPane.YAxis.Scale.Max = _hMax;
                updateZedGraphSteps();

            }
        }

        private void updateZedGraphSteps()
        {
            zedGraphPane.YAxis.Scale.MajorStep = Math.Abs(_hMax - _hMin) / 5;
            zedGraphPane.YAxis.Scale.MinorStep = Math.Abs(_hMax - _hMin) / 10;
            zedGraphPane.XAxis.Scale.MajorStep = _R / 10;
            zedGraphPane.XAxis.Scale.MinorStep = _R / 20;
        }

        public string stationType;
        public string subRange;
        public int waveNumber;

        // Длина волны(м)
        private double _lambda;
        public double lambda
        {
            get { return _lambda; }
            set
            {
                if (_lambda == value)
                    return;
                _lambda = value;

            }
        }
        

        public ZedGraph.ZedGraphControl zedGraphControl { get; private set; }
        public ZedGraph.GraphPane zedGraphPane  { get; private set; }

        

        private void updateGraph()
        {
           // zedGraphPane.CurveList.Clear();
            if (_isShowEarthCurve)
            {
                zedGraphPane.CurveList.Add(interval.earthCurve);
            }

            zedGraphControl.Invalidate(); 

        }   

        private void disableEditOnGraph()
        {
            zedGraphControl.IsEnableHEdit = false;
            zedGraphControl.IsEnableVEdit = false;
        }

        private void enableEditOnGraph()
        {
            zedGraphControl.IsEnableHEdit = true;
            zedGraphControl.IsEnableVEdit = true;
        }

        public void zedGraphLeftDown(PointF p)
        {
            ZedGraph.CurveItem curve;
            int index;
            bool isOnCurve = zedGraphPane.FindNearestPoint(p, zedGraphPane.CurveList, out curve, out index);
            
            disableEditOnGraph();
            if (!isOnCurve || graphic == null || graphic.getCurve() != curve)
            {
                double x, y;
                zedGraphPane.ReverseTransform(p, out x, out y);
                try
                {
                    graphic.addPoint(new ZedGraph.PointPair(x, y));
                    enableEditOnGraph();
                    oldPointPos = new ZedGraph.PointPair(x, y);
                }
                catch (InvalidPointPositon e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (BarierIsFull e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (BarierIntersection e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else if (graphic != null && graphic.getCurve() == curve)
            {
                enableEditOnGraph();
                oldPointPos = curve.Points[index];
                // Если первая или последняя точка интервала
                if (currentState.GetType().Name.Equals("InputProfilePoints")
                    && (index == 0 || index == curve.Points.Count - 1))
                {                   
                    // То разрешить редактирование только по Y
                    zedGraphControl.IsEnableHEdit = false;
                }
            }
            updateGraph();
           
        }

        public void zedGraphRightDown(PointF p)
        {
            ZedGraph.CurveItem curve;
            int index;
            bool isOnCurve = zedGraphPane.FindNearestPoint(p, zedGraphPane.CurveList, out curve, out index);

            if (isOnCurve && graphic!= null && curve == graphic.getCurve()
                && !(currentState.GetType().Name.Equals("InputProfilePoints") && (index == 0 || index == curve.Points.Count - 1)))
            {
                zedGraphControl.IsShowContextMenu = true;
                selectedPointIndex = index;
            }
            else
            {
                zedGraphControl.IsShowContextMenu = false;
            }   
            
        }

        public void calculation()
        {
            // Добаление к координатом профиля кривизны дуги земной поверхности

           
             
            /*RRL.RRLCalculator calc = new RRL.RRLCalculator(profileWithEarthCurve, _lambda, _antennaH);
            double[] x;
            double[] y;
            listToArrays(calc.getH0(), out x, out y);
            updateGraph();
            zedGraphPane.AddCurve("Линия критических просветов", x, y, Color.Green, ZedGraph.SymbolType.None);
            zedGraphControl.Invalidate();
            switch (calc.getIntervalType())
            {
                case RRL.IntervalType.opened : Console.WriteLine("Opened"); break;
                case RRL.IntervalType.halfOpened : Console.WriteLine("HalfOpened"); break;
                case RRL.IntervalType.closed : Console.WriteLine("Closed"); break;
            }*/
        }

        public void editPoint(ZedGraph.CurveItem curve, int index)
        {
            if (graphic == null || graphic.getCurve() != curve)
            {
                curve[index].X = oldPointPos.X;
                curve[index].Y = oldPointPos.Y;
            }
            else
            {
                try
                {
                    graphic.editPoint(index, curve.Points[index], oldPointPos);
                }
                catch (InvalidPointPositon e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            updateGraph();
        }

        public void removePoint()
        {
            try
            {
                graphic.removePoint(selectedPointIndex);
            }
            catch (InvalidIndex e)
            {
                MessageBox.Show(e.Message);
            }
            selectedPointIndex = -1;
            updateGraph();
        }
        public void showGraph()
        {
            zedGraphControl.Visible = true;
        }

        public void parametersChange()
        {
            if (!currentState.GetType().Name.Equals("InputIntervalParameters"))
            {
                MessageBox.Show("Для изменения параметров интервала нажмите \"Применить\"");
            }
            
        }

        public void resetGraph()
        {
            // Устанавливает график в начальное состояние

            selectedPointIndex = -1;
            zedGraphPane.XAxis.Scale.Max = interval.R;
            zedGraphPane.YAxis.Scale.Min = interval.minH;
            zedGraphPane.YAxis.Scale.Max = interval.maxH;

            updateZedGraphSteps();          

            updateGraph();
        }

        public void selectBarier(int i)
        {
            if (interval.currentBarier != interval.bariers[i] && i != -1)
            {
                try
                {
                    goNextState(new ChangeBarier(null, i, this));
                    interval.setBarier(i);
                    graphic = interval.currentBarier;
                    zedGraphControl.Invalidate();
                }
                catch (BarierIsUncompleted exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }            
        }

        public void removeCurrentBarier()
        {
            interval.removeCurrentBarier();
            graphic = interval.currentBarier;
            updateBarierListBox();
        }

        public void updateBarierListBox()
        {
            bariersListBox.Items.Clear();
            for (int i = 0; i < interval.bariers.Count; ++i)
            {
                bariersListBox.Items.Add("Препятствие " + Convert.ToString(i + 1));
            }
            if (bariersListBox.SelectedIndex < 0 || bariersListBox.SelectedIndex >= interval.bariers.Count)
            {
                bariersListBox.SelectedIndex = interval.bariers.Count - 1;
            }
            surfaceTypeComboBox.SelectedItem = interval.currentBarier.barierType;

        }

        public InterfaceManager(ZedGraph.ZedGraphControl zgc, ListBox listBox, ComboBox comboBox)
        {
            zedGraphControl = zgc;
            zedGraphPane = zgc.GraphPane;
            bariersListBox = listBox;
            surfaceTypeComboBox = comboBox;

            _R = 20;
            _hMax = 200;
            _antennaH = 20;
            _lambda = 2.5;            
           
            zedGraphControl.IsShowContextMenu = false;
            ZedGraph.Line.Default.IsSmooth = true;
            ZedGraph.Line.Default.SmoothTension = 0.3F;
            ZedGraph.GraphPane.Default.NearestTol = 10;
            zedGraphControl.EditButtons = MouseButtons.Left;
            zedGraphPane.Title.Text = "Профиль интервала РРЛ";
            zedGraphPane.XAxis.Title.Text = "Расстояние";
            zedGraphPane.YAxis.Title.Text = "Высота";

            FinalState finalState = new FinalState(this);
            InputBarier inputBarierState = new InputBarier(null, this);
            InputProfilePoints inputProfilePoints =  new InputProfilePoints(inputBarierState, this);            
            currentState = new InputIntervalParameters(inputProfilePoints, this);

        }
    }

    
}
