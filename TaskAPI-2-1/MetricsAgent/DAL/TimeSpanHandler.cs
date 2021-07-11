using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace MetricsAgent.DAL
{
    public class TimeSpanHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value)
        
            => DateTimeOffset.FromUnixTimeSeconds((Int32)value);
        
        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            => parameter.Value = value;

    }
}
