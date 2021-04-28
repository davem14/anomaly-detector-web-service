using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Models
{
    public class AnomalyDetectorService : IAnomalyDetectorService
    {
        private int id;
        private IDictionary<int, Model> models;

        public AnomalyDetectorService()
        {
            id = 1;
            models = new Dictionary<int, Model>();
        }

        public Model CreateModel(string model_type, Data train_data)
        {
            try
            {
                while (models.ContainsKey(id))
                    id++;
                models.Add(id, new Model(id, model_type));
                return models[id];
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteModel(int id)
        {
            return models.Remove(id);
        }

        public Model GetModel(int id)
        {
            Model m;
            if (models.TryGetValue(id, out m))
                return m;
            else
                return null;
        }

        public IEnumerable<Model> GetAllModels()
        {
            return models.Values;
        }

        public Anomaly Detect(int model_id, Data predict_data)
        {
            return new Anomaly();
        }
    }
}
