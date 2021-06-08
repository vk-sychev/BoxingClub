using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class UpdatedTournaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "International Boxing Competition");

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "Id", "City", "Country", "Date", "IsMedCertificateRequired", "Name" },
                values: new object[] { 1, "Moscow", "Russia", new DateTime(2021, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Moscow boxing championship" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
