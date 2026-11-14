using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class restrictdeletingshiftwhenitstiedtoemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Shifts_DefaultShiftId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Shifts_DefaultShiftId",
                table: "Employees",
                column: "DefaultShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Shifts_DefaultShiftId",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Shifts_DefaultShiftId",
                table: "Employees",
                column: "DefaultShiftId",
                principalTable: "Shifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
