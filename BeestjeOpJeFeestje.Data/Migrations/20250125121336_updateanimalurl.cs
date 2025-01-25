using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Migrations
{
    /// <inheritdoc />
    public partial class updateanimalurl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageURL",
                value: "/img/Aap.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageURL",
                value: "/img/Olifant.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageURL",
                value: "/img/Zebra.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageURL",
                value: "/img/Leeuw.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageURL",
                value: "/img/Hond.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageURL",
                value: "/img/Ezel.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageURL",
                value: "/img/Koe.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageURL",
                value: "/img/Eend.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageURL",
                value: "/img/Kuiken.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageURL",
                value: "/img/Pinguin.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageURL",
                value: "/img/IJsbeer.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageURL",
                value: "/img/Zeehond.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageURL",
                value: "/img/Kameel.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageURL",
                value: "/img/Slang.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageURL",
                value: "/img/T-Rex.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageURL",
                value: "/img/Unicorn.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Aap.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Olifant.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Zebra.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Leeuw.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Hond.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Ezel.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Koe.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Eend.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Kuiken.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Pinguin.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\IJsbeer.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Zeehond.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Kameel.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Slang.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 15,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\T-Rex.png");

            migrationBuilder.UpdateData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 16,
                column: "ImageURL",
                value: "BeestjeOpJeFeestje\\wwwroot\\img\\Unicorn.png");
        }
    }
}
