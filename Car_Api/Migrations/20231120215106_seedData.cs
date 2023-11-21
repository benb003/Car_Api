using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car_Api.Migrations
{
    public partial class seedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Country", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Sweden", "v60 is cool car", "Volvo" },
                    { 2, "Germany", "530e is nice car", "Bmw" },
                    { 3, "USA", "i have no idea about this one", "Tesla" },
                    { 4, "Japan", "Reliable car, they say", "Toyoto" },
                    { 5, "South Korea", "my favorite xceed", "Kia" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "Description", "IsElectric", "Model", "Year" },
                values: new object[,]
                {
                    { 1, 1, "Car of the year", false, "V60", 2021 },
                    { 2, 2, "cool", false, "530e", 2022 },
                    { 3, 3, "car car car", true, "Model X", 2021 },
                    { 4, 3, "maybe maybe", true, "Model 3", 2023 },
                    { 5, 4, "good one", false, "Corolla Hybrid", 2020 },
                    { 6, 5, "crossover like car", true, "xCeed", 2023 },
                    { 7, 5, "tractor", false, "Sorento", 2011 },
                    { 8, 5, "not a car bicycle", false, "Picanto", 2019 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
