using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class IdentityUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DeleteData(
                table: "Workspace",
                keyColumn: "Id",
                keyValue: 2);

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

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AUser");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AUser",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AUser",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "AUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AUser",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "AUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "AUser",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "AUser",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "AUser",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "AUser",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "AUser",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AUser",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AUser",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ATask",
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
                    table.PrimaryKey("PK_ATask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ATask_AUser_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_ATask_AUser_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ATask_Workspace_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Workspace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ATask_AssigneeId",
                table: "ATask",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_ATask_CreatorId",
                table: "ATask",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ATask_WorkspaceId",
                table: "ATask",
                column: "WorkspaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATask");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "AUser");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AUser");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "AUser",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AUser",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AUser",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssigneeId = table.Column<string>(type: "text", nullable: true),
                    CreatorId = table.Column<string>(type: "text", nullable: false),
                    WorkspaceId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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
        }
    }
}
