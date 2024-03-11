using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingScheduleSalePaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 10, 20, 33, 39, 972, DateTimeKind.Utc).AddTicks(1284),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 8, 20, 24, 53, 154, DateTimeKind.Utc).AddTicks(464));

            migrationBuilder.CreateTable(
                name: "SchedulePayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    InitialAmount = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LimitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountRemaining = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: false),
                    SchedulePaymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulePayment_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SchedulePayment_SaleId",
                table: "SchedulePayment",
                column: "SaleId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SchedulePayment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateSale",
                table: "Sale",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 8, 20, 24, 53, 154, DateTimeKind.Utc).AddTicks(464),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 10, 20, 33, 39, 972, DateTimeKind.Utc).AddTicks(1284));
        }
    }
}
