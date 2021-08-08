using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Agents.Model;

namespace TaskAPI_2_1.DAL.Repository
{
    public class AgentNetWorkMetric : IAgentNetWorkMetric
    {
        public void Create(NetWorkAgent item, int id)
        {
            throw new NotImplementedException();
        }

        public List<AgentInfo> GetAgentAdress()
        {
            throw new NotImplementedException();
        }

        public IList<NetWorkAgent> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public IList<NetWorkAgent> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            throw new NotImplementedException();
        }

        public DateTimeOffset GetMaxDateTime(int id)
        {
            throw new NotImplementedException();
        }
    }
}
