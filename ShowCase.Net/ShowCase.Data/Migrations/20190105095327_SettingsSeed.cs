using Microsoft.EntityFrameworkCore.Migrations;

namespace ShowCase.Data.Migrations
{
    public partial class SettingsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1f01161-fd90-43a0-9d73-2c8b7395ada8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4097cb57-9762-45aa-8be7-de600e238c19", "9b7c2f92-1240-4552-a485-d6ef0ac5427d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "FooterContent", "LogoUrl" },
                values: new object[] { 1, "", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4097cb57-9762-45aa-8be7-de600e238c19");

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1f01161-fd90-43a0-9d73-2c8b7395ada8", "82522199-ac4b-4d16-905b-00b8cd6d80e5", "Admin", "ADMIN" });
        }
    }
}
