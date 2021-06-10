using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.DAL.Implementation.Migrations
{
    public partial class Initial : Migration
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "c63c6960-f83b-4483-b37f-002cb8fbc321", "Admin", "ADMIN" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "98b44946-7e62-425e-bc15-5423f9861031", "Manager", "MANAGER" },
                    { "db460306-31c6-457a-989e-9e4317be99b9", "a3c5dc55-16e4-4b63-9ce2-8c032309a57e", "User", "USER" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "8e157458-5c21-42a7-836e-9bb7e4f1d3ae", "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BornDate", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "fda7dfec-9828-41b2-bd9c-53dccbef2bb8", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "03dce5a5-1a46-4252-a0cd-3c572bf74732", null, "Manager1@gmail.com", false, false, null, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEIHojiGKwNgqcYXCzsFc/7DOfkRf8NporFLIPUNSfF2aKqBekBeBgZa/56/kiFnEdg==", null, null, false, "a2de1cce-a784-428f-9de0-f5eb40e49d0d", null, false, "Manager1" },
                    { "2d4254a5-7782-4b9c-a987-42a83d30669a", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "26577900-a538-491b-92f6-8862528e5adb", null, "Manager2@gmail.com", false, false, null, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEKadT44EsizBV9P/hDwGdc2NM/aAetdpExMO/CK480E1sigS2LN1k+oOq/2KJ2Bv3A==", null, null, false, "d6ff8e8e-2314-4b25-9396-9a8b64f6ae44", null, false, "Manager2" },
                    { "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef", 0, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "d9c9a02c-da1e-45ef-b638-0568a04c2202", null, "Admin@gmail.com", false, false, null, "Vasya", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOqDX55kTWgixwGkXdMrtAeqBnXL4nKoUZp/eLZiXPn9/YoXupMz6LT+I0RQC0ctdg==", "Konstantinovich", null, false, "be0d2e62-5155-414a-ac58-38de4fb1983a", "Sychev", false, "Admin" },
                    { "19759de3-ce1d-4cfd-8340-4e64eb245eb4", 0, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "2454270e-decd-4923-8861-30dd31253c2e", "CMS in boxing", "Coach1@gmail.com", false, false, null, "Pavel", "COACH1@GMAIL.COM", "COACH1", "AQAAAAEAACcQAAAAED5QOdL96URXUhcsXLEChVmkRvTus7x/kmJevaYIqU2esCp1ZzAxdVt85ByqKp8Jag==", "Nikolayevich", null, false, "c16b4149-0250-4fbb-8d0e-05649065ce6c", "Dorochin", false, "Coach1" },
                    { "060342c3-9dc3-4597-bae1-9f19c991ebe9", 0, new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "4dddd32d-b021-4827-9491-85660bd62236", "CMS in boxing", "Coach2@gmail.com", false, false, null, "Vlad", "COACH2@GMAIL.COM", "COACH2", "AQAAAAEAACcQAAAAEHPlRHhdDmCFeDGYd0XgCac/S8LEkAa+v7gkQCOtTQFJ+LSUz+/YXEvs1/ATCzCpYw==", "Nikolayevich", null, false, "3a4b5008-f2b1-4f4d-b810-1d0c9a44f910", "Dorochin", false, "Coach2" },
                    { "a50a06a5-df07-4728-b6a0-93173c2ce4cf", 0, new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "fb81771a-fcec-4f92-948d-fba6166a4597", "MS in boxing", "Coach3@gmail.com", false, false, null, "Sergey", "COACH3@GMAIL.COM", "COACH3", "AQAAAAEAACcQAAAAEFnYQqFTvRAiLicPcNot6YVRHzFTrggbUM1H+8zK686g+MGZ3v1pjBM0d6OvEykPog==", null, null, false, "594d112a-da0a-4e2f-9e49-564952e9209f", "Goncharov", false, "Coach3" }
                });

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
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
