using System;
using Xunit;
using TaskAPI_2_1.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MetricsManagerTest
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
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
      }
        public class RamMetricsControllerUnitTests
    {
        private RamMetricsController controller;
        public RamMetricsControllerUnitTests()
        {
            controller = new RamMetricsController();
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
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
        public class HddMetricsControllerUnitTest
    {
        private HddMetricsController controller;
        public HddMetricsControllerUnitTest()
        {
            controller = new HddMetricsController();
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
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
        public class NetworkMetricsControllerUnitTest
    {
        private NetworkMetricsController controller;
        public NetworkMetricsControllerUnitTest()
        {
            controller = new NetworkMetricsController();
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
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
        public class DotNetMetricsControllerUnitTest
    {
        private DotNetMetricsController controller;
        public DotNetMetricsControllerUnitTest()
        {
            controller = new DotNetMetricsController();
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
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    
    
}
