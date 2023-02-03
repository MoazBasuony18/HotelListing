using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class addingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2884a35f-a220-411c-b0c3-e7c24a88a331");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "460249fa-724b-4187-936f-37969509e2c0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11b16993-92f2-4762-b56b-8af55dec601e", "4e35c688-becc-4aa6-9c46-ac513d0614f9", "user", "USER" },
                    { "9a90fea6-f892-4cb8-a55d-9f4b86526239", "df212ba8-f7e0-4a89-ae47-163671ccfb4a", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11b16993-92f2-4762-b56b-8af55dec601e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a90fea6-f892-4cb8-a55d-9f4b86526239");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2884a35f-a220-411c-b0c3-e7c24a88a331", "96eb87a6-571b-449a-a9e3-07eae88f6cf7", "user", "User" },
                    { "460249fa-724b-4187-936f-37969509e2c0", "7321115a-0399-498b-b2b4-1e6a63eb2782", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
