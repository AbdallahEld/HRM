using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LocationShifts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Locations_LocationId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_LocationId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Attendances");

            migrationBuilder.CreateTable(
                name: "LocationShifts",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationShifts", x => new { x.LocationId, x.ShiftId });
                    table.ForeignKey(
                        name: "FK_LocationShifts_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationShifts_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationShifts_ShiftId",
                table: "LocationShifts",
                column: "ShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationShifts");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LocationId",
                table: "Attendances",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Locations_LocationId",
                table: "Attendances",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
