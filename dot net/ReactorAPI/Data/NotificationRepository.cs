using DataAccess.MSSQL;
using Logic.Interfaces.Repositories;
using Npgsql;
using System;
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
                VALUES(@UserId, @Title, @Content, CURRENT_TIMESTAMP);
            ";

            await using var conn = GetSqlConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("UserId", userId);
            cmd.Parameters.AddWithValue("Title", title);
            cmd.Parameters.AddWithValue("Content", content);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
