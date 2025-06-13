using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VolApp.Migrations
{
    /// <inheritdoc />
    public partial class lastStepHopefully : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateArrivee",
                value: new DateTime(2023, 10, 15, 23, 45, 0, 0, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "Vols",
                columns: new[] { "Id", "DateArrivee", "DateDepart", "Depart", "Destination", "NombrePlacesMax", "PlacesDisponibles", "Prix" },
                values: new object[,]
                {
                    { 3, new DateTime(2023, 10, 21, 7, 30, 0, 0, DateTimeKind.Utc), new DateTime(2023, 10, 20, 18, 15, 0, 0, DateTimeKind.Utc), "Chicago", "London", 250, 180, 650.00m },
                    { 4, new DateTime(2023, 11, 6, 19, 30, 0, 0, DateTimeKind.Utc), new DateTime(2023, 11, 5, 22, 0, 0, 0, DateTimeKind.Utc), "New York", "Dubai", 350, 300, 950.00m },
                    { 5, new DateTime(2023, 10, 27, 6, 15, 0, 0, DateTimeKind.Utc), new DateTime(2023, 10, 25, 23, 45, 0, 0, DateTimeKind.Utc), "Los Angeles", "Sydney", 280, 220, 1200.00m },
                    { 6, new DateTime(2023, 11, 11, 18, 40, 0, 0, DateTimeKind.Utc), new DateTime(2023, 11, 10, 13, 20, 0, 0, DateTimeKind.Utc), "San Francisco", "Singapore", 320, 270, 1100.00m },
                    { 7, new DateTime(2023, 10, 31, 19, 25, 0, 0, DateTimeKind.Utc), new DateTime(2023, 10, 30, 15, 10, 0, 0, DateTimeKind.Utc), "Vancouver", "Hong Kong", 290, 240, 1050.00m },
                    { 8, new DateTime(2023, 11, 15, 23, 45, 0, 0, DateTimeKind.Utc), new DateTime(2023, 11, 15, 9, 30, 0, 0, DateTimeKind.Utc), "Miami", "Berlin", 230, 190, 700.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateArrivee",
                value: new DateTime(2023, 10, 16, 10, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
