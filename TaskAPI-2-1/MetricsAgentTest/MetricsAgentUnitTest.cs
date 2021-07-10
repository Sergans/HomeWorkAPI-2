using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;

namespace MetricsAgentTest
{
    public class AgentMetricsCpuUnitTest
    {
        private AgentMetricsCpuController controller;
        public AgentMetricsCpuUnitTest()
        {
            controller = new AgentMetricsCpuController();
        }

        [Fact]
        public void AgentMetricsCpu_ResultOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsCpu( fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
    public class AgentMetricsRamUnitTest
    {
        private AgentMetricsRamController controller;
        public AgentMetricsRamUnitTest()
        {
            controller = new AgentMetricsRamController();
        }
        [Fact]
        public void AgentMetricsRam_ResultOk()
        {
           
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);
                var result = controller.GetMetricsRam(fromTime, toTime);
                _ = Assert.IsAssignableFrom<IActionResult>(result);
  
        }
       
    }
    public class AgentMetricsDotNetUnitTest
    {
        private AgentMetricsDotNetController controller;
        public AgentMetricsDotNetUnitTest()
        {
            controller = new AgentMetricsDotNetController();
        }
        [Fact]
        public void AgentMetricsDotNet_ResultOk()
        {

            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsDotNet(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
    public class AgentMetricsNetWorkUnitTest
    {
        private AgentMetricsNetworkController controller;
        public AgentMetricsNetWorkUnitTest()
        {
            controller = new AgentMetricsNetworkController();
        }
        [Fact]
        public void AgentMetricsNetWork_ResultOk()
        {

            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsNetWork(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
    }
}
