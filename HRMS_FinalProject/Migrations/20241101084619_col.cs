using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class col : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Employees_EmployeeID",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequest_EmployeeID",
                table: "LeaveRequest");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequest_EmployeeID",
                table: "LeaveRequest",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequest_Employees_EmployeeID",
                table: "LeaveRequest",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
