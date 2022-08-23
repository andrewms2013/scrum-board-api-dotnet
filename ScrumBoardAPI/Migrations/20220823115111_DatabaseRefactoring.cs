using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class DatabaseRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceUser_AUser_UserId",
                table: "WorkspaceUser");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceUser_Workspace_WorkspaceId",
                table: "WorkspaceUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkspaceUser",
                table: "WorkspaceUser");

            migrationBuilder.DropIndex(
                name: "IX_WorkspaceUser_UserId",
                table: "WorkspaceUser");

            migrationBuilder.DeleteData(
                table: "WorkspaceUser",
                keyColumns: new[] { "UserId", "WorkspaceId" },
                keyColumnTypes: new [] { "uniqueidentifier", "uniqueidentifier" },
                keyValues: new object[] { "2", 1 });

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkspaceUser",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "WorkspaceId",
                table: "WorkspaceUser",
                newName: "WorkspacesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkspaceUser",
                table: "WorkspaceUser",
                columns: new[] { "UsersId", "WorkspacesId" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUser_WorkspacesId",
                table: "WorkspaceUser",
                column: "WorkspacesId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceUser_AUser_UsersId",
                table: "WorkspaceUser",
                column: "UsersId",
                principalTable: "AUser",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceUser_AUser_UsersId",
                table: "WorkspaceUser");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkspaceUser_Workspace_WorkspacesId",
                table: "WorkspaceUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkspaceUser",
                table: "WorkspaceUser");

            migrationBuilder.DropIndex(
                name: "IX_WorkspaceUser_WorkspacesId",
                table: "WorkspaceUser");

            migrationBuilder.RenameColumn(
                name: "WorkspacesId",
                table: "WorkspaceUser",
                newName: "WorkspaceId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "WorkspaceUser",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkspaceUser",
                table: "WorkspaceUser",
                columns: new[] { "WorkspaceId", "UserId" });

            migrationBuilder.InsertData(
                table: "WorkspaceUser",
                columns: new[] { "UserId", "WorkspaceId" },
                values: new object[] { "2", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUser_UserId",
                table: "WorkspaceUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceUser_AUser_UserId",
                table: "WorkspaceUser",
                column: "UserId",
                principalTable: "AUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkspaceUser_Workspace_WorkspaceId",
                table: "WorkspaceUser",
                column: "WorkspaceId",
                principalTable: "Workspace",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
