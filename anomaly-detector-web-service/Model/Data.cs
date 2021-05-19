using System;
using System.Collections.Generic;
using System.Linq;

namespace anomaly_detector_web_service.Types
{
    public class Data
    {
        public Dictionary<string, List<double>> ts { get; set; }
        public int numOfLines { get; set; }
        public Data()
        {
            ts = new Dictionary<string, List<double>>();
            numOfLines = 0;
        }
        public Data(Dictionary<string, List<double>> d)
        {
            ts = d;
            numOfLines = d[ts.ElementAt(0).Key].Count;
        }
    }
}