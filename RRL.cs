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

            double WpDop = getWpDop(interval.stationType, interval.subRange, interval.waveNumber, /*interval.intervalCount*/1);//TODO: при добавлении изменяемого количества интервалов, последним параметром подставить подставить их количетсво
            double Wp =0; //величина ослабления радиоволн
            double q = 0; //Запас уровня ВЧ радиосигнала 
            // Если профиль интервала пересекает линию прямой видимости, то интервал закрытый
            if (intersectWithLOS.Count > 0)
            {
                calcClosed(interval.bariers, WpDop, out Wp, out q);
            }
            // Если профиль интервала пересекает линию критических просветов, то полуоткрытый
            else if (intersectWithH0.Count > 0)
            {
                calcHalfOpened(interval.bariers, WpDop, out Wp, out q);
            }
            // Итервал открытый
            else
            {
                calcOpened(/*interval.typeOfSurface*/"Среднепересечённая местность", WpDop, out Wp, out q);
            }

        }
        private bool merge(Barier left, Barier right)
        {
            double T1 = left.points[1].X;
            double T2 = right.points[1].X;
            if (T2 > 0.4126 * T1 * T1 - 1.3899 * T1 + 0.9837 && T2 < -T1 + 1) return true;
            else return false;
        }

        private double calcBarier(Barier barier)//расчёт препятствия для полуоткрытых инетрвалов
        {
            PointPair A = barier.points[0];
            PointPair B = barier.points[1];
            PointPair C = barier.points[2];
            double Y = B.Y - (((A.Y - C.Y) * B.X + (A.X * C.Y - A.Y * C.X)) / (C.X - A.X));//высота У(см. формулу 15 в методичке Садомовского)
            double r = C.X - A.X; //ширина препятствия
            double Rpr = (r * r) / (8 * Y);//формула 15(Садомовский)
            double K = B.X / R;
            double H0 = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(B.X, tension) - interval.H0.SplineInterpolateX(B.X, tension);
            double Mu = Math.Pow((R * R * K * K * (1 - K) * (1 - K)) / (H0 * Rpr), 0.333);
            double H = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(B.X, tension) - interval.profile.points.SplineInterpolateX(B.X, tension);
            double h = H / H0;
            double Wp0 = 15.832 * Math.Pow(Mu, -0.853);
            double Wp = Wp0 * (1 - h);
            return Wp;
        }

        private double getWpDop(string stationType, string poddiap, int waveNumber, int intervalCount)//вычисление допустимого Wp 
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

        private string calcOpened(string typeOfSurface, double WpDop, out double Wp, out double q)
        {
            double H = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(R/2, tension) - interval.profile.points.SplineInterpolateX(R/2, tension);
            double H0 = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(R/2, tension) - interval.H0.SplineInterpolateX(R/2, tension);
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
            q = q0 + Wp;
            if (Wp < WpDop) return "Пригоден";
            else return "Непригоден";
        }
        private string calcHalfOpened(List<Barier> bariers, double WpDop, out double Wp, out double q)
        {
            Wp = 0;
            double q0 = 0.0038 * R * R - 0.5762 * R + 50.857;
            if (bariers.Count==1)
            {
                Wp = calcBarier(bariers[0]);
                q = q0 + Wp;
                if (Wp < WpDop) return "Пригоден";
                else return "Непригоден";
            }
            else
            {
                bool prevousMerged = false;
                Barier barier = null;
                for (int i=1; i<bariers.Count; i++)
                {
                    if (prevousMerged)//если предыдыщие препятствия слились
                    {
                        if (merge(barier, bariers[i]))
                        { //слияние препятствий
                            Barier newBarier = new Barier(interval);
                            newBarier.addPoint(barier.points[0]);
                            double middlePointX = barier.points[1].X + (bariers[i].points[1].X - barier.points[1].X) / 2;
                            double middlePointY = barier.points[1].Y > bariers[i].points[1].Y ? barier.points[1].Y : bariers[i].points[1].Y;
                            newBarier.addPoint(new PointPair(middlePointX, middlePointY));
                            newBarier.addPoint(bariers[i].points[2]);
                            Wp += calcBarier(newBarier);
                            barier = newBarier;
                            prevousMerged = true;
                            i++;
                        }
                        else
                        {
                            Wp += calcBarier(bariers[i]);
                            prevousMerged = false;
                        }
                    }
                    else
                    {
                        if (merge(bariers[i-1], bariers[i]))
                        { //слияние препятствий
                            Barier newBarier = new Barier(interval);
                            newBarier.addPoint(bariers[i - 1].points[0]);
                            double middlePointX = bariers[i - 1].points[1].X + (bariers[i].points[1].X - bariers[i - 1].points[1].X) / 2;
                            double middlePointY = bariers[i - 1].points[1].Y > bariers[i].points[1].Y ? bariers[i - 1].points[1].Y : bariers[i].points[1].Y;
                            newBarier.addPoint(new PointPair(middlePointX, middlePointY));
                            newBarier.addPoint(bariers[i].points[2]);
                            barier = newBarier;
                            Wp += calcBarier(barier);
                            prevousMerged = true;
                            i++;
                        }
                        else
                        {
                            Wp += calcBarier(bariers[i - 1]);
                            Wp += calcBarier(bariers[i]);
                            prevousMerged = false;
                        }
                    }
                }
                q = q0 + Wp;
                if (Wp < WpDop) return "Пригоден";
                else return "Непригоден";
            }
        }
        private string calcClosed(List<Barier> bariers, double WpDop, out double Wp, out double q)
        {
            List<Barier> closedBariers = new List<Barier>();
            for (int i = 0; i < bariers.Count; i++)
                if (bariers[i].points[1].Y >= (interval.profile.points).SplineInterpolateX(bariers[i].points[1].X, tension))
                {
                    closedBariers.Add(bariers[i]);
                }
            Wp = 0;
            double q0 = 0.0038 * R * R - 0.5762 * R + 50.857;
            if (bariers.Count == 1)
            {
                Wp = calcBarier(bariers[0]);
                q = q0 + Wp;
                if (Wp < WpDop) return "Пригоден";
                else return "Непригоден";
            }
            else
            {
                if (closedBariers.Count == 1)
                {
                    #region Одно препятствие закрывающее линию прямой видимости
                    bool prevousMerged = false;
                    Barier barier = null;
                    for (int i = 1; i < bariers.Count; i++)
                    {
                        if (prevousMerged)//если предыдыщие препятствия слились
                        {
                            if (merge(barier, bariers[i]))
                            { //слияние препятствий
                                Barier newBarier = new Barier(interval);
                                newBarier.addPoint(barier.points[0]);
                                double middlePointX = barier.points[1].X + (bariers[i].points[1].X - barier.points[1].X) / 2;
                                double middlePointY = barier.points[1].Y > bariers[i].points[1].Y ? barier.points[1].Y : bariers[i].points[1].Y;
                                newBarier.addPoint(new PointPair(middlePointX, middlePointY));
                                newBarier.addPoint(bariers[i].points[2]);
                                Wp += calcBarier(newBarier);
                                barier = newBarier;
                                prevousMerged = true;
                                i++;
                            }
                            else
                            {
                                Wp += calcBarier(bariers[i]);
                                prevousMerged = false;
                            }
                        }
                        else
                        {
                            if (merge(bariers[i - 1], bariers[i]))
                            { //слияние препятствий
                                Barier newBarier = new Barier(interval);
                                newBarier.addPoint(bariers[i - 1].points[0]);
                                double middlePointX = bariers[i - 1].points[1].X + (bariers[i].points[1].X - bariers[i - 1].points[1].X) / 2;
                                double middlePointY = bariers[i - 1].points[1].Y > bariers[i].points[1].Y ? bariers[i - 1].points[1].Y : bariers[i].points[1].Y;
                                newBarier.addPoint(new PointPair(middlePointX, middlePointY));
                                newBarier.addPoint(bariers[i].points[2]);
                                barier = newBarier;
                                Wp += calcBarier(barier);
                                prevousMerged = true;
                                i++;
                            }
                            else
                            {
                                Wp += calcBarier(bariers[i - 1]);
                                Wp += calcBarier(bariers[i]);
                                prevousMerged = false;
                            }
                        }
                    }
                    q = q0 + Wp;
                    if (Wp < WpDop) return "Пригоден";
                    else return "Непригоден";
                    #endregion
                }
                else
                {
                    #region Несколько препятствий закрывающих линию прямой видимости
                    PointPair A = closedBariers[0].points[0];
                    PointPair B = closedBariers[0].points[1];
                    PointPair C = closedBariers[0].points[2];
                    double Y = B.Y - (((A.Y - C.Y) * B.X + (A.X * C.Y - A.Y * C.X)) / (C.X - A.X));//высота У(см. формулу 15 в методичке Садомовского)
                    double r = C.X - A.X; //ширина препятствия
                    double Rpr = (r * r) / (8 * Y);//формула 15(Садомовский)
                    double K = B.X / R;
                    double H0 = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(B.X, tension) - interval.H0.SplineInterpolateX(B.X, tension);
                    double Mu = Math.Pow((R * R * K * K * (1 - K) * (1 - K)) / (H0 * Rpr), 0.333);
                    PointPairList line = new PointPairList();
                    line.Add(interval.profile.points[0]);
                    line.Add(new PointPair(closedBariers[closedBariers.Count - 1].points[1]));
                    PointPairList points = getIntersections(line, closedBariers[0].points);
                    double H = closedBariers[0].points[1].Y - points[0].Y;
                    double h = H / H0;
                    double Wp0 = 15.832 * Math.Pow(Mu, -0.853);
                    Wp = Wp0 * (1 - h);

                    A = closedBariers[closedBariers.Count-1].points[0];
                    B = closedBariers[closedBariers.Count-1].points[1];
                    C = closedBariers[closedBariers.Count-1].points[2];
                    Y = B.Y - (((A.Y - C.Y) * B.X + (A.X * C.Y - A.Y * C.X)) / (C.X - A.X));//высота У(см. формулу 15 в методичке Садомовского)
                    r = C.X - A.X; //ширина препятствия
                    Rpr = (r * r) / (8 * Y);//формула 15(Садомовский)
                    K = B.X / R;
                    H0 = ((PointPairList)interval.lineOfSight.Points).SplineInterpolateX(B.X, tension) - interval.H0.SplineInterpolateX(B.X, tension);
                    Mu = Math.Pow((R * R * K * K * (1 - K) * (1 - K)) / (H0 * Rpr), 0.333);
                    line = new PointPairList();
                    line.Add(interval.profile.points[interval.profile.points.Count-1]);
                    line.Add(new PointPair(closedBariers[0].points[1]));
                    points = getIntersections(line, closedBariers[closedBariers.Count - 1].points);
                    H = closedBariers[closedBariers.Count - 1].points[1].Y - points[0].Y;
                    h = H / H0;
                    Wp0 = 15.832 * Math.Pow(Mu, -0.853);
                    Wp += Wp0 * (1 - h);

                    q = q0 + Wp;
                    if (Wp < WpDop) return "Пригоден";
                    else return "Непригоден";
                    #endregion
                }
            }
        }
    }
}

