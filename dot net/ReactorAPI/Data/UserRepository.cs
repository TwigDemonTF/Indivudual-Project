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
    /// <summary>
    /// Handles user-related database operations using PostgreSQL.
    /// </summary>
    public class UserRepository : BaseRepository, IUserRepository
    {
        /// <summary>
        /// Creates a new user and stores them in the database.
        /// </summary>
        /// <param name="registerDto">User registration data.</param>
        public async Task<UserDTO> CreateUser(RegisterDTO registerDto)
        {
            const string sql = @"
                INSERT INTO ""User"" (""minecraftUsername"", email, password, ""reactorId"")
                VALUES (@MinecraftUsername, @Email, @Password, @ReactorId)
                RETURNING id, email, ""minecraftUsername"", password, ""reactorId"";
            ";

            await using var conn = GetSqlConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("MinecraftUsername", registerDto.minecraftUsername ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("Email", registerDto.Email ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("Password", registerDto.Password ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("ReactorId", 0); // force initialize to 0

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new UserDTO
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Email = reader.GetString(reader.GetOrdinal("email")),
                    minecraftUsername = reader.GetString(reader.GetOrdinal("minecraftUsername")),
                    Password = reader.GetString(reader.GetOrdinal("password")),
                    reactorId = reader.GetInt32(reader.GetOrdinal("reactorId"))
                };
            }

            return null;
        }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        public UserDTO GetUser(int id)
        {
            string sql = "SELECT * FROM \"User\" WHERE id = @Id";

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
            throw new Exception("No User Found");
        }

        /// <summary>
        /// Authenticates a user by checking email and password.
        /// </summary>
        /// <param name="loginDto">Login credentials.</param>
        public UserDTO AuthenticateUser(LoginDTO loginDto)
        {
            string sql = "SELECT * FROM \"User\" WHERE email = @Email AND password = @Password";

            using var conn = GetSqlConnection();
            conn.Open();

            using var command = conn.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("Email", loginDto.Email);
            command.Parameters.AddWithValue("Password", loginDto.Password);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return MapToUserDTO(reader);
            }

            throw new Exception("Invalid Credentials");
        }

        /// <summary>
        /// Updates a user’s record to associate them with a reactor.
        /// </summary>
        /// <param name="dto">User ID and reactor ID binding data.</param>
        public async Task<bool> BindReactorToUser(BindReactorDTO dto)
        {
            const string sql = @"
            UPDATE ""User""
            SET ""reactorId"" = @ReactorId
            WHERE id = @UserId;
            ";

            await using var conn = GetSqlConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("ReactorId", dto.reactorId);
            cmd.Parameters.AddWithValue("UserId", dto.userId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }

        private UserDTO MapToUserDTO(NpgsqlDataReader reader)
        {
            return new UserDTO()
            {
                Id = Convert.ToInt32(reader["id"]),
                minecraftUsername = Convert.ToString(reader["minecraftUsername"]) ?? string.Empty,
                Email = Convert.ToString(reader["email"]) ?? string.Empty,
                Password = Convert.ToString(reader["password"]) ?? string.Empty,
                reactorId = Convert.ToInt32(reader["reactorId"]),
            };
        }
    }
}
