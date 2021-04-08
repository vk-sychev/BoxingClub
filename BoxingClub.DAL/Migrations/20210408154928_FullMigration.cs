using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class FullMigration : Migration
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
                    { "87dbcf4c-9669-4b1a-a742-231d26146c90", "706ccc8a-b6c0-4b43-9694-ebfa956a118f", "Admin", "ADMIN" },
                    { "8b295461-1a6c-45e7-a03b-9f73951bb63c", "4d6235aa-6f0d-4159-af49-910226fd878e", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "541b65ba-b24f-4075-b8bd-99caf784e5d3", 0, "9a827a1f-665c-4a17-8623-68a502607280", "Manager1@gmail.com", false, false, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEER6cjXYqFOj0JDhRkKMlCZYzdyuNxE5wyxKh/v859jaTXvKNiB48i+P3y2/RI2zYw==", null, false, "752e272d-3728-4f5c-aab9-39551bbead47", false, "Manager1" },
                    { "9d1b9166-25ce-445c-8a9b-bf8707b704f4", 0, "f701a43d-b4d4-4ed3-9bfc-88a3c4d3624d", "Manager2@gmail.com", false, false, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEGdtpPhixSSEx7H/P7fqh+mYLxY9tkc5mGqXzWDwMQixTz3HqnvveFtHBa78LX7Jbg==", null, false, "73a7690d-4177-4840-8346-9eafbe2cb218", false, "Manager2" },
                    { "2df7bdf4-2d02-4353-a151-bad2a8964169", 0, "54901665-52b5-442e-a61d-9740f9a62bb0", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEOfhynGNfGn5cIgO59xevg5QCx56nDMPgnIqz/pTzYzS4DEBDBHru/an3NZ7EZd6Gg==", null, false, "c719f9cf-af67-4772-8f5a-f8392288417a", false, "Admin" }
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
                values: new object[] { 4, new DateTime(2000, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 176, "Andrew", "Sergeevich", "Solovyev", 73.0 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "8b295461-1a6c-45e7-a03b-9f73951bb63c", "541b65ba-b24f-4075-b8bd-99caf784e5d3" },
                    { "8b295461-1a6c-45e7-a03b-9f73951bb63c", "9d1b9166-25ce-445c-8a9b-bf8707b704f4" },
                    { "87dbcf4c-9669-4b1a-a742-231d26146c90", "2df7bdf4-2d02-4353-a151-bad2a8964169" }
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
                values: new object[] { 1, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 175, "Vasiliy", "Konstantinovich", "Sychev", 88.0 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 3, new DateTime(2001, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 175, "Ivan", null, "Pavlov", 81.0 });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BornDate", "BoxingGroupId", "DateOfEntry", "Height", "Name", "Patronymic", "Surname", "Weight" },
                values: new object[] { 2, new DateTime(1991, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Igor", null, "Zhuravlev", 0.0 });

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
