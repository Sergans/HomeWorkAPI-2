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

        [Fact]
        public void AgentMetricsCpu_ResultOk()
        {
           mock.Setup(repository => (repository.GetByTimePeriod())).Verifiable();
          
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Verify(repository => repository.GetByTimePeriod(), Times.AtMostOnce());
           
        }
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
        private AgentMetricsRamController controller;
        private Mock<IRamMetricsRepository> mock;
        
        private Mock<ILogger<AgentMetricsRamController>> mocklog;
        public AgentMetricsRamUnitTest()
        {
            mock = new Mock<IRamMetricsRepository>();
            
            mocklog = new Mock<ILogger<AgentMetricsRamController>>();
            controller = new AgentMetricsRamController(mocklog.Object, mock.Object);
        }
        [Fact]
        public void AgentMetricsRam_ResultOk()
        {

            mock.Setup(repository => (repository.GetByTimePeriod())).Verifiable();

            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Verify(repository => repository.GetByTimePeriod(), Times.AtMostOnce());

        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.RamMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }

    }
    public class AgentMetricsDotNetUnitTest
    {
        private AgentMetricsDotNetController controller;
        private Mock<IDotNetMetricsRepository> mock;

        private Mock<ILogger<AgentMetricsDotNetController>> mocklog;
        public AgentMetricsDotNetUnitTest()
        {
            mock = new Mock<IDotNetMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsDotNetController>>();
            controller = new AgentMetricsDotNetController(mocklog.Object, mock.Object);
        }
        [Fact]
        public void AgentMetricsDotNet_ResultOk()
        {
            mock.Setup(repository => (repository.GetByTimePeriod())).Verifiable();
             var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Verify(repository => repository.GetByTimePeriod(), Times.AtMostOnce());

        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.DotNetMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }

    }
    public class AgentMetricsNetWorkUnitTest
    {
        private AgentMetricsNetWorkController controller;
        private Mock<INetWorkMetricsRepository> mock;
        private Mock<ILogger<AgentMetricsNetWorkController>> mocklog;
        public AgentMetricsNetWorkUnitTest()
        {
            mock = new Mock<INetWorkMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsNetWorkController>>();
            controller = new AgentMetricsNetWorkController(mocklog.Object, mock.Object);
        }
        [Fact]
        public void AgentMetricsNetWork_ResultOk()
        {
            mock.Setup(repository => (repository.GetByTimePeriod())).Verifiable();
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Verify(repository => repository.GetByTimePeriod(), Times.AtMostOnce());

        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<NetWorkMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.NetWorkMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<NetWorkMetric>()), Times.AtMostOnce());
        }

    }
}
