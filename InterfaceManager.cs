using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalculationRRL
{
    public class CurveInfo
    {
        public CurveInfo(String name, ZedGraph.CurveItem c, bool isEdit)
        {
            curve = c;
            isAllowEdit = isEdit;
            this.name = name;
        }
        public ZedGraph.CurveItem curve;
        public bool isAllowEdit;
        public String name;
    }
    class InterfaceManager
    {
        // Выделенная точка, на которую нажали правой
        int selectedPoint;        

        // Текущее состояние
        private State currentState;
        public void goNextState()
        {
            currentState.doAfter();
            currentState = currentState.getNext();
            currentState.doBefore();
        }
        

        public CurveInfo getCurveInfo(String name)
        {
            return curveList.Find(x => x.name.Equals(name));
        }

        // <Имя кривой, информация о кривой>
        public List<CurveInfo> curveList;

        public List<List<ZedGraph.CurveItem> > bariersList;
        
        public void addNewBarier()
        {
            bariersList.Add(new List<ZedGraph.CurveItem>());
            currentBarier = bariersList[bariersList.Count - 1];
        }

        List<ZedGraph.CurveItem> currentBarier;
        




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

                if (_isShowEarthCurve)
                {
                    zedGraphPane.AddCurve("Линия кривизны дуги земной поверхности", earthCurveX, earthCurveY, Color.Red, ZedGraph.SymbolType.None);
                    curveList.Add(new CurveInfo("earthCurve", zedGraphPane.CurveList[zedGraphPane.CurveList.Count - 1], false));
                }
                else
                {
                    zedGraphPane.CurveList.Remove(curveList.Find(x => x.name.Equals("earthCurve")).curve);
                }
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
            insertDataToCurve(x, y, zedGraphPane.CurveList[1]);
            // Добавление кривой (линия прямой видимости)
            //zedGraphPane.AddCurve("Линия прямой видимости", x, y, Color.Blue, ZedGraph.SymbolType.UserDefined);
        }

        private void insertDataToCurve(double[] x, double[] y, ZedGraph.CurveItem curve)
        {
            curve.Clear();
            for (int i = 0; i < x.Length; ++i)
            {
                curve.AddPoint(x[i], y[i]);
            }
        }

        private void updateGraph()
        {
            // Профиль интервала
            insertDataToCurve(profileX, profileY, zedGraphPane.CurveList[0]);
          
            drawLineOfSight();

            if (_isShowEarthCurve)
            {
                zedGraphPane.AddCurve("", earthCurveX, earthCurveY, Color.Red, ZedGraph.SymbolType.None);
            }

            zedGraphControl.Invalidate(); 

        }

        public void addPointOnCurve(RRL.PointD p)
        {
            if (_isShowEarthCurve)
            {
                p.y -= getEarthCurveH(p.x);
            }
            // Добавление новой точки к препятствию
            if (currentState.GetType().Name.Equals("InputBarier"))
            {
                if (currentBarier.Count < 3)
                {
                    double[] x = new double[1];
                    double[] y = new double[1];

                    x[0] = p.x;
                    y[0] = p.y;

                    zedGraphPane.AddCurve("", x, y, Color.Red, ZedGraph.SymbolType.Circle);
                    currentBarier.Add(zedGraphPane.CurveList[zedGraphPane.CurveList.Count - 1]);                    
                }
                else
                {
                    MessageBox.Show("Введены три точки препятствия, для ввода еще одного препятствия нажмите \"Добавить препятствие\"." +
                        "Для расчета пригодности интервала нажмите \"Расчет\"");
                }
            }
            if (currentState.GetType().Name.Equals("InputProfilePoints"))
            {     
                int i = profile.FindIndex(a => a.x > p.x);
                if (i < 0)
                {
                    i = profile.Count;
                }
                profile.Insert(i, p);
                updateProfile();
            }
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

        public void editPointOnProfile(ZedGraph.CurveItem curve, int index, RRL.PointD p)
        {
            if (isPointInGraph(p.x, p.y))
            {
                if (currentState.GetType().Name.Equals("InputProfilePoints"))
                {
                    profile[index] = p;
                }
                if (currentState.GetType().Name.Equals("InputBarier"))
                {
                    curve.Points[0].Y = getYCoordOfProfile(p.x);
                }                

                if (_isShowEarthCurve)
                {
                    p.y -= getEarthCurveH(p.x);
                }
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
            ZedGraph.CurveItem curve;
            int index;
            bool isOnCurve = zedGraphPane.FindNearestPoint(p, zedGraphPane.CurveList, out curve, out index);

            if (isOnCurve)
            {
                for (int i = 0; i < curveList.Count; ++i)
                {
                    if (curve == curveList[i].curve)
                    {
                        if (curveList[i].isAllowEdit)
                        {
                            enableEditOnGraph();
                            // Если первая или последняя точка интервала
                            if (curveList[i].name.Equals("profile") && (index == 0 || index == curve.Points.Count - 1))
                            {
                                // То разрешить редактирование только по Y
                                zedGraphControl.IsEnableHEdit = false;
                            }
                        }
                        else
                        {
                            disableEditOnGraph();
                        }
                    }
                }
            }
            else
            {
                double x, y;
                zedGraphPane.ReverseTransform(p, out x, out y);
                if (isPointInGraph(x, y))
                {
                    addPointOnCurve(new RRL.PointD(x, y));
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

            selectedPoint = -1;
            profile.Insert(0, new RRL.PointD(0, (_hMax + _hMin) / 2.0));
            profile.Insert(1, new RRL.PointD(_R, (_hMax + _hMin) / 2.0));
            zedGraphPane.XAxis.Scale.Max = _R;
            zedGraphPane.YAxis.Scale.Min = hMin;
            zedGraphPane.YAxis.Scale.Max = hMax;

            updateZedGraphSteps();

            zedGraphPane.AddCurve("Профиль интевала", profileX, profileY, Color.Black, ZedGraph.SymbolType.Circle);
            curveList.Add(new CurveInfo("profile", zedGraphPane.CurveList[0], true));

            zedGraphPane.AddCurve("Линия прямой видимости", profileX, profileY, Color.Blue, ZedGraph.SymbolType.None);
            curveList.Add(new CurveInfo("lineOfSight", zedGraphPane.CurveList[1], false));

            updateEarthCurve();
            updateProfile();
            updateGraph();
        }


        private double getYCoordOfProfile(double x)
        {
            return ((ZedGraph.PointPairList)curveList.Find(a => a.name.Equals("profile")).curve.Points).SplineInterpolateX(x, ZedGraph.Line.Default.SmoothTension);
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
            
            profileX = new double[1];
            profileY = new double[1];
            earthCurveX = new double[1];
            earthCurveY = new double[1];
            curveList = new List<CurveInfo>();
            bariersList = new List<List<ZedGraph.CurveItem>>();
           
            zedGraphControl.IsShowContextMenu = false;
            ZedGraph.Line.Default.IsSmooth = true;
            ZedGraph.Line.Default.SmoothTension = 0.3F;
            ZedGraph.GraphPane.Default.NearestTol = 10;
            zedGraphControl.EditButtons = MouseButtons.Left;
            zedGraphPane.Title.Text = "Профиль интервала РРЛ";
            zedGraphPane.XAxis.Title.Text = "Расстояние";
            zedGraphPane.YAxis.Title.Text = "Высота";

            FinalState finalState = new FinalState(this);
            InputBarier inputBarierState = new InputBarier(finalState, this);
            InputProfilePoints inputProfilePoints =  new InputProfilePoints(inputBarierState, this);            
            currentState = new InputIntervalParameters(inputProfilePoints, this);

        }
    }

    
}
