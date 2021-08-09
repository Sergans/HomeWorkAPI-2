using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Spi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using TaskAPI_2_1.DAL.Repository;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using TaskAPI_2_1.Client;
using TaskAPI_2_1.Request;

namespace TaskAPI_2_1.Jobs
{
    public class CpuManagerJob : IJob
    {
        private IAgentCpuMetric _repository;
       // private readonly ILogger _logger;
        public CpuManagerJob(IAgentCpuMetric repository)
        {
           // _logger = logger;
            _repository = repository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var agents = _repository.GetAgentAdress();
            foreach (var agent in agents)
            {
                var request = new GetAllCpuMetricsApiRequest();
                request.ToTime = DateTimeOffset.Now;
                request.FromTime = _repository.GetMaxDateTime(agent.AgentId);
                request.ClientBaseAddress = agent.AgentUrl;

                var client = new HttpClient();
                var response = new MetricsAgentClient(client);
                var metrics = response.GetAllCpuMetrics(request);
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
    public class SingletonJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            
        }
    }
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType;
            CronExpression = cronExpression;
        }

        public Type JobType { get; }
        public string CronExpression { get; }
    }
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;
        public QuartzHostedService(
        IAgentCpuMetric repository, IAgentDotNetMetric repositorydotnet, 
        IAgentNetWorkMetric repositorynetwork, IAgentHddMetric repositoryhdd, IAgentRamMetric repositoryram,
        ISchedulerFactory schedulerFactory,
        IJobFactory jobFactory,
        IEnumerable<JobSchedule> jobSchedules)
        {
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJobDetail(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
        private static IJobDetail CreateJobDetail(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }
        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
            .Create()
            .WithIdentity($"{schedule.JobType.FullName}.trigger")
            .WithCronSchedule(schedule.CronExpression)
            .WithDescription(schedule.CronExpression)
            .Build();
        }
    }

}
