using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Students.DAL.Implementation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxingGroups", x => x.Id);
                });

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
                    NumberOfFights = table.Column<int>(type: "int", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BoxingGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_BoxingGroups_BoxingGroupId",
                        column: x => x.BoxingGroupId,
                        principalTable: "BoxingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCertificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                table: "BoxingGroups",
                columns: new[] { "Id", "CoachId", "Name" },
                values: new object[] { 1, "19759de3-ce1d-4cfd-8340-4e64eb245eb4", "Vityaz" });

            migrationBuilder.InsertData(
                table: "BoxingGroups",
                columns: new[] { "Id", "CoachId", "Name" },
                values: new object[] { 2, "19759de3-ce1d-4cfd-8340-4e64eb245eb4", "Warrior" });

            migrationBuilder.InsertData(
                table: "BoxingGroups",
                columns: new[] { "Id", "CoachId", "Name" },
                values: new object[] { 3, "060342c3-9dc3-4597-bae1-9f19c991ebe9", "Sarmat" });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Gender", "Height", "Name", "NumberOfFights", "Patronymic", "Surname", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2015, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 175, "Vasiliy", 3, "Konstantinovich", "Sychev", 88.0 },
                    { 13, new DateTime(2003, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2015, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 170, "Alexander", 5, null, "Kirillov", 74.0 },
                    { 7, new DateTime(2000, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 175, "Vlad", 1, "Sergeevich", "Safonov", 74.0 },
                    { 5, new DateTime(1998, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2018, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 165, "Vika", 4, null, "Zhukova", 55.0 },
                    { 16, new DateTime(2000, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2016, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 180, "Julia", 5, null, "Belikova", 63.0 },
                    { 10, new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2016, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 178, "Dmitry", 4, "Dmitrievich", "Kustovinov", 69.0 },
                    { 9, new DateTime(2000, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 180, "Viktoria", 1, null, "Narkevich", 60.0 },
                    { 14, new DateTime(2003, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2012, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 165, "Nikolay", 6, null, "Leshev", 67.0 },
                    { 6, new DateTime(2001, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2018, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 172, "Ivan", 3, null, "Shabanov", 66.0 },
                    { 2, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2014, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 183, "Igor", 5, null, "Zhuravlev", 70.0 },
                    { 15, new DateTime(2004, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 170, "Valeria", 7, null, "Malahova", 52.0 },
                    { 12, new DateTime(1995, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2017, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 169, "Evgeniy", 6, null, "Baranin", 57.0 },
                    { 11, new DateTime(1989, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2012, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 166, "Alexey", 10, null, "Fedorov", 54.0 },
                    { 8, new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2016, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 173, "Anastasia", 6, "Antonovna", "Efimova", 60.0 },
                    { 3, new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2018, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 170, "Ivan", 2, null, "Pavlov", 66.0 },
                    { 4, new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2013, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 174, "Andrew", 10, "Sergeevich", "Solovyev", 72.0 },
                    { 17, new DateTime(1990, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2010, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 174, "Tatyana", 10, null, "Lelikova", 57.0 }
                });

            migrationBuilder.InsertData(
                table: "MedicalCertificates",
                columns: new[] { "Id", "ClinicName", "DateOfIssue", "Result", "StudentId" },
                values: new object[,]
                {
                    { 1, "Polyclinic 4", new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 17, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 },
                    { 11, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 9, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 20, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 16 },
                    { 14, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10 },
                    { 13, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9 },
                    { 10, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 8, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 },
                    { 18, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 14 },
                    { 5, "VODC", new DateTime(2021, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 19, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 15 },
                    { 16, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12 },
                    { 15, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11 },
                    { 12, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 7, "Polyclinic 1", new DateTime(2021, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 6, "Polyclinic 4", new DateTime(2020, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3 },
                    { 3, "Polyclinic 13", new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, "Polyclinic 4", new DateTime(2020, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 4, "VODC", new DateTime(2018, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 21, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 17 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificates_StudentId",
                table: "MedicalCertificates",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BoxingGroupId",
                table: "Students",
                column: "BoxingGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalCertificates");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "BoxingGroups");
        }
    }
}
