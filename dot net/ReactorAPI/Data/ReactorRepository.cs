using DataAccess.MSSQL;
using Logic.DTO_s;
using Logic.Interfaces.Repositories;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ReactorRepository : BaseRepository, IReactorRepository
    {
        public async Task AddReactorData(ReactorHistoryDTO reactorHistoryDto)
        {
            const string sql = @"
        INSERT INTO ""ReactorHistory""
            (""reactorId"", ""energySaturation"", ""temperature"", ""fieldStrength"", ""fuelExhaustion"", ""timeStamp"")
        VALUES
            (@ReactorId, @EnergySaturation, @Temperature, @FieldStrength, @FuelExhaustion, CURRENT_TIMESTAMP);
    ";

            await using var conn = GetSqlConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("ReactorId", reactorHistoryDto.ReactorId);
            cmd.Parameters.AddWithValue("EnergySaturation", reactorHistoryDto.EnergySaturation);
            cmd.Parameters.AddWithValue("Temperature", reactorHistoryDto.Temperature);
            cmd.Parameters.AddWithValue("FieldStrength", reactorHistoryDto.FieldStrength);
            cmd.Parameters.AddWithValue("FuelExhaustion", reactorHistoryDto.FuelExhaustion);

            await cmd.ExecuteNonQueryAsync();
        }

        public List<ReactorStatusDTO> GetLatestReactorData(DateTime fromUtc, int reactorId)
        {
            const string query = @"SELECT * FROM ""ReactorHistory"" 
                           WHERE ""timeStamp"" > @FromUtc AND ""reactorId"" = @ReactorId";

            using var conn = GetSqlConnection();
            conn.Open();

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("FromUtc", fromUtc);
            cmd.Parameters.AddWithValue("ReactorId", reactorId);

            using var reader = cmd.ExecuteReader();
            var results = new List<ReactorStatusDTO>();

            while (reader.Read())
            {
                results.Add(new ReactorStatusDTO
                {
                    TimeStamp = reader.GetDateTime(reader.GetOrdinal("timeStamp")),
                    Temperature = reader.GetDouble(reader.GetOrdinal("temperature")),
                    FieldStrength = reader.GetDouble(reader.GetOrdinal("fieldStrength")),
                    EnergySaturation = reader.GetDouble(reader.GetOrdinal("energySaturation")),
                    FuelExhaustion = reader.GetDouble(reader.GetOrdinal("fuelExhaustion")),
                    ReactorId = reader.GetInt32(reader.GetOrdinal("reactorId"))
                });
            }

            return results;
        }

        public ReactorDTO GetReactor(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> GetUserIdByReactorIdAsync(int reactorId)
        {
            const string sql = @"SELECT ""id"" FROM ""User"" WHERE ""reactorId"" = @ReactorId";

            await using var conn = GetSqlConnection();
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("ReactorId", reactorId);

            var result = await cmd.ExecuteScalarAsync();
            return result == null ? null : (int?)result;
        }
    }
}
