using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeestjeOpJeFeestje.Migrations
{
    /// <inheritdoc />
    public partial class animalTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "BookingId", "ImageURL", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, "https://example.com/aap.jpg", "Aap", 50f, "Jungle" },
                    { 2, null, "https://example.com/olifant.jpg", "Olifant", 200f, "Jungle" },
                    { 3, null, "https://example.com/zebra.jpg", "Zebra", 150f, "Jungle" },
                    { 4, null, "https://example.com/leeuw.jpg", "Leeuw", 300f, "Jungle" },
                    { 5, null, "https://example.com/hond.jpg", "Hond", 30f, "Boerderij" },
                    { 6, null, "https://example.com/ezel.jpg", "Ezel", 60f, "Boerderij" },
                    { 7, null, "https://example.com/koe.jpg", "Koe", 120f, "Boerderij" },
                    { 8, null, "https://example.com/eend.jpg", "Eend", 20f, "Boerderij" },
                    { 9, null, "https://example.com/kuiken.jpg", "Kuiken", 10f, "Boerderij" },
                    { 10, null, "https://example.com/pinguin.jpg", "Pinguïn", 80f, "Sneeuw" },
                    { 11, null, "https://example.com/ijsbeer.jpg", "IJsbeer", 250f, "Sneeuw" },
                    { 12, null, "https://example.com/zeehond.jpg", "Zeehond", 100f, "Sneeuw" },
                    { 13, null, "https://example.com/kameel.jpg", "Kameel", 180f, "Woestijn" },
                    { 14, null, "https://example.com/slang.jpg", "Slang", 70f, "Woestijn" },
                    { 15, null, "https://example.com/t-rex.jpg", "T-Rex", 1000f, "VIP" },
                    { 16, null, "https://example.com/unicorn.jpg", "Unicorn", 1200f, "VIP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 16);
        }
    }
}
