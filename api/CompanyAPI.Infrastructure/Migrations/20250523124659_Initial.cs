﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanyAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Isin = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "Exchange", "Isin", "Name", "Ticker", "Website" },
                values: new object[,]
                {
                    { 1, "NASDAQ", "US0378331005", "Apple Inc.", "AAPL", "http://www.apple.com" },
                    { 2, "Pink Sheets", "US1104193065", "British Airways Plc", "BAIRY", null },
                    { 3, "Euronext Amsterdam", "NL0000009165", "Heineken NV", "HEIA", null },
                    { 4, "Tokyo Stock Exchange", "JP3866800000", "Panasonic Corp", "6752", "http://www.panasonic.co.jp" },
                    { 5, "Deutsche Börse", "DE000PAH0038", "Porsche Automobil", "PAH3", "https://www.porsche.com/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
