using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricsAgent.Model;
using MetricsAgent.Requests;
using MetricsAgent.Responses;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class AgentMetricsHddController : ControllerBase
    {
        private readonly ILogger<AgentMetricsHddController>_logger;
        private IHddMetricsRepository repository;

        public AgentMetricsHddController(ILogger<AgentMetricsHddController> logger, IHddMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            
            var metrics = repository.GetByTimePeriod();
            var periodmetrics = new List<HddMetric>();
            foreach (var metric in metrics)
            {
                if (metric.Time > fromTime && metric.Time < toTime)
                {
                    periodmetrics.Add(metric);
                }
            }
            return Ok(periodmetrics);
           
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation($"{request.Time},{request.Value}");
            repository.Create(new HddMetric
            {

                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
    }
}
