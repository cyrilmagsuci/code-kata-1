using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Database;

//only used for local development when creating EF migrations
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<ApplicationDbContext>();

        string connectionString =
            "Host=localhost;Port=5433;Database=clean-architecture;Username=postgres;Password=postgres;Include Error Detail=true";

        optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default))
            .UseSnakeCaseNamingConvention()
            .UseSnakeCaseMigrationHistory();
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
