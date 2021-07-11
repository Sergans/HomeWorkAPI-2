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
    public class RamMetricsRepository:IRamMetricsRepository
    {
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }

        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }
        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<RamMetric>("SELECT id,value,time FROM rammetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds()
            }).ToList();
        }

    }
}
