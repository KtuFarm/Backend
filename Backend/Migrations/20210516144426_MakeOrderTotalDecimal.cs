using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class MakeOrderTotalDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalSum",
                table: "Orders",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSum",
                table: "Orders");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Orders",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
