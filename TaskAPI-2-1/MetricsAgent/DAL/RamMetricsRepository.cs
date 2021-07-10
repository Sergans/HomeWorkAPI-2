using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;
using System.Data.SQLite;
using MetricsAgent.IConectionManager;


namespace MetricsAgent.DAL
{
    public class RamMetricsRepository:IRamMetricsRepository
    {
        
        IConectionOpen connectionstring = new ConectionOpen();
        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO rammetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();


        }
        public IList<RamMetric> GetByTimePeriod(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(connectionstring.GetOpenedConection());
            
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT id,value,time FROM rammetrics WHERE time>@fromTime AND time<@toTime";
            cmd.Parameters.AddWithValue("@fromTime", fromTime.ToUnixTimeSeconds());
            cmd.Parameters.AddWithValue("@toTime", toTime.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            var returnList = new List<RamMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new RamMetric
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
