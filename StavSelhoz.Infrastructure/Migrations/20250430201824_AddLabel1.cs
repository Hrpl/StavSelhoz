using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StavSelhoz.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLabel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "summary_price",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "count",
                table: "order_products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_id",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "count",
                table: "order_products");

            migrationBuilder.AddColumn<double>(
                name: "summary_price",
                table: "orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
