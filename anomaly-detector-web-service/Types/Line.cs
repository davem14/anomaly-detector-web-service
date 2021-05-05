using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Types
{
    public class Line
    {
        public double a { get; set; }
        public double b { get; set; }
        public Line(double a, double b) { this.a = a; this.b=b;}
    }
}