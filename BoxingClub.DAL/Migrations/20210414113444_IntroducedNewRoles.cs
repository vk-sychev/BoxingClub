using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class IntroducedNewRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxingGroups_Coaches_CoachId",
                table: "BoxingGroups");

            migrationBuilder.DropTable(
                name: "Coaches");

/*            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5d73a819-a418-47b8-8c2d-c2e2ff6974db", "a0c933c1-2c8e-4919-a9b1-4187941b48dd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5d73a819-a418-47b8-8c2d-c2e2ff6974db", "c00ded26-e898-462b-9374-e17433d618c9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "43ee9747-ff30-45e7-a3b6-3d1df4274879", "eb4bf1eb-37d2-4128-9e58-3debcaae344c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43ee9747-ff30-45e7-a3b6-3d1df4274879");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d73a819-a418-47b8-8c2d-c2e2ff6974db");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0c933c1-2c8e-4919-a9b1-4187941b48dd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c00ded26-e898-462b-9374-e17433d618c9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb4bf1eb-37d2-4128-9e58-3debcaae344c");*/

            migrationBuilder.AlterColumn<string>(
                name: "CoachId",
                table: "BoxingGroups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "BornDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
/*                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "28c18050-2080-4daa-85d7-63181f16ca0e", "Admin", "ADMIN" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "4bce291f-b263-4af2-b909-b34f83fe140e", "Manager", "MANAGER" },*/
                    { "db460306-31c6-457a-989e-9e4317be99b9", "f22b0614-a9a2-4d39-bf69-4d569abcb44b", "User", "USER" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "1aa92723-3e34-4d8b-a67a-fdafa6f28345", "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BornDate", "ConcurrencyStamp", "Description", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Patronymic", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    /*{ "fda7dfec-9828-41b2-bd9c-53dccbef2bb8", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "7d6270c5-62cb-4598-90b8-712a18c47f88", null, "Manager1@gmail.com", false, false, null, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEBIOTZu5j+mlmvOT9mA5QEoaiHMMrUuhQpHVWur5GVxnGHdzvvN/xBzpjDtrldJcug==", null, null, false, "b934d20c-ebb8-4cde-b743-1e806647e5a3", null, false, "Manager1" },
                    { "2d4254a5-7782-4b9c-a987-42a83d30669a", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "0eef8bfd-0bfb-43b7-9888-6ddc8f37511d", null, "Manager2@gmail.com", false, false, null, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEErkYh50oJwNHDYsoo9qirg+3FOHtbvNZa1MS7GQ6YgF4MS9Hvd00lg3pd6w7d/0dA==", null, null, false, "95d7baf6-da67-4277-bb6c-bfa9f58d3825", null, false, "Manager2" },
                    { "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef", 0, new DateTime(2000, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "9d877141-b47b-434b-b13d-08f4c7ca5447", null, "Admin@gmail.com", false, false, null, "Vasya", "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEcYBNPkGgCxhBa7RHEq4qdd3A1mHSZuXugT1e02Eu4rdI23i4oSD/v5WnWKMjXPnw==", "Konstantinovich", null, false, "08a1295e-5f60-41f2-a601-75d17a143c1f", "Sychev", false, "Admin" },*/
                    { "19759de3-ce1d-4cfd-8340-4e64eb245eb4", 0, new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "8ff9d236-d0d3-486b-8099-64260b7b2901", "CMS in boxing", "Coach1@gmail.com", false, false, null, "Pavel", "COACH1@GMAIL.COM", "COACH1", "AQAAAAEAACcQAAAAEO37bAmZ57QgDNQqz5hCm0fPpq5Y0e2RRUAajHxi7Dt5d+LJD6VfBqDZVxo4yJiHZg==", "Nikolayevich", null, false, "c4f444d9-57c0-4aa4-ac8c-b24b9deddf89", "Dorochin", false, "Coach1" },
                    { "060342c3-9dc3-4597-bae1-9f19c991ebe9", 0, new DateTime(1991, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ce4a3e53-a924-4c12-8083-229161279b44", "CMS in boxing", "Coach2@gmail.com", false, false, null, "Vlad", "COACH2@GMAIL.COM", "COACH2", "AQAAAAEAACcQAAAAEGEwVprG6C6orWnK1pdm4+llqFMyw1WKBOG/+YoNejaeNsGLgM4DbcsfTUbh7g6m/w==", "Nikolayevich", null, false, "66015801-47a3-4d61-aeb2-60dca6f2ec5a", "Dorochin", false, "Coach2" },
                    { "a50a06a5-df07-4728-b6a0-93173c2ce4cf", 0, new DateTime(1970, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "45ee15bb-8cf5-4043-a91d-86a798c10874", "MS in boxing", "Coach3@gmail.com", false, false, null, "Sergey", "COACH3@GMAIL.COM", "COACH3", "AQAAAAEAACcQAAAAEA/wCoxlJYH6Hpr05F6O8W4B/OgL5lkLqGURn7J4Vts53MypelCGu7/xNGh0J6N8vw==", null, null, false, "988ae349-ed99-4a46-9cc4-77706a9be935", "Goncharov", false, "Coach3" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    //{ "291c0120-8c27-47c5-83fe-9d7deb36f73c", "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef" },
                    //{ "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "fda7dfec-9828-41b2-bd9c-53dccbef2bb8" },
                    //{ "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "2d4254a5-7782-4b9c-a987-42a83d30669a" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "19759de3-ce1d-4cfd-8340-4e64eb245eb4" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "060342c3-9dc3-4597-bae1-9f19c991ebe9" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "a50a06a5-df07-4728-b6a0-93173c2ce4cf" }
                });

            migrationBuilder.UpdateData(
                table: "BoxingGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoachId",
                value: "19759de3-ce1d-4cfd-8340-4e64eb245eb4");

            migrationBuilder.UpdateData(
                table: "BoxingGroups",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoachId",
                value: "19759de3-ce1d-4cfd-8340-4e64eb245eb4");

            migrationBuilder.UpdateData(
                table: "BoxingGroups",
                keyColumn: "Id",
                keyValue: 3,
                column: "CoachId",
                value: "060342c3-9dc3-4597-bae1-9f19c991ebe9");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxingGroups_AspNetUsers_CoachId",
                table: "BoxingGroups",
                column: "CoachId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxingGroups_AspNetUsers_CoachId",
                table: "BoxingGroups");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db460306-31c6-457a-989e-9e4317be99b9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8da509ca-2005-457d-8ca3-105792f04013", "060342c3-9dc3-4597-bae1-9f19c991ebe9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8da509ca-2005-457d-8ca3-105792f04013", "19759de3-ce1d-4cfd-8340-4e64eb245eb4" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "2d4254a5-7782-4b9c-a987-42a83d30669a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "8da509ca-2005-457d-8ca3-105792f04013", "a50a06a5-df07-4728-b6a0-93173c2ce4cf" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "fda7dfec-9828-41b2-bd9c-53dccbef2bb8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "291c0120-8c27-47c5-83fe-9d7deb36f73c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8da509ca-2005-457d-8ca3-105792f04013");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "060342c3-9dc3-4597-bae1-9f19c991ebe9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19759de3-ce1d-4cfd-8340-4e64eb245eb4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d4254a5-7782-4b9c-a987-42a83d30669a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50a06a5-df07-4728-b6a0-93173c2ce4cf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda7dfec-9828-41b2-bd9c-53dccbef2bb8");

            migrationBuilder.DropColumn(
                name: "BornDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CoachId",
                table: "BoxingGroups",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BornDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43ee9747-ff30-45e7-a3b6-3d1df4274879", "c4c5d049-123e-4ee2-9c04-60f0e76cd74d", "Admin", "ADMIN" },
                    { "5d73a819-a418-47b8-8c2d-c2e2ff6974db", "07b09e58-4db6-497f-8036-bbb64550dcf8", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c00ded26-e898-462b-9374-e17433d618c9", 0, "50483227-be18-497f-a39b-75fc2b7146a3", "Manager1@gmail.com", false, false, null, "MANAGER1@GMAIL.COM", "MANAGER1", "AQAAAAEAACcQAAAAEOKDAa6REK0q7uDBK8GhpOMjxdHyZqtTv5VQKXGDM4EcIFC2OS3CBnYfOw8z5yjeng==", null, false, "fe792ace-fe9c-4e43-af4a-09c044735b4c", false, "Manager1" },
                    { "a0c933c1-2c8e-4919-a9b1-4187941b48dd", 0, "17ca5a78-61bb-495d-adfe-a1da7511f1ec", "Manager2@gmail.com", false, false, null, "MANAGER2@GMAIL.COM", "MANAGER2", "AQAAAAEAACcQAAAAEAxGqUSjNMbDSS9nYVitNaRqPk3h5LYHF/wmE3/FCtSEyARXCFfkhZHnNVMF9odQIw==", null, false, "21418062-e54c-4837-abbb-be87c8f070ec", false, "Manager2" },
                    { "eb4bf1eb-37d2-4128-9e58-3debcaae344c", 0, "66b445c5-58f9-43b9-a13b-f13952aa94d6", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEAwekAU7cT3iqpDqkuJECQzwKl9l3enwlvq8vnllGD/i3Nr/OS8DEQC8mCWKoqTW9Q==", null, false, "f8d1ba61-21cb-45f7-848f-71d52896a768", false, "Admin" }
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
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "5d73a819-a418-47b8-8c2d-c2e2ff6974db", "c00ded26-e898-462b-9374-e17433d618c9" },
                    { "5d73a819-a418-47b8-8c2d-c2e2ff6974db", "a0c933c1-2c8e-4919-a9b1-4187941b48dd" },
                    { "43ee9747-ff30-45e7-a3b6-3d1df4274879", "eb4bf1eb-37d2-4128-9e58-3debcaae344c" }
                });

            migrationBuilder.UpdateData(
                table: "BoxingGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "CoachId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "BoxingGroups",
                keyColumn: "Id",
                keyValue: 2,
                column: "CoachId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "BoxingGroups",
                keyColumn: "Id",
                keyValue: 3,
                column: "CoachId",
                value: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_BoxingGroups_Coaches_CoachId",
                table: "BoxingGroups",
                column: "CoachId",
                principalTable: "Coaches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
