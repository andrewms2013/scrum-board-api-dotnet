using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScrumBoardAPI.Migrations
{
    public partial class TaskStatusAndPriorityToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ATask");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "ATask",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ATask",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ATask",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Priority", "Status" },
                values: new object[] { 2, 0 });

            migrationBuilder.UpdateData(
                table: "ATask",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Priority", "Status" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "ATask");

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "ATask",
                type: "integer",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ATask");

            migrationBuilder.UpdateData(
                table: "ATask",
                keyColumn: "Id",
                keyValue: 1,
                column: "Priority",
                value: "High");

            migrationBuilder.UpdateData(
                table: "ATask",
                keyColumn: "Id",
                keyValue: 2,
                column: "Priority",
                value: "Medium");
        }
    }
}
