using System;
using System.Collections.Generic;
using ZedGraph;

namespace CalculationRRL
{
    // Тип интервала 
    enum IntervalType { opened, halfOpened, closed }

    class RRLCalculator
    {
        Interval interval;
        double R;
        double tension = Line.Default.SmoothTension;
        public RRLCalculator(Interval interval)
        {
            this.interval = interval;
            R = this.interval.R;
        }

        public PointPairList getIntersections(PointPairList points1, PointPairList points2)
        {
            // Возвращает список точек пересечения ломаной с линией f
            // step - точность с которой неообходимо найти точки пересечения
            PointPairList p = new PointPairList();
            double xl = points1[0].X;
            double xend = points1[points1.Count - 1].X;
            double step = 0.01;
            while (xl < xend)
            {
                double xr = xl + step;
                if ((points1.SplineInterpolateX(xl, tension) - points2.SplineInterpolateX(xl, tension))
                    * (points1.SplineInterpolateX(xr, tension) - points2.SplineInterpolateX(xr, tension)) <= 0)
                {
                    p.Add((xl + xr) / 2.0, points1.SplineInterpolateX((xl + xr) / 2.0, tension));
                }
                xl = xr;

            }
            return p;
        }

        // Расчет РРЛ
        public void calculate()
        {
            // Определение типа интервала

            // Точки пересечения профиля местности с линией критических просветов 
            PointPairList intersectWithH0 = getIntersections(interval.H0, interval.profile.points);

            // Точки пересечения линии прямой видимости с профилем интервала
            PointPairList intersectWithLOS = getIntersections(((PointPairList)interval.lineOfSight.Points), interval.profile.points);

            // Если профиль интервала пересекает линию прямой видимости, то интервал закрытый
            if (intersectWithLOS.Count > 0)
            {
                //calcClosed();
            }
            // Если профиль интервала пересекает линию критических просветов, то полуоткрытый
            else if (intersectWithH0.Count > 0)
            {
                // calcHalfOpened();
            }
            // Итервал открытый
            else
            {
                //calcOpened();
            }

        }
        public static double norma(PointPair a, PointPair b)
        {
            return Math.Sqrt(Math.Pow((b.X - a.X), 2) + Math.Pow((b.Y - a.Y), 2));
        }

