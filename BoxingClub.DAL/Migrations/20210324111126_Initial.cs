using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 1, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 175, "Vasiliy", "Konstantinovich", "Sychev", 88.0 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 2, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Igor", null, "Zhuravlev", 0.0 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 3, new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 175, "Ivan", null, "Pavlov", 81.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
