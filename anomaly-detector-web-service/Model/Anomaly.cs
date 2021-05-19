using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Types
{
    public class Anomaly
    {
        public string f1 { get; set; }
        public string f2 { get; set; }
        public int timestep { get; set; }
        public Anomaly(string f1, string f2, int i) { this.f1 = f1;this.f2 = f2; timestep = i; }
    }
}