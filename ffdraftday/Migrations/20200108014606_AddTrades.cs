using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ffdraftday.Migrations
{
    public partial class AddTrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DraftId = table.Column<int>(nullable: false),
                    Team1Id = table.Column<int>(nullable: false),
                    Team2Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trade_Draft_DraftId",
                        column: x => x.DraftId,
                        principalTable: "Draft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trade_Team_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trade_Team_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TradeItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TradeId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    IsPlayer = table.Column<bool>(nullable: false),
                    Round = table.Column<int>(nullable: false),
                    Selection = table.Column<int>(nullable: true),
                    PlayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradeItem_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeItem_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradeItem_Trade_TradeId",
                        column: x => x.TradeId,
                        principalTable: "Trade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trade_DraftId",
                table: "Trade",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_Team1Id",
                table: "Trade",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_Team2Id",
                table: "Trade",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItem_PlayerId",
                table: "TradeItem",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItem_TeamId",
                table: "TradeItem",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TradeItem_TradeId",
                table: "TradeItem",
                column: "TradeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeItem");

            migrationBuilder.DropTable(
                name: "Trade");
        }
    }
}
