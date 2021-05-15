using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Backend.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaysOfWeek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaysOfWeek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

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
                name: "OrderStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

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
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OpenTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DayOfWeekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_DaysOfWeek_DayOfWeekId",
                        column: x => x.DayOfWeekId,
                        principalTable: "DaysOfWeek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Registers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Cash = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registers_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AddressFrom = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    AddressTo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    OrderStateId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "double", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStates_OrderStateId",
                        column: x => x.OrderStateId,
                        principalTable: "OrderStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Email = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Username = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DismissalDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Position = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyWorkingHours",
                columns: table => new
                {
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    WorkingHoursId = table.Column<int>(type: "int", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacturers_Users_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderUsers",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderUsers", x => new { x.OrderId, x.UserId });
                    table.ForeignKey(
                        name: "FK_OrderUsers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    PriceWithoutVat = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Vat = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PaymentTypeId = table.Column<int>(type: "int", nullable: false),
                    RegisterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Registers_RegisterId",
                        column: x => x.RegisterId,
                        principalTable: "Registers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ReimbursePercentage = table.Column<double>(type: "double", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PharmaceuticalFormId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicaments_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Medicaments_PharmaceuticalForms_PharmaceuticalFormId",
                        column: x => x.PharmaceuticalFormId,
                        principalTable: "PharmaceuticalForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductBalances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: true),
                    WarehouseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBalances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBalances_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBalances_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBalances_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBalances_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequiredMedicamentAmounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(type: "double", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PharmacyId = table.Column<int>(type: "int", nullable: false),
                    MedicamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredMedicamentAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredMedicamentAmounts_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredMedicamentAmounts_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductBalances",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductBalanceId = table.Column<int>(type: "int", nullable: false),
                    IsSoftDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductBalances", x => new { x.OrderId, x.ProductBalanceId });
                    table.ForeignKey(
                        name: "FK_OrderProductBalances_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductBalances_ProductBalances_ProductBalanceId",
                        column: x => x.ProductBalanceId,
                        principalTable: "ProductBalances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DaysOfWeek",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Monday" },
                    { 2, "Tuesday" },
                    { 3, "Wednesday" },
                    { 4, "Thursday" },
                    { 5, "Friday" },
                    { 6, "Saturday" },
                    { 7, "Sunday" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, "Manufacturer" },
                    { 5, "Transportation" },
                    { 4, "Admin" },
                    { 3, "Warehouse" },
                    { 2, "Pharmacy" },
                    { 1, "None" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeState",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Working" },
                    { 2, "OnVacation" },
                    { 3, "Fired" }
                });

            migrationBuilder.InsertData(
                table: "OrderStates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 9, "Canceled" },
                    { 8, "Returned" },
                    { 7, "Returning" },
                    { 6, "Delivered" },
                    { 3, "InPreparation" },
                    { 4, "Ready" },
                    { 2, "Approved" },
                    { 1, "Created" },
                    { 5, "InTransit" }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cash" },
                    { 2, "Card" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                table: "PharmaceuticalForms",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 8, "Ointment" },
                    { 1, "Other" },
                    { 2, "Tablets" },
                    { 3, "Syrup" },
                    { 4, "Suspension" },
                    { 5, "Lozenge" },
                    { 6, "Spray" },
                    { 7, "Drops" },
                    { 9, "Injection" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_SupplierId",
                table: "Manufacturers",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicaments_ManufacturerId",
                table: "Medicaments",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicaments_PharmaceuticalFormId",
                table: "Medicaments",
                column: "PharmaceuticalFormId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductBalances_ProductBalanceId",
                table: "OrderProductBalances",
                column: "ProductBalanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStateId",
                table: "Orders",
                column: "OrderStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PharmacyId",
                table: "Orders",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseId",
                table: "Orders",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUsers_UserId",
                table: "OrderUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyWorkingHours_WorkingHoursId",
                table: "PharmacyWorkingHours",
                column: "WorkingHoursId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalances_MedicamentId",
                table: "ProductBalances",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalances_PharmacyId",
                table: "ProductBalances",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalances_TransactionId",
                table: "ProductBalances",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBalances_WarehouseId",
                table: "ProductBalances",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_PharmacyId",
                table: "Registers",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredMedicamentAmounts_MedicamentId",
                table: "RequiredMedicamentAmounts",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredMedicamentAmounts_PharmacyId",
                table: "RequiredMedicamentAmounts",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentTypeId",
                table: "Transactions",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PharmacyId",
                table: "Transactions",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RegisterId",
                table: "Transactions",
                column: "RegisterId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeStateId",
                table: "Users",
                column: "EmployeeStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PharmacyId",
                table: "Users",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WarehouseId",
                table: "Users",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_DayOfWeekId",
                table: "WorkingHours",
                column: "DayOfWeekId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProductBalances");

            migrationBuilder.DropTable(
                name: "OrderUsers");

            migrationBuilder.DropTable(
                name: "PharmacyWorkingHours");

            migrationBuilder.DropTable(
                name: "RequiredMedicamentAmounts");

            migrationBuilder.DropTable(
                name: "ProductBalances");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "OrderStates");

            migrationBuilder.DropTable(
                name: "DaysOfWeek");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "PharmaceuticalForms");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Registers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "EmployeeState");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Warehouses");
        }
    }
}
