using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Agents.Model;
using Dapper;
using System.Data.SQLite;
using TaskAPI_2_1.IConectionManager;
using TaskAPI_2_1.Responses;
using TaskAPI_2_1;




namespace TaskAPI_2_1.DAL.Repository
{
   public interface IRepository<T>where T:class
   {
      IList<T> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
      IList<T> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime);
      DateTimeOffset GetMaxDateTime(int id);
      void Create(T item,int id);
      List<AgentInfo> GetAgentAdress();
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
        public DateTimeOffset GetMaxDateTime(int id)
        {   
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
           
            return connection.Query<DateTimeOffset>("SELECT time FROM cpuagentmetrics WHERE agentId=@id",new { id = id }).Max();

        }
        public void Create(CpuAgent item,int id)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO cpuagentmetrics(agentId,value,time) VALUES(@agentId,@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentId = id
                }) ;
        }
        public List<AgentInfo> GetAgentAdress()
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<AgentInfo>("SELECT * FROM agents").ToList();
        }
    }
}
