using System;
namespace anomaly_detector_web_service.Types
{
     public class CorrelatedFeatures : IEquatable<CorrelatedFeatures>
    {
        // names of the correlated features
        public string feature1 { get; set; }
        public string feature2 { get; set; }
        public double correlation { get; set; }
        public Line lin_reg { get; set; }
        // MEC's radius OR max deviation allowed from linear regression line
        public double threshold { get; set; }
        public Circle MEC { get; set; }
        public CorrelatedFeatures() { }
        public CorrelatedFeatures(CorrelatedFeatures c)
        {
            this.feature1 = c.feature1;
            this.feature2 = c.feature2;
            this.correlation = c.correlation;
            this.threshold = c.threshold;
            this.MEC = new Circle(new Point(c.MEC.c.x, c.MEC.c.y), c.MEC.r);
            this.lin_reg = new Line(c.lin_reg.a, c.lin_reg.b);
        }
        public bool Equals(CorrelatedFeatures c) { return c.feature1 == this.feature2 || this.feature1 == c.feature2 || this.feature1 == c.feature1; }
     }
}