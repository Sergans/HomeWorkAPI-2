using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MetricsAgent.Controllers;
using Moq;
using MetricsAgent.DAL;
using MetricsAgent.Model;
using Microsoft.Extensions.Logging;
using AutoMapper;



namespace MetricsAgentTest
{
    public class AgentMetricsCpuUnitTest
    {
        private AgentMetricsCpuController controller;
        private Mock<ICpuMetricsRepository> mock;
        private Mock<ILogger<AgentMetricsCpuController>> mocklog;
        private Mock<IMapper> mockmapper;
        public AgentMetricsCpuUnitTest()
        {
            mock = new Mock<ICpuMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsCpuController>>();
            mockmapper = new Mock<IMapper>();
            controller = new AgentMetricsCpuController(mock.Object,mocklog.Object,mockmapper.Object);
        }

        [Fact]
        public void AgentMetricsCpu_ResultOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Setup(repository => (repository.GetByTimePeriod(fromTime,toTime))).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(fromTime,toTime), Times.AtMostOnce());
           
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
        private Mock<IMapper> mockmapper;
        private Mock<ILogger<AgentMetricsRamController>> mocklog;
        public AgentMetricsRamUnitTest()
        {
            mock = new Mock<IRamMetricsRepository>();
            mockmapper = new Mock<IMapper>();
            mocklog = new Mock<ILogger<AgentMetricsRamController>>();
            controller = new AgentMetricsRamController(mocklog.Object, mock.Object, mockmapper.Object);
        }
        [Fact]
        public void AgentMetricsRam_ResultOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Setup(repository => (repository.GetByTimePeriod(fromTime, toTime))).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtMostOnce());

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
        private Mock<IMapper> mockmapper;
        private Mock<ILogger<AgentMetricsDotNetController>> mocklog;
        public AgentMetricsDotNetUnitTest()
        {
            mockmapper = new Mock<IMapper>();
            mock = new Mock<IDotNetMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsDotNetController>>();
            controller = new AgentMetricsDotNetController(mocklog.Object, mock.Object, mockmapper.Object);
        }
        [Fact]
        public void AgentMetricsDotNet_ResultOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Setup(repository => (repository.GetByTimePeriod(fromTime, toTime))).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtMostOnce());

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
        private Mock<IMapper> mockmapper;
        private AgentMetricsNetWorkController controller;
        private Mock<INetWorkMetricsRepository> mock;
        private Mock<ILogger<AgentMetricsNetWorkController>> mocklog;
        public AgentMetricsNetWorkUnitTest()
        {
            mockmapper = new Mock<IMapper>();
            mock = new Mock<INetWorkMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsNetWorkController>>();
            controller = new AgentMetricsNetWorkController(mocklog.Object, mock.Object, mockmapper.Object);
        }
        [Fact]
        public void AgentMetricsNetWork_ResultOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Setup(repository => (repository.GetByTimePeriod(fromTime, toTime))).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<NetWorkMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.NetWorkMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<NetWorkMetric>()), Times.AtMostOnce());
        }

    }
    public class AgentMetricsHddUnitTest
    {
        private AgentMetricsHddController controller;
        private Mock<IHddMetricsRepository> mock;
        private Mock<ILogger<AgentMetricsHddController>> mocklog;
        private Mock<IMapper> mockmapper;
        public AgentMetricsHddUnitTest()
        {
            mock = new Mock<IHddMetricsRepository>();
            mocklog = new Mock<ILogger<AgentMetricsHddController>>();
            mockmapper = new Mock<IMapper>();
            controller = new AgentMetricsHddController(mocklog.Object, mock.Object, mockmapper.Object);
        }

        [Fact]
        public void AgentMetricsCpu_ResultOk()
        {
            var fromTime = DateTimeOffset.FromUnixTimeSeconds(0);
            var toTime = DateTimeOffset.FromUnixTimeSeconds(0);
            mock.Setup(repository => (repository.GetByTimePeriod(fromTime, toTime))).Verifiable();
            mock.Verify(repository => repository.GetByTimePeriod(fromTime, toTime), Times.AtMostOnce());

        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();
            var result = controller.Create(new MetricsAgent.Requests.HddMetricCreateRequest { Time = DateTimeOffset.FromUnixTimeSeconds(1), Value = 50 });
            mock.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }

    }
}
