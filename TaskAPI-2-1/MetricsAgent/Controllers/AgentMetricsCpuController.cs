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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class AgentMetricsCpuController : ControllerBase
    {
        private readonly ILogger<AgentMetricsCpuController> _logger;
        private ICpuMetricsRepository repository;

        public AgentMetricsCpuController(ICpuMetricsRepository repository, ILogger<AgentMetricsCpuController> logger)
        {
            this.repository = repository;
            
            
                _logger = logger;
                _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
           
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            var metrics = repository.GetByTimePeriod(fromTime,toTime);
            //var periodmetrics = new List<CpuMetric>();
            //foreach (var metric in metrics)
            //{
            //    if (metric.Time > fromTime && metric.Time < toTime)
            //    {
            //        periodmetrics.Add(metric);
            //    }
            //}
            return Ok(metrics);
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation($"{request.Time},{request.Value}");
            repository.Create(new CpuMetric
            {

                Time = request.Time,
                Value = request.Value
            });

            return Ok();
        }
       
       




    }
}
