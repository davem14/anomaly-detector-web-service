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
    public class AnomalyDetectorController : ControllerBase
    {
        [Route("")]
        [Route("detect")]
        [HttpPost]
        public async Task<ActionResult<AnomaliesReport>> PostAnomaly()
        {
            string model = Request.Form["anomalyModels"];
            IFormFile train = Request.Form.Files["trainFile"];
            IFormFile test = Request.Form.Files["testFile"];
            if (model == null || train == null || test == null)
                return BadRequest("Read params failed");

            IAnomalyDetectorService anomalyDetectorService = new AnomalyDetectorService(model, train, test);
            ActionResult<AnomaliesReport> anomaly = await Task.Run(anomalyDetectorService.FindAnomaly);
            return anomaly;
        }
    }
}