        public double Radius(PointPair a, PointPair b, PointPair c)
        {
            double A, B, C, S;

            A = norma(a, b);
            B = norma(b, c);
            C = norma(a, c);

            S = Math.Sqrt((A + B + C) * (-A + B + C) * (A - B + C) * (A + B - C));
            return A * B * C / S;
        }
        private double getWpDop(string stationType, string poddiap, int waveNumber, int intervalCount)
        {
            if (stationType.Equals("Р-409"))
            {
                if (poddiap.Equals("Б"))
                {
                    if (waveNumber >= 0 && waveNumber < 151)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.235 * R - 45.68); }
                            case 2: { return (0.238 * R - 44.26); }
                            case 4: { return (0.235 * R - 42.5); }
                            case 6: { return (0.235 * R - 41.5); }
                            default: { return (0.235 * R - 40.5); }
                        }
                    }
                    else if (waveNumber >= 151 && waveNumber < 301)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.247 * R - 44.22); }
                            case 2: { return (0.248 * R - 42.68); }
                            case 4: { return (0.245 * R - 41); }
                            case 6: { return (0.245 * R - 39.5); }
                            default: { return (0.239 * R - 38.7); }
                        }
                    }
                    else if (waveNumber >= 301 && waveNumber < 451)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.24 * R - 42.3); }
                            case 2: { return (0.24 * R - 40.8); }
                            case 4: { return (0.245 * R - 39.3); }
                            case 6: { return (0.245 * R - 38.3); }
                            default: { return (0.245 * R - 37.3); }
                        }
                    }
                    else if (waveNumber >= 451 && waveNumber < 599)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.237 * R - 41.12); }
                            case 2: { return (0.235 * R - 39.5); }
                            case 4: { return (0.25 * R - 38.4); }
                            case 6: { return (0.24 * R - 36.9); }
                            default: { return (0.24 * R - 35.9); }
                        }
                    }
                    else
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.24 * R - 40.3); }
                            case 2: { return (0.235 * R - 38.5); }
                            case 4: { return (0.238 * R - 36.78); }
                            case 6: { return (0.238 * R - 35.78); }
                            default: { return (0.238 * R - 34.78); }
                        }
                    }
                }
                else
                {
                    if (waveNumber >= 0 && waveNumber < 151)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.23 * R - 43.5); }
                            case 2: { return (0.22 * R - 41.6); }
                            case 4: { return (0.235 * R - 40.5); }
                            case 6: { return (0.235 * R - 39.5); }
                            default: { return (0.225 * R - 38.2); }
                        }
                    }
                    else if (waveNumber >= 151 && waveNumber < 301)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.235 * R - 42); }
                            case 2: { return (0.235 * R - 40.5); }
                            case 4: { return (0.235 * R - 39); }
                            case 6: { return (0.235 * R - 38); }
                            default: { return (0.235 * R - 37); }
                        }
                    }
                    else if (waveNumber >= 301 && waveNumber < 451)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.24 * R - 40.8); }
                            case 2: { return (0.225 * R - 38.9); }
                            case 4: { return (0.24 * R - 37.8); }
                            case 6: { return (0.24 * R - 36.8); }
                            default: { return (0.24 * R - 35.8); }
                        }
                    }
                    else if (waveNumber >= 451 && waveNumber < 599)
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.225 * R - 39); }
                            case 2: { return (0.235 * R - 38); }
                            case 4: { return (0.225 * R - 36); }
                            case 6: { return (0.225 * R - 35); }
                            default: { return (0.225 * R - 34); }
                        }
                    }
                    else
                    {
                        switch (intervalCount)
                        {
                            case 1: { return (0.225 * R - 38); }
                            case 2: { return (0.225 * R - 36.5); }
                            case 4: { return (0.245 * R - 35.6); }
                            case 6: { return (0.245 * R - 34.6); }
                            default: { return (0.245 * R - 33.6); }
                        }
                    }
                }
            }
            else
            {
                if (poddiap.Equals("1")) return (-0.0016 * R * R + 0.6039 * R - 43.7);
                else return (-0.0011 * R * R + 0.6521 * R - 37.2);
            }
        }

        private double getW0(double Mu)
        {
            return 15.832 * Math.Pow(Mu, -0.853);
        }
        private string calcOpened(string typeOfSurface, PointPair leftCoord, PointPair rightCoord, out double Wp, out double WpDop, out double q1)
        {
            double point = leftCoord.X + (rightCoord.X - leftCoord.X) / 2;
            double H = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(point, tension) - interval.profile.points.SplineInterpolateX(point, tension);
            double H0 = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(point, tension) - interval.H0.SplineInterpolateX(point, tension);
            double h = H / H0;
            double Fe;
            switch (typeOfSurface)
            {
                case "Равнина, луга, солончаки": { Fe = 0.95; break; }
                case "Ровная лесистая поверхность": { Fe = 0.9; break; }
                case "Среднепересечённая местность": { Fe = 0.7; break; }
                default: { Fe = 0.6; break; }
            }
            Wp = (-10 * Math.Log10(1 + Fe * Fe - 2 * Fe * Math.Cos(h * h * Math.PI / 3)));
            double q0 = 0.0038 * R * R - 0.5762 * R + 50.857;
            q1 = q0 + Wp;
            WpDop = getWpDop(interval.stationType, interval.subRange, interval.waveNumber, 1);//interval.intervalCount);
            if (Wp < WpDop) return "Пригоден";
            else return "Непригоден";
        }
        private string calcHalfOpened(PointPair leftcoord, PointPair rightcoord, PointPair midlecoord, out double Wp, out double WpDop)
        {
            double K = leftcoord.X / R;
            double point = leftcoord.X + (rightcoord.X - leftcoord.X) / 2;
            double H0 = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(point, tension) - interval.H0.SplineInterpolateX(point, tension);
            double Mu = Math.Pow((R * R * K * K * (1 - K) * (1 - K)) / (H0 * Radius(leftcoord, midlecoord, rightcoord)), 0.333);
            double Wp0 = getW0(Mu);
            double H = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(point, tension) - interval.profile.points.SplineInterpolateX(point, tension);
            double h = H / H0;
            Wp = Wp0 * (1 - h);
            WpDop = getWpDop(interval.stationType, interval.subRange, interval.waveNumber, 0); //interval.intervalCount);
            if (Wp < WpDop) return "Пригоден";
            else return "Непригоден";
        }
        private void calcClosed()
        {
        }
    }
}

