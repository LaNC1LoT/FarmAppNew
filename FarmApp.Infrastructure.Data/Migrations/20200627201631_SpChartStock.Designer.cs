﻿// <auto-generated />
using System;
using FarmApp.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FarmApp.Infrastructure.Data.Migrations
{
    [DbContext(typeof(FarmAppContext))]
    [Migration("20200627201631_SpChartStock")]
    partial class SpChartStock
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.ApiMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiMethodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(350)")
                        .HasMaxLength(350);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)")
                        .HasMaxLength(4000);

                    b.Property<string>("HttpMethod")
                        .IsRequired()
                        .HasColumnType("varchar(350)");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsNeedAuthentication")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsNotNullParam")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("PathUrl")
                        .IsRequired()
                        .HasColumnType("varchar(350)");

                    b.Property<string>("StoredProcedureName")
                        .HasColumnType("nvarchar(350)")
                        .HasMaxLength(350);

                    b.HasKey("Id");

                    b.HasIndex("ApiMethodName")
                        .IsUnique();

                    b.ToTable("ApiMethods","api");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.ApiMethodRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApiMethodId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApiMethodId");

                    b.HasIndex("RoleId");

                    b.ToTable("ApiMethodRoles","api");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.CodeAthType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("CodeAthId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("NameAth")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.HasIndex("CodeAthId");

                    b.ToTable("CodeAthTypes","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.DosageFormType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DosageForm")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("Id");

                    b.ToTable("DosageFormTypes","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodeAthTypeId")
                        .HasColumnType("int");

                    b.Property<int>("DosageFormTypeId")
                        .HasColumnType("int");

                    b.Property<string>("DrugName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsGeneric")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CodeAthTypeId");

                    b.HasIndex("DosageFormTypeId");

                    b.HasIndex("VendorId");

                    b.ToTable("Drugs","tab");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(4000)");

                    b.Property<DateTime?>("CreateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(4000)");

                    b.Property<Guid>("GroupLogId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Header")
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("HttpMethod")
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("LogType")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PathUrl")
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusCode")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Logs","log");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsMode")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsNetwork")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<string>("PharmacyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Pharmacies","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("Population")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("0");

                    b.Property<int?>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("RegionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("RegionTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.HasIndex("RegionTypeId");

                    b.ToTable("Regions","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.RegionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("RegionTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("RegionTypes","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Sale", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("MONEY");

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsDiscount")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("MONEY");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SaleDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int?>("SaleImportFileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("SaleImportFileId");

                    b.ToTable("Sales","tab");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.SaleImportFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<bool?>("IsCompleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SaleImportFiles","tab");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreateDate")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Stocks","tab");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((2))");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("IsDeleted")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<bool?>("IsDomestic")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((0))");

                    b.Property<int>("RegionId")
                        .HasColumnType("int");

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Vendors","dist");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.ApiMethodRole", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.ApiMethod", "ApiMethod")
                        .WithMany("ApiMethodRoles")
                        .HasForeignKey("ApiMethodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FarmApp.Domain.Core.Entity.Role", "Role")
                        .WithMany("ApiMethodRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.CodeAthType", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.CodeAthType", "CodeAth")
                        .WithMany("CodeAthTypes")
                        .HasForeignKey("CodeAthId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Drug", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.CodeAthType", "CodeAthType")
                        .WithMany("Drugs")
                        .HasForeignKey("CodeAthTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FarmApp.Domain.Core.Entity.DosageFormType", "DosageFormType")
                        .WithMany("Drugs")
                        .HasForeignKey("DosageFormTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FarmApp.Domain.Core.Entity.Vendor", "Vendor")
                        .WithMany("Drugs")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Pharmacy", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.Pharmacy", "ParentPharmacy")
                        .WithMany("Pharmacies")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FarmApp.Domain.Core.Entity.Region", "Region")
                        .WithMany("Pharmacies")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Region", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.Region", "ParentRegion")
                        .WithMany("Regions")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("FarmApp.Domain.Core.Entity.RegionType", "RegionType")
                        .WithMany("Regions")
                        .HasForeignKey("RegionTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Sale", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.Drug", "Drug")
                        .WithMany("Sales")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FarmApp.Domain.Core.Entity.Pharmacy", "Pharmacy")
                        .WithMany("Sales")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FarmApp.Domain.Core.Entity.SaleImportFile", "SaleImportFile")
                        .WithMany()
                        .HasForeignKey("SaleImportFileId");
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Stock", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.Drug", "Drug")
                        .WithMany("Stocks")
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FarmApp.Domain.Core.Entity.Pharmacy", "Pharmacy")
                        .WithMany("Stocks")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.User", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FarmApp.Domain.Core.Entity.Vendor", b =>
                {
                    b.HasOne("FarmApp.Domain.Core.Entity.Region", "Region")
                        .WithMany("Vendors")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
