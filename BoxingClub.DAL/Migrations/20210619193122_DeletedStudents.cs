using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class DeletedStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TournamentRequests_Students_StudentId",
                table: "TournamentRequests");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MedicalCertificates");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "BoxingGroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_TournamentRequests_StudentId",
                table: "TournamentRequests");

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsMedCertificateRequired",
                value: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoxingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxingGroups_AspNetUsers_CoachId",
                        column: x => x.CoachId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BoxingGroupId = table.Column<int>(type: "int", nullable: true),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfFights = table.Column<int>(type: "int", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_BoxingGroups_BoxingGroupId",
                        column: x => x.BoxingGroupId,
                        principalTable: "BoxingGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "78b107fd-eead-4499-a1d5-c27761807721", "Admin", "ADMIN" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "3e3a2ab6-aa12-40f0-894c-83895ff25325", "Manager", "MANAGER" },
                    { "db460306-31c6-457a-989e-9e4317be99b9", "1d13fec8-2e7f-4e53-940d-a88c76d7f3c0", "User", "USER" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "0f697910-a781-42f5-9a67-dcb556af3eaa", "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BornDate", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "fda7dfec-9828-41b2-bd9c-53dccbef2bb8", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a1454166-59ef-4c1d-a9c6-6744e2b6911f", null, "Manager1@gmail.com", false, false, null, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEPzshqTCxr9MPQSgzwFtplcOWIhiXYny6oHhn3011Hho0lGZ4AxY6flbPuWA0+u5Aw==", null, null, false, "a1c4196f-772c-4490-b974-9a2a83009045", null, false, "Manager1" },
                    { "2d4254a5-7782-4b9c-a987-42a83d30669a", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9fd6a539-b039-4210-b565-b1dbd08a729b", null, "Manager2@gmail.com", false, false, null, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEBliWmfc+0AFHX7agr36Xs6n3cQmIzGHALsQH0aiAY+1t2FYFZTvVrRXawvhLWr6jA==", null, null, false, "6ba0b0b5-43b4-4261-aa9f-ac525a079baa", null, false, "Manager2" },
                    { "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef", 0, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "a90e2c4c-7a91-49f9-b6ad-d198441d0633", null, "Admin@gmail.com", false, false, null, "Vasya", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAELopQt7N3tFY0PsyGbY4g9/R7bYGplDWbCH9PpUqEwta7qyBeKMBYJhshhj8gm4mOw==", "Konstantinovich", null, false, "7529c4d1-799d-4ec0-84d6-81b11f6094e6", "Sychev", false, "Admin" },
                    { "19759de3-ce1d-4cfd-8340-4e64eb245eb4", 0, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "b9cc9093-640e-40c4-8644-9c4ae347d7b8", "CMS in boxing", "Coach1@gmail.com", false, false, null, "Pavel", "COACH1@GMAIL.COM", "COACH1", "AQAAAAEAACcQAAAAEAzeWKMHP6E4yPwGrsmyQtPXlV4z4V51WvpJGpaTRw9p1hnfX/FSf8aFv/l3hpxC6g==", "Nikolayevich", null, false, "27454ca9-7e17-471c-9a94-bed25237f065", "Dorochin", false, "Coach1" },
                    { "060342c3-9dc3-4597-bae1-9f19c991ebe9", 0, new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "5782eb47-4961-4715-8662-bed778e01ba7", "CMS in boxing", "Coach2@gmail.com", false, false, null, "Vlad", "COACH2@GMAIL.COM", "COACH2", "AQAAAAEAACcQAAAAEChH1xDnw5jdkzXybtJFLmRlNEuYdTljKyBBgRJp38UJnG2aC8HXThrtilppOsH5NA==", "Nikolayevich", null, false, "d6f9c118-8068-42cc-983f-b425a94acfef", "Dorochin", false, "Coach2" },
                    { "a50a06a5-df07-4728-b6a0-93173c2ce4cf", 0, new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7b6b2279-708a-43ef-b9cc-6806e8db2ce8", "MS in boxing", "Coach3@gmail.com", false, false, null, "Sergey", "COACH3@GMAIL.COM", "COACH3", "AQAAAAEAACcQAAAAEMVTmezB5fMgtPnmDJzkLV4vNk83wXNRfJv7hJncokeMfrG3dzA+77v1/SDVDG6bjA==", null, null, false, "798461f7-884b-4fe7-abd3-61b7a2e42dd2", "Goncharov", false, "Coach3" }
                });

            migrationBuilder.UpdateData(
                table: "Tournaments",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsMedCertificateRequired",
                value: true);

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "fda7dfec-9828-41b2-bd9c-53dccbef2bb8" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "2d4254a5-7782-4b9c-a987-42a83d30669a" },
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "19759de3-ce1d-4cfd-8340-4e64eb245eb4" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "060342c3-9dc3-4597-bae1-9f19c991ebe9" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "a50a06a5-df07-4728-b6a0-93173c2ce4cf" }
                });

            migrationBuilder.InsertData(
                table: "BoxingGroups",
                columns: new[] { "Id", "CoachId", "Name" },
                values: new object[,]
                {
                    { 1, "19759de3-ce1d-4cfd-8340-4e64eb245eb4", "Vityaz" },
                    { 2, "19759de3-ce1d-4cfd-8340-4e64eb245eb4", "Warrior" },
                    { 3, "060342c3-9dc3-4597-bae1-9f19c991ebe9", "Sarmat" }
                });

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
                name: "IX_TournamentRequests_StudentId",
                table: "TournamentRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BoxingGroups_CoachId",
                table: "BoxingGroups",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCertificates_StudentId",
                table: "MedicalCertificates",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_BoxingGroupId",
                table: "Students",
                column: "BoxingGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TournamentRequests_Students_StudentId",
                table: "TournamentRequests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
