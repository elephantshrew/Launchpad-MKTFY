using Microsoft.EntityFrameworkCore.Migrations;

namespace Launchpad.App.Migrations
{
    public partial class DisplayInSearches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayInSearches",
                table: "Listings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayInSearches",
                table: "Listings");
        }
    }
}
