using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class newAttr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefaultAvailableDays",
                table: "LeaveType",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultAvailableDays",
                table: "LeaveType");
        }
    }
}
