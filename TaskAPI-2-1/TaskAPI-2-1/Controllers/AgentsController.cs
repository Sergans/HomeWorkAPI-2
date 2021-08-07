using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.IConectionManager;
using System.Data.SQLite;
using Dapper;

namespace TaskAPI_2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            IConectionOpen connectionstring = new ConectionOpen();
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO agents(agentId,AgentUrl) VALUES(@agentId,@AgentUrl)",
                new
                {
                    AgentUrl = agentInfo.AgentAddress.ToString(),
                    agentId = agentInfo.AgentId
                });
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }
        [HttpGet("registerlist")]
        public IActionResult GetListAgents()
        {
            return Ok();
        }

    }
    

}
