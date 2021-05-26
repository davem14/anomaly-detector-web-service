using anomaly_detector_web_service.Models;
using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace anomaly_detector_web_service.Controllers
{
    [ApiController]
    [Route("")]
    public class AnomalyDetectorController : ControllerBase
    {

        private readonly ILogger<AnomalyDetectorController> _logger;

        public AnomalyDetectorController(ILogger<AnomalyDetectorController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<AnomaliesReport>> PostAnomalyFirst()
        {
            string model = Request.Form["anomalyModels"];
            var train = Request.Form.Files["trainFile"];
            var test = Request.Form.Files["testFile"];

            IAnomalyDetectorService anomalyDetectorService = new AnomalyDetectorService(model, train, test);
            ActionResult<AnomaliesReport> anomaly = await Task.Run(anomalyDetectorService.FindAnomaly);
            return anomaly;
        }

        [HttpPost("detect")]
        public async Task<ActionResult<AnomaliesReport>> PostAnomalySecond()
        {
            string model = Request.Form["anomalyModels"];
            var train = Request.Form.Files["trainFile"];
            var test = Request.Form.Files["testFile"];

            IAnomalyDetectorService anomalyDetectorService = new AnomalyDetectorService(model, train, test);
            ActionResult<AnomaliesReport> anomaly = await Task.Run(anomalyDetectorService.FindAnomaly);
            return anomaly;
        }
    }
}
