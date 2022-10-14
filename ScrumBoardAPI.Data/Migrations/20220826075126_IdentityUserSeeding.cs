using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class IdentityUserSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Workspace",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "a8e2a385-44c4-4c82-a9b4-f3cf151d2207", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEB7I+4XvbduvxjIG9aFA4kRffQpe7bNHj5fb5Z3s2zQBIC1iyrqwdnPwlk8OjbMgMw==", null, false, "66a962a0-4295-4a92-a4e4-0a39b5649f97", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });

            migrationBuilder.InsertData(
                table: "Workspace",
                columns: new[] { "Id", "AdminId", "Name" },
                values: new object[,]
                {
                    { 1, "1", "Workspace 1" },
                    { 2, "1", "Workspace 2" }
                });

            migrationBuilder.InsertData(
                table: "ATask",
                columns: new[] { "Id", "AssigneeId", "CreatorId", "Description", "Name", "Priority", "WorkspaceId" },
                values: new object[,]
                {
                    { 1, "1", "1", "Task 1 description", "Task 1", "High", 1 },
                    { 2, "1", "1", "Task 2 description", "Task 2", "Medium", 1 }
                });

            migrationBuilder.InsertData(
                table: "AUserWorkspace",
                columns: new[] { "UsersId", "WorkspacesId" },
                values: new object[,]
                {
                    { "1", 1 },
                    { "1", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workspace_AdminId",
                table: "Workspace",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspace_AspNetUsers_AdminId",
                table: "Workspace",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspace_AspNetUsers_AdminId",
                table: "Workspace");

            migrationBuilder.DropIndex(
                name: "IX_Workspace_AdminId",
                table: "Workspace");

            migrationBuilder.DeleteData(
                table: "ATask",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ATask",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AUserWorkspace",
                keyColumns: new[] { "UsersId", "WorkspacesId" },
                keyValues: new object[] { "1", 1 });

            migrationBuilder.DeleteData(
                table: "AUserWorkspace",
                keyColumns: new[] { "UsersId", "WorkspacesId" },
                keyValues: new object[] { "1", 2 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "Workspace",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Workspace",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Workspace");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "77fa5e58-7ef2-4346-836e-3c357e8c00dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "26be2aaf-e775-471e-8bc6-ac023fe7a759");
        }
    }
}
