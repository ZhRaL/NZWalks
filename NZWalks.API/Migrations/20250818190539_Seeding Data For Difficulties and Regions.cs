using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0eaa4913-c760-4981-baf6-9c0a5b81addc"), "Easy" },
                    { new Guid("672bb987-2f0b-4308-9e35-9dbac5d2cd9a"), "Medium" },
                    { new Guid("fb4f04bc-2615-496e-8b86-299509acc4a5"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[] { new Guid("d8df10ac-4da7-490f-b99d-c23c48893415"), "AKL", "Auckland", "some-image.png" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("0eaa4913-c760-4981-baf6-9c0a5b81addc"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("672bb987-2f0b-4308-9e35-9dbac5d2cd9a"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("fb4f04bc-2615-496e-8b86-299509acc4a5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d8df10ac-4da7-490f-b99d-c23c48893415"));
        }
    }
}
