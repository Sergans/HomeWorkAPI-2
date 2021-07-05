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
    [Route("api/metrics/network")]
    [ApiController]
    public class AgentMetricsNetworkController : ControllerBase
    {
        private readonly ILogger<AgentMetricsNetworkController> _logger;
        private INetWorkMetricsRepository repository;
        public AgentMetricsNetworkController(ILogger<AgentMetricsNetworkController> logger, INetWorkMetricsRepository repository)
        {
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");

            var metrics = repository.GetByTimePeriod();
            var periodmetrics = new List<NetWorkMetric>();
            foreach (var metric in metrics)
            {
                if (metric.Time > fromTime && metric.Time < toTime)
                {
                    periodmetrics.Add(metric);
                }
            }
            return Ok(periodmetrics);
        }
    }
}
