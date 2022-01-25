using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class FkUpdateForEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Entry_UserID",
                table: "Entry",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "UserID",
                table: "Entry",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "UserID",
                table: "Entry");

            migrationBuilder.DropIndex(
                name: "IX_Entry_UserID",
                table: "Entry");
        }
    }
}
