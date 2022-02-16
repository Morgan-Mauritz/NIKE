using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _20220214_updatedCommentsForeignkeyVersion2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EntryID",
                table: "Comment");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Entry_EntryID",
                table: "Comment",
                column: "EntryID",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Entry_EntryID",
                table: "Comment");

            migrationBuilder.AddForeignKey(
                name: "EntryID",
                table: "Comment",
                column: "EntryID",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
