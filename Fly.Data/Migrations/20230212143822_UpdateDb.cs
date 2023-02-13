using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fly.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1b472fdf-fa38-48ce-b7fc-85e72733965e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "893d684b-a096-44d6-9523-c733a2843a6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6fdedca-a191-439c-8d28-4d77abcc8a85");

            migrationBuilder.RenameColumn(
                name: "AirporId",
                table: "Airports",
                newName: "IATALocationIdentifier");

            migrationBuilder.AddColumn<int>(
                name: "Altitude",
                table: "Airports",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Airports",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Airports",
                type: "double precision",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "058c5473-6cc0-4697-8366-4919d4f5bd4a", null, "Manager", "MANAGER" },
                    { "2d5daaba-1704-4fd0-9f58-c794a78fe7db", null, "Administrator", "ADMINISTRATOR" },
                    { "60865857-11f3-481c-89f4-7beadb6afe16", null, "Passenger", "PASSENGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "058c5473-6cc0-4697-8366-4919d4f5bd4a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d5daaba-1704-4fd0-9f58-c794a78fe7db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60865857-11f3-481c-89f4-7beadb6afe16");

            migrationBuilder.DropColumn(
                name: "Altitude",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Airports");

            migrationBuilder.RenameColumn(
                name: "IATALocationIdentifier",
                table: "Airports",
                newName: "AirporId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1b472fdf-fa38-48ce-b7fc-85e72733965e", null, "Manager", "MANAGER" },
                    { "893d684b-a096-44d6-9523-c733a2843a6c", null, "Passenger", "PASSENGER" },
                    { "c6fdedca-a191-439c-8d28-4d77abcc8a85", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
