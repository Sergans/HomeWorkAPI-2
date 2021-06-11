using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAPI_2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentInfo _agent;
        AgentsController(AgentInfo agent)
        {
            _agent = agent;
        }
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
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
        [HttpGet("getlist")]
        public IActionResult GetListAgents()
        {
            return Ok(_agent.AgentId);
        }

    }
    

}
