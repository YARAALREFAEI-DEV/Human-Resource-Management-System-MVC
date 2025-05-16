using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class editFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allowances",
                table: "Payroll");

            migrationBuilder.DropColumn(
                name: "TaxDeduction",
                table: "Payroll");

            migrationBuilder.AlterColumn<double>(
                name: "NetSalary",
                table: "Payroll",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "NetSalary",
                table: "Payroll",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<double>(
                name: "Allowances",
                table: "Payroll",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TaxDeduction",
                table: "Payroll",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
