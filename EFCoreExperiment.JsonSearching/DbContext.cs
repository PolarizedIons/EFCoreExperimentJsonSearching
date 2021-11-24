using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreExperiment.JsonSearching
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MyEntity> MyEntities { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var getTranslationsFunction = modelBuilder
                .HasDbFunction(typeof(MyDbFunctions).GetMethod(nameof(MyDbFunctions.GetTranslations)))
                .HasTranslation(args =>
                    SqlFunctionExpression.Create("json_extract_path_text", args, typeof(string), new StringTypeMapping("NVARCHAR(MAX)"))
                );
            getTranslationsFunction.HasParameter("column").Metadata.TypeMapping = new StringTypeMapping("NVARCHAR(MAX)");
            getTranslationsFunction.HasParameter("language").Metadata.TypeMapping = new StringTypeMapping("NVARCHAR(MAX)");
            getTranslationsFunction.HasParameter("key").Metadata.TypeMapping = new StringTypeMapping("NVARCHAR(MAX)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
