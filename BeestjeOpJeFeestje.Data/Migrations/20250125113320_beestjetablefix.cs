using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeestjeOpJeFeestje.Migrations
{
    /// <inheritdoc />
    public partial class beestjetablefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "BookingId", "ImageURL", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Aap.png", "Aap", 50f, "Jungle" },
                    { 2, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Olifant.png", "Olifant", 200f, "Jungle" },
                    { 3, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Zebra.png", "Zebra", 150f, "Jungle" },
                    { 4, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Leeuw.png", "Leeuw", 300f, "Jungle" },
                    { 5, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Hond.png", "Hond", 30f, "Boerderij" },
                    { 6, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Ezel.png", "Ezel", 60f, "Boerderij" },
                    { 7, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Koe.png", "Koe", 120f, "Boerderij" },
                    { 8, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Eend.png", "Eend", 20f, "Boerderij" },
                    { 9, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Kuiken.png", "Kuiken", 10f, "Boerderij" },
                    { 10, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Pinguin.png", "Pinguïn", 80f, "Sneeuw" },
                    { 11, null, "BeestjeOpJeFeestje\\wwwroot\\img\\IJsbeer.png", "IJsbeer", 250f, "Sneeuw" },
                    { 12, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Zeehond.png", "Zeehond", 100f, "Sneeuw" },
                    { 13, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Kameel.png", "Kameel", 180f, "Woestijn" },
                    { 14, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Slang.png", "Slang", 70f, "Woestijn" },
                    { 15, null, "BeestjeOpJeFeestje\\wwwroot\\img\\T-Rex.png", "T-Rex", 1000f, "VIP" },
                    { 16, null, "BeestjeOpJeFeestje\\wwwroot\\img\\Unicorn.png", "Unicorn", 1200f, "VIP" }
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
