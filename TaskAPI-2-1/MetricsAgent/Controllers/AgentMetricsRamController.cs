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
    [Route("api/metrics/ram")]
    [ApiController]
    public class AgentMetricsRamController : ControllerBase
    {
        private readonly ILogger<AgentMetricsRamController> _logger;
        private IRamMetricsRepository repository;
        public AgentMetricsRamController(ILogger<AgentMetricsRamController> logger, IRamMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");

            var metrics = repository.GetByTimePeriod();
            var periodmetrics = new List<RamMetric>();
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
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation($"{request.Time},{request.Value}");
            repository.Create(new RamMetric
            {

                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }

    }
}
