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
    [Route("api/metrics/ram")]
    [ApiController]
    public class AgentMetricsRamController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<AgentMetricsRamController> _logger;
        private IRamMetricsRepository repository;
        public AgentMetricsRamController(ILogger<AgentMetricsRamController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"{fromTime},{toTime}");

            var metrics = repository.GetByTimePeriod(fromTime, toTime);
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<RamMetricDto>(metric));
            }

            return Ok(response);
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
