using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "IdUser", "Email", "IsAdmin", "Login", "Password", "RefreshToken", "RefreshTokenExp", "Salt" },
                values: new object[] { 1, "a@a.com", true, "admin", "admin", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "IdCategory", "CategoryName" },
                values: new object[,]
                {
                    { 1L, "Business" },
                    { 2L, "Education" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "IdDiscount", "Name", "Offer", "TimeEnd", "TimeStart", "Value" },
                values: new object[,]
                {
                    { 1L, "Summer Sale", "15% off", new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15.0m },
                    { 2L, "Winter Sale", "25% off", new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 25.0m }
                });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "IdSoftware", "Description", "IdCategory", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, "A comprehensive business management software", 1L, "Business Suite", 5000.00m },
                    { 2L, "An interactive educational platform", 2L, "Education Hub", 3000.00m }
                });

            migrationBuilder.InsertData(
                table: "Versions",
                columns: new[] { "IdVersion", "Comments", "Date", "IdSoftware", "VersionNumber" },
                values: new object[,]
                {
                    { 1L, "Initial release", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "1.0" },
                    { 2L, "Major update", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "2.0" },
                    { 3L, "Minor update", new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "2.1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "IdUser",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Versions",
                keyColumn: "IdVersion",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Versions",
                keyColumn: "IdVersion",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Versions",
                keyColumn: "IdVersion",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "IdSoftware",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "IdCategory",
                keyValue: 2L);
        }
    }
}
