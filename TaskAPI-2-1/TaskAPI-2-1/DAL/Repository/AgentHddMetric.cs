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
    public class AgentHddMetric : IAgentHddMetric
    {
        public AgentHddMetric()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(HddAgent item, int id)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO hddagentmetrics(agentId,value,time) VALUES(@agentId,@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds(),
                    agentId = id
                });
        }

        public List<AgentInfo> GetAgentAdress()
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<AgentInfo>("SELECT * FROM agents").ToList();
        }

        public IList<HddAgent> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<HddAgent>("SELECT id,agentId,value,time FROM hddagentmetrics WHERE agentId=@agentId AND time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds(),
                agentId = agentId.ToString()
            }).ToList();
        }

        public IList<HddAgent> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<HddAgent>("SELECT id,agentId,value,time FROM hddagentmetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds(),

            }).ToList();
        }

        public DateTimeOffset GetMaxDateTime(int id)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());

            return connection.Query<DateTimeOffset>("SELECT time FROM hddagentmetrics WHERE agentId=@id", new { id = id }).Max();
        }
    }
}
