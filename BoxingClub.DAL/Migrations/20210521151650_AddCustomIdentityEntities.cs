using Microsoft.EntityFrameworkCore.Migrations;

namespace BoxingClub.DAL.Migrations
{
    public partial class AddCustomIdentityEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserRole<string>",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserRole<string>", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "060342c3-9dc3-4597-bae1-9f19c991ebe9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00d3805f-9d96-4e28-8931-6071c6833a7b", "AQAAAAEAACcQAAAAEBNX5AceCCXlj+hopvxv9z/bgq70gBIxu1xelIKSVIsLLqsfEOcJju1Agsyqh4i5ng==", "632c2453-60b9-450e-91ee-1bc34cedd921" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f64ffe76-7d0f-43d7-bc1c-b72b028f699d", "AQAAAAEAACcQAAAAEPQ/YpKDWdkaMr9pvZ55eaJnGiS0D8uuf4vnLAKFi4Bu+7Wa4K0B0iOGC4PuF/JwwQ==", "8d94f63a-0cd7-4b83-96bf-c0d7655e6b35" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d4254a5-7782-4b9c-a987-42a83d30669a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34c11eb2-dff9-4e53-ac95-e50fce218148", "AQAAAAEAACcQAAAAENaL6oOR2H0cpq+EEdfCa1iUfmued/ICKFo5pntkpuyUQslUon4Y0YCIKqjLQC5EoA==", "03ade30c-3cfd-4c73-989b-bc3bee4e9c91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69a7246f-f25a-4f0d-b644-73de1fa42f42", "AQAAAAEAACcQAAAAEHKAeUKEDze2CQM2tZED8t9SqkpLy7TWHIs5CaGIt/aSetGKyKULmNlsVgQvsnALIw==", "513c2b38-5e4e-4474-8e08-6d0f40787320" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50a06a5-df07-4728-b6a0-93173c2ce4cf",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78a2d369-c976-4c0c-b454-cb4fc6217fb8", "AQAAAAEAACcQAAAAELigIBDYb9DyV9udqYPd+jc/65QhPi14WHCZArIwCr2NpcP+69OU9Eccjm4sS+C7Qg==", "37646ce0-6169-4afb-ba30-40067aaa0dae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda7dfec-9828-41b2-bd9c-53dccbef2bb8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97db3428-6252-4adc-82be-fd647b0cdf78", "AQAAAAEAACcQAAAAEEMCmfo3dSaPwTGpzm6few+wj9ihFCigNnlraYLzb7WH36nrBhOhXEm7XyKLwv1/Vg==", "5824ceff-87b9-4c92-b470-13e189a3873b" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "0578fa02-59b6-4d71-ac94-a4fa31899643", "Admin", "ADMIN" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "0bb5f543-5495-44af-958f-8540a09ea1db", "Manager", "MANAGER" },
                    { "db460306-31c6-457a-989e-9e4317be99b9", "c19f2a97-1ea5-4ce0-9102-a36292708cac", "User", "USER" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "ddd55c4c-eec8-4ab8-bc2d-463e28319460", "Coach", "COACH" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUserRole<string>",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "fda7dfec-9828-41b2-bd9c-53dccbef2bb8" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "2d4254a5-7782-4b9c-a987-42a83d30669a" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "19759de3-ce1d-4cfd-8340-4e64eb245eb4" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "060342c3-9dc3-4597-bae1-9f19c991ebe9" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "a50a06a5-df07-4728-b6a0-93173c2ce4cf" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.DropTable(
                name: "IdentityUserRole<string>");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "f2b8b99b-8db4-499a-92e0-e5873e7c1f27", "Admin", "ADMIN" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "84d77038-26da-46d4-9548-130a804840d3", "Manager", "MANAGER" },
                    { "db460306-31c6-457a-989e-9e4317be99b9", "b0c33778-5be2-420d-a9cf-06ded9b9f3fe", "User", "USER" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "381ad310-3f88-4b31-b61b-4c8aa2b87a77", "Coach", "COACH" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "060342c3-9dc3-4597-bae1-9f19c991ebe9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2e9f7eca-a524-43af-99a4-7277100f3a97", "AQAAAAEAACcQAAAAEHlHOALwGiXS7XAalkJKB/D7mfEMKhphoP69y1br2bf97ixI5lMLIejMkXDgNPRIQQ==", "54561360-a982-4bda-9b30-48e338f5ca65" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "19759de3-ce1d-4cfd-8340-4e64eb245eb4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa6b12fb-cefb-4e02-90d1-a24fbf1731b4", "AQAAAAEAACcQAAAAEG3vGOQ010FUuygNLG4DqUYHnqa1NYInqRgUmxfCg5dmQMeX1hyRzNNfhSC5NNdSmA==", "3c8c130c-8f37-48e0-95d6-cae31f96d09f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2d4254a5-7782-4b9c-a987-42a83d30669a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25cf79de-c326-41d4-9e0a-a73beadd9613", "AQAAAAEAACcQAAAAEBrFwWZJEOKVMBH6CXTA32st46V7iaNbnsRqik0pgsXEGEmwCnJjRnB+lJgHP0uHPQ==", "e4deb6a1-bcb5-4f27-b848-60bc03dded6c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92a4454a-f0e2-4de4-b93b-d0c3a4d6ec65", "AQAAAAEAACcQAAAAEGCVc5uJT1D+HcKBXdgoYp32p23gMUm0sfk8U/2TajMyaMab6InzN8oYEEBuYtaq8g==", "337226a2-d8ad-4ec6-9fc1-bce69eea3541" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a50a06a5-df07-4728-b6a0-93173c2ce4cf",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dcaba96b-16fe-4fcd-805c-c6eea9330e4c", "AQAAAAEAACcQAAAAEBrXgwTaf6Zs+3OYzniUlCXin+ZXeXDK+3j57jFvUOulkZDhTy/u385yWCPvZ39SxQ==", "2f174eff-6277-463d-b130-3c566a56fb49" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fda7dfec-9828-41b2-bd9c-53dccbef2bb8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5acc2f70-f875-4539-bf4f-17ab60cc6f22", "AQAAAAEAACcQAAAAEAwylYLdKHi1YUcFqsnjBr4CA+itbu/hXw0DScNf98RXlpNS9RLw2yaOBWfO+qLNPg==", "6e9264e5-ded3-4174-8322-b71639136f63" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "291c0120-8c27-47c5-83fe-9d7deb36f73c", "7dc730f1-78ec-41f5-a079-7d5e5d6b39ef" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "fda7dfec-9828-41b2-bd9c-53dccbef2bb8" },
                    { "7bb8b4c7-de76-4b77-b5cf-ce4ef11d83a6", "2d4254a5-7782-4b9c-a987-42a83d30669a" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "19759de3-ce1d-4cfd-8340-4e64eb245eb4" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "060342c3-9dc3-4597-bae1-9f19c991ebe9" },
                    { "8da509ca-2005-457d-8ca3-105792f04013", "a50a06a5-df07-4728-b6a0-93173c2ce4cf" }
                });
        }
    }
}
