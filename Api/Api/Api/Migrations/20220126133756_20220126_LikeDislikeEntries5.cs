using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _20220126_LikeDislikeEntries5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EntryId",
                table: "LikeDislikeEntry");

            migrationBuilder.DropForeignKey(
                name: "UserId",
                table: "LikeDislikeEntry");

            migrationBuilder.AddForeignKey(
                name: "EID",
                table: "LikeDislikeEntry",
                column: "EntryId",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UID",
                table: "LikeDislikeEntry",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EID",
                table: "LikeDislikeEntry");

            migrationBuilder.DropForeignKey(
                name: "UID",
                table: "LikeDislikeEntry");

            migrationBuilder.AddForeignKey(
                name: "EntryId",
                table: "LikeDislikeEntry",
                column: "EntryId",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "UserId",
                table: "LikeDislikeEntry",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
