using Microsoft.EntityFrameworkCore;

namespace EFCoreExperiment.JsonSearching
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MyEntity> MyEntities { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
