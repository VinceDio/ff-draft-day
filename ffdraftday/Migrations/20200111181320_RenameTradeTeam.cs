using Microsoft.EntityFrameworkCore.Migrations;

namespace ffdraftday.Migrations
{
    public partial class RenameTradeTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeItem_Team_TeamId",
                table: "TradeItem");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TradeItem",
                newName: "FromTeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItem_TeamId",
                table: "TradeItem",
                newName: "IX_TradeItem_FromTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItem_Team_FromTeamId",
                table: "TradeItem",
                column: "FromTeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradeItem_Team_FromTeamId",
                table: "TradeItem");

            migrationBuilder.RenameColumn(
                name: "FromTeamId",
                table: "TradeItem",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TradeItem_FromTeamId",
                table: "TradeItem",
                newName: "IX_TradeItem_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradeItem_Team_TeamId",
                table: "TradeItem",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
