using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class InitialDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "IdDiscount", "Name", "Offer", "TimeEnd", "TimeStart", "Value" },
                values: new object[] { 1L, "Returning clients discount", "Discount for upfront cost", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Unspecified).AddTicks(9999), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "IdDiscount",
                keyValue: 1L);
        }
    }
}
