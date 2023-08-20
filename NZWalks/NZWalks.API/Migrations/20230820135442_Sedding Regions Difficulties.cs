using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeddingRegionsDifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3a139880-627e-4cc7-a0b9-f8376bd5f5ab"), "Easy" },
                    { new Guid("5bd76ab1-9491-4f73-bfcc-ae8950123298"), "Medium" },
                    { new Guid("9c7ddd09-242c-4e99-a7c8-19f899cd2fc0"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("2fd626a6-b60c-43cd-aa9e-0cf11b19e13e"), "NSN", "nsn-img", "Nelson" },
                    { new Guid("713e5460-bc6c-45f0-b55c-3b281f618e8b"), "STL", "stl-img", "Southland" },
                    { new Guid("80d1aa52-e48e-42ae-a49a-ea185151be97"), "NTL", "ntl-image", "Northland" },
                    { new Guid("88c597da-60c3-4b23-b03b-cd96c5d9bb25"), "BOP", "bop-img", "Bay Of Plenty" },
                    { new Guid("c88de96a-0b49-4fbe-afe8-79515f052b37"), "AKL", "akl-img", "Auckland" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3a139880-627e-4cc7-a0b9-f8376bd5f5ab"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5bd76ab1-9491-4f73-bfcc-ae8950123298"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9c7ddd09-242c-4e99-a7c8-19f899cd2fc0"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2fd626a6-b60c-43cd-aa9e-0cf11b19e13e"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("713e5460-bc6c-45f0-b55c-3b281f618e8b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("80d1aa52-e48e-42ae-a49a-ea185151be97"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("88c597da-60c3-4b23-b03b-cd96c5d9bb25"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c88de96a-0b49-4fbe-afe8-79515f052b37"));
        }
    }
}
