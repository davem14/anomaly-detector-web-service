using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Types
{
    public class Circle
    {
        public Point c { get; set; }
        public double r { get; set; }
        public Circle(Point p, double r) { this.c = p; this.r = r; }

		public static readonly Circle INVALID = new Circle(new Point(0, 0), -1);

		private const double MULTIPLICATIVE_EPSILON = 1 + 1e-14;


		public bool Contains(Point p)
		{
			return c.Distance(p) <= r * MULTIPLICATIVE_EPSILON;
		}


		public bool Contains(ICollection<Point> ps)
		{
			foreach (Point p in ps)
			{
				if (!Contains(p))
					return false;
			}
			return true;
		}




        /* 
 * Smallest enclosing circle - Library (C#)
 * 
 * Copyright (c) 2020 Project Nayuki
 * https://www.nayuki.io/page/smallest-enclosing-circle
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with this program (see COPYING.txt and COPYING.LESSER.txt).
 * If not, see <http://www.gnu.org/licenses/>.
 */

public sealed class SmallestEnclosingCircle
	{

		/* 
		 * Returns the smallest circle that encloses all the given points. Runs in expected O(n) time, randomized.
		 * Note: If 0 points are given, a circle of radius -1 is returned. If 1 point is given, a circle of radius 0 is returned.
		 */
		// Initially: No boundary points known
		public static Circle MakeCircle(IList<Point> points)
		{
			// Clone list to preserve the caller's data, do Durstenfeld shuffle
			List<Point> shuffled = new List<Point>(points);
			Random rand = new Random();
			for (int i = shuffled.Count - 1; i > 0; i--)
			{
				int j = rand.Next(i + 1);
				Point temp = shuffled[i];
				shuffled[i] = shuffled[j];
				shuffled[j] = temp;
			}

			// Progressively add points to circle or recompute circle
			Circle c = Circle.INVALID;
			for (int i = 0; i < shuffled.Count; i++)
			{
				Point p = shuffled[i];
				if (c.r < 0 || !c.Contains(p))
					c = MakeCircleOnePoint(shuffled.GetRange(0, i + 1), p);
			}
			return c;
		}


		// One boundary point known
		private static Circle MakeCircleOnePoint(List<Point> points, Point p)
		{
			Circle c = new Circle(p, 0);
			for (int i = 0; i < points.Count; i++)
			{
				Point q = points[i];
				if (!c.Contains(q))
				{
					if (c.r == 0)
						c = MakeDiameter(p, q);
					else
						c = MakeCircleTwoPoints(points.GetRange(0, i + 1), p, q);
				}
			}
			return c;
		}


		// Two boundary points known
		private static Circle MakeCircleTwoPoints(List<Point> points, Point p, Point q)
		{
			Circle circ = MakeDiameter(p, q);
			Circle left = Circle.INVALID;
			Circle right = Circle.INVALID;

			// For each point not in the two-point circle
			Point pq = q.Subtract(p);
			foreach (Point r in points)
			{
				if (circ.Contains(r))
					continue;

				// Form a circumcircle and classify it on left or right side
				double cross = pq.Cross(r.Subtract(p));
				Circle c = MakeCircumcircle(p, q, r);
				if (c.r < 0)
					continue;
				else if (cross > 0 && (left.r < 0 || pq.Cross(c.c.Subtract(p)) > pq.Cross(left.c.Subtract(p))))
					left = c;
				else if (cross < 0 && (right.r < 0 || pq.Cross(c.c.Subtract(p)) < pq.Cross(right.c.Subtract(p))))
					right = c;
			}

			// Select which circle to return
			if (left.r < 0 && right.r < 0)
				return circ;
			else if (left.r < 0)
				return right;
			else if (right.r < 0)
				return left;
			else
				return left.r <= right.r ? left : right;
		}


		public static Circle MakeDiameter(Point a, Point b)
		{
			Point c = new Point((a.x + b.x) / 2, (a.y + b.y) / 2);
			return new Circle(c, Math.Max(c.Distance(a), c.Distance(b)));
		}


		public static Circle MakeCircumcircle(Point a, Point b, Point c)
		{
			// Mathematical algorithm from Wikipedia: Circumscribed circle
			double ox = (Math.Min(Math.Min(a.x, b.x), c.x) + Math.Max(Math.Max(a.x, b.x), c.x)) / 2;
			double oy = (Math.Min(Math.Min(a.y, b.y), c.y) + Math.Max(Math.Max(a.y, b.y), c.y)) / 2;
			double ax = a.x - ox, ay = a.y - oy;
			double bx = b.x - ox, by = b.y - oy;
			double cx = c.x - ox, cy = c.y - oy;
			double d = (ax * (by - cy) + bx * (cy - ay) + cx * (ay - by)) * 2;
			if (d == 0)
				return Circle.INVALID;
			double x = ((ax * ax + ay * ay) * (by - cy) + (bx * bx + by * by) * (cy - ay) + (cx * cx + cy * cy) * (ay - by)) / d;
			double y = ((ax * ax + ay * ay) * (cx - bx) + (bx * bx + by * by) * (ax - cx) + (cx * cx + cy * cy) * (bx - ax)) / d;
			Point p = new Point(ox + x, oy + y);
			double r = Math.Max(Math.Max(p.Distance(a), p.Distance(b)), p.Distance(c));
			return new Circle(p, r);
		}

	}
		public Circle(List<Point> ps) { 
			Circle c = SmallestEnclosingCircle.MakeCircle(ps);
			this.c = c.c;
			this.r = c.r;
		}
	}
}