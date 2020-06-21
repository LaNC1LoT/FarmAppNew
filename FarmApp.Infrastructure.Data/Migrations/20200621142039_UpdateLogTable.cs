using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FarmApp.Infrastructure.Data.Migrations
{
    public partial class UpdateLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FactTime",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "HeaderRequest",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "HeaderResponse",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "MethodRoute",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Param",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RequestTime",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ResponseId",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ResponseTime",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Result",
                schema: "log",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                schema: "log",
                table: "Logs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                schema: "log",
                table: "Logs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Header",
                schema: "log",
                table: "Logs",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                schema: "log",
                table: "Logs",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                schema: "log",
                table: "Logs",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupLogId",
                schema: "log",
                table: "Logs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LogType",
                schema: "log",
                table: "Logs",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "GroupLogId",
                schema: "log",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "LogType",
                schema: "log",
                table: "Logs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "log",
                table: "Logs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                schema: "log",
                table: "Logs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Header",
                schema: "log",
                table: "Logs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FactTime",
                schema: "log",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderRequest",
                schema: "log",
                table: "Logs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderResponse",
                schema: "log",
                table: "Logs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MethodRoute",
                schema: "log",
                table: "Logs",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Param",
                schema: "log",
                table: "Logs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestTime",
                schema: "log",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResponseId",
                schema: "log",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseTime",
                schema: "log",
                table: "Logs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "log",
                table: "Logs",
                type: "nvarchar(4000)",
                maxLength: 4000,
                nullable: true);
        }
    }
}
