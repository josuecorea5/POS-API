using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAuditableEntityToSaleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "SaleDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SaleDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SaleDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "SaleDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 8, 20, 24, 53, 154, DateTimeKind.Utc).AddTicks(464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 8, 20, 18, 38, 262, DateTimeKind.Utc).AddTicks(8820));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Sale",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Sale",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "SaleDetail");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Sale");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Sale");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 8, 20, 18, 38, 262, DateTimeKind.Utc).AddTicks(8820),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 8, 20, 24, 53, 154, DateTimeKind.Utc).AddTicks(464));
        }
    }
}
