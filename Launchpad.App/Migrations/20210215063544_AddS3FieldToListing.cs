using Microsoft.EntityFrameworkCore.Migrations;

namespace Launchpad.App.Migrations
{
    public partial class AddS3FieldToListing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "S3Key",
                table: "Listings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "S3Key",
                table: "Listings");
        }
    }
}
