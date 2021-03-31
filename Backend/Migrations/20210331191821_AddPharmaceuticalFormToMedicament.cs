using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddPharmaceuticalFormToMedicament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicaments_PharmaceuticalForms_PharmaceuticalFormId",
                table: "Medicaments");

            migrationBuilder.DropColumn(
                name: "PharmaceuticalForm",
                table: "Medicaments");

            migrationBuilder.AlterColumn<int>(
                name: "PharmaceuticalFormId",
                table: "Medicaments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicaments_PharmaceuticalForms_PharmaceuticalFormId",
                table: "Medicaments",
                column: "PharmaceuticalFormId",
                principalTable: "PharmaceuticalForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicaments_PharmaceuticalForms_PharmaceuticalFormId",
                table: "Medicaments");

            migrationBuilder.AlterColumn<int>(
                name: "PharmaceuticalFormId",
                table: "Medicaments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PharmaceuticalForm",
                table: "Medicaments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicaments_PharmaceuticalForms_PharmaceuticalFormId",
                table: "Medicaments",
                column: "PharmaceuticalFormId",
                principalTable: "PharmaceuticalForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
