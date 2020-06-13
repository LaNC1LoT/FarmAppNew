using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "api");

            migrationBuilder.EnsureSchema(
                name: "dist");

            migrationBuilder.EnsureSchema(
                name: "tab");

            migrationBuilder.EnsureSchema(
                name: "log");

            migrationBuilder.CreateTable(
                name: "ApiMethods",
                schema: "api",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiMethodName = table.Column<string>(maxLength: 350, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    StoredProcedureName = table.Column<string>(maxLength: 350, nullable: true),
                    PathUrl = table.Column<string>(maxLength: 350, nullable: false),
                    HttpMethod = table.Column<string>(maxLength: 350, nullable: false),
                    IsNotNullParam = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsNeedAuthentication = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodeAthTypes",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeAthId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    NameAth = table.Column<string>(maxLength: 350, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeAthTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CodeAthTypes_CodeAthTypes_CodeAthId",
                        column: x => x.CodeAthId,
                        principalSchema: "dist",
                        principalTable: "CodeAthTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionTypes",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionTypeName = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(maxLength: 255, nullable: false),
                    ProducingCountry = table.Column<string>(maxLength: 255, nullable: false),
                    IsDomestic = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(maxLength: 50, nullable: true),
                    RoleId = table.Column<string>(maxLength: 50, nullable: true),
                    HttpMethod = table.Column<string>(maxLength: 255, nullable: true),
                    PathUrl = table.Column<string>(maxLength: 255, nullable: true),
                    MethodRoute = table.Column<string>(maxLength: 255, nullable: true),
                    HeaderRequest = table.Column<string>(maxLength: 4000, nullable: true),
                    RequestTime = table.Column<DateTime>(nullable: true),
                    FactTime = table.Column<DateTime>(nullable: true),
                    Param = table.Column<string>(maxLength: 4000, nullable: true),
                    StatusCode = table.Column<int>(nullable: true),
                    HeaderResponse = table.Column<string>(maxLength: 4000, nullable: true),
                    ResponseId = table.Column<Guid>(nullable: true),
                    ResponseTime = table.Column<DateTime>(nullable: true),
                    Header = table.Column<string>(maxLength: 255, nullable: true),
                    Result = table.Column<string>(maxLength: 4000, nullable: true),
                    Exception = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleImportFiles",
                schema: "tab",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleImportFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionId = table.Column<int>(nullable: true),
                    RegionTypeId = table.Column<int>(nullable: false),
                    RegionName = table.Column<string>(maxLength: 255, nullable: false),
                    Population = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regions_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "dist",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Regions_RegionTypes_RegionTypeId",
                        column: x => x.RegionTypeId,
                        principalSchema: "dist",
                        principalTable: "RegionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiMethodRoles",
                schema: "api",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiMethodId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethodRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiMethodRoles_ApiMethods_ApiMethodId",
                        column: x => x.ApiMethodId,
                        principalSchema: "api",
                        principalTable: "ApiMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiMethodRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dist",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: false),
                    PasswordSalt = table.Column<byte[]>(nullable: false),
                    RoleId = table.Column<int>(maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dist",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                schema: "dist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyId = table.Column<int>(nullable: true),
                    PharmacyName = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false),
                    IsMode = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsType = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsNetwork = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalSchema: "dist",
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Regions_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "dist",
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "tab",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyId = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalSchema: "dist",
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                schema: "tab",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugName = table.Column<string>(maxLength: 255, nullable: false),
                    CodeAthTypeId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false),
                    IsGeneric = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    StockId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drugs_CodeAthTypes_CodeAthTypeId",
                        column: x => x.CodeAthTypeId,
                        principalSchema: "dist",
                        principalTable: "CodeAthTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drugs_Stocks_StockId",
                        column: x => x.StockId,
                        principalSchema: "tab",
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drugs_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalSchema: "dist",
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                schema: "tab",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrugId = table.Column<int>(nullable: false),
                    PharmacyId = table.Column<int>(nullable: false),
                    SaleImportFileId = table.Column<int>(nullable: true),
                    SaleDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Price = table.Column<decimal>(type: "MONEY", nullable: false),
                    Amount = table.Column<decimal>(type: "MONEY", nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    IsDiscount = table.Column<bool>(nullable: false, defaultValueSql: "((0))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Drugs_DrugId",
                        column: x => x.DrugId,
                        principalSchema: "tab",
                        principalTable: "Drugs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Pharmacies_PharmacyId",
                        column: x => x.PharmacyId,
                        principalSchema: "dist",
                        principalTable: "Pharmacies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_SaleImportFiles_SaleImportFileId",
                        column: x => x.SaleImportFileId,
                        principalSchema: "tab",
                        principalTable: "SaleImportFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "api",
                table: "ApiMethods",
                columns: new[] { "Id", "ApiMethodName", "Description", "HttpMethod", "IsDeleted", "IsNeedAuthentication", "IsNotNullParam", "PathUrl", "StoredProcedureName" },
                values: new object[,]
                {
                    { 1, "Authenticate", "", "POST", false, false, true, "/api/Users/authenticate", null },
                    { 27, "PostRegion", "", "POST", false, true, true, "/api/Roles/PostRegion", null },
                    { 29, "GetPharmacies", "", "GET", false, true, false, "/api/Roles/GetPharmacies", null },
                    { 30, "GetPharmacy", "", "GET", false, true, true, "/api/Roles/GetPharmacy", null },
                    { 31, "PutPharmacy", "", "PUT", false, true, true, "/api/Roles/PutPharmacy", null },
                    { 32, "PostPharmacy", "", "POST", false, true, true, "/api/Roles/PostPharmacy", null },
                    { 33, "DeletePharmacy", "", "DELETE", false, true, true, "/api/Roles/DeletePharmacy", null },
                    { 34, "GetDrugs", "", "GET", false, true, false, "/api/Roles/GetDrugs", null },
                    { 35, "GetDrug", "", "GET", false, true, true, "/api/Roles/GetDrug", null },
                    { 36, "PutDrug", "", "PUT", false, true, true, "/api/Roles/PutDrug", null },
                    { 37, "PostDrug", "", "POST", false, true, true, "/api/Roles/PostDrug", null },
                    { 38, "DeleteDrug", "", "DELETE", false, true, true, "/api/Roles/DeleteDrug", null },
                    { 39, "GetCodeAthTypes", "", "GET", false, true, false, "/api/Roles/GetCodeAthTypes", null },
                    { 26, "PutRegion", "", "PUT", false, true, true, "/api/Roles/PutRegion", null },
                    { 40, "GetCodeAthType", "", "GET", false, true, true, "/api/Roles/GetCodeAthType", null },
                    { 42, "PostCodeAthType", "", "POST", false, true, true, "/api/Roles/PostCodeAthType", null },
                    { 43, "DeleteCodeAthType", "", "DELETE", false, true, true, "/api/Roles/DeleteCodeAthType", null },
                    { 44, "GetApiMethods", "", "GET", false, true, false, "api/Roles/GetApiMethods", null },
                    { 45, "GetApiMethod", "", "GET", false, true, true, "api/Roles/GetApiMethod", null },
                    { 46, "PutApiMethod", "", "PUT", false, true, true, "api/Roles/PutApiMethod", null },
                    { 47, "PostApiMethod", "", "POST", false, true, true, "api/Roles/PostApiMethod", null },
                    { 48, "DeleteApiMethod", "", "DELETE", false, true, true, "api/Roles/DeleteApiMethod", null },
                    { 49, "GetApiMethodRoles", "", "GET", false, true, false, "/api/Roles/GetApiMethodRoles", null },
                    { 50, "GetApiMethodRole", "", "GET", false, true, true, "/api/Roles/GetApiMethodRole", null },
                    { 51, "PutApiMethodRole", "", "PUT", false, true, true, "/api/Roles/PutApiMethodRole", null },
                    { 52, "PostApiMethodRole", "", "POST", false, true, true, "/api/Roles/PostApiMethodRole", null },
                    { 53, "DeleteApiMethodRole", "", "DELETE", false, true, true, "/api/Roles/DeleteApiMethodRole", null },
                    { 41, "PutCodeAthType", "", "PUT", false, true, true, "/api/Roles/PutCodeAthType", null },
                    { 25, "GetRegion", "", "GET", false, true, true, "/api/Roles/GetRegion", null },
                    { 28, "DeleteRegion", "", "DELETE", false, true, true, "/api/Roles/DeleteRegion", null },
                    { 23, "DeleteRegionType", "", "DELETE", false, true, true, "/api/RegionTypes/GetDeleteRoleUser", null },
                    { 2, "Register", "", "GET", false, false, true, "/api/Users/register", null },
                    { 3, "GetAll", "", "GET", false, true, true, "/api/Users/getAll", null },
                    { 4, "GetById", "", "GET", false, true, false, "/api/Users/getById", null },
                    { 5, "Update", "", "PUT", false, true, true, "/api/Users/update", null },
                    { 6, "Delete", "", "DELETE", false, true, false, "/api/Users/delete", null },
                    { 7, "GetUsersByRoleAsync", "", "GET", false, true, true, "/api/Users/getUsersByRoleAsync", null },
                    { 8, "SearchUser", "", "GET", false, true, true, "/api/Users/searchUser", null },
                    { 9, "GetVendors", "", "GET", false, true, false, "/api/Vendors/GetVendors", null },
                    { 10, "GetVendor", "", "GET", false, true, true, "/api/Vendors/GetVendor", null },
                    { 11, "PutVendor", "", "PUT", false, true, true, "/api/Vendors/PutVendor", null },
                    { 24, "GetRegions", "", "GET", false, true, false, "/api/Roles/GetRegions", null },
                    { 13, "DeleteVendor", "", "DELETE", false, true, true, "/api/Vendors/DeleteVendor", null },
                    { 14, "GetSales", "", "GET", false, true, false, "/api/Sales/GetSales", null },
                    { 12, "PostVendor", "", "POST", false, true, true, "/api/Vendors/PostVendor", null },
                    { 16, "PutSale", "", "PUT", false, true, true, "/api/Sales/PutSale", null },
                    { 22, "PostRegionType", "", "POST", false, true, true, "/api/RegionTypes/PostRole", null },
                    { 21, "PutRegionType", "", "PUT", false, true, true, "/api/RegionTypes/PutRole", null },
                    { 15, "GetSale", "", "GET", false, true, true, "/api/Sales/GetSale", null },
                    { 19, "GetRegionTypes", "", "GET", false, true, false, "/api/RegionTypes/GetRoles", null },
                    { 58, "DeleteRole", "", "DELETE", false, true, true, "/api/Roles/GetDeleteRoleUser", null },
                    { 20, "GetRegionType", "", "GET", false, true, true, "/api/RegionTypes/GetRole", null },
                    { 56, "PutRole", "", "PUT", false, true, true, "/api/Roles/PutRole", null },
                    { 55, "GetRole", "", "GET", false, true, true, "/api/Roles/GetRole", null },
                    { 54, "GetRoles", "", "GET", false, true, false, "/api/Roles/GetRoles", null },
                    { 18, "DeleteSale", "", "DELETE", false, true, true, "/api/Sales/DeleteSale", null },
                    { 17, "PostSale", "", "POST", false, true, true, "/api/Sales/PostSale", null },
                    { 57, "PostRole", "", "POST", false, true, true, "/api/Roles/PostRole", null }
                });

            migrationBuilder.InsertData(
                schema: "dist",
                table: "RegionTypes",
                columns: new[] { "Id", "IsDeleted", "RegionTypeName" },
                values: new object[,]
                {
                    { 1, false, "Государство" },
                    { 2, false, "Субъект(регион)" },
                    { 3, false, "Город" },
                    { 4, false, "Сёла, деревни и др." },
                    { 5, false, "Микрорайон" }
                });

            migrationBuilder.InsertData(
                schema: "dist",
                table: "Roles",
                columns: new[] { "Id", "IsDeleted", "RoleName" },
                values: new object[,]
                {
                    { 1, false, "admin" },
                    { 2, false, "user" }
                });

            migrationBuilder.InsertData(
                schema: "api",
                table: "ApiMethodRoles",
                columns: new[] { "Id", "ApiMethodId", "IsDeleted", "RoleId" },
                values: new object[] { 1, 1, false, 1 });

            migrationBuilder.InsertData(
                schema: "api",
                table: "ApiMethodRoles",
                columns: new[] { "Id", "ApiMethodId", "IsDeleted", "RoleId" },
                values: new object[] { 3, 2, false, 1 });

            migrationBuilder.InsertData(
                schema: "api",
                table: "ApiMethodRoles",
                columns: new[] { "Id", "ApiMethodId", "IsDeleted", "RoleId" },
                values: new object[] { 2, 1, false, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethodRoles_ApiMethodId",
                schema: "api",
                table: "ApiMethodRoles",
                column: "ApiMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethodRoles_RoleId",
                schema: "api",
                table: "ApiMethodRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiMethods_ApiMethodName",
                schema: "api",
                table: "ApiMethods",
                column: "ApiMethodName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodeAthTypes_CodeAthId",
                schema: "dist",
                table: "CodeAthTypes",
                column: "CodeAthId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_PharmacyId",
                schema: "dist",
                table: "Pharmacies",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_RegionId",
                schema: "dist",
                table: "Pharmacies",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionId",
                schema: "dist",
                table: "Regions",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionTypeId",
                schema: "dist",
                table: "Regions",
                column: "RegionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                schema: "dist",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_CodeAthTypeId",
                schema: "tab",
                table: "Drugs",
                column: "CodeAthTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_StockId",
                schema: "tab",
                table: "Drugs",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Drugs_VendorId",
                schema: "tab",
                table: "Drugs",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DrugId",
                schema: "tab",
                table: "Sales",
                column: "DrugId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PharmacyId",
                schema: "tab",
                table: "Sales",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SaleImportFileId",
                schema: "tab",
                table: "Sales",
                column: "SaleImportFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PharmacyId",
                schema: "tab",
                table: "Stocks",
                column: "PharmacyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiMethodRoles",
                schema: "api");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dist");

            migrationBuilder.DropTable(
                name: "Logs",
                schema: "log");

            migrationBuilder.DropTable(
                name: "Sales",
                schema: "tab");

            migrationBuilder.DropTable(
                name: "ApiMethods",
                schema: "api");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dist");

            migrationBuilder.DropTable(
                name: "Drugs",
                schema: "tab");

            migrationBuilder.DropTable(
                name: "SaleImportFiles",
                schema: "tab");

            migrationBuilder.DropTable(
                name: "CodeAthTypes",
                schema: "dist");

            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "tab");

            migrationBuilder.DropTable(
                name: "Vendors",
                schema: "dist");

            migrationBuilder.DropTable(
                name: "Pharmacies",
                schema: "dist");

            migrationBuilder.DropTable(
                name: "Regions",
                schema: "dist");

            migrationBuilder.DropTable(
                name: "RegionTypes",
                schema: "dist");
        }
    }
}
