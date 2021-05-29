using anomaly_detector_web_service.Models;
using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Http;
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
            string model = "";
            IFormFile train = null;
            IFormFile test = null;

            try
            {
                model = Request.Form["anomalyModels"];
                train = Request.Form.Files["trainFile"];
                test = Request.Form.Files["testFile"];
            }
            catch
            {
                Ok("{Incorrect field names}");
            }

            IAnomalyDetectorService anomalyDetectorService = new AnomalyDetectorService(model, train, test);
            ActionResult<AnomaliesReport> anomaly = await Task.Run(anomalyDetectorService.FindAnomaly);
            if (anomaly == null)
                return Ok("{Incorrect field names or Invalid files}");

            return anomaly;
        }

        [HttpPost("detect")]
        public async Task<ActionResult<AnomaliesReport>> PostAnomalySecond()
        {
            string model = "";
            IFormFile train = null;
            IFormFile test = null;

            try
            {
                model = Request.Form["anomalyModels"];
                train = Request.Form.Files["trainFile"];
                test = Request.Form.Files["testFile"];
            }
            catch
            {
                Ok("{Incorrect field names}");
            }

            IAnomalyDetectorService anomalyDetectorService = new AnomalyDetectorService(model, train, test);
            ActionResult<AnomaliesReport> anomaly = await Task.Run(anomalyDetectorService.FindAnomaly);
            if (anomaly == null)
                return Ok("{Incorrect field names or Invalid files}");

            return anomaly;
        }
    }
}
