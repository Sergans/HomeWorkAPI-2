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
    public class AgentRamMetric : IAgentRamMetric
    {
        public void Create(RamAgent item, int id)
        {
            throw new NotImplementedException();
        }

        public List<AgentInfo> GetAgentAdress()
        {
            throw new NotImplementedException();
        }

        public IList<RamAgent> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public IList<RamAgent> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset GetMaxDateTime(int id)
        {
            throw new NotImplementedException();
        }
    }
}
