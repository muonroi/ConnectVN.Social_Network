using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectVN.Social_Network.Migrations
{
    public partial class addproperty_slug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "AppStory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "AppStory");
        }
    }
}
