using System;
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

    interface IGraphic
    {
        void addPoint(PointPair p);
        void editPoint(int index, PointPair newP);
        void removePoint(int index);
    }


    class Barier : IGraphic
    {
        private Interval interval;
        public PointPairList points { get; private set; }
        public ZedGraph.CurveItem curve { get; private set; }

        public Barier(Interval interval)
        {
            points = new PointPairList();
            this.interval = interval;
            interval.graphPane.AddCurve("", points, Color.Black, SymbolType.Diamond);
            this.curve = interval.graphPane.CurveList[interval.graphPane.CurveList.Count - 1];
            curve.IsVisible = false;  // Видимы только точки, без линий
        }

        public void addPoint(PointPair p)
        {            
            if (points.Count >= 3)
            {
                throw new BarierIsFull("Введены все точки барьераф");
            }
            p.Y = interval.profile.getY(p.X);            
        }

        public void editPoint(int index, PointPair p)
        {
            p.Y = interval.profile.getY(p.X);
            points[index] = p;
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

        private int findPointIndexOnProfile(PointPair p)
        {
            return points.FindIndex(a => a.X > p.X);
        }

        public Profile(PointPairList p, Interval interval)
        {
            points = p;
            this.interval = interval;
            interval.graphPane.AddCurve("", points, Color.Black, SymbolType.Circle);
            this.curve = interval.graphPane.CurveList[interval.graphPane.CurveList.Count - 1];
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
            if (i <= 0 || i >= points.Count - 1)
            {
                throw new InvalidPointPositon(p.ToString());
            }
            points.Insert(i, p);       
        }

        // Редактирование позиции точки на профиле интервала
        public void editPoint(int index, PointPair p)
        {
            if (!interval.isPointOnInterval(p))
            {
                throw new InvalidPointPositon(p.ToString());
            }
            int i = findPointIndexOnProfile(p);
            if (i != index)
            {
                throw new InvalidPointPositon(p.ToString());
            }
            points[index] = p;
        }

        public void removePoint(int index)
        {
            // Первая или последняя точка интервала
            if (index <= 0 || index >= points.Count - 1)
            {
                throw new InvalidIndex("Невозможно удалить первую и последнюю точку профиля");
            }

            points.RemoveAt(index);
        }
    }

    class Interval
    {
        public double minH { get; private set; }
        public double maxH { get; private set; }
        public double R { get; private set; }
        public double lambda { get; private set; }
        public Profile profile { get; private set; }
        public PointPairList lineOfSight { get; private set; }
        public List<Barier> bariers { get; private set; }
        public Barier currentBarier { get; private set; }
        public ZedGraph.GraphPane graphPane { get; private set; }

        public Interval(double R, double minH, double maxH, double lambda, ZedGraph.GraphPane graphPane)
        {
            this.R = R;
            this.minH = minH;
            this.maxH = maxH;
            this.lambda = lambda;
            this.graphPane = graphPane;
            
            lineOfSight = new PointPairList();
            bariers = new List<Barier>();

            PointPair begin = new PointPair(0, (minH + maxH) / 2.0);
            PointPair end = new PointPair(R, (minH + maxH) / 2.0);
         
            lineOfSight.Add(begin);
            lineOfSight.Add(end);

            graphPane.CurveList.Clear();
            profile = new Profile(lineOfSight, this);
            bariers = new List<Barier>();
        }

        // Лежит ли точка внутри интервала
        public bool isPointOnInterval(PointPair p)
        {
            return p.X >= 0 && p.X <= R && p.Y >= minH && p.Y <= maxH;
        }       

        public void addBarier()
        {
            currentBarier = new Barier(this);
            bariers.Add(currentBarier);
        }        
    }
}
