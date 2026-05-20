using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePayroll.Migrations
{
    /// <inheritdoc />
    public partial class AddSalariesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salary_Employees_EmployeeId",
                table: "Salary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salary",
                table: "Salary");

            migrationBuilder.RenameTable(
                name: "Salary",
                newName: "Salaries");

            migrationBuilder.RenameIndex(
                name: "IX_Salary_EmployeeId",
                table: "Salaries",
                newName: "IX_Salaries_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                table: "Salaries",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Employees_EmployeeId",
                table: "Salaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Salaries",
                table: "Salaries");

            migrationBuilder.RenameTable(
                name: "Salaries",
                newName: "Salary");

            migrationBuilder.RenameIndex(
                name: "IX_Salaries_EmployeeId",
                table: "Salary",
                newName: "IX_Salary_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Salary",
                table: "Salary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salary_Employees_EmployeeId",
                table: "Salary",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
