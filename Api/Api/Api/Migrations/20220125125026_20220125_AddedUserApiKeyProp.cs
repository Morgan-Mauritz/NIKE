using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class _20220125_AddedUserApiKeyProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "User",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "User");
        }
    }
}
