﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
namespace CalculationRRL
{
    class InvalidPointPositon : Exception
    {
        public InvalidPointPositon(String mes) :
            base(mes)
        {}
    }
    class InvalidIndex : Exception
    {
        public InvalidIndex(String mes) :
            base(mes)
        {}
    }
    class BarierIsFull : Exception
    {
        public BarierIsFull(String mes) :
            base(mes)
        {}
    }

    class BarierIsUncompleted : Exception
    {
        public BarierIsUncompleted(String mes) :
            base(mes)
        {}
    }

    class BarierIntersection : Exception
    {
        public BarierIntersection(String mes) :
            base(mes)
        {}
    }

    class OneBarierNecessary : Exception
    {
        public OneBarierNecessary(String mes) :
            base(mes)
        { }
    }

    

    interface IGraphic
    {
        CurveItem getCurve();
        void addPoint(PointPair p);
        void editPoint(int index, PointPair newP, PointPair oldP);
        void removePoint(int index);
    }

    class SyntheticBarier : IGraphic
    {
        private Interval interval;
        public PointPairList points { get; private set; }
        public CurveItem curve { get; private set; }
        public String barierType;

        public SyntheticBarier(Interval interval, PointPair p)
        {
            points = new PointPairList();
            this.interval = interval;
            Symbol.Default.FillType = FillType.Solid;
            curve = new LineItem("", points, Color.Sienna, SymbolType.Square);
            Symbol.Default.FillType = FillType.Brush;
            interval.graphPane.CurveList.Add(curve);
            curve.IsVisible = true;  // Видимы только точки, без линий
            barierType = "Лес";
        }

        public CurveItem getCurve()
        {
            return curve;
        }

        public void addPoint(PointPair p)
        {            
            if (points.Count >= 2)
            {
                throw new BarierIsFull("Введены все точки препятствия");
            }
            p.Y = interval.profile.getY(p.X);
            points.Add(p);
            points.Sort();
        }

        public void editPoint(int index, PointPair p, PointPair oldP)
        {
            if (!interval.isPointOnInterval(p))
            {
                points[index] = oldP;
                throw new InvalidPointPositon(p.ToString());
            }   
           
            p.Y = interval.profile.getY(p.X);
            points[index] = p;
            points.Sort();
        }

        public void removePoint(int index)
        {
            if (index < 0 || index >= points.Count)
            {
                throw new InvalidIndex("Точка не существует");
            }
            points.RemoveAt(index);
        }
    }

    class Barier : IGraphic
    {

        private Interval interval;
        public PointPairList points { get; private set; }
        public LineItem curve { get; private set; }
        public String barierType;

        public Barier(Interval interval)
        {
            points = new PointPairList();
            this.interval = interval;
            curve = new LineItem("", points, Color.SteelBlue, SymbolType.Diamond);
            curve.Line.IsVisible = false;
            curve.Symbol.Fill.Color = Color.SteelBlue;
            curve.Symbol.Fill.Type = FillType.Solid;
            interval.graphPane.CurveList.Add(curve);
            barierType = "Равнины, луга, соланчаки";
        }

        public CurveItem getCurve()
        {
            return curve;
        }

        public bool isInsideBarier(double x)
        {
            return x >= points[0].X && x <= points[2].X;
        }
        public void addPoint(PointPair p)
        {            
            if (points.Count >= 3)
            {
                throw new BarierIsFull("Введены все точки препятствия");
            }
            p.Y = interval.profile.getY(p.X);
            points.Add(p);
            points.Sort();
        }

        public void editPoint(int index, PointPair p, PointPair oldP)
        {
            if (!interval.isPointOnInterval(p))
            {
                points[index] = oldP;
                throw new InvalidPointPositon(p.ToString());
            }   
           
            p.Y = interval.profile.getY(p.X);
            points[index] = p;
            points.Sort();
        }

        public void removePoint(int index)
        {
            if (index < 0 || index >= points.Count)
            {
                throw new InvalidIndex("Точка не существует");
            }
            points.RemoveAt(index);
        }
    }

    class Profile : IGraphic
    {
        private Interval interval;
        public PointPairList points { get; private set; }
        public CurveItem curve { get; private set; }

        private bool _earthCurveShow;
        public bool earthCurveShow
        {
            get { return _earthCurveShow; }
            set
            {
                _earthCurveShow = value;
                
            }
        }

        // Перед каким узлом джна стоять точка с координатой p.x
        private int findPointIndexOnProfile(PointPair p)
        {
            int i = points.FindIndex(a => a.X > p.X);
            if (i == -1)
            {
                i = points.Count;
            }
            return i;
        }

        public Profile(PointPair begin, PointPair end, Interval interval)
        {
            points = new PointPairList();
            points.Add(begin);
            points.Add(end);
            this.interval = interval;
            curve = new LineItem("Профиль интервала", points, Color.Black, SymbolType.Circle);
            interval.graphPane.CurveList.Add(curve);
        }

        public CurveItem getCurve()
        {
            return curve;
        }

        public double getY(double x)
        {
            if (x < 0 || x > interval.R)
            {
                throw new InvalidPointPositon(x.ToString());
            }
            return points.SplineInterpolateX(x, ZedGraph.Line.Default.SmoothTension);
        }

