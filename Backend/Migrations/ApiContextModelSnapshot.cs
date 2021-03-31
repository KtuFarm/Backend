﻿// <auto-generated />
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
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Backend.Models.Database.Medicament", b =>
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

                    b.Property<bool>("IsSellable")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("PharmaceuticalFormId")
                        .HasColumnType("int");

                    b.Property<int>("ReimbursePercentage")
                        .HasColumnType("int");

                    b.Property<double>("Surcharge")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("PharmaceuticalFormId");

                    b.ToTable("Medicaments");
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

            modelBuilder.Entity("Backend.Models.Database.Pharmacy", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("Backend.Models.Database.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cash")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("Backend.Models.Database.Medicament", b =>
                {
                    b.HasOne("Backend.Models.Database.PharmaceuticalForm", "PharmaceuticalForm")
                        .WithMany("Medicaments")
                        .HasForeignKey("PharmaceuticalFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PharmaceuticalForm");
                });

            modelBuilder.Entity("Backend.Models.Database.Register", b =>
                {
                    b.HasOne("Backend.Models.Database.Pharmacy", "Pharmacy")
                        .WithMany("Registers")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("Backend.Models.Database.PharmaceuticalForm", b =>
                {
                    b.Navigation("Medicaments");
                });

            modelBuilder.Entity("Backend.Models.Database.Pharmacy", b =>
                {
                    b.Navigation("Registers");
                });
#pragma warning restore 612, 618
        }
    }
}
