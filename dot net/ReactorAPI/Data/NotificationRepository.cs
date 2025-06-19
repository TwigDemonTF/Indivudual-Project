using DataAccess.MSSQL;
using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public async Task AddNotificationAsync(int userId, string title, string content)
        {
            const string sql = @"
                INSERT INTO ""Notification"" (""userId"", ""title"", ""content"", ""datetime"")
                VALUES(@UserId, @Title, @Content, @DateTime);
            ";

            await using var conn = GetSqlConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("UserId", userId);
            cmd.Parameters.AddWithValue("Title", title);
            cmd.Parameters.AddWithValue("Content", content);
            cmd.Parameters.AddWithValue("DateTime", DateTime.Now);

            await cmd.ExecuteNonQueryAsync();
        }

        public bool DeleteNotification(int notificationId)
        {
            const string sql = @"DELETE FROM ""Notification"" WHERE id = @NotificationId";
            try
            {
                using var conn = GetSqlConnection();
                conn.Open();

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("NotificationId", notificationId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }

        public List<NotificationDTO> GetNotifications(int userId)
        {
            const string sql = @"SELECT * FROM ""Notification"" WHERE ""userId"" = @Id";

            using var conn = GetSqlConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", userId);

            using var reader = cmd.ExecuteReader();
            var results = new List<NotificationDTO>();

            while (reader.Read())
            {
                results.Add(new NotificationDTO
                {
                    NotificationId = reader.GetInt32(reader.GetOrdinal("id")),
                    Title = reader.GetString(reader.GetOrdinal("title")),
                    Content = reader.GetString(reader.GetOrdinal("content")),
                    TimeStamp = reader.GetString(reader.GetOrdinal("datetime")),
                });
            }

            return results;
        }
    }
}
