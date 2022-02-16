using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _20220214_updatedCommentsForeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "CommentID",
                table: "Comment");

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "Comment",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_EntryID",
                table: "Comment",
                column: "EntryID");

            migrationBuilder.AddForeignKey(
                name: "EntryID",
                table: "Comment",
                column: "EntryID",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "EntryID",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_EntryID",
                table: "Comment");

            migrationBuilder.AlterColumn<long>(
                name: "ID",
                table: "Comment",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "CommentID",
                table: "Comment",
                column: "ID",
                principalTable: "Entry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
