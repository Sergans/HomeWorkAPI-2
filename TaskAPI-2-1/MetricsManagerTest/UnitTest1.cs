using System;
using Xunit;
using TaskAPI_2_1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTest
{
    public class UnitTest1
    {
        
        public class CpuMetricsControllerUnitTests

        {
            private CpuMetricsController controller;
            public CpuMetricsControllerUnitTests()
            {
                controller = new CpuMetricsController();
            }
            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                var agentId = 1;
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);
                var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
                _ = Assert.IsAssignableFrom<IActionResult>(result);

            }


        }
    }
}
