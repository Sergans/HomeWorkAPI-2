﻿using System;
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
        public AgentRamMetric()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }
        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(RamAgent item, int id)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO ramagentmetrics(agentId,value,time) VALUES(@agentId,@value, @time)",
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

        public IList<RamAgent> GetAgentMetricPeriod(int agentId, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<RamAgent>("SELECT id,agentId,value,time FROM ramagentmetrics WHERE agentId=@agentId AND time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds(),
                agentId = agentId.ToString()
            }).ToList();
        }

        public IList<RamAgent> GetAllMetricPeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            return connection.Query<RamAgent>("SELECT id,agentId,value,time FROM ramagentmetrics WHERE time>@fromTime AND time<@toTime", new
            {
                fromTime = fromTime.ToUnixTimeSeconds(),
                toTime = toTime.ToUnixTimeSeconds(),

            }).ToList();
        }

        public DateTimeOffset GetMaxDateTime(int id)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());

            return connection.Query<DateTimeOffset>("SELECT time FROM ramagentmetrics WHERE agentId=@id", new { id = id }).Max();
        }
    }
}
