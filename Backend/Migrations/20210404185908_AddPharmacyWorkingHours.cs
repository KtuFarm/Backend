using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class AddPharmacyWorkingHours : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PharmacyWorkingHours",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    WorkingHoursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyWorkingHours", x => new { x.PharmacyId, x.WorkingHoursId });
                    table.ForeignKey(
                        name: "FK_PharmacyWorkingHours_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PharmacyWorkingHours_WorkingHours_WorkingHoursId",
                        column: x => x.WorkingHoursId,
                        principalTable: "WorkingHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyWorkingHours_WorkingHoursId",
                table: "PharmacyWorkingHours",
                column: "WorkingHoursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PharmacyWorkingHours");
        }
    }
}
