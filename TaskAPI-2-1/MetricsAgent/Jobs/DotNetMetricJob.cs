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
    public class DotNetMetricJob:IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _DotNetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _DotNetCounter = new PerformanceCounter("ASP.NET", "Error Events Raised");

        }

        public Task Execute(IJobExecutionContext context)
        {
            // теперь можно записать что-то при помощи репозитория
            var DotNetInPercents = Convert.ToInt32(_DotNetCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new Model.DotNetMetric { Time = time, Value = DotNetInPercents });

            return Task.CompletedTask;
        }
    }
}
