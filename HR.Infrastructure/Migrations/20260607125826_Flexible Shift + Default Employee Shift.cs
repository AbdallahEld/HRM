using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FlexibleShiftDefaultEmployeeShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFlexible",
                table: "Shifts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequiredHours",
                table: "Shifts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DefaultShiftId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DefaultShiftId",
                table: "Employees",
                column: "DefaultShiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Shifts_DefaultShiftId",
                table: "Employees",
                column: "DefaultShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Shifts_DefaultShiftId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DefaultShiftId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsFlexible",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "RequiredHours",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "DefaultShiftId",
                table: "Employees");
        }
    }
}
