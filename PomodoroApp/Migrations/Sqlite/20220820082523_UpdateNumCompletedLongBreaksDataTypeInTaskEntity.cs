using Microsoft.EntityFrameworkCore.Migrations;

namespace PomodoroApp.Migrations.Sqlite
{
    public partial class UpdateNumCompletedLongBreaksDataTypeInTaskEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompletedLongBreak",
                table: "Tasks",
                newName: "NumCompletedLongBreaks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumCompletedLongBreaks",
                table: "Tasks",
                newName: "IsCompletedLongBreak");
        }
    }
}
