﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;
using System.Data.SQLite;
using MetricsAgent.IConectionManager;


namespace MetricsAgent.DAL
{
   public interface IRepository<T> where T : class
    {
        void Create(T item);
        
        IList<T> GetByTimePeriod();

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
        
        //private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        
        public void Create(CpuMetric item)
        {

            IConectionOpen connectionstring = new ConectionOpen();
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time",  item.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            

        }

       
        public IList<CpuMetric> GetByTimePeriod()
        {

            IConectionOpen connectionstring = new ConectionOpen();
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
             connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new CpuMetric
                    {
                        Id = (int)reader.GetInt64(0),
                        Value = (int)reader.GetInt64(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds((int)reader.GetInt64(2))
                    });
                }
            }

            return returnList;

        }
    }
}
