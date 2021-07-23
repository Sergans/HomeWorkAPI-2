using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAPI_2_1.Request
{
    public class GetAllCpuMetricsApiRequest
    {
       public DateTimeOffset FromTime { get; set; }
       public DateTimeOffset ToTime { get; set; }
       public string ClientBaseAddress { get; set; }
    }
}
