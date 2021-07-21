using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Agents.Model;
using Dapper;
using System.Data.SQLite;
using TaskAPI_2_1.IConectionManager;



namespace TaskAPI_2_1.DAL.Repository
{
   public interface IRepository<T>where T:class
   {
      IList<T> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime);
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
    }
}
