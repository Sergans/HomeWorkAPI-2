using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskAPI_2_1.DAL.Repository;
using System.Net.Http;
using TaskAPI_2_1.Client;
using TaskAPI_2_1.Request;

namespace TaskAPI_2_1.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IAgentCpuMetric repository;
        private readonly IMetricsAgentClient client;
        public CpuMetricsController(ILogger<CpuMetricsController> logger, IAgentCpuMetric repository,IMetricsAgentClient client)
        {
            this.repository = repository;
            this.client = client;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }
        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute]int agentId,[FromRoute]DateTimeOffset fromTime,[FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{agentId},{fromTime},{toTime}");
            var metrics = repository.GetAgentMetricPeriod(agentId, fromTime, toTime);
            return Ok(metrics);
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            //var metrics = repository.GetAllMetricPeriod(fromTime, toTime);
            var metrics = client.GetAllCpuMetrics(new GetAllCpuMetricsApiRequest { FromTime = fromTime, ToTime = toTime, ClientBaseAddress = "http://localhost:5000" });
            return Ok(metrics);
        }
       
    }
}
