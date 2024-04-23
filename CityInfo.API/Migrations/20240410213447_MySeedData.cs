using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CityInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class MySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "This is Tehran.", "Tehran" },
                    { 2, "This is Shiraz.", "Shiraz" },
                    { 3, "This is Tabriz.", "Tabriz" }
                });

            migrationBuilder.InsertData(
                table: "PointsOfInterest",
                columns: new[] { "ID", "CityID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "This is first point of interest for Tehran", "First Point" },
                    { 2, 1, "This is second point of interest for Tehran", "Second Point" },
                    { 3, 1, "This is third point of interest for Tehran", "Third Point" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PointsOfInterest",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
