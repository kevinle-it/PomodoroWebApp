using Microsoft.EntityFrameworkCore.Migrations;

namespace PomodoroApp.Migrations.Sqlite
{
    public partial class AddPomodoroConfigurationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PomodoroConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PomodoroLength = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 25),
                    ShortBreakLength = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 5),
                    LongBreakLength = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 15),
                    AutoStartPom = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoStartBreak = table.Column<bool>(type: "INTEGER", nullable: false),
                    LongBreakInterval = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 4)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PomodoroConfigurations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PomodoroConfigurations",
                columns: new[] { "Id", "AutoStartBreak", "AutoStartPom", "LongBreakInterval", "LongBreakLength", "PomodoroLength", "ShortBreakLength" },
                values: new object[] { 1, false, false, 4, 15, 25, 5 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PomodoroConfigurations");
        }
    }
}
