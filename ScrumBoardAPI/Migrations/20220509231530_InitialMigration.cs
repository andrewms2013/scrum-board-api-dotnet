using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workspace",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspace", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkspaceId = table.Column<int>(type: "integer", nullable: false),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    AssigneeId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_AUser_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Task_AUser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Workspace_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceUser",
                columns: table => new
                {
                    WorkspaceId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceUser", x => new { x.WorkspaceId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WorkspaceUser_AUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkspaceUser_Workspace_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_AssigneeId",
                table: "Task",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_CreatorId",
                table: "Task",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_WorkspaceId",
                table: "Task",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceUser_UserId",
                table: "WorkspaceUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "WorkspaceUser");

            migrationBuilder.DropTable(
                name: "AUser");

            migrationBuilder.DropTable(
                name: "Workspace");
        }
    }
}
