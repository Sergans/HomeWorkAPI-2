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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class AgentMetricsDotNetController : ControllerBase
    {
        private IDotNetMetricsRepository repository;
        private readonly ILogger<AgentMetricsDotNetController> _logger;

        public AgentMetricsDotNetController(ILogger<AgentMetricsDotNetController> logger, IDotNetMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            var metrics = repository.GetByTimePeriod(fromTime, toTime);
            
            return Ok(metrics);
            
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            _logger.LogInformation($"{request.Time},{request.Value}");
            repository.Create(new DotNetMetric
            {

                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
    }
}
