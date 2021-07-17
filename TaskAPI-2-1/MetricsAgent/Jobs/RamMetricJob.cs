using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.DAL;
using Quartz;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using Microsoft.Extensions.Hosting;
using System.Threading;
using MetricsAgent.Model;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob:IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;
        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }
        public Task Execute(IJobExecutionContext context)
        {
            // теперь можно записать что-то при помощи репозитория
            var ramUsage = Convert.ToInt32(_ramCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new Model.RamMetric { Time = time, Value = ramUsage });

            return Task.CompletedTask;
        }

    }
    
}
