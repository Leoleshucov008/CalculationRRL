using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalculationRRL
{
    class InterfaceManager
    {
        // Выделенная точка, на которую нажали правой
        int selectedPoint;
        
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
                updateProfile();
                updateEarthCurve();
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

                updateProfile();
                updateEarthCurve();
                updateGraph();
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
                updateGraph();

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

                updateGraph();

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
                updateGraph();

            }
        }

        private void updateZedGraphSteps()
        {
            zedGraphPane.YAxis.Scale.MajorStep = Math.Abs(_hMax - _hMin) / 5;
            zedGraphPane.YAxis.Scale.MinorStep = Math.Abs(_hMax - _hMin) / 10;
            zedGraphPane.XAxis.Scale.MajorStep = _R / 10;
            zedGraphPane.XAxis.Scale.MinorStep = _R / 20;
        }
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
                updateGraph();

            }
        }
        // Узлы профиля интервала
        private List<RRL.PointD> profile;

        private ZedGraph.ZedGraphControl zedGraphControl;
        private ZedGraph.GraphPane zedGraphPane;


        private double[] earthCurveX;
        private double[] earthCurveY;

        private double[] profileX;
        private double[] profileY;

        private double getEarthCurveH(double x)
        {
            // Радиус земли километров
            const double EarthR = 6371;
            return 1000.0 * (Math.Sqrt(EarthR * EarthR - Math.Pow(x - R / 2, 2.0)) - Math.Sqrt(EarthR * EarthR - R * R / 4));
        }

        private void updateEarthCurve()
        {
            // Строим линию кривизны земной поверхности
            double xPos = 0;
            // Количество узлов построения
            int earthCurveNodeCount = 100;
            // Шаг при построении линии кривизны земной поверхности            
            double xStep = _R / Convert.ToDouble(earthCurveNodeCount);

            // Перевыделение памяти если требуется
            if (earthCurveX.Length != earthCurveNodeCount)
            {
                earthCurveX = new double[earthCurveNodeCount];
                earthCurveY = new double[earthCurveNodeCount];
            }
            
            for (int i = 0; i < earthCurveNodeCount; ++i)
            {
                earthCurveX[i] = xPos;
                earthCurveY[i] = getEarthCurveH(xPos);
                xPos += xStep;
            }
        }

        private void updateProfile()
        {
            int size = profile.Count;

            // Перевыделяем память если требуется
            if (profileX.Length != size)
            {
                profileX = new double[size];
                profileY = new double[size];
            }

            for (int i = 0; i < size; ++i)
            {
                profileX[i] = profile[i].x;
                profileY[i] = profile[i].y;
            }


            // Добавляем высоту земной поверхности
            if (_isShowEarthCurve)
            {
                for (int i = 0; i < size; ++i)
                {
                    profileY[i] += getEarthCurveH(profileX[i]);
                }
            }
 
        }

        // Линия прямой видимости
        private void drawLineOfSight()
        {
            double[] x = new double[2] {0, R};
            double[] y = new double[2] {profileY[0] + _antennaH, profileY[profileY.Length-1] + _antennaH};
            zedGraphPane.AddCurve("Линия прямой видимости", x, y, Color.Blue, ZedGraph.SymbolType.UserDefined);
        }

        private void updateGraph()
        {
            zedGraphPane.CurveList.Clear();

            zedGraphPane.AddCurve("", profileX, profileY, Color.Black, ZedGraph.SymbolType.Circle);
            drawLineOfSight();
            if (_isShowEarthCurve)
            {
                zedGraphPane.AddCurve("", earthCurveX, earthCurveY, Color.Red, ZedGraph.SymbolType.None);
            }

            zedGraphControl.Invalidate(); 

        }

        public void addPointOnProfile(System.Drawing.PointF p)
        {
            // Добавление новой точки на графике
            double x, y;
            zedGraphPane.ReverseTransform(p, out x, out y);
            if (!isPointInGraph(x, y))
            {
                return;
            }
            int i = profile.FindIndex(a => a.x > x);
            if (i < 0)
            {
                i = profile.Count;
            }
            profile.Insert(i, new RRL.PointD(x, y));
            updateProfile();
            updateGraph();
        }

        public void removeSelectedPoint()
        {
            // Если точка не выделена
            if (selectedPoint < 0)
                return;
            // Удаление узла профиля интервала
            profile.RemoveAt(selectedPoint);
            updateProfile();
            updateGraph();

            selectedPoint = -1;
        }

        public void editPointOnProfile(int index, RRL.PointD p)
        {
            if (isPointInGraph(p.x, p.y))
            {
                profile[index] = p;
            }
            updateProfile();
            updateGraph();
        }


        private bool isPointInGraph(double x, double y)
        {
            return x >= 0 && x <= R && y >= _hMin && y <= _hMax;
        }

        public void showHint(PointF p, ToolTip toolTip)
        {
            double x, y;
            
            zedGraphPane.ReverseTransform(p, out x, out y);

            if (!isPointInGraph(x, y))
            {
                toolTip.Hide(zedGraphControl);               
            }
            else
            {
                Point point= new Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
                toolTip.Show(x.ToString("F2") + ":" + y.ToString("F2"), zedGraphControl, point);           
            }

            
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
            enableEditOnGraph();
            
            ZedGraph.CurveItem curve;
            int index;
            bool isOnCurve = zedGraphPane.FindNearestPoint(p, zedGraphPane.CurveList, out curve, out index);
            
            // Если первая или последняя точка интервала
            if (isOnCurve && curve == zedGraphPane.CurveList[0] && (index == 0 || index == profile.Count - 1))
            {
                // Запрет редактирования по x
                zedGraphControl.IsEnableHEdit = false;
            }

            // Если точка не на профиле интервала
            if (isOnCurve && curve != zedGraphPane.CurveList[0])
            {
                // Запрет редактирования
                disableEditOnGraph();
            }

            if (!isOnCurve || curve != zedGraphPane.CurveList[0])
            {    double x, y;
                zedGraphPane.ReverseTransform(p, out x, out y);
                if (isPointInGraph(x, y))
                {
                    addPointOnProfile(p);
                }
            }
            
        }

        public void zedGraphRightDown(PointF p)
        {
            ZedGraph.CurveItem curve;
            int index;
            bool isOnCurve = zedGraphPane.FindNearestPoint(p, zedGraphPane.CurveList, out curve, out index);

            if (isOnCurve && curve == zedGraphPane.CurveList[0] && index > 0 && index < profile.Count - 1)
            {
                zedGraphControl.IsShowContextMenu = true;
                selectedPoint = index;
            }
            else
            {
                zedGraphControl.IsShowContextMenu = false;
            }            
        }
        public void listToArrays(List<RRL.PointD> list, out double[] x, out double[] y)
        {
            x = new double[list.Count];
            y = new double[list.Count];
            for (int i = 0; i < list.Count; ++i)
            {
                x[i] = list[i].x;
                y[i] = list[i].y;
            }
        }

        public void calculation()
        {
            // Добаление к координатом профиля кривизны дуги земной поверхности

            List<RRL.PointD> profileWithEarthCurve = new List<RRL.PointD>(profile.Count);
            for (int i = 0; i < profile.Count; ++i)
            {
                profileWithEarthCurve.Add(new RRL.PointD(profile[i]));
                profileWithEarthCurve[i].y += getEarthCurveH(profileWithEarthCurve[i].x);
            }
             
            RRL.RRLCalculator calc = new RRL.RRLCalculator(profileWithEarthCurve, _lambda, _antennaH);
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
            }
        }

        
        

        public InterfaceManager(ZedGraph.ZedGraphControl zgc)
        {
            zedGraphControl = zgc;
            zedGraphPane = zgc.GraphPane;
            
            _R = 20;
            _hMax = 200;
            _antennaH = 20;
            _lambda = 2.5;
            profile = new List<RRL.PointD>(20);
            profile.Insert(0, new RRL.PointD(0, (_hMax + _hMin) / 2.0));
            profile.Insert(1, new RRL.PointD(_R, (_hMax + _hMin) / 2.0));
            profileX = new double[1];
            profileY = new double[1];
            earthCurveX = new double[1];
            earthCurveY = new double[1];
            zedGraphControl.IsShowContextMenu = false;
            ZedGraph.Line.Default.IsSmooth = true;
            ZedGraph.Line.Default.SmoothTension = 0.3F;
            selectedPoint = -1;
            ZedGraph.GraphPane.Default.NearestTol = 10;
            zedGraphPane.Title.Text = "Профиль интервала РРЛ";
            zedGraphPane.XAxis.Title.Text = "Расстояние";
            zedGraphPane.YAxis.Title.Text = "Высота";
            zedGraphPane.XAxis.Scale.Max = _R;
            zedGraphPane.YAxis.Scale.Min = hMin;
            zedGraphPane.YAxis.Scale.Max = hMax;
            zedGraphControl.EditButtons = MouseButtons.Left;
            updateZedGraphSteps();
            updateEarthCurve();
            updateProfile();
            updateGraph();
        }
    }
}
