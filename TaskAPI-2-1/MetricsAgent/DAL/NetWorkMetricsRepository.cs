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
    public class NetWorkMetricsRepository:INetWorkMetricsRepository
    {
        public NetWorkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new TimeSpanHandler());
        }


        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(NetWorkMetric item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                });
        }

        public IList<NetWorkMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<NetWorkMetric>("SELECT id,value,time FROM networkmetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds()
            }).ToList();
        }
    }
}
