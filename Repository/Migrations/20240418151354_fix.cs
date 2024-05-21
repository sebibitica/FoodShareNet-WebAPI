using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodShareNet.Repository.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Donors",
                columns: new[] { "Id", "Address", "CityId", "Name" },
                values: new object[,]
                {
                    { 1, "adr1", 1, "Mihai" },
                    { 2, "adr2", 2, "Dan" }
                });

            migrationBuilder.InsertData(
                table: "Donations",
                columns: new[] { "Id", "DonorId", "ExpirationDate", "ProductId", "Quantity", "StatusId" },
                values: new object[] { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Donations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Donors",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
