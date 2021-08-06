using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Agents.Model;
using Dapper;
using System.Data.SQLite;
using TaskAPI_2_1.IConectionManager;
using TaskAPI_2_1.Responses;



namespace TaskAPI_2_1.DAL.Repository
{
   public interface IRepository<T>where T:class
   {
      IList<T> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
      IList<T> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
      DateTimeOffset GetMaxDateTime();
        void Create(T item);
   }
    public interface IAgentCpuMetric : IRepository<CpuAgent>
    {
        
    }
    public class AgentCpuMetric:IAgentCpuMetric
    {
        public AgentCpuMetric()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        IConectionOpen connectionstring = new ConectionOpen();
        public IList<CpuAgent> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<CpuAgent>("SELECT id,agentId,value,time FROM cpuagentmetrics WHERE agentId=@agentId AND time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds(),
                agentId=agentId.ToString()
            }).ToList();
        }
        public IList<CpuAgent> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<CpuAgent>("SELECT id,agentId,value,time FROM cpuagentmetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds(),
                
            }).ToList();
        }
        public DateTimeOffset GetMaxDateTime()
        {   
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            //var MaxDate =connection.Query<DateTimeOffset>("SELECT MAX(time) as max FROM cpuagentmetrics").Max();
            
            return connection.Query<DateTimeOffset>("SELECT time FROM cpuagentmetrics").Max();
        }
        public void Create(CpuAgent item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO cpuagentmetrics(agentId,value,time) VALUES(@agentId,@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentId = item.AgentId
                });
        }
    }
}
