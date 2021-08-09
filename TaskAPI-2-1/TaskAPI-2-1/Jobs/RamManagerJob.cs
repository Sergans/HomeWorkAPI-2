using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.DAL.Repository;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using TaskAPI_2_1.Client;
using TaskAPI_2_1.Request;
using Quartz;
using Quartz.Spi;

namespace TaskAPI_2_1.Jobs
{
    public class RamManagerJob : IJob
    {
        private IAgentRamMetric _repository;
        // private readonly ILogger _logger;
        public RamManagerJob(IAgentRamMetric repository)
        {
            // _logger = logger;
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var agents = _repository.GetAgentAdress();
            foreach (var agent in agents)
            {
                var request = new GetAllRamMetricsApiRequest();
                request.ToTime = DateTimeOffset.Now;
                request.FromTime = _repository.GetMaxDateTime(agent.AgentId);
                request.ClientBaseAddress = agent.AgentUrl;

                var client = new HttpClient();
                var response = new MetricsAgentClient(client);
                var metrics = response.GetAllRamMetrics(request);
                if (metrics != null)
                {
                    foreach (var metric in metrics.Metrics)
                    {
                        _repository.Create(metric, agent.AgentId);
                    }
                }

            }
            return Task.CompletedTask;
        }
    }
}
