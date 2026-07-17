using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class compositeforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LocationId_ShiftId",
                table: "Attendances",
                columns: new[] { "LocationId", "ShiftId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_LocationShifts_LocationId_ShiftId",
                table: "Attendances",
                columns: new[] { "LocationId", "ShiftId" },
                principalTable: "LocationShifts",
                principalColumns: new[] { "LocationId", "ShiftId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Locations_LocationId",
                table: "Attendances",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_LocationShifts_LocationId_ShiftId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Locations_LocationId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_LocationId_ShiftId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Attendances");
        }
    }
}
