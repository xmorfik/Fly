using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fly.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightState",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "AircraftLocations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AircraftLocations_FlightId",
                table: "AircraftLocations",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftLocations_Flights_FlightId",
                table: "AircraftLocations",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftLocations_Flights_FlightId",
                table: "AircraftLocations");

            migrationBuilder.DropIndex(
                name: "IX_AircraftLocations_FlightId",
                table: "AircraftLocations");

            migrationBuilder.DropColumn(
                name: "FlightState",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "AircraftLocations");
        }
    }
}
