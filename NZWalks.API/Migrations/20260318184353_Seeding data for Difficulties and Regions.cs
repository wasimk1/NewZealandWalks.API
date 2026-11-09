using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("05c3ecb3-2140-442b-b6d0-f5ae363d0852"), "Easy" },
                    { new Guid("341dffdd-be80-4697-b6e6-7eb220a1ac9d"), "Hard" },
                    { new Guid("c8a3553f-c5d9-48c1-a166-7bb29e40cf58"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2b18d4a3-a4ca-4603-8e5e-ab337221b0ea"), "AUK", "Auckland", "aukland-image.png" },
                    { new Guid("570181fe-2035-4ff2-9638-abab61a78489"), "NL", "Northland", "Northland-image.png" },
                    { new Guid("86b65284-6442-4539-a2d9-69a0b29d58d2"), "WTC", "West Coast", "West-Coast-image.png" },
                    { new Guid("93db5ab0-e66d-4d49-9b44-ca94538f52b2"), "WGN", "Wellington", "Wellington-image.png" },
                    { new Guid("a3ba384f-6e56-451f-bad0-fa206f0cdeec"), "STL", "Southland", "Southland-image.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("05c3ecb3-2140-442b-b6d0-f5ae363d0852"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("341dffdd-be80-4697-b6e6-7eb220a1ac9d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c8a3553f-c5d9-48c1-a166-7bb29e40cf58"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2b18d4a3-a4ca-4603-8e5e-ab337221b0ea"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("570181fe-2035-4ff2-9638-abab61a78489"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("86b65284-6442-4539-a2d9-69a0b29d58d2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("93db5ab0-e66d-4d49-9b44-ca94538f52b2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a3ba384f-6e56-451f-bad0-fa206f0cdeec"));
        }
    }
}
