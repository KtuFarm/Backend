﻿// <auto-generated />
using System;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(ApiContext))]
    partial class ApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Backend.Models.Database.DayOfWeek", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DaysOfWeek");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Monday"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tuesday"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Wednesday"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Thursday"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Friday"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Saturday"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Sunday"
                        });
                });

            modelBuilder.Entity("Backend.Models.Database.EmployeeState", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmployeeState");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Working"
                        },
                        new
                        {
                            Id = 2,
                            Name = "OnVacation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Fired"
                        });
                });

            modelBuilder.Entity("Backend.Models.Database.OrderProductBalance", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductBalanceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("OrderId", "ProductBalanceId");

                    b.HasIndex("ProductBalanceId");

                    b.ToTable("OrderProductBalance");
                });

            modelBuilder.Entity("Backend.Models.Database.OrderState", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OrderStates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Created"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            Name = "InPreparation"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ready"
                        },
                        new
                        {
                            Id = 5,
                            Name = "InTransit"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Delivered"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Returning"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Returned"
                        });
                });

            modelBuilder.Entity("Backend.Models.Database.OrderUser", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("OrderId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderUsers");
                });

            modelBuilder.Entity("Backend.Models.Database.PaymentType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cash"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Card"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("Backend.Models.Database.PharmaceuticalForm", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PharmaceuticalForms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Other"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Tablets"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Syrup"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Suspension"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Lozenge"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Spray"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Drops"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Ointment"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Injection"
                        });
                });

            modelBuilder.Entity("Backend.Models.Database.PharmacyWorkingHours", b =>
                {
                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("WorkingHoursId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("PharmacyId", "WorkingHoursId");

                    b.HasIndex("WorkingHoursId");

                    b.ToTable("PharmacyWorkingHours");
                });

            modelBuilder.Entity("Backend.Models.Database.RequiredMedicamentAmount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicamentId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("RequiredMedicamentAmounts");
                });

            modelBuilder.Entity("Backend.Models.ManufacturerEntity.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("Backend.Models.MedicamentEntity.Medicament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ActiveSubstance")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsPrescriptionRequired")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsReimbursed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PharmaceuticalFormId")
                        .HasColumnType("int");

                    b.Property<double>("ReimbursePercentage")
                        .HasColumnType("double");

                    b.Property<double>("Surcharge")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("PharmaceuticalFormId");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("Backend.Models.OrderEntity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddressFrom")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("AddressTo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("OrderStateId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("double");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderStateId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Backend.Models.PharmacyEntity.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Backend.Models.ProductBalanceEntity.ProductBalance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedicamentId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("TransactionId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ProductBalances");
                });

            modelBuilder.Entity("Backend.Models.RegisterEntity.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cash")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("Backend.Models.TransactionEntity.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceWithoutVat")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("RegisterId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Vat")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("RegisterId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Backend.Models.UserEntity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DismissalDate")
                        .HasColumnType("datetime");

                    b.Property<int>("EmployeeStateId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeStateId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Backend.Models.WarehouseEntity.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("Backend.Models.WorkingHoursEntity.WorkingHours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan>("CloseTime")
                        .HasColumnType("time");

                    b.Property<int>("DayOfWeekId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("OpenTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("DayOfWeekId");

                    b.ToTable("WorkingHours");
                });

            modelBuilder.Entity("Backend.Models.Database.OrderProductBalance", b =>
                {
                    b.HasOne("Backend.Models.OrderEntity.Order", "Order")
                        .WithMany("OrderProductBalances")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.ProductBalanceEntity.ProductBalance", "ProductBalance")
                        .WithMany("OrderProductBalances")
                        .HasForeignKey("ProductBalanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ProductBalance");
                });

            modelBuilder.Entity("Backend.Models.Database.OrderUser", b =>
                {
                    b.HasOne("Backend.Models.OrderEntity.Order", "Order")
                        .WithMany("OrderUsers")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.UserEntity.User", "User")
                        .WithMany("OrderUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Backend.Models.Database.PharmacyWorkingHours", b =>
                {
                    b.HasOne("Backend.Models.PharmacyEntity.Pharmacy", "Pharmacy")
                        .WithMany("PharmacyWorkingHours")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.WorkingHoursEntity.WorkingHours", "WorkingHours")
                        .WithMany("PharmacyWorkingHours")
                        .HasForeignKey("WorkingHoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("Backend.Models.Database.RequiredMedicamentAmount", b =>
                {
                    b.HasOne("Backend.Models.MedicamentEntity.Medicament", "Medicament")
                        .WithMany("RequiredMedicamentAmounts")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PharmacyEntity.Pharmacy", "Pharmacy")
                        .WithMany("RequiredMedicamentAmounts")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicament");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Backend.Models.ManufacturerEntity.Manufacturer", b =>
                {
                    b.HasOne("Backend.Models.UserEntity.User", "Supplier")
                        .WithMany("Manufacturers")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Backend.Models.MedicamentEntity.Medicament", b =>
                {
                    b.HasOne("Backend.Models.ManufacturerEntity.Manufacturer", "Manufacturer")
                        .WithMany("Medicaments")
                        .HasForeignKey("ManufacturerId");

                    b.HasOne("Backend.Models.Database.PharmaceuticalForm", "PharmaceuticalForm")
                        .WithMany("Medicaments")
                        .HasForeignKey("PharmaceuticalFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("PharmaceuticalForm");
                });

            modelBuilder.Entity("Backend.Models.OrderEntity.Order", b =>
                {
                    b.HasOne("Backend.Models.Database.OrderState", "OrderState")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.WarehouseEntity.Warehouse", "Warehouse")
                        .WithMany("Orders")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderState");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Backend.Models.ProductBalanceEntity.ProductBalance", b =>
                {
                    b.HasOne("Backend.Models.MedicamentEntity.Medicament", "Medicament")
                        .WithMany("Balances")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PharmacyEntity.Pharmacy", "Pharmacy")
                        .WithMany("Products")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("Backend.Models.TransactionEntity.Transaction", "Transaction")
                        .WithMany("Medicaments")
                        .HasForeignKey("TransactionId");

                    b.HasOne("Backend.Models.WarehouseEntity.Warehouse", "Warehouse")
                        .WithMany("Products")
                        .HasForeignKey("WarehouseId");

                    b.Navigation("Medicament");

                    b.Navigation("Pharmacy");

                    b.Navigation("Transaction");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Backend.Models.RegisterEntity.Register", b =>
                {
                    b.HasOne("Backend.Models.PharmacyEntity.Pharmacy", "Pharmacy")
                        .WithMany("Registers")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Backend.Models.TransactionEntity.Transaction", b =>
                {
                    b.HasOne("Backend.Models.Database.PaymentType", "PaymentType")
                        .WithMany("Transactions")
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PharmacyEntity.Pharmacy", "Pharmacy")
                        .WithMany("Transactions")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.RegisterEntity.Register", "Register")
                        .WithMany("Transactions")
                        .HasForeignKey("RegisterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.UserEntity.User", "Pharmacist")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId");

                    b.Navigation("PaymentType");

                    b.Navigation("Pharmacy");

                    b.Navigation("Pharmacist");

                    b.Navigation("Register");
                });

            modelBuilder.Entity("Backend.Models.UserEntity.User", b =>
                {
                    b.HasOne("Backend.Models.Database.EmployeeState", "EmployeeState")
                        .WithMany("Employees")
                        .HasForeignKey("EmployeeStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.PharmacyEntity.Pharmacy", "Pharmacy")
                        .WithMany("Pharmacists")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("Backend.Models.WarehouseEntity.Warehouse", "Warehouse")
                        .WithMany("Employees")
                        .HasForeignKey("WarehouseId");

                    b.Navigation("EmployeeState");

                    b.Navigation("Pharmacy");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Backend.Models.WorkingHoursEntity.WorkingHours", b =>
                {
                    b.HasOne("Backend.Models.Database.DayOfWeek", "DayOfWeek")
                        .WithMany("WorkingHours")
                        .HasForeignKey("DayOfWeekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DayOfWeek");
                });

            modelBuilder.Entity("Backend.Models.Database.DayOfWeek", b =>
                {
                    b.Navigation("WorkingHours");
                });

            modelBuilder.Entity("Backend.Models.Database.EmployeeState", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Backend.Models.Database.OrderState", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Backend.Models.Database.PaymentType", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Backend.Models.Database.PharmaceuticalForm", b =>
                {
                    b.Navigation("Medicaments");
                });

            modelBuilder.Entity("Backend.Models.ManufacturerEntity.Manufacturer", b =>
                {
                    b.Navigation("Medicaments");
                });

            modelBuilder.Entity("Backend.Models.MedicamentEntity.Medicament", b =>
                {
                    b.Navigation("Balances");

                    b.Navigation("RequiredMedicamentAmounts");
                });

            modelBuilder.Entity("Backend.Models.OrderEntity.Order", b =>
                {
                    b.Navigation("OrderProductBalances");

                    b.Navigation("OrderUsers");
                });

            modelBuilder.Entity("Backend.Models.PharmacyEntity.Pharmacy", b =>
                {
                    b.Navigation("Pharmacists");

                    b.Navigation("PharmacyWorkingHours");

                    b.Navigation("Products");

                    b.Navigation("Registers");

                    b.Navigation("RequiredMedicamentAmounts");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Backend.Models.ProductBalanceEntity.ProductBalance", b =>
                {
                    b.Navigation("OrderProductBalances");
                });

            modelBuilder.Entity("Backend.Models.RegisterEntity.Register", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Backend.Models.TransactionEntity.Transaction", b =>
                {
                    b.Navigation("Medicaments");
                });

            modelBuilder.Entity("Backend.Models.UserEntity.User", b =>
                {
                    b.Navigation("Manufacturers");

                    b.Navigation("OrderUsers");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Backend.Models.WarehouseEntity.Warehouse", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Backend.Models.WorkingHoursEntity.WorkingHours", b =>
                {
                    b.Navigation("PharmacyWorkingHours");
                });
#pragma warning restore 612, 618
        }
    }
}
