using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace anomaly_detector_web_service.Models
{
    
    public class AnomalyDetectorService : IAnomalyDetectorService
    {
        private int id;
        private IDictionary<int, Model> DB;

        public AnomalyDetectorService()
        {
            id = 1;
            DB = new Dictionary<int, Model>();
        }

        public Model CreateModel(string model_type, Data train_data)
        {
            try
            {
                while (DB.ContainsKey(id))
                    id++;
                Model m = new Model(id, model_type, train_data);
                DB.Add(id, m);  // add to Data-Base
                return m;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteModel(int id)
        {
            return DB.Remove(id);
        }

        public Model GetModel(int id)
        {
            Model m;
            if (DB.TryGetValue(id, out m))
                return m;
            else
                return null;
        }

        public IEnumerable<Model> GetAllModels()
        {
            return DB.Values;
        }

        public Anomaly Detect(int model_id, Data predict_data)
        {
            // Data-Base
            bool modelType = DB[model_id].isRegression;
            List<CorrelatedFeatures> acf = DB[model_id].cf;

            // Anomaly-Detector
            AD ad = new AD(modelType);
            AnomaliesReport ar = ad.detect(predict_data, acf);
            return new Anomaly("A", "B", 12);
        }
    }
}
