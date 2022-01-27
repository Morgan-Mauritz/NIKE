using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _20220126_LikeDislikeEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Entry");

            migrationBuilder.CreateTable(
                name: "LikeDislikeEntry",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntryId = table.Column<long>(type: "INTEGER", nullable: false),
                    Likes = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDislikeEntry", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LikeDislikeEntry",
                        column: x => x.EntryId,
                        principalTable: "Entry",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikeUser",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikeEntry_EntryId",
                table: "LikeDislikeEntry",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikeEntry_UserId",
                table: "LikeDislikeEntry",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDislikeEntry");

            migrationBuilder.AddColumn<long>(
                name: "Likes",
                table: "Entry",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
