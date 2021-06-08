using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class SeedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MedicalCertificates",
                columns: new[] { "Id", "ClinicName", "DateOfIssue", "Result", "StudentId" },
                values: new object[] { 8, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 4 });

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "BoxingGroupId",
                value: 2);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Gender", "Height", "Name", "NumberOfFights", "Patronymic", "Surname", "Weight" },
                values: new object[,]
                {
                    { 17, new DateTime(1990, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2010, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 174, "Tatyana", 10, null, "Lelikova", 57.0 },
                    { 16, new DateTime(2000, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2016, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 180, "Julia", 5, null, "Belikova", 63.0 },
                    { 14, new DateTime(2003, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2012, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 165, "Nikolay", 6, null, "Leshev", 67.0 },
                    { 13, new DateTime(2003, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2015, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 170, "Alexander", 5, null, "Kirillov", 74.0 },
                    { 12, new DateTime(1995, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2017, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 169, "Evgeniy", 6, null, "Baranin", 57.0 },
                    { 11, new DateTime(1989, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2012, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 166, "Alexey", 10, null, "Fedorov", 54.0 },
                    { 10, new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2016, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 178, "Dmitry", 4, "Dmitrievich", "Kustovinov", 69.0 },
                    { 9, new DateTime(2000, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2020, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 180, "Viktoria", 1, null, "Narkevich", 60.0 },
                    { 8, new DateTime(2000, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2016, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 173, "Anastasia", 6, "Antonovna", "Efimova", 60.0 },
                    { 7, new DateTime(2000, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2020, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 175, "Vlad", 1, "Sergeevich", "Safonov", 74.0 },
                    { 6, new DateTime(2001, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2018, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 172, "Ivan", 3, null, "Shabanov", 66.0 },
                    { 5, new DateTime(1998, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2018, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 165, "Vika", 4, null, "Zhukova", 55.0 },
                    { 15, new DateTime(2004, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 170, "Valeria", 7, null, "Malahova", 52.0 }
                });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "International  Boxing Competition");

            migrationBuilder.InsertData(
                table: "MedicalCertificates",
                columns: new[] { "Id", "ClinicName", "DateOfIssue", "Result", "StudentId" },
                values: new object[,]
                {
                    { 9, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 10, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6 },
                    { 11, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7 },
                    { 12, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 8 },
                    { 13, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 9 },
                    { 14, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10 },
                    { 15, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 11 },
                    { 16, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 12 },
                    { 17, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 13 },
                    { 18, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 14 },
                    { 19, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 15 },
                    { 20, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 16 },
                    { 21, "VODC", new DateTime(2021, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 17 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "MedicalCertificates",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4,
                column: "BoxingGroupId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "International Women's Boxing Competition");
        }
    }
}
