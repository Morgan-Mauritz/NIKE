using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _20220214_updatedCommentsForeignkeyVersion4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comment");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserID",
                table: "Comment",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_User_UserID",
                table: "Comment",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_User_UserID",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_UserID",
                table: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comment",
                type: "TEXT",
                nullable: true);
        }
    }
}
