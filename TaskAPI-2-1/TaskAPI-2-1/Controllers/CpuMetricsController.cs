using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TaskAPI_2_1.DAL.Repository;
using System.Net.Http;

namespace TaskAPI_2_1.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IAgentCpuMetric repository;
        public CpuMetricsController(ILogger<CpuMetricsController> logger, IAgentCpuMetric repository)
        {
            this.repository = repository;
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
            var request = new HttpRequestMessage(HttpMethod.Get,
           "http://localhost:50343/api/cpumetrics/from/1/to/999999?var=val&var1=val1");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            var client = clientFactory.CreateClient();
            HttpResponseMessage response = client.SendAsync(request).Result;

            return Ok(metrics);
        }
       
    }
}
