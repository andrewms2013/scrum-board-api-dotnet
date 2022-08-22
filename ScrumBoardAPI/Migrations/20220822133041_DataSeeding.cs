using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class DataSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AUser",
                columns: new[] { "Id", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { "1", "example@example.com", "John Doe", "pass" },
                    { "2", "example2@example.com", "Jane Doe", "pass2" }
                });

            migrationBuilder.InsertData(
                table: "Workspace",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Workspace 1" },
                    { 2, "Workspace 2" }
                });

            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "AssigneeId", "CreatorId", "Description", "Name", "Priority", "WorkspaceId" },
                values: new object[,]
                {
                    { 1, "1", "1", "Task 1 description", "Task 1", "High", 1 },
                    { 2, "1", "2", "Task 2 description", "Task 2", "Medium", 1 }
                });

            migrationBuilder.InsertData(
                table: "WorkspaceUser",
                columns: new[] { "UserId", "WorkspaceId" },
                values: new object[,]
                {
                    { "1", 1 },
                    { "2", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Workspace",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkspaceUser",
                keyColumns: new[] { "UserId", "WorkspaceId" },
                keyValues: new object[] { "1", 1 });

            migrationBuilder.DeleteData(
                table: "WorkspaceUser",
                keyColumns: new[] { "UserId", "WorkspaceId" },
                keyValues: new object[] { "2", 1 });

            migrationBuilder.DeleteData(
                table: "AUser",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AUser",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Workspace",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
