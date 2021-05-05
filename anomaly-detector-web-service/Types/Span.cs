using System;

namespace anomaly_detector_web_service.Types
{
    public class Span
    {
        public int start { get; set; }
        public int end { get; set; }
        public string withFeature { get; set; }
        public Span(int start, int end, string withFeature)
        {
            this.end = end;
            this.start = start;
            this.withFeature = withFeature;
        }
    }
}