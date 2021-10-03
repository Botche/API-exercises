using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class AddCalculatedPropertyToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemperatureF",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                computedColumnSql: "CAST((32 + ([TemperatureC] / 0.5556)) as INTEGER)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemperatureF",
                table: "WeatherForecasts");
        }
    }
}
