using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class PCmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoxingGroups_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    DateOfEntry = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "6ab550b8-7392-4793-b87f-672bea80a163", "Admin", "ADMIN" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "84f84536-55ff-4715-bea3-afb97dac9d3d", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "fda7dfec-9828-41b2-bd9c-53dccbef2bb8", 0, "7049f435-9b35-481a-b97d-da94c627f75a", "Manager1@gmail.com", false, false, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEALHcgn10nfulcxov9gtfDCUG3qDLqpiWqTMy/AV3hpO2RUBZ7OOlpo0cSH0QdLEEQ==", null, false, "1af415f4-2af7-431b-8dfe-92c225c9b64d", false, "Manager1" },
                    { "2d4254a5-7782-4b9c-a987-42a83d30669a", 0, "57ef55a5-f78d-465f-ae96-a8df7d15ca54", "Manager2@gmail.com", false, false, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEJQvbpWWktqqGThy551mr/4m9iZA2LpcblYIdBWz5pHNsAV0Jsim/ROwZMlhjNZWVA==", null, false, "d49c32c5-6604-4803-9150-cdecabf91cde", false, "Manager2" },
                    { "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef", 0, "6499a3ce-6a59-4bd4-ae20-d44f5639aafb", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEKmo3W36SXXvAiJXBmbznlPR/HzbIB7OZnGCC+s2U35+4WAi5W1hiT5PT9L2Rj/bPQ==", null, false, "43ecb5aa-e093-4871-b220-9f241eee8fe0", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "BornDate", "Description", "Name", "Patronymic", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CMS in boxing", "Pavel", "Nikolayevich", "Dorochin" },
                    { 2, new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "CMS in boxing", "Vlad", "Nikolayevich", "Dorochin" },
                    { 3, new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "MS in boxing", "Sergey", null, "Goncharov" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 4, new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2021, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 176, "Andrew", "Sergeevich", "Solovyev", 73.0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "fda7dfec-9828-41b2-bd9c-53dccbef2bb8" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "2d4254a5-7782-4b9c-a987-42a83d30669a" },
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef" }
                });

            migrationBuilder.InsertData(
                table: "BoxingGroups",
                columns: new[] { "Id", "CoachId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Vityaz" },
                    { 2, 1, "Warrior" },
                    { 3, 2, "Sarmat" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 1, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 175, "Vasiliy", "Konstantinovich", "Sychev", 88.0 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 3, new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 175, "Ivan", null, "Pavlov", 81.0 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 2, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 180, "Igor", null, "Zhuravlev", 87.0 });

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
                name: "IX_Students_BoxingGroupId",
                table: "Students",
                column: "BoxingGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Students");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BoxingGroups");

            migrationBuilder.DropTable(
                name: "Coaches");
        }
    }
}
