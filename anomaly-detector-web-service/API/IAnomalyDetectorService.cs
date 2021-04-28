using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Models
{
    public interface IAnomalyDetectorService
    {
        public Model CreateModel(string model_type, Data train_data);
        public Model GetModel(int id);
        public bool DeleteModel(int id);
        public IEnumerable<Model> GetAllModels();
        public Anomaly Detect(int model_id, Data predict_data);
    }
}
