using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddSoftDeletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "RequiredMedicamentAmounts",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Registers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "PharmacyWorkingHours",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Pharmacies",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSoftDeleted",
                table: "Medicaments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "RequiredMedicamentAmounts");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "PharmacyWorkingHours");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Pharmacies");

            migrationBuilder.DropColumn(
                name: "IsSoftDeleted",
                table: "Medicaments");
        }
    }
}
