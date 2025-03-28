using Microsoft.EntityFrameworkCore;
using ReactorApi.Models;

namespace ReactorApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Reactor> Reactors { get; set; }
        public DbSet<ReactorValues> ReactorsValues { get; set; }
        public DbSet<ReactorData> ReactorDatas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
