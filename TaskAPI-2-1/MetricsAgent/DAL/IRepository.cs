using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;
using System.Data.SQLite;
using MetricsAgent.IConectionManager;
using Dapper;


namespace MetricsAgent.DAL
{
   public interface IRepository<T> where T : class
    {
        void Create(T item);
        
        IList<T> GetByTimePeriod(DateTimeOffset fromTime,DateTimeOffset toTime);

    }
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {

    }
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {

    }
    public interface INetWorkMetricsRepository : IRepository<NetWorkMetric>
    {

    }
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {

    }
    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        //public CpuMetricsRepository()
        //{
        //    SqlMapper.AddTypeHandler(new TimeSpanHandler());
        //}
        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(CpuMetric item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                new
                {
                    value = item.Value,
                    time = item.Time.ToUnixTimeSeconds()
                }) ;
           
        }

       
        public IList<CpuMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            //return connection.Query<CpuMetric>("SELECT id,value,time FROM cpumetrics WHERE time>@fromTime AND time<@toTime", new
            //{
            //    fromTime = fromTime.ToUnixTimeSeconds(),
            //    toTime = toTime.ToUnixTimeSeconds()
            //}).ToList();
            return connection.Query<CpuMetric>("SELECT id,value,time FROM cpumetrics").ToList();
            // connection.Open();
            // using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            //cmd.CommandText = "SELECT id,value,time FROM cpumetrics WHERE time>@fromTime AND time<@toTime";
            //cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            //cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();
            //cmd.CommandText = "SELECT * FROM cpumetrics";

            //var returnList = new List<CpuMetric>();

            //using (SQLiteDataReader reader = cmd.ExecuteReader())
            //{
            //    // пока есть что читать -- читаем
            //    while (reader.Read())
            //    {
            //        // добавляем объект в список возврата
            //        returnList.Add(new CpuMetric
            //        {
            //            Id = (int)reader.GetInt64(0),
            //            Value = (int)reader.GetInt64(1),
            //            // налету преобразуем прочитанные секунды в метку времени
            //            Time = DateTimeOffset.FromUnixTimeSeconds((int)reader.GetInt64(2))
            //        });
            //    }
            //}

            //return returnList;

        }
    }
}
