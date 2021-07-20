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
   public interface IRepository<T> where T : class
    {
        void Create(T item);
        
        IList<T> GetByTimePeriod(DateTimeOffset fromTime,DateTimeOffset toTime);

    }
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {

    }
    public interface INetWorkMetricsRepository : IRepository<NetWorkMetric>
    {

    }
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {

    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                }) ;
        }

        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<CpuMetric>("SELECT id,value,time FROM cpumetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds()
           }).ToList();

        }
    }
}
