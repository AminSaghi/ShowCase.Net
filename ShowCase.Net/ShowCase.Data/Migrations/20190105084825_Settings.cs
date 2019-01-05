using Microsoft.EntityFrameworkCore.Migrations;

namespace ShowCase.Data.Migrations
{
    public partial class Settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ccb7c483-b367-4d07-983a-1f82f3d71ac4");

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LogoUrl = table.Column<string>(nullable: true),
                    FooterContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c1f01161-fd90-43a0-9d73-2c8b7395ada8", "82522199-ac4b-4d16-905b-00b8cd6d80e5", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1f01161-fd90-43a0-9d73-2c8b7395ada8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ccb7c483-b367-4d07-983a-1f82f3d71ac4", "e737eac8-d43c-4975-a06f-63387df95228", "Admin", "ADMIN" });
        }
    }
}
