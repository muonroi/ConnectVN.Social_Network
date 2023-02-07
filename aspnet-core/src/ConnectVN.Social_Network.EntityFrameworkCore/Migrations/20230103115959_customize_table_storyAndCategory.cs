using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectVN.Social_Network.Migrations
{
    public partial class customize_table_storyAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCategoryInStory");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "AppStory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppStory_CategoryId",
                table: "AppStory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppStory_AppCategory_CategoryId",
                table: "AppStory",
                column: "CategoryId",
                principalTable: "AppCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppStory_AppCategory_CategoryId",
                table: "AppStory");

            migrationBuilder.DropIndex(
                name: "IX_AppStory_CategoryId",
                table: "AppStory");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppStory");

            migrationBuilder.CreateTable(
                name: "AppCategoryInStory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    StoryGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCategoryInStory", x => new { x.CategoryId, x.StoryGuid });
                    table.ForeignKey(
                        name: "FK_AppCategoryInStory_AppCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppCategoryInStory_AppStory_StoryGuid",
                        column: x => x.StoryGuid,
                        principalTable: "AppStory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCategoryInStory_StoryGuid",
                table: "AppCategoryInStory",
                column: "StoryGuid");
        }
    }
}
