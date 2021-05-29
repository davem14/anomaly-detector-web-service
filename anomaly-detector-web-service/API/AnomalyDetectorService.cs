using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace anomaly_detector_web_service.Models
{
    public class AnomalyDetectorService : IAnomalyDetectorService
    {
        public List<CorrelatedFeatures> cf;
        
        string model_type;
        IFormFile train_file;
        IFormFile test_file;


        public AnomalyDetectorService(string model_type, IFormFile train_file, IFormFile test_file)
        {
            this.model_type = model_type;
            this.train_file = train_file;
            this.test_file = test_file;
        }

        public ActionResult<AnomaliesReport> FindAnomaly()
        {
            List<string> train = new List<string>();

            using (var reader = new StreamReader(train_file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    train.Add(reader.ReadLine());
                }
            }

            List<string> test = new List<string>();
            using (var reader = new StreamReader(test_file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    test.Add(reader.ReadLine());
                }
            }

            return Detect(train, test);
        }


        public ActionResult<AnomaliesReport> Detect(List<string> train, List<string> test)
        {
            Dictionary<string, List<double>> train_data = new Dictionary<string, List<double>>();

            try
            {
                string[] featuresTrain = train[0].Split(',');
                foreach (string i in featuresTrain)
                {
                    if (String.Compare("", i) != 0)
                        train_data.Add(i, new List<double>());
                }
                for (int i = 1; i < train.Count; i++)
                {
                    double[] values = Array.ConvertAll(train[i].Split(','), double.Parse);
                    for (int j = 0; j < values.Length; j++)
                    {
                        List<double> list;
                        train_data.TryGetValue(featuresTrain[j], out list);
                        list.Add(values[j]);
                    }
                }

            }
            catch
            {
                return null;
            }

            Dictionary<string, List<double>> test_data = new Dictionary<string, List<double>>();

            try
            {
                string[] featuresTest = test[0].Split(',');
                foreach (string i in featuresTest)
                {
                    test_data.Add(i, new List<double>());
                }
                for (int i = 1; i < test.Count; i++)
                {
                    double[] values = Array.ConvertAll(test[i].Split(','), double.Parse);
                    for (int j = 0; j < values.Length; j++)
                    {
                        List<double> list;
                        test_data.TryGetValue(featuresTest[j], out list);
                        list.Add(values[j]);
                    }
                }
            }
            catch
            {
                return null;
            }

            AnomaliesReport ar = null;

            try
            {
                // Learn
                AD ad1 = new AD(new Data(train_data));
                this.cf = ad1.cf;

                // Anomaly-Detector
                if (this.model_type == null)
                {
                    return null;
                }

                AD ad2 = new AD(model_type.ToLower() == "regression");
                ar = ad2.detect(new Data(test_data), this.cf);

            }
            catch
            {
                return null;
            }

            return ar;
        }
    }
}
