using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class TypeVarChar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathUrl",
                schema: "api",
                table: "ApiMethods",
                type: "varchar(350)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)");

            migrationBuilder.AlterColumn<string>(
                name: "HttpMethod",
                schema: "api",
                table: "ApiMethods",
                type: "varchar(350)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(350)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathUrl",
                schema: "api",
                table: "ApiMethods",
                type: "nvarchar(350)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(350)");

            migrationBuilder.AlterColumn<string>(
                name: "HttpMethod",
                schema: "api",
                table: "ApiMethods",
                type: "nvarchar(350)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(350)");
        }
    }
}
