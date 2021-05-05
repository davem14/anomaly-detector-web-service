
using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Types
{
    public class Model
    {
        public int model_id { get; }
        public DateTime upload_time { get; }
        private string stat;
        public string status { get => stat; }
        public bool isRegression { get; }
        public List<CorrelatedFeatures> cf { get; }

        public Model(int model_id, string model_type, Data train_data)
        {
            this.model_id = model_id;
            this.upload_time = DateTime.Now;
            this.stat = "pending";
            AD ad = new AD(train_data);
            this.cf = ad.cf;
            isRegression = model_type.ToLower() == "regression";
        }



            void setReady() => this.stat = "ready";

        }
    }