using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;


namespace MetricsAgent.DAL
{
   public interface IRepository<T> where T : class
    {
        void Create(T item);
        void GetByTimePeriod(T item);

    }
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        public void Create(CpuMetric item)
        {
            throw new NotImplementedException();
        }

        public void GetByTimePeriod(CpuMetric item)
        {
            throw new NotImplementedException();
        }
    }
}
