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
                   // ��������� ��������� SQLite 
                   .AddSQLite()
                   // ������������� ������ �����������
                   .WithGlobalConnectionString(connectionString)
                   // ������������ ��� ������ ������ � ����������
                   .ScanIn(typeof(Startup).Assembly).For.Migrations()
               ).AddLogging(lb => lb
                   .AddFluentMigratorConsole());
            services.AddControllers();
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
