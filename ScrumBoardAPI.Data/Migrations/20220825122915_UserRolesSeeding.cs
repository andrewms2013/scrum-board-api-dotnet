using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class UserRolesSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceUser_AspNetUsers_UsersId",
                table: "WorkspaceUser");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceUser_Workspace_WorkspacesId",
                table: "WorkspaceUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkspaceUser",
                table: "WorkspaceUser");

            migrationBuilder.DeleteData(
                table: "WorkspaceUser",
                keyColumns: new[] { "UsersId", "WorkspacesId" },
                keyColumnTypes: new [] { "uniqueidentifier", "uniqueidentifier" },
                keyValues: new object[] { "1", 1 });

            migrationBuilder.RenameTable(
                name: "WorkspaceUser",
                newName: "AUserWorkspace");

            migrationBuilder.RenameIndex(
                name: "IX_WorkspaceUser_WorkspacesId",
                table: "AUserWorkspace",
                newName: "IX_AUserWorkspace_WorkspacesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AUserWorkspace",
                table: "AUserWorkspace",
                columns: new[] { "UsersId", "WorkspacesId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "77fa5e58-7ef2-4346-836e-3c357e8c00dc", "Administrator", "ADMINISTRATOR" },
                    { "2", "26be2aaf-e775-471e-8bc6-ac023fe7a759", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AUserWorkspace_AspNetUsers_UsersId",
                table: "AUserWorkspace",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AUserWorkspace_Workspace_WorkspacesId",
                table: "AUserWorkspace",
                column: "WorkspacesId",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AUserWorkspace_AspNetUsers_UsersId",
                table: "AUserWorkspace");

            migrationBuilder.DropForeignKey(
                name: "FK_AUserWorkspace_Workspace_WorkspacesId",
                table: "AUserWorkspace");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AUserWorkspace",
                table: "AUserWorkspace");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.RenameTable(
                name: "AUserWorkspace",
                newName: "WorkspaceUser");

            migrationBuilder.RenameIndex(
                name: "IX_AUserWorkspace_WorkspacesId",
                table: "WorkspaceUser",
                newName: "IX_WorkspaceUser_WorkspacesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkspaceUser",
                table: "WorkspaceUser",
                columns: new[] { "UsersId", "WorkspacesId" });

            migrationBuilder.InsertData(
                table: "WorkspaceUser",
                columns: new[] { "UsersId", "WorkspacesId" },
                values: new object[] { "1", 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceUser_AspNetUsers_UsersId",
                table: "WorkspaceUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceUser_Workspace_WorkspacesId",
                table: "WorkspaceUser",
                column: "WorkspacesId",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
