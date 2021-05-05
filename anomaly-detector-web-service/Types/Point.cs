using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Types
{
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public Point(double x, double y) { this.x = x; this.y = y; }

		public Point Subtract(Point p)
		{
			return new Point(x - p.x, y - p.y);
		}


		public double Distance(Point p)
		{
			double dx = x - p.x;
			double dy = y - p.y;
			return Math.Sqrt(dx * dx + dy * dy);
		}


		// Signed area / determinant thing
		public double Cross(Point p)
		{
			return x * p.y - y * p.x;
		}

	}
}