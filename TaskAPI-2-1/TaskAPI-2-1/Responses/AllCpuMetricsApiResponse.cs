using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Agents.Model;

namespace TaskAPI_2_1.Responses
{
    public class AllCpuMetricsApiResponse
    {
       public List<CpuAgent> Metrics { get; set; }
       //public DateTimeOffset Time { get; set; }
       //public int Id { get; set; }
       //public int Value { get; set; }

    }
}
