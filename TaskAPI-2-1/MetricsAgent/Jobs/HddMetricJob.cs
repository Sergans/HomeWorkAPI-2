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
    public class HddMetricJob:IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _HddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _HddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

        }

        public Task Execute(IJobExecutionContext context)
        {
            // теперь можно записать что-то при помощи репозитория
            var HddInPercents = Convert.ToInt32(_HddCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new Model.HddMetric { Time = time, Value = HddInPercents });

            return Task.CompletedTask;
        }
    }
}
