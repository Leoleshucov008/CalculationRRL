using System;
using System.Collections.Generic;

namespace RRL
{

    public class PointD
    {
        // Точка, с прегруженными операциями сложения, вычитания и умножения на число
        public double x {get; set;} 
        public double y {get; set;}
        public PointD(){}
        public PointD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public PointD(PointD p)
        {
            this.x = p.x;
            this.y = p.y;
        }

        public static PointD operator + (PointD a, PointD b)
        {
            return new PointD(a.x + b.x, a.y + b.y);
        }

        public static PointD operator - (PointD a, PointD b)
        {
            return new PointD(a.x - b.x, a.y - b.y);
        }

        public static PointD operator * (PointD a, double b)
        {
            return new PointD(a.x * b, a.y * b);
        }



    }
    
    public abstract class Line
    {
        // Абстракный класс линии
        // Метод at(x) возвращает значение по y в точке x
        public Line() { }
        ~Line(){}
        abstract public double at(double x);
    }

    public class Polyline : Line
    {
        // Ломаная линия
        // Задается списком узлов
        // getIntersection находит точки пересечения ломаной с линией заданной функцией
        private List<PointD> points;

        private static int ComparePointD(PointD a, PointD b)
        {
            if (a.x < b.x)            
                return -1;
            if (a.x > b.x)
                return 1;
            if (a.y < b.y)
                return -1;
            if (b.y > b.x)
                return 1;
            return 0;
        }

        public Polyline(List<PointD> points)
        {
            this.points = new List<PointD>();
            for (int i = 0; i < points.Count; ++i)
            {
                this.points.Add(points[i]);
            }            
        }

        public override double at(double x)
        {
            int i = 0;
            while (i < points.Count - 1)
            {
                if (points[i].x <= x && points[i + 1].x >= x)
                    break;
                ++i;
            }
            if (i == points.Count - 1)
                return 0; // TODO: thorw exception
            
            PointD l = points[i], r = points[i + 1];
            double k = (r.y - l.y) / (r.x - l.x);
            return l.y - k * l.x + k * x;               
        }

        public List<PointD> getIntersections(Line f, double step)
        {
            // Возвращает список точек пересечения ломаной с линией f
            // step - точность с которой неообходимо найти точки пересечения
            List<PointD> p = new List<PointD>();
            for (int i = 0; i < points.Count - 1; ++i)
            {
                double xl = points[i].x;
                double yl = points[i].y;
                double xr = points[i].x + step;
                double yr;
                while (xr <= points[i + 1].x)
                {
                    yr = at(xr);
                    double ldy = f.at(xl) - yl, rdy = f.at(xr) - yr;
                    if (ldy == 0.0)
                    {
                        p.Add(new PointD(xl, yl));
                    }
                    else if (rdy == 0.0)
                    {
                        p.Add(new PointD(xr, yr));
                    }
                    else if (Math.Sign(ldy) != Math.Sign(rdy))
                    {
                        p.Add(new PointD((xl + xr) / 2.0, (yl + yr) / 2.0));
                    }
                    xl = xr;
                    yl = yr;
                    xr += step;
                }
            }
                
            return p;
        }
    }

    public class LineByFunc : Line
    {
        // Произвольная функция от x
        private Func<double, double> f;
        public LineByFunc(Func<double, double> f)
        {
            this.f = f;
        }

        public override double at(double x)
        {
            return f(x);
        }
    }

    public class Ellipse : Line
    {
        // Эллипс с наклоном к оси ox
        // x0, y0 - координаты центра
        // a, b - радиусы 
        // alpha - угол наклона
        private readonly double a;
        private readonly double b;
        private readonly double alpha;
        private readonly double x0;
        private readonly double y0;
        public Ellipse(double x0, double y0, double a, double b, double alpha)
        {
            this.x0 = x0;
            this.y0 = y0;
            if (Math.Abs(alpha) == Math.PI / 2.0)
            {
                this.a = b;
                this.b = a;
                this.alpha = 0;
            }
            else
            {
                this.a = a;
                this.b = b;
                this.alpha = alpha;
            }

        }

