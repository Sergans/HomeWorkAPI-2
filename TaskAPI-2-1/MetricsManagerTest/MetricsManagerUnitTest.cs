using System;
using Xunit;
using TaskAPI_2_1.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MetricsManagerTest
{
    
    
        
        public class CpuMetricsControllerUnitTests

      {
            private CpuMetricsController controller;
            private Mock<ILogger<CpuMetricsController>> mocklog;
        public CpuMetricsControllerUnitTests()
            {
            mocklog = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(mocklog.Object);
            }
            [Fact]
            public void GetMetricsFromAgent_ReturnsOk()
            {
                var agentId = 1;
                var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
                var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
                var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
                _ = Assert.IsAssignableFrom<IActionResult>(result);

            }
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
      }
        public class RamMetricsControllerUnitTests
    {
        private Mock<ILogger<RamMetricsController>> mocklog;
        private RamMetricsController controller;
        public RamMetricsControllerUnitTests()
        {
            mocklog = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(mocklog.Object);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
        public class HddMetricsControllerUnitTest
    {
        private Mock<ILogger<HddMetricsController>> mocklog;
        private HddMetricsController controller;
        public HddMetricsControllerUnitTest()
        {
            mocklog = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mocklog.Object);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
        public class NetworkMetricsControllerUnitTest
    {
        private Mock<ILogger<NetworkMetricsController>> mocklog;
        private NetworkMetricsController controller;
        public NetworkMetricsControllerUnitTest()
        {
            mocklog = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(mocklog.Object);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
        public class DotNetMetricsControllerUnitTest
    {
        private Mock<ILogger<DotNetMetricsController>> mocklog;
        private DotNetMetricsController controller;
        public DotNetMetricsControllerUnitTest()
        {
            mocklog = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mocklog.Object);
        }
        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);

        }
        [Fact]
        public void GetMetricsFromAllCluster_ReturnOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }

    
    
}
