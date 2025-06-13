using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VolApp.Migrations
{
    /// <inheritdoc />
    public partial class seedVol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Vols",
                columns: new[] { "Id", "DateArrivee", "DateDepart", "Depart", "Destination", "NombrePlacesMax", "PlacesDisponibles", "Prix" },
                values: new object[,]
                {
                    { 9, new DateTime(2025, 7, 1, 8, 0, 11, 0, DateTimeKind.Utc), new DateTime(2025, 7, 1, 11, 33, 1, 0, DateTimeKind.Utc), "Los Angeles", "Hong Kong", 360, 314, 82.00m },
                    { 10, new DateTime(2025, 5, 21, 14, 40, 11, 0, DateTimeKind.Utc), new DateTime(2025, 5, 22, 3, 16, 10, 0, DateTimeKind.Utc), "Vancouver", "Dubai", 330, 275, 487.00m },
                    { 11, new DateTime(2025, 8, 4, 5, 40, 11, 0, DateTimeKind.Utc), new DateTime(2025, 8, 4, 16, 37, 30, 0, DateTimeKind.Utc), "Sydney", "Shanghai", 390, 376, 340.00m },
                    { 12, new DateTime(2025, 4, 14, 4, 20, 11, 0, DateTimeKind.Utc), new DateTime(2025, 4, 14, 15, 32, 46, 0, DateTimeKind.Utc), "Berlin", "Miami", 400, 341, 381.00m },
                    { 13, new DateTime(2025, 6, 12, 23, 40, 11, 0, DateTimeKind.Utc), new DateTime(2025, 6, 13, 0, 46, 40, 0, DateTimeKind.Utc), "New York", "London", 310, 193, 138.00m },
                    { 14, new DateTime(2025, 9, 17, 23, 35, 11, 0, DateTimeKind.Utc), new DateTime(2025, 9, 18, 12, 41, 10, 0, DateTimeKind.Utc), "New York", "Berlin", 250, 225, 81.00m },
                    { 15, new DateTime(2025, 4, 2, 14, 15, 11, 0, DateTimeKind.Utc), new DateTime(2025, 4, 3, 1, 36, 34, 0, DateTimeKind.Utc), "Dubai", "Tokyo", 300, 245, 302.00m },
                    { 16, new DateTime(2025, 4, 12, 12, 20, 11, 0, DateTimeKind.Utc), new DateTime(2025, 4, 12, 15, 15, 18, 0, DateTimeKind.Utc), "Vancouver", "Sydney", 230, 145, 524.00m },
                    { 17, new DateTime(2025, 8, 30, 16, 55, 11, 0, DateTimeKind.Utc), new DateTime(2025, 8, 30, 18, 36, 54, 0, DateTimeKind.Utc), "Tokyo", "Toronto", 350, 232, 231.00m },
                    { 18, new DateTime(2025, 5, 26, 22, 45, 11, 0, DateTimeKind.Utc), new DateTime(2025, 5, 27, 9, 41, 28, 0, DateTimeKind.Utc), "Shanghai", "Singapore", 150, 148, 577.00m },
                    { 19, new DateTime(2025, 6, 17, 23, 0, 11, 0, DateTimeKind.Utc), new DateTime(2025, 6, 18, 2, 39, 57, 0, DateTimeKind.Utc), "Buenos Aires", "Rome", 180, 117, 595.00m },
                    { 20, new DateTime(2025, 8, 25, 2, 35, 11, 0, DateTimeKind.Utc), new DateTime(2025, 8, 25, 9, 30, 43, 0, DateTimeKind.Utc), "Rome", "Madrid", 220, 169, 454.00m },
                    { 120, new DateTime(2025, 3, 30, 3, 50, 11, 0, DateTimeKind.Utc), new DateTime(2025, 3, 30, 15, 3, 37, 0, DateTimeKind.Utc), "São Paulo", "Toronto", 350, 350, 504.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Vols",
                keyColumn: "Id",
                keyValue: 120);
        }
    }
}
