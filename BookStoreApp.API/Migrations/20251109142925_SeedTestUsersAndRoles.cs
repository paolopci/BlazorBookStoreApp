using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedTestUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "30d4f3ef-fc0d-4c19-a35f-40b8f6b59fed", null, "Administrator", "ADMINISTRATOR" },
                    { "37613aa2-4096-4ee8-b13d-c6b3ae21d04f", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8d92e972-f96d-4a6e-b2cb-a2cf8625995b", 0, "d3db3d33-086b-4f66-a7ec-0dedd4dae99d", "testr.admin@bookstore.com", true, "Testr", "Admin", false, null, "TESTR.ADMIN@BOOKSTORE.COM", "TESTR.ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEB9JcaF9S8auz6KZiMUDyJR4gW0r3NDGvmDCMbnyffXlsBF1bfzayMLpipjqT8OsOg==", null, false, "8b52c9cd-2e50-4f4c-8819-8c4efc8e1604", false, "testr.admin@bookstore.com" },
                    { "9d9d80f7-6fd3-4a61-9792-e58df7cbec5c", 0, "9f06e19e-0400-4536-9744-d1c119726c08", "testr.user@bookstore.com", true, "Testr", "User", false, null, "TESTR.USER@BOOKSTORE.COM", "TESTR.USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEN8jtICYFtb4uGpiGvXUuRHg4Ua6J2O6XsM23OSbQNtXHRo/wEnTLGT9Os2JXBRIdw==", null, false, "2bedacff-06a5-4be1-b140-7672683e312d", false, "testr.user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "30d4f3ef-fc0d-4c19-a35f-40b8f6b59fed", "8d92e972-f96d-4a6e-b2cb-a2cf8625995b" },
                    { "37613aa2-4096-4ee8-b13d-c6b3ae21d04f", "9d9d80f7-6fd3-4a61-9792-e58df7cbec5c" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "30d4f3ef-fc0d-4c19-a35f-40b8f6b59fed", "8d92e972-f96d-4a6e-b2cb-a2cf8625995b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "37613aa2-4096-4ee8-b13d-c6b3ae21d04f", "9d9d80f7-6fd3-4a61-9792-e58df7cbec5c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30d4f3ef-fc0d-4c19-a35f-40b8f6b59fed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37613aa2-4096-4ee8-b13d-c6b3ae21d04f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d92e972-f96d-4a6e-b2cb-a2cf8625995b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d9d80f7-6fd3-4a61-9792-e58df7cbec5c");
        }
    }
}
