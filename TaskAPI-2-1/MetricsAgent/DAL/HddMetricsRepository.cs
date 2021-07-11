using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;
using System.Data.SQLite;
using MetricsAgent.IConectionManager;
using Dapper;


namespace MetricsAgent.DAL
{
    
    
        public class HddMetricsRepository: IHddMetricsRepository
        {
        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

            public IList<HddMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<HddMetric>("SELECT id,value,time FROM hddmetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds()
            }).ToList();
        }

        }
    }