        public override double at(double x)
        {
            double xProj = (x - x0) / Math.Cos(alpha);
            return -xProj * Math.Sin(alpha) + b * Math.Sqrt(1 - xProj * xProj / a / a) * Math.Cos(alpha) + y0;
        }
    }
    // Тип интервала 
    enum IntervalType { opened, halfOpened, closed }

    class RRLCalculator
    {
        // Профиль интервала
        private Polyline intervalProfile;
        private Polyline lineOfSight;
        // Длина волны на которой ведется передача
        private double lambda;
        // Длина интервала
        private double R;
        // Линия критических просветов
        private LineByFunc H0;
        // Высота антенн
        private double antennaH;
        
        

        public RRLCalculator(List<PointD> p, double l, double h)
        {
            // Калькулятор РРЛ
            // На вход список точек - узлов ломаной определяющей профиль интревала,
            // в порядке увеичения координаты x
            // в первой точке x = 0
            // в последнй x = R
            // l - длина волны передачи
            // h - высота антенн

            intervalProfile = new Polyline(p);
            List<PointD> line = new List<PointD>(2);
            line.Add(new PointD(p[0]));
            line.Add(new PointD(p[p.Count - 1]));
            line[0].y += h;
            line[1].y += h;
            lineOfSight = new Polyline(line);
            lambda = l;
            antennaH = h;
            R = p[p.Count - 1].x;
            H0 = new LineByFunc(x =>  lineOfSight.at(x) - Math.Sqrt(1.0 / 3.0 * lambda * R * 1000 * x / R * (1.0 - x / R)));
        }

        // Расчет РРЛ
        public void calculate()
        {
            // Определение типа интервала
            
            // Точки пересечения профиля местности с линией критических просветов 
            List<PointD> intersectWithH0 = intervalProfile.getIntersections(H0, R / 1000.0);

            // Линия прямой видимости
            List<PointD> p = new List<PointD>(2);
            p.Add(new PointD(0, intervalProfile.at(0) + antennaH));
            p.Add(new PointD(R, intervalProfile.at(R) + antennaH));
            Polyline lineOfSight = new Polyline(p);
            p = null;
            
            // Точки пересечения линии прямой видимости с профилем интервала
            List<PointD> intersectWithLOS = intervalProfile.getIntersections(lineOfSight, R / 1000.0);

            // Если профиль интервала пересекает линию прямой видимости, то интервал закрытый
            if (intersectWithLOS.Count > 0)
            {
                calcClosed();
            }
            // Если профиль интервала пересекает линию критических просветов, то полуоткрытый
            else if (intersectWithH0.Count > 0)
            {
                calcHalfOpened();
            }
            // Итервал открытый
            else
            {
                calcOpened();
            }
            
        }

        public IntervalType getIntervalType()
        {
            // Определение типа интервала

            // Точки пересечения профиля местности с линией критических просветов 
            List<PointD> intersectWithH0 = intervalProfile.getIntersections(H0, R / 100000.0);

           
            // Точки пересечения линии прямой видимости с профилем интервала
            List<PointD> intersectWithLOS = intervalProfile.getIntersections(lineOfSight, R / 100000.0);

            // Если профиль интервала пересекает линию прямой видимости, то интервал закрытый
            if (intersectWithLOS.Count > 0)
            {
                return IntervalType.closed;
            }
            // Если профиль интервала пересекает линию критических просветов, то полуоткрытый
            else if (intersectWithH0.Count > 0)
            {
                return IntervalType.halfOpened;
            }
            // Итервал открытый
            else
            {
                return IntervalType.opened;
            }
           
        }

        public List<PointD> getH0()
        {
            List<PointD> list = new List<PointD>();
            double xPos = 0;
            double xStep = R / 100;
            while (xPos <= R)
            {
                list.Add(new PointD(xPos, H0.at(xPos)));
                xPos += xStep;
            }
            return list;
        }

        private void calcOpened()
        {
            //ящерица
        }
        private void calcHalfOpened()
        {
        }
        private void calcClosed()
        {
        }
    }
}

