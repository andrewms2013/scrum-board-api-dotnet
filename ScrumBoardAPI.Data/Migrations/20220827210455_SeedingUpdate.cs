using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class SeedingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "d5b825fb-9241-4cef-ac23-321c2effbaf9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e6df610e-8bf4-40c7-9e35-1b75e0ac76f3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "48ac3eba-f103-4e4e-98cc-ae393f588bdf", "AQAAAAEAACcQAAAAEGUuopabKpzVzsn4fiKILzwLqcIBYJgaQ0bch0XIGjegBcOCUUwu0w06kxYxKTbbig==", "af2c375f-56d3-4350-9918-1f298da4222c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "88dc5e78-ea5c-4a92-97bc-e3757396041f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "fa46e43a-7635-49cc-bc23-74e91748eb02");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8e2a385-44c4-4c82-a9b4-f3cf151d2207", "AQAAAAEAACcQAAAAEB7I+4XvbduvxjIG9aFA4kRffQpe7bNHj5fb5Z3s2zQBIC1iyrqwdnPwlk8OjbMgMw==", "66a962a0-4295-4a92-a4e4-0a39b5649f97" });
        }
    }
}
