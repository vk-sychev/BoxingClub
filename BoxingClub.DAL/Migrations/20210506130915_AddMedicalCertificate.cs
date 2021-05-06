using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class AddMedicalCertificate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicalCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCertificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalCertificates_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MedicalCertificates",
                columns: new[] { "Id", "ClinicName", "DateOfIssue", "Result", "StudentId" },
                values: new object[,]
                {
                    { 6, "Polyclinic 4", new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3 },
                    { 5, "VODC", new DateTime(2021, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 4, "VODC", new DateTime(2018, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, "Polyclinic 13", new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "Polyclinic 4", new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 7, "Polyclinic 1", new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 1, "Polyclinic 4", new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificates_StudentId",
                table: "MedicalCertificates",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalCertificates");
        }
    }
}
