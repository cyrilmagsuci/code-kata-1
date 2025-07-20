using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Migrations.Internal;

namespace Infrastructure.Database;

/// <summary>
/// Uses snake_case column names in the "__EFMigrationsHistory" table
/// to match the existing database schema.
/// </summary>
internal sealed class SnakeCaseHistoryRepository : NpgsqlHistoryRepository
{
    public SnakeCaseHistoryRepository(HistoryRepositoryDependencies dependencies)
        : base(dependencies)
    {
    }

    protected override string MigrationIdColumnName => "migration_id";

    protected override string ProductVersionColumnName => "product_version";
}
