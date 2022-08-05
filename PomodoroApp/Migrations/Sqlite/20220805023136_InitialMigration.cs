using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PomodoroApp.Migrations.Sqlite
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    NumEstimatedPoms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumCompletedPoms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumCompletedShortBreaks = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompletedLongBreak = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
