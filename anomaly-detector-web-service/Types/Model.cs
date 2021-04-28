
using System;

namespace anomaly_detector_web_service.Types
{
    public class Model
    {
        public int model_id { get; }
        public DateTime upload_time { get; }
        private string stat;
        public string status { get => stat; }
        private string model_type { get; }

        public Model(int model_id, string model_type)
        {
            this.model_id = model_id;
            this.upload_time = DateTime.Now;
            this.stat = "pending";
        }

        public void setReady() => this.stat = "ready";
    }
}