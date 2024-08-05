using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeHR.Migrations
{
    /// <inheritdoc />
    public partial class updateDepModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation",
                schema: "dbo",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation2",
                schema: "dbo",
                table: "Departments",
                type: "char(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "test",
                schema: "dbo",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Abbreviation2",
                schema: "dbo",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "test",
                schema: "dbo",
                table: "Departments");

            migrationBuilder.AddColumn<string>(
                name: "Abbreviation",
                schema: "dbo",
                table: "Departments",
                type: "char(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }
    }
}
