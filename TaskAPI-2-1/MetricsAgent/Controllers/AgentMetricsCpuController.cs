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
using AutoMapper;


namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class AgentMetricsCpuController : ControllerBase
    {
        private readonly ILogger<AgentMetricsCpuController> _logger;
        private ICpuMetricsRepository repository;
        private readonly IMapper mapper;

        public AgentMetricsCpuController(ICpuMetricsRepository repository, ILogger<AgentMetricsCpuController> logger,IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
          
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");
            var metrics = repository.GetByTimePeriod(fromTime,toTime);
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
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
