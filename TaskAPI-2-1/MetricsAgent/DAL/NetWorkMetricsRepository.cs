using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsAgent.Model;
using System.Data.SQLite;

namespace MetricsAgent.DAL
{
    public class NetWorkMetricsRepository:INetWorkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";

        public void Create(NetWorkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            cmd.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(@value, @time)";
            cmd.Parameters.AddWithValue("@value", item.Value);
            cmd.Parameters.AddWithValue("@time", item.Time.ToUnixTimeSeconds());
            cmd.Prepare();
            cmd.ExecuteNonQuery();


        }

        public IList<NetWorkMetric> GetByTimePeriod()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);

            // прописываем в команду SQL запрос на получение всех данных из таблицы
            cmd.CommandText = "SELECT * FROM networkmetrics";

            var returnList = new List<NetWorkMetric>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                // пока есть что читать -- читаем
                while (reader.Read())
                {
                    // добавляем объект в список возврата
                    returnList.Add(new NetWorkMetric
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        // налету преобразуем прочитанные секунды в метку времени
                        Time = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }

            return returnList;

        }
    }
}
