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
using System.Net.Http.Headers;
using System.Net;
using Microsoft.Extensions.Http;
using TaskAPI_2_1.Responses;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using TaskAPI_2_1.Agents.Model;

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
        public IActionResult GetMetricsFromAllClusterAsync([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
           _logger.LogInformation($"{fromTime},{toTime}");
            var metrics = repository.GetAllMetricPeriod(fromTime, toTime);
            return Ok(metrics);
        }
        [HttpGet("get")]
        public IActionResult Get()
        {
            var agents = repository.GetAgentAdress();
            foreach(var agent in agents)
            {
                var request = new GetAllCpuMetricsApiRequest();
                request.ToTime = DateTimeOffset.Now;
                request.FromTime = repository.GetMaxDateTime(agent.AgentId);
                request.ClientBaseAddress = agent.AgentUrl;
                
                var client = new HttpClient();
                var response = new MetricsAgentClient(client);
                var metrics = response.GetAllCpuMetrics(request);
                if (metrics != null)
                {
                    foreach (var metric in metrics.Metrics)
                    {
                        repository.Create(metric, agent.AgentId);
                    }
                }
               
            }
            
            return Ok();
        }
        
    }
}
