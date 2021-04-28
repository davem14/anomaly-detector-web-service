using anomaly_detector_web_service.Models;
using anomaly_detector_web_service.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anomaly_detector_web_service.Controllers
{
    [ApiController]
    [Route("api")]
    public class AnomalyDetectorController : ControllerBase
    {
        private static IAnomalyDetectorService service = new AnomalyDetectorService();

        private readonly ILogger<AnomalyDetectorController> _logger;

        public AnomalyDetectorController(ILogger<AnomalyDetectorController> logger)
        {
            _logger = logger;
        }

        [HttpPost("model")]
        public ActionResult<Model> CreateModel([FromQuery] string model_type, [FromBody] Data train_data)
        {
            return Ok(service.CreateModel(model_type, train_data));
        }

        [HttpGet("model")]
        public ActionResult<Model> GetModel([FromQuery] int model_id)
        {
            return service.GetModel(model_id);
        }

        [HttpDelete("model")]
        public ActionResult DeleteModel([FromQuery] int model_id)
        {
            if (service.DeleteModel(model_id))
                return Ok();
            else
                return NotFound();
        }

        [HttpGet("models")]
        public IEnumerable<Model> GetAllModels()
        {
            return service.GetAllModels();
        }

        [HttpPost("anomaly")]
        public ActionResult<Anomaly> Detect([FromQuery] int model_id, [FromBody] Data predict_data)
        {
            return service.Detect(model_id, predict_data);
        }
    }
}
