
using System;
using System.Collections.Generic;
namespace anomaly_detector_web_service.Types
{
    public class AD
    {
        public List<CorrelatedFeatures> cf { get; }
        public bool isRegression { get; set; }
        public AD(Data train) // Ctor for Model's Ctor (learnNormal, generates cf)
        {
            cf = new List<CorrelatedFeatures>();
            learnNormal(train);
        }
        public AD(bool isRegression) // Ctor for detect(consuming cf)
        {
            this.isRegression = isRegression;
        }

        double avg(List<double> x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; sum += x[i], i++) ;
            return sum / size;
        }

        // returns the variance of X and Y
        double variance(List<double> x, int size)
        {
            double av = avg(x, size);
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * x[i];
            }
            return sum / size - av * av;
        }

        // returns the covariance of X and Y
        double cov(List<double> x, List<double> y, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= size;

            return sum - avg(x, size) * avg(y, size);
        }


        // returns the Pearson correlation coefficient of X and Y
        double pearson(List<double> x, List<double> y, int size)
        {
            double temp = (Math.Sqrt(variance(x, size)) * Math.Sqrt(variance(y, size)));
            if (temp == 0)
            {
                return 0;
            }
            return cov(x, y, size) / temp;
        }

        // performs a linear regression and returns the line equation
        Line linear_reg(List<Point> points, int size)
        {
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            for (int i = 0; i < size; i++)
            {
                x.Add(points[i].x);
                y.Add(points[i].y);
            }
            double a = cov(x, y, size) / variance(x, size);
            double b = avg(y, size) - a * (avg(x, size));

            return new Line(a, b);
        }

        // returns the deviation between point p and the line equation of the points
        double dev(Point p, List<Point> points, int size)
        {
            Line l = linear_reg(points, size);
            return dev(p, l);
        }

        // returns the deviation between point p and the line
        double dev(Point p, Line l)
        {
            return Math.Abs(p.y - (l.a * p.x + l.b));
        }



        double dist(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }


        // checks if a point is in circle or on it's perimeter
        bool isInCircle(Circle c, Point p) 
        { 
            return dist(c.c, p) <= c.r; 
        }








    // methods ruteuns correlation between features i and j
    double calcFeaturesCorrelation(Data ts, List<double> i, List<double> j)
        {
            return Math.Abs(pearson(i, j,i.Count));
        }

        // checks for every feature j if J-I correlation is bigger than given correlation
        // (where i is the 2nd feature)
        bool isMaxCorelForFeature(string feature, double correlation, List<CorrelatedFeatures> cfVec)
        {
            foreach (var cf in cfVec)
            {
                if (cf.feature2 == feature && cf.correlation >= correlation)
                {
                    return false;
                }
            }
            return true;
        }

        // set threshold as max{dev(p in points, linear_reg)} -
        // max deviation of all points from linerreg line
        double findThreshold(List<Point> parray, Line reg, Data ts)
        {
            double threshold = 0;
            for (int i = 0; i < parray.Count; i++)
            {
                double temp_thres = dev(parray[i], reg);
                if (temp_thres > threshold)
                {
                    threshold = temp_thres;
                }
            }
            return threshold;
        }

        List<Point> toPoints(List<double> x, List<double> y)
        {
            List<Point> ps = new List<Point>();
            for (int i = 0; i < x.Count; i++)
            {
                ps.Add(new Point(x[i], y[i]));
            }
            return ps;
        }
        // input is  timeseries and a correlation of 2 features,
        // method sets linearreg line and max deviation (threshold) of all points from linearreg line
        void set_CF_threshold(CorrelatedFeatures correlf, Data train)
        {
            // set liner regression line and MEC
            // get features' vectors
            List<double> v1 = train.ts[correlf.feature1];
            List<double> v2 = train.ts[correlf.feature2];
            // points array in size of timeseries' entries (rows - 1)
            // each point represent (f1,f2) at given time
            List<Point> points = toPoints(v1, v2);
            // if 2 features correlation is above normal_threshold,
            // anomaly check by max deviation from linearreg Line
            // otherwise (0.5 < correl < normal_threshold),
            // anomaly can be checked by MEC
            correlf.lin_reg = linear_reg(points, points.Count);
            correlf.threshold = findThreshold(points, correlf.lin_reg, train) * 1.1; // * 110%
            correlf.MEC = new Circle(points);
            correlf.MEC.r *= 1.1; // * 110%
                                       //deletePoints(points, v1.size());
        }

        void learnNormal(Data data)
        {
            CorrelatedFeatures correlf = new CorrelatedFeatures();
            foreach (var entry in data.ts)
            {
                // for every feature i
                correlf.feature1 = entry.Key;
                correlf.feature2 = "";
                // init feature i (max)correlation with any other feature to be 0
                correlf.correlation = 0;
                // check correlation with feature j (j > i, upper triangular matrix)
                foreach (var inner in data.ts)
                {
                    if(entry.Key == inner.Key) { continue; }
                    // if i correlate with j more than any previous j,
                    // AND i,j correlation is above minimum threshold, update correlf
                    double correlIJ = calcFeaturesCorrelation(data, entry.Value, inner.Value);
                    if (correlIJ > correlf.correlation && correlIJ > 0.5)
                    {
                        correlf.correlation = correlIJ;
                        correlf.feature2 = inner.Key;
                        set_CF_threshold(correlf, data);
                    }
                }
                // if correlation was found for feature i - push to cf vector
                if (correlf.feature2.Length != 0 && !this.cf.Contains(correlf))
                {
                    this.cf.Add(new CorrelatedFeatures(correlf));
                }
            }
        }


        bool isAnomalous(double x, double y, CorrelatedFeatures c)
        {
            if (this.isRegression)
            {
                return Math.Abs(y - (c.lin_reg.a*x+c.lin_reg.b)) > c.threshold;
            } else {
                if (Math.Abs(c.correlation) > Math.Abs(0.9/*normal threshold*/))
                {
                    return Math.Abs(y - (c.lin_reg.a * x + c.lin_reg.b)) > c.threshold;
                }
                return !isInCircle(c.MEC, new Point(x, y));
            }
        }


        AnomaliesReport unifySpans(List<Anomaly> ar)
        {
            AnomaliesReport unifiedSpans = new AnomaliesReport();
            for (int i = 0; i < ar.Count; i++)
            {
                List<Span> s = new List<Span>();
                int start, end;
                string description = ar[i].f1;
                start = ar[i].timestep;
                while (i < ar.Count - 1 && ar[i + 1].f1.CompareTo(description) == 0)
                {
                    while (i < ar.Count - 1 && ar[i + 1].timestep - ar[i].timestep == 1)
                    {
                        ++i;
                    }
                    end = ar[i].timestep;
                    s.Add(new Span(start, end, ar[i].f2));
                }
                if (s.Count == 0) { s.Add(new Span(start, start, ar[i].f2)); }
                unifiedSpans.reports.Add(description, s);
            }
            return unifiedSpans;
        }

        public AnomaliesReport detect(Data test, List<CorrelatedFeatures> cf) {
            List<Anomaly> arVec = new List<Anomaly>();    // vector of AnomalyReport s
        // int correlNum = this->cf.size();	// number of correlations found (above normal threshold)
	// for every correl of 2 features in this.cf
	for (int j = 0; j<cf.Count; j++) {
		// and for every row in TimeSeries:
		// check if in row i in TS 2 features of cf[j]
		// are deviated from their linearreg more then allowed (in cf[j].threshold)
		for (int i = 0; i<test.numOfLines; i++) {
			CorrelatedFeatures temp = cf[j];
			double x = test.ts[temp.feature1][i];
            double y = test.ts[temp.feature2][i];
			// if at any time 2 features deviated more than allowed - report anomaly between them
			if (isAnomalous(x, y, temp)) {
				arVec.Add(new Anomaly(temp.feature1,temp.feature2, i + 1));
			}
        }
	
    }
	return unifySpans(arVec);
}

    }
}