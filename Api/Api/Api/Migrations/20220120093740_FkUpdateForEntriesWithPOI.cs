using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class FkUpdateForEntriesWithPOI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Entry",
                newName: "POIID");

            migrationBuilder.CreateIndex(
                name: "IX_Entry_POIID",
                table: "Entry",
                column: "POIID");

            migrationBuilder.AddForeignKey(
                name: "POIID",
                table: "Entry",
                column: "POIID",
                principalTable: "POI",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "POIID",
                table: "Entry");

            migrationBuilder.DropIndex(
                name: "IX_Entry_POIID",
                table: "Entry");

            migrationBuilder.RenameColumn(
                name: "POIID",
                table: "Entry",
                newName: "LocationID");
        }
    }
}
