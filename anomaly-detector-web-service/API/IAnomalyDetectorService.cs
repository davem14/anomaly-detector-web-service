using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace anomaly_detector_web_service.Models
{
    public interface IAnomalyDetectorService
    {
        public ActionResult<AnomaliesReport> FindAnomaly();
        public ActionResult<AnomaliesReport> Detect(List<string> train, List<string> test);
    }
}