using Npgsql;


namespace DataAccess.MSSQL
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        protected BaseRepository() 
            : this("Host=localhost;Username=postgres;Password=root;Database=reactor")
        {
            
        }

        protected BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected NpgsqlConnection GetSqlConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}