using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeePayroll.Migrations
{
    /// <inheritdoc />
    public partial class SeedSalaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Salary",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Salary",
                columns: new[] { "Id", "Date", "Deductions", "EmployeeId", "GrossSalary", "NetSalary", "PayDate", "ProcessedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 1, 50000m, 50000m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SystemAdmin" },
                    { 2, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 2, 45000m, 45000m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SystemAdmin" },
                    { 3, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m, 3, 60000m, 60000m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SystemAdmin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Salary",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Salary");
        }
    }
}
