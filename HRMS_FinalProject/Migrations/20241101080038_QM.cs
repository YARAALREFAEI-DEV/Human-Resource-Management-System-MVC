using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class QM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "LeaveRequest",
                newName: "EmployeeID");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "LeaveRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequest_Employees_EmployeeID",
                table: "LeaveRequest");

            migrationBuilder.DropIndex(
                name: "IX_LeaveRequest_EmployeeID",
                table: "LeaveRequest");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "LeaveRequest",
                newName: "EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
