using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class UpdateMedicament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSellable",
                table: "Medicaments");

            migrationBuilder.AlterColumn<double>(
                name: "ReimbursePercentage",
                table: "Medicaments",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ReimbursePercentage",
                table: "Medicaments",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AddColumn<bool>(
                name: "IsSellable",
                table: "Medicaments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
