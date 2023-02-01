using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectVN.Social_Network.Migrations
{
    public partial class AddPropertySlugToTableChapter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "AppChapter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "AppChapter");
        }
    }
}
