using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Database;

public static class DbContextOptionsBuilderExtensions
{
    /// <summary>
    /// Uses snake_case naming for the migrations history table columns.
    /// Call this right after UseNpgsql(..) when configuring the DbContext.
    /// </summary>
    public static DbContextOptionsBuilder UseSnakeCaseMigrationHistory(
        this DbContextOptionsBuilder builder)
    {
        return builder.ReplaceService<IHistoryRepository, SnakeCaseHistoryRepository>();
    }
}
