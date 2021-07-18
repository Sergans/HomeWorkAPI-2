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
    public class NetWorkMetricJob:IJob
    {
        private INetWorkMetricsRepository _repository;
        private PerformanceCounter _NetWorkCounter;

        public NetWorkMetricJob(INetWorkMetricsRepository repository)
        {
            _repository = repository;
            _NetWorkCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", "Realtek RTL8822BE 802.11ac PCIe Adapter");

        }

        public Task Execute(IJobExecutionContext context)
        {
            // теперь можно записать что-то при помощи репозитория
            var NetWorkInPercents = Convert.ToInt32(_NetWorkCounter.NextValue());
            var time = DateTimeOffset.Now;
            _repository.Create(new Model.NetWorkMetric { Time = time, Value = NetWorkInPercents });

            return Task.CompletedTask;
        }
    }
}
