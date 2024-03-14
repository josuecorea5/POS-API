using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingOneToManySchedulePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchedulePayment_SaleId",
                table: "SchedulePayment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 14, 16, 6, 31, 265, DateTimeKind.Utc).AddTicks(8448),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 12, 20, 1, 13, 133, DateTimeKind.Utc).AddTicks(4355));

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePayment_SaleId",
                table: "SchedulePayment",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SchedulePayment_SaleId",
                table: "SchedulePayment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 12, 20, 1, 13, 133, DateTimeKind.Utc).AddTicks(4355),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 14, 16, 6, 31, 265, DateTimeKind.Utc).AddTicks(8448));

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePayment_SaleId",
                table: "SchedulePayment",
                column: "SaleId",
                unique: true);
        }
    }
}
