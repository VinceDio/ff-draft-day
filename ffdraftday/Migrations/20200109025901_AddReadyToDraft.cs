using Microsoft.EntityFrameworkCore.Migrations;

namespace ffdraftday.Migrations
{
    public partial class AddReadyToDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReadyToDraft",
                table: "Draft",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadyToDraft",
                table: "Draft");
        }
    }
}
