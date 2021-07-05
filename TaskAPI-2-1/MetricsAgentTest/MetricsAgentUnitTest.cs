using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Model;
using Microsoft.Extensions.Logging;

namespace MetricsAgentTest
{
    public class AgentMetricsCpuUnitTest
    {
        private AgentMetricsCpuController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<AgentMetricsCpuController>> mocklog;
        public AgentMetricsCpuUnitTest()
        {
            mock = new Mock<ICpuMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsCpuController>>();

            controller = new AgentMetricsCpuController(mock.Object,mocklog.Object);
        }

        //[Fact]
        //public void AgentMetricsCpu_ResultOk()
        //{
        //    var fromTime = DateTimeOffset.FromUnixTimeSeconds(1);
        //    var toTime = DateTimeOffset.FromUnixTimeSeconds(100);
        //    var result = controller.GetMetricsFromAgent(fromTime, toTime);
        //    _ = Assert.IsAssignableFrom<IActionResult>(result);

        //}
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.CpuMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }

    }
    public class AgentMetricsRamUnitTest
    {
        private Mock<ILogger<AgentMetricsRamController>> mock;
        private AgentMetricsRamController controller;
        public AgentMetricsRamUnitTest()
        {
           mock = new Mock<ILogger<AgentMetricsRamController>>();
           controller = new AgentMetricsRamController(mock.Object);
        }
        [Fact]
        public void AgentMetricsRam_ResultOk()
        {
           
                var fromTime = TimeSpan.FromSeconds(0);
                var toTime = TimeSpan.FromSeconds(100);
                var result = controller.GetMetricsFromAgent(fromTime, toTime);
                _ = Assert.IsAssignableFrom<IActionResult>(result);
  
        }
       
    }
    //public class AgentMetricsDotNetUnitTest
    //{
    //    private AgentMetricsDotNetController controller;
    //    public AgentMetricsDotNetUnitTest()
    //    {
    //        controller = new AgentMetricsDotNetController();
    //    }
    //    [Fact]
    //    public void AgentMetricsDotNet_ResultOk()
    //    {

    //        var fromTime = TimeSpan.FromSeconds(0);
    //        var toTime = TimeSpan.FromSeconds(100);
    //        var result = controller.GetMetricsFromAgent(fromTime, toTime);
    //        _ = Assert.IsAssignableFrom<IActionResult>(result);

    //    }
    //}
    //public class AgentMetricsNetWorkUnitTest
    //{
    //    private AgentMetricsNetworkController controller;
    //    public AgentMetricsNetWorkUnitTest()
    //    {
    //        controller = new AgentMetricsNetworkController();
    //    }
    //    [Fact]
    //    public void AgentMetricsNetWork_ResultOk()
    //    {

    //        var fromTime = TimeSpan.FromSeconds(0);
    //        var toTime = TimeSpan.FromSeconds(100);
    //        var result = controller.GetMetricsFromAgent(fromTime, toTime);
    //        _ = Assert.IsAssignableFrom<IActionResult>(result);

    //    }
    //}
}
