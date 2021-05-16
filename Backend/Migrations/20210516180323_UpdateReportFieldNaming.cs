using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class UpdateReportFieldNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOrderAmount",
                table: "Reports");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalOrderSum",
                table: "Reports",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalOrderSum",
                table: "Reports");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalOrderAmount",
                table: "Reports",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
