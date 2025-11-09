using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserHashFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d92e972-f96d-4a6e-b2cb-a2cf8625995b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b1e0644-7e1a-4da9-a71e-269f47649a8f", "AQAAAAEAACcQAAAAEAARIjNEVWZ3iJmqu8zd7v/L4tXE3Fx6RFiupoMZ+S+qviTO7Vw8RS3Of/OTxhcdjg==", "e8ec48b0-bd3d-4929-8ea8-62215eee9de4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d9d80f7-6fd3-4a61-9792-e58df7cbec5c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a834e7d-9df6-4a8e-aa7e-c424bd16018f", "AQAAAAEAACcQAAAAEP/u3cy7qpmId2ZVRDMiEQDix52n/rnAe4X/9vYSVT9RhE0ar7SDxeqMkGgFLv2E+Q==", "ba6a606a-0dd2-46ec-bc22-30b02c06c748" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8d92e972-f96d-4a6e-b2cb-a2cf8625995b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d3db3d33-086b-4f66-a7ec-0dedd4dae99d", "AQAAAAIAAYagAAAAEB9JcaF9S8auz6KZiMUDyJR4gW0r3NDGvmDCMbnyffXlsBF1bfzayMLpipjqT8OsOg==", "8b52c9cd-2e50-4f4c-8819-8c4efc8e1604" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9d9d80f7-6fd3-4a61-9792-e58df7cbec5c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f06e19e-0400-4536-9744-d1c119726c08", "AQAAAAIAAYagAAAAEN8jtICYFtb4uGpiGvXUuRHg4Ua6J2O6XsM23OSbQNtXHRo/wEnTLGT9Os2JXBRIdw==", "2bedacff-06a5-4be1-b140-7672683e312d" });
        }
    }
}
