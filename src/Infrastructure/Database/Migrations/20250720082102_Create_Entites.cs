using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class Create_Entites : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "public");

        migrationBuilder.CreateTable(
            name: "checkouts",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                user_session_id = table.Column<Guid>(type: "uuid", nullable: false),
                promo_codes = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_checkouts", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "pricing_rules",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                is_bundle = table.Column<bool>(type: "boolean", nullable: false),
                price_per_unit = table.Column<decimal>(type: "numeric", nullable: false),
                promo_code = table.Column<string>(type: "text", nullable: false),
                quantity = table.Column<decimal>(type: "numeric", nullable: false),
                sku = table.Column<string>(type: "text", nullable: false),
                uom = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_pricing_rules", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "checkout_items",
            schema: "public",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                checkout_id = table.Column<Guid>(type: "uuid", nullable: false),
                quantity = table.Column<decimal>(type: "numeric", nullable: false),
                sku = table.Column<string>(type: "text", nullable: false),
                uom = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_checkout_items", x => x.id);
                table.ForeignKey(
                    name: "fk_checkout_items_checkouts_checkout_id",
                    column: x => x.checkout_id,
                    principalSchema: "public",
                    principalTable: "checkouts",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "ix_checkout_items_checkout_id",
            schema: "public",
            table: "checkout_items",
            column: "checkout_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "checkout_items",
            schema: "public");

        migrationBuilder.DropTable(
            name: "pricing_rules",
            schema: "public");

        migrationBuilder.DropTable(
            name: "checkouts",
            schema: "public");
    }
}
