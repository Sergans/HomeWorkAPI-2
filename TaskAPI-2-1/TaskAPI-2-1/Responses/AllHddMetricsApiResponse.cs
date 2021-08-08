using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskAPI_2_1.Agents.Model;

namespace TaskAPI_2_1.Responses
{
    public class AllHddMetricsApiResponse
    {
        public List<HddAgent> Metrics { get; set; }
    }
}
