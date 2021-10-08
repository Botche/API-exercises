using Microsoft.EntityFrameworkCore.Migrations;

namespace Test.Migrations
{
    public partial class AddConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "WeatherForecasts",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TemperatureF",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                computedColumnSql: "CAST((32 + ([TemperatureC] / 0.5556)) as INTEGER)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "32 + (int)([TemperatureC] / 0.5556)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "WeatherForecasts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TemperatureF",
                table: "WeatherForecasts",
                type: "int",
                nullable: false,
                computedColumnSql: "32 + (int)([TemperatureC] / 0.5556)",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "CAST((32 + ([TemperatureC] / 0.5556)) as INTEGER)");
        }
    }
}
