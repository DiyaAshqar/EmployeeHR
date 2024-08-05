using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeHR.Migrations
{
    /// <inheritdoc />
    public partial class tazbetat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalSalary",
                schema: "dbo",
                table: "Payrolls",
                newName: "NetSalary");

            migrationBuilder.RenameColumn(
                name: "Salary",
                schema: "dbo",
                table: "Employees",
                newName: "BasicSalary");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NetSalary",
                schema: "dbo",
                table: "Payrolls",
                newName: "TotalSalary");

            migrationBuilder.RenameColumn(
                name: "BasicSalary",
                schema: "dbo",
                table: "Employees",
                newName: "Salary");
        }
    }
}
