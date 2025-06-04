using DataAccess.MSSQL;
using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using MySqlConnector;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        async public Task CreateUser(int id, string name, string email, string password)
        {
            await using var conn = GetSqlConnection();

            var cmd = new NpgsqlCommand("", conn);

            await using var reader = await cmd.ExecuteReaderAsync();
        }

        public UserDTO GetUser(int id)
        {
            string sql = "SELECT id, email, \"minecraftUsername\", password FROM \"User\" WHERE id = @Id";

            using var conn = GetSqlConnection();
            conn.Open();

            using var command = conn.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToUserDTO(reader);
            }

            Console.WriteLine(MapToUserDTO(reader));
            throw new Exception("Dead man");
        }

        private UserDTO MapToUserDTO(NpgsqlDataReader reader)
        {
            return new UserDTO()
            {
                Id = Convert.ToInt32(reader["id"]),
                minecraftUsername = Convert.ToString(reader["minecraftUsername"]) ?? string.Empty,
                Email = Convert.ToString(reader["email"]) ?? string.Empty,
                Password = Convert.ToString(reader["password"]) ?? string.Empty,
            };
        }
    }
}
