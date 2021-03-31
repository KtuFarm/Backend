using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Backend.Migrations
{
    public partial class AddMedicaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PharmaceuticalForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmaceuticalForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    ActiveSubstance = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    BarCode = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsPrescriptionRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsReimbursed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Country = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Surcharge = table.Column<double>(type: "double", nullable: false),
                    IsSellable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ReimbursePercentage = table.Column<int>(type: "int", nullable: false),
                    PharmaceuticalForm = table.Column<int>(type: "int", nullable: false),
                    PharmaceuticalFormId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicaments_PharmaceuticalForms_PharmaceuticalFormId",
                        column: x => x.PharmaceuticalFormId,
                        principalTable: "PharmaceuticalForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PharmaceuticalForms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Other" },
                    { 2, "Tablets" },
                    { 3, "Syrup" },
                    { 4, "Suspension" },
                    { 5, "Lozenge" },
                    { 6, "Spray" },
                    { 7, "Drops" },
                    { 8, "Ointment" },
                    { 9, "Injection" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicaments_PharmaceuticalFormId",
                table: "Medicaments",
                column: "PharmaceuticalFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "PharmaceuticalForms");
        }
    }
}
