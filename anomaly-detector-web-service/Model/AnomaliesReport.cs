
using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Types
{
    public class AnomaliesReport
    {
        public Dictionary<string, List<Span>> reports{get; set;}
        // private string reason;
        public AnomaliesReport() { reports = new Dictionary<string, List<Span>>(); }
    }
}