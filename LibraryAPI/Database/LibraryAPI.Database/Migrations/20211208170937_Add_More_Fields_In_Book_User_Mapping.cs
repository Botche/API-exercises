using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryAPI.Database.Migrations
{
    public partial class Add_More_Fields_In_Book_User_Mapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeadLine",
                table: "BooksUsersMapping",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "BooksUsersMapping",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "BooksUsersMapping",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeadLine",
                table: "BooksUsersMapping");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "BooksUsersMapping");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "BooksUsersMapping");
        }
    }
}
