using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class newFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designations_Departments_DepartmentID",
                table: "Designations");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Designations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_Departments_DepartmentID",
                table: "Designations",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Designations_Departments_DepartmentID",
                table: "Designations");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Designations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Designations_Departments_DepartmentID",
                table: "Designations",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");
        }
    }
}
