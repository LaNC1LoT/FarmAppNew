using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class UpdateVendorsAndDrugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDomestic",
                schema: "dist",
                table: "Vendors");

            migrationBuilder.AddColumn<bool>(
                name: "IsDomestic",
                schema: "tab",
                table: "Drugs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethodRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethodRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethodRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, false, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, false, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDomestic",
                schema: "tab",
                table: "Drugs");

            migrationBuilder.AddColumn<bool>(
                name: "IsDomestic",
                schema: "dist",
                table: "Vendors",
                type: "bit",
                nullable: false,
                defaultValueSql: "((0))");

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethodRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethodRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethodRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, false, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, false, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, false });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "api",
                table: "ApiMethods",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "IsDeleted", "IsNeedAuthentication", "IsNotNullParam" },
                values: new object[] { false, true, true });

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "RegionTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                schema: "dist",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);
        }
    }
}
