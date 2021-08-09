using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using FluentMigrator.Runner;
using TaskAPI_2_1.DAL.Repository;
using TaskAPI_2_1.IConectionManager;
using TaskAPI_2_1.Client;
using TaskAPI_2_1.Request;
using TaskAPI_2_1.Responses;
using TaskAPI_2_1.Agents.Model;
using Polly;
using TaskAPI_2_1.Jobs;
using Quartz;
using Quartz.Spi;
using Quartz.Impl;





namespace TaskAPI_2_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private const string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSqlLiteConnection(services);
            services.AddFluentMigratorCore()
               .ConfigureRunner(rb => rb
                   // добавляем поддержку SQLite 
                   .AddSQLite()
                   // устанавливаем строку подключения
                   .WithGlobalConnectionString(connectionString)
                   // подсказываем где искать классы с миграциями
                   .ScanIn(typeof(Startup).Assembly).For.Migrations()
               ).AddLogging(lb => lb
                   .AddFluentMigratorConsole());
            services.AddControllers();
            services.AddHostedService<QuartzHostedService>();
            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000))); ;
            services.AddSingleton<CpuAgent>();
            services.AddSingleton<AgentInfo>();
            services.AddSingleton<IConectionOpen, ConectionOpen>();
            services.AddSingleton<IAgentCpuMetric, AgentCpuMetric>();
            services.AddSingleton<IAgentDotNetMetric, AgentDotNetMetric>();
            services.AddSingleton<IAgentNetWorkMetric, AgentNetWorkMetric>();
            services.AddSingleton<IAgentHddMetric, AgentHddMetric>();
            services.AddSingleton<IAgentRamMetric, AgentRamMetric>();
           // services.AddSingleton<GetAllCpuMetricsApiRequest>();
           // services.AddSingleton<AllCpuMetricsApiResponse>();
            // ДОбавляем сервисы
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<CpuManagerJob>();
            services.AddSingleton<DotNetManagerJob>();
            services.AddSingleton<NetWorkManagerJob>();
            services.AddSingleton<HddManagerJob>();
            services.AddSingleton<RamManagerJob>();
            services.AddSingleton(new JobSchedule(
            jobType: typeof(CpuManagerJob),
            cronExpression: "0/30 * * * * ?"));
            services.AddSingleton(new JobSchedule(
            jobType: typeof(DotNetManagerJob),
            cronExpression: "0/30 * * * * ?"));
            services.AddSingleton(new JobSchedule(
            jobType: typeof(NetWorkManagerJob),
            cronExpression: "0/30 * * * * ?"));
            services.AddSingleton(new JobSchedule(
            jobType: typeof(HddManagerJob),
            cronExpression: "0/30 * * * * ?"));
            services.AddSingleton(new JobSchedule(
            jobType: typeof(RamManagerJob),
            cronExpression: "0/30 * * * * ?"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskAPI_2_1", Version = "v1" });
            });
        }
        private void ConfigureSqlLiteConnection(IServiceCollection services)
        {
            //IConectionOpen connectionstring = new ConectionOpen();
            //string connectionString = connectionstring.GetOpenedConection();
            string connectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskAPI_2_1 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            migrationRunner.MigrateUp();
        }
    }
}
