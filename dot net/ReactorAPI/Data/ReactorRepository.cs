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


        public ReactorDTO GetReactor(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