        public void addPoint(PointPair p)
        {
            if (!interval.isPointOnInterval(p))
            {
                throw new InvalidPointPositon(p.ToString());
            }

            int i = findPointIndexOnProfile(p);

            // Первая или последняя точка интервала
            if (i <= 0 || i >= points.Count)
            {
                throw new InvalidPointPositon(p.ToString());
            }
            points.Insert(i, p);
        }

        // Редактирование позиции точки на профиле интервала
        public void editPoint(int index, PointPair p, PointPair oldP)
        {
            
            if (!interval.isPointOnInterval(p))
            {
                points[index] = oldP;
                throw new InvalidPointPositon(p.ToString());
            }
            int i = findPointIndexOnProfile(p);
            if (i - 1  != index)
            {
                points[index] = oldP;
                throw new InvalidPointPositon("Невозможно изменить позицию узла профиля интервала");
            }
            points[index] = p;
            if (index == 0)
            {
                interval.earthCurve.Points[0].Y = p.Y;
            }
            if (index == points.Count - 1)
            {
                interval.earthCurve.Points[1].Y = p.Y;
            }
        }

        public void removePoint(int index)
        {
            // Первая или последняя точка интервала
            if (index <= 0 || index >= points.Count - 1)
            {
                throw new InvalidIndex("Невозможно удалить первую и последнюю точку профиля");
            }

            points.RemoveAt(index);
            curve.Points = points;
        }
    }

    class Interval
    {
        public double minH { get; private set; }
        public double maxH { get; private set; }
        public double R { get; private set; }
        public double lambda { get; private set; }
        public double antennaH { get; private set; }
        public Profile profile { get; private set; }
        public CurveItem lineOfSight { get; private set; }
        public List<Barier> bariers { get; private set; }
        public Barier currentBarier { get; private set; }
        public CurveItem earthCurve { get; private set; }
        public ZedGraph.GraphPane graphPane { get; private set; }
        public PointPairList H0 { get; private set; }
        public String stationType { get; private set; }
        public String subRange { get; private set; }
        public int waveNumber { get; private set; }

        private double getEarthCurveH(double x)
        {
            // Радиус земли километров
            const double EarthR = 6371;
            return 1000.0 * (Math.Sqrt(EarthR * EarthR - Math.Pow(x - R / 2, 2.0)) - Math.Sqrt(EarthR * EarthR - R * R / 4));
        }
        
        public Interval(double R, double minH, double maxH, double lambda, ZedGraph.GraphPane graphPane, 
            string stationType, string subRange, int waveNumber)
        {
            this.R = R;
            this.minH = minH;
            this.maxH = maxH;
            this.lambda = lambda;
            this.antennaH = antennaH;
            this.graphPane = graphPane;
            this.stationType = stationType;
            this.subRange = subRange;
            this.waveNumber = waveNumber;
            
            bariers = new List<Barier>();

            PointPair begin = new PointPair(0, (minH + maxH) / 2.0);
            PointPair end = new PointPair(R, (minH + maxH) / 2.0);
         
            lineOfSight = new LineItem("Линия прямой видимости", new PointPairList(), Color.Blue, SymbolType.None);
            lineOfSight.AddPoint(begin.X, begin.Y + antennaH);
            lineOfSight.AddPoint(end.X, end.Y + antennaH);

            PointPairList points = new PointPairList();
            double x = 0;
            double stepX = R / 500;
            while (x <= R)
            {
                points.Add(x, getEarthCurveH(x));
                x += stepX;
            }
            earthCurve = new ZedGraph.LineItem("Дуга кривизны земной поверхности", points, Color.Green, SymbolType.None);

            graphPane.CurveList.Clear();
            profile = new Profile(begin, end, this);
            bariers = new List<Barier>();

            H0 = new PointPairList();
            x = 0;
            double step = 0.01;
            while (x <= R)
            {
                H0.Add(x, getH0(x));
                x += step;
            }
        }

        // Лежит ли точка внутри интервала
        public bool isPointOnInterval(PointPair p)
        {
            return p.X >= 0 && p.X <= R && p.Y >= minH && p.Y <= maxH;
        }

        public double getH0(double x)
        {
            return ((PointPairList)lineOfSight.Points).SplineInterpolateX(x, ZedGraph.Line.Default.SmoothTension)
                 - Math.Sqrt(1.0 / 3.0 * lambda * R * 1000 * x / R * (1.0 - x / R));
        }

        public void setBarier(int index)
        {
            if (index < 0 || index >= bariers.Count)
            {
                throw new InvalidIndex("Неверный номер барьера");
            }
            if (currentBarier != null)
            {
                currentBarier.curve.Symbol.Fill.Color = Color.SteelBlue;
            }
            currentBarier = bariers[index];
            currentBarier.curve.Symbol.Fill.Color = Color.Red;
        }

        public void removeCurrentBarier()
        {
            graphPane.CurveList.Remove(currentBarier.curve);
            bariers.Remove(currentBarier);
            currentBarier = null;
            if (bariers.Count == 0)
            {
                addBarier();
            }
            else
            {
                setBarier(bariers.Count - 1);
            }
        }

        public void addBarier()
        {
            bariers.Add(new Barier(this));
            setBarier(bariers.Count - 1);
        }
    }
}
