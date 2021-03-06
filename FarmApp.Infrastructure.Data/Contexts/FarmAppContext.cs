﻿using FarmApp.Domain.Core.Entity;
using FarmApp.Domain.Core.StoredProcedure;
using FarmApp.Infrastructure.Data.DataBaseHelper;
using Microsoft.EntityFrameworkCore;

namespace FarmApp.Infrastructure.Data.Contexts
{
    public class FarmAppContext : DbContext
    {
        public virtual DbSet<ApiMethod> ApiMethods { get; set; }
        public virtual DbSet<ApiMethodRole> ApiMethodRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<CodeAthType> CodeAthTypes { get; set; }
        public virtual DbSet<Pharmacy> Pharmacies { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegionType> RegionTypes { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<SaleImportFile> SaleImportFiles { get; set; }
        public virtual DbSet<DosageFormType> DosageFormTypes { get; set; }
        public virtual DbSet<ChartSale> ChartSales { get; set; }
        public virtual DbSet<ChartStock> ChartStocks { get; set; }

        public FarmAppContext(DbContextOptions<FarmAppContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartSale>(entity =>
            {
                entity.HasNoKey();
                entity.Property(p => p.Country);
                entity.Property(p => p.Region);
                entity.Property(p => p.City);
                entity.Property(p => p.ParentPharmacy);
                entity.Property(p => p.PharmacyName);
                entity.Property(p => p.DrugName);
                entity.Property(p => p.Code);
                entity.Property(p => p.IsGeneric);
                entity.Property(p => p.SaleDate).HasColumnType(CommandSql.DateTime);
                entity.Property(p => p.Price).HasColumnType(CommandSql.Money);
                entity.Property(p => p.Quantity);
                entity.Property(p => p.Amount).HasColumnType(CommandSql.Money);
                entity.Property(p => p.IsDiscount);
            });

            modelBuilder.Entity<ChartStock>(entity =>
            {
                entity.HasNoKey();
                entity.Property(p => p.Country);
                entity.Property(p => p.Region);
                entity.Property(p => p.City);
                entity.Property(p => p.ParentPharmacy);
                entity.Property(p => p.PharmacyName);
                entity.Property(p => p.DrugName);
                entity.Property(p => p.IsGeneric);
                entity.Property(p => p.CreateDate).HasColumnType(CommandSql.DateTime);
                entity.Property(p => p.Quantity);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable(Table.Stock, Schema.Tab);
                entity.Property(p => p.PharmacyId).IsRequired();
                entity.Property(p => p.DrugId).IsRequired();
                entity.Property(p => p.CreateDate).IsRequired().HasDefaultValueSql(CommandSql.GetDate);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.Drug).WithMany(w => w.Stocks).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Pharmacy).WithMany(w => w.Stocks).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable(Table.Logs, Schema.Log);
                entity.Property(p => p.GroupLogId).IsRequired();
                entity.Property(p => p.LogType).IsRequired().HasMaxLength(50);
                entity.Property(p => p.CreateDate).IsRequired().HasDefaultValueSql(CommandSql.GetDate);
                entity.Property(p => p.UserId);
                entity.Property(p => p.RoleId);
                entity.Property(p => p.StatusCode);
                entity.Property(p => p.PathUrl).HasColumnType("nvarchar(255)");
                entity.Property(p => p.HttpMethod).HasColumnType("nvarchar(255)");
                entity.Property(p => p.Header).HasColumnType("nvarchar(4000)");
                entity.Property(p => p.Body).HasColumnType("nvarchar(4000)");
                entity.Property(p => p.Exception).HasColumnType("nvarchar(4000)");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(Table.Role, Schema.Dist);
                entity.Property(p => p.RoleName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
            });

            modelBuilder.Entity<ApiMethod>(entity =>
            {
                entity.ToTable(Table.ApiMethod, Schema.Api);
                entity.Property(p => p.ApiMethodName).IsRequired().HasMaxLength(350);
                entity.HasIndex(p => p.ApiMethodName).IsUnique();
                entity.Property(p => p.Description).IsRequired().HasMaxLength(4000);
                entity.Property(p => p.StoredProcedureName).HasMaxLength(350);
                entity.Property(p => p.PathUrl).IsRequired().HasColumnType("varchar(350)");
                entity.Property(p => p.HttpMethod).IsRequired().HasColumnType("varchar(350)");
                entity.Property(p => p.IsNotNullParam).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsNeedAuthentication).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
            });

            modelBuilder.Entity<ApiMethodRole>(entity =>
            {
                entity.ToTable(Table.ApiMethodRole, Schema.Api);
                entity.Property(p => p.ApiMethodId).IsRequired();
                entity.Property(p => p.RoleId).IsRequired();
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(p => p.Role).WithMany(w => w.ApiMethodRoles).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.ApiMethod).WithMany(w => w.ApiMethodRoles).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable(Table.User, Schema.Dist);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.UserName).IsRequired().HasMaxLength(20);
                entity.Property(p => p.RoleId).IsRequired().HasDefaultValueSql(CommandSql.DefaultUser);
                entity.Property(p => p.PasswordHash).IsRequired();
                entity.Property(p => p.PasswordSalt).IsRequired();
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.Role).WithMany(w => w.Users).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DosageFormType>(entity =>
            {
                entity.ToTable(Table.DosageFormType, Schema.Dist);
                entity.Property(p => p.DosageForm).IsRequired().HasMaxLength(500);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
            });

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable(Table.Drug, Schema.Tab);
                entity.Property(p => p.CodeAthTypeId).IsRequired();
                entity.Property(p => p.VendorId).IsRequired();
                entity.Property(p => p.DosageFormTypeId).IsRequired();
                entity.Property(p => p.DrugName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsGeneric).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.CodeAthType).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Vendor).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.DosageFormType).WithMany(w => w.Drugs).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CodeAthType>(entity =>
            {
                entity.ToTable(Table.CodeAthType, Schema.Dist);
                entity.Property(p => p.CodeAthId);
                entity.Property(p => p.Code).IsRequired().HasMaxLength(50);
                entity.Property(p => p.NameAth).IsRequired().HasMaxLength(500);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.CodeAth).WithMany(w => w.CodeAthTypes).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable(Table.Sale, Schema.Tab);
                entity.Property(p => p.DrugId).IsRequired();
                entity.Property(p => p.PharmacyId).IsRequired();
                entity.Property(p => p.SaleImportFileId);
                entity.Property(p => p.SaleDate).IsRequired().HasColumnType(CommandSql.DateTime).HasDefaultValueSql(CommandSql.GetDate);
                entity.Property(p => p.Price).IsRequired().HasColumnType(CommandSql.Money);
                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.Amount).IsRequired().HasColumnType(CommandSql.Money);
                entity.Property(p => p.IsDiscount).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.Drug).WithMany(w => w.Sales).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Pharmacy).WithMany(w => w.Sales).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pharmacy>(entity =>
            {
                entity.ToTable(Table.Pharmacy, Schema.Dist);
                entity.Property(p => p.PharmacyId);
                entity.Property(p => p.RegionId).IsRequired();
                entity.Property(p => p.IsMode).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsType).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsNetwork).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.ParentPharmacy).WithMany(w => w.Pharmacies).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.Region).WithMany(w => w.Pharmacies).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RegionType>(entity =>
            {
                entity.ToTable(Table.RegionType, Schema.Dist);
                entity.Property(p => p.RegionTypeName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable(Table.Region, Schema.Dist);
                entity.Property(p => p.RegionId);
                entity.Property(p => p.RegionTypeId).IsRequired();
                entity.Property(p => p.RegionName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.Population).IsRequired().HasDefaultValueSql(CommandSql.Zero);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(h => h.ParentRegion).WithMany(w => w.Regions).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(h => h.RegionType).WithMany(w => w.Regions).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.ToTable(Table.Vendor, Schema.Dist);
                entity.Property(p => p.VendorName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.RegionId).IsRequired();
                entity.Property(p => p.IsDomestic).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.HasOne(p => p.Region).WithMany(w => w.Vendors).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SaleImportFile>(entity =>
            {
                entity.ToTable(Table.SaleImportFile, Schema.Tab);
                entity.Property(p => p.FileName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.CreateDate).IsRequired().HasColumnType(CommandSql.DateTime);
                entity.Property(p => p.FileName).IsRequired().HasMaxLength(255);
                entity.Property(p => p.IsCompleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
                entity.Property(p => p.IsDeleted).IsRequired().HasDefaultValueSql(CommandSql.DefaultFalse);
            });
        }
    }
}
