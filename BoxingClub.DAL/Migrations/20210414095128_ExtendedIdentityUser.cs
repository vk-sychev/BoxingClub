using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Implementation.Migrations
{
    /*
    public partial class ExtendedIdentityUser : Migration
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

            migrationBuilder.CreateTable(
                name: "BoxingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoachId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    { "db460306-31c6-457a-989e-9e4317be99b9", "0defa253-5b52-40a6-b807-08e805b3768a", "Admin", "ADMIN" },
                    { "2dc83959-9dca-4cf3-855e-809741ce1e0d", "588857b3-4c40-4719-94e4-452ed3d547b1", "Manager", "MANAGER" },
                    { "5ca8984c-31f5-4883-b68e-d9ff541fbfb2", "be5b604c-f2dc-48a2-b6e6-a891d4e8878a", "User", "USER" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "442c310a-e9db-4b21-83cc-5f26a31cefaa", "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BornDate", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "5189818d-2b4c-4509-88cd-7d83d9f1fd91", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "adcdd681-36fd-4e31-be4c-a0d83248489a", null, "Manager1@gmail.com", false, false, null, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEG3T/NnozQkRpUNejb8CS3Wf+AFI/FvfXA8sj4T1tEGyyZ18/8lI6CnbSoT77SCdUQ==", null, null, false, "47a5c985-7765-4581-afc1-a8f91b14beb0", null, false, "Manager1" },
                    { "532e9d3b-8503-4903-9575-99e70457d131", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2af03aac-a6d9-4a67-80c5-ffebe7f64ba8", null, "Manager2@gmail.com", false, false, null, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEKiyOOKJrBGLRDYYADbN8UUhNfYXmsFtaxfbSURLGf8SS4PVJ60q8XawdSeITvU+ww==", null, null, false, "ddd6cf09-106d-460f-b790-9ed01553b699", null, false, "Manager2" },
                    { "b65d237d-aa7d-409b-be76-3143f959c5f0", 0, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "f741af9d-f554-420c-90c8-966e408f13e0", null, "Admin@gmail.com", false, false, null, "Vasya", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEPbKEr8sWmSvMFtLoHA2A/J75WGQtOl0gtoZIvfw1vcaBZC9gyBRjPUxgyu1mChl/Q==", "Konstantinovich", null, false, "825735b4-6fa4-4564-b153-7b3d412b043b", "Sychev", false, "Admin" },
                    { "19759de3-ce1d-4cfd-8340-4e64eb245eb4", 0, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "f25f5df4-3115-4862-978b-0c2ca1885f8b", "CMS in boxing", "Coach1@gmail.com", false, false, null, "Pavel", "COACH1@GMAIL.COM", "COACH1", "AQAAAAEAACcQAAAAEA6yp+FMcAwMcRJmmq8f7Q/+9NUJ/v9XnrQnNQe8b+snx8+fF6kQOvmVXLap4rjtqQ==", "Nikolayevich", null, false, "a1eca29c-eb29-431c-968c-b415a9409a02", "Dorochin", false, "Coach1" },
                    { "060342c3-9dc3-4597-bae1-9f19c991ebe9", 0, new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "75ed5f26-91e5-4430-8400-1583125c76ea", "CMS in boxing", "Coach2@gmail.com", false, false, null, "Vlad", "COACH2@GMAIL.COM", "COACH2", "AQAAAAEAACcQAAAAECYJ6ir9Cr/zsTBqlcGaWebIhDzIA8Bo9JDc8xVlgUSYPLeDtnu8Rpb9uGfawykzJA==", "Nikolayevich", null, false, "64180435-d042-4109-8697-f5eece9835c3", "Dorochin", false, "Coach2" },
                    { "a50a06a5-df07-4728-b6a0-93173c2ce4cf", 0, new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ceb819ea-4c0e-4d52-9b7e-a73eb2af8ac5", "MS in boxing", "Coach3@gmail.com", false, false, null, "Sergey", "COACH3@GMAIL.COM", "COACH3", "AQAAAAEAACcQAAAAEOWThQhPNJrnt4UczkcOqfU5kXlYLdpw8b4Jodp9UrK0srZaPN+N9MGJYkwz8rLJvA==", null, null, false, "03922e60-a760-4c7d-a201-566620987208", "Goncharov", false, "Coach3" }
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
                    { "db460306-31c6-457a-989e-9e4317be99b9", "b65d237d-aa7d-409b-be76-3143f959c5f0" },
                    { "2dc83959-9dca-4cf3-855e-809741ce1e0d", "5189818d-2b4c-4509-88cd-7d83d9f1fd91" },
                    { "2dc83959-9dca-4cf3-855e-809741ce1e0d", "532e9d3b-8503-4903-9575-99e70457d131" },
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
                name: "BoxingGroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
    */
}
