using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class mmnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_DesignationID",
                table: "Employees",
                column: "DesignationID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DepartmentManagerID",
                table: "Departments",
                column: "DepartmentManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_DepartmentManagerID",
                table: "Departments",
                column: "DepartmentManagerID",
                principalTable: "Employees",
                principalColumn: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Designations_DesignationID",
                table: "Employees",
                column: "DesignationID",
                principalTable: "Designations",
                principalColumn: "DesignationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_DepartmentManagerID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Designations_DesignationID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DesignationID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DepartmentManagerID",
                table: "Departments");
        }
    }
}
