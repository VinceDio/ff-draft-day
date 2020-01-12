using Microsoft.EntityFrameworkCore.Migrations;

namespace ffdraftday.Migrations
{
    public partial class AddDraftStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadyToDraft",
                table: "Draft");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Draft",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Draft");

            migrationBuilder.AddColumn<bool>(
                name: "ReadyToDraft",
                table: "Draft",
                nullable: false,
                defaultValue: false);
        }
    }
}
