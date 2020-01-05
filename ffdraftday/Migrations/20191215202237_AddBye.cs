using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ffdraftday.Migrations
{
    public partial class AddBye : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "PlayerRank");

            migrationBuilder.AddColumn<int>(
                name: "Bye",
                table: "PlayerRank",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "PlayerRank",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bye",
                table: "PlayerRank");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "PlayerRank");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "PlayerRank",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
