using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Backend.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Manufacturers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Surname = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EmployeeStateId = table.Column<int>(type: "int", nullable: false),
                    DismissalDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Position = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_EmployeeState_EmployeeStateId",
                        column: x => x.EmployeeStateId,
                        principalTable: "EmployeeState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EmployeeState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Working" });

            migrationBuilder.InsertData(
                table: "EmployeeState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "OnVacation" });

            migrationBuilder.InsertData(
                table: "EmployeeState",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Fired" });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_SupplierId",
                table: "Manufacturers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeStateId",
                table: "Users",
                column: "EmployeeStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PharmacyId",
                table: "Users",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Users_SupplierId",
                table: "Manufacturers",
                column: "SupplierId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Users_SupplierId",
                table: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EmployeeState");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_SupplierId",
                table: "Manufacturers");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Manufacturers");
        }
    }
}
