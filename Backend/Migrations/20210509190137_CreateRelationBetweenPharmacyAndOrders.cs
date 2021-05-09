using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class CreateRelationBetweenPharmacyAndOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PharmacyId",
                table: "Orders",
                column: "PharmacyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Pharmacies_PharmacyId",
                table: "Orders",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Pharmacies_PharmacyId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PharmacyId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "Orders");
        }
    }
}
