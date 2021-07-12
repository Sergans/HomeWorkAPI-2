using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MetricsAgent.Model;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<NetWorkMetric, NetWorkMetricDto>();
        }

    }
}
