using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace MetricsAgent.IConectionManager
{
    public interface IConectionOpen
    {
         public string GetOpenedConection();
    }
    public class ConectionOpen : IConectionOpen
    {
        public string conect { get; set; }
        public string GetOpenedConection()
        {
            conect = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            
            return conect;
        }
    }
}
