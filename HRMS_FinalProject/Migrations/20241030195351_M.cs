using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class M : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "increment",
                table: "Payroll",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "increment",
                table: "Payroll");
        }
    }
}
