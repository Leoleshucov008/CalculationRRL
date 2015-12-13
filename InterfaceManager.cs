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
        private int _numberOfChannel;
        public int numberOfChannel { get; set; }
        private string _poddiap;
        public string poddiap { get; set; }
        private string _stationType;
        public string stationType { get; set; }
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
        private double _lamda;
        public double lamda
        {
            get { return _lamda; }
            set
            {
                if (_lamda == value)
                    return;
                _lamda = value;
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
            profile[index] = p;
            updateProfile();
            updateGraph();
        }
        static int iii = 0;
        public void showHint(PointF p, ToolTip toolTip)
        {
            double x, y;
            
            zedGraphPane.ReverseTransform(p, out x, out y);

            if (x < 0 || x > _R || y < _hMin || y > _hMax)
            {
                toolTip.Hide(zedGraphControl);
                Console.WriteLine("HIde " + iii.ToString());
            }
            else
            {
                Point point= new Point(Convert.ToInt32(p.X), Convert.ToInt32(p.Y));
                toolTip.Show(x.ToString("F2") + ":" + y.ToString("F2"), zedGraphControl, point);
                Console.WriteLine("Show" + iii.ToString());
            }
            ++iii;
            
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
            
            if (!isOnCurve)
            {
                double x, y;
                zedGraphPane.ReverseTransform(p, out x, out y);
                if (x > 0 && x < _R)
                {
                    addPointOnProfile(p);
                }
            }
            if (isOnCurve && curve == zedGraphPane.CurveList[0] && (index == 0 || index == profile.Count - 1))
            {
                zedGraphControl.IsEnableHEdit = false;
            }

            if (isOnCurve && curve != zedGraphPane.CurveList[0])
            {
                disableEditOnGraph();
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

        public void calculation()
        {
            List<RRL.PointD> profileWithEarthCurve = new List<RRL.PointD>(profile);
            foreach (RRL.PointD p in profileWithEarthCurve)
            {
                p.y += getEarthCurveH(p.x);
            }
            RRL.RRLCalculator calc = new RRL.RRLCalculator(profileWithEarthCurve, _numberOfChannel, _poddiap, _stationType, _antennaH);
            List<RRL.PointD> h0 = calc.getH0();
            double[] x = new double[h0.Count];
            double[] y = new double[h0.Count];
            for (int i = 0; i < h0.Count; ++i)
            {
                x[i] = h0[i].x;
                y[i] = h0[i].y;
            }
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
            _lamda = 0;
            profile = new List<RRL.PointD>(20);
            profile.Insert(0, new RRL.PointD(0, (_hMax + _hMin) / 2.0));
            profile.Insert(1, new RRL.PointD(_R, (_hMax + _hMin) / 2.0));
            profileX = new double[1];
            profileY = new double[1];
            earthCurveX = new double[1];
            earthCurveY = new double[1];
            zedGraphControl.IsShowContextMenu = false;
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
