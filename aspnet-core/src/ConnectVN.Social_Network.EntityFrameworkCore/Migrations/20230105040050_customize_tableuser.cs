using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConnectVN.Social_Network.Migrations
{
    public partial class customize_tableuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppBookMarkStory_AppUserMember_UserGuid",
                table: "AppBookMarkStory");

            migrationBuilder.DropForeignKey(
                name: "FK_AppFollowingAuthor_AppUserMember_UserGuid",
                table: "AppFollowingAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AppStoryNotifications_AppUserMember_UserGuid",
                table: "AppStoryNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_AppStoryPublish_AppUserMember_UserGuid",
                table: "AppStoryPublish");

            migrationBuilder.DropForeignKey(
                name: "FK_AppStoryReview_AppUserMember_UserGuid",
                table: "AppStoryReview");

            migrationBuilder.DropTable(
                name: "AppUserMember");

            migrationBuilder.AddColumn<Guid>(
                name: "ActiveToken",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AbpUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AbpUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AbpUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ForgotToken",
                table: "AbpUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "AbpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "AbpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AbpUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LockReason",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoginAttemp",
                table: "AbpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AbpUsers",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AbpUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpiredTime",
                table: "AbpUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AbpUsers_GroupId",
                table: "AbpUsers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AbpUsers_AppGroupUserMember_GroupId",
                table: "AbpUsers",
                column: "GroupId",
                principalTable: "AppGroupUserMember",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBookMarkStory_AbpUsers_UserGuid",
                table: "AppBookMarkStory",
                column: "UserGuid",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppFollowingAuthor_AbpUsers_UserGuid",
                table: "AppFollowingAuthor",
                column: "UserGuid",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStoryNotifications_AbpUsers_UserGuid",
                table: "AppStoryNotifications",
                column: "UserGuid",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStoryPublish_AbpUsers_UserGuid",
                table: "AppStoryPublish",
                column: "UserGuid",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStoryReview_AbpUsers_UserGuid",
                table: "AppStoryReview",
                column: "UserGuid",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbpUsers_AppGroupUserMember_GroupId",
                table: "AbpUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppBookMarkStory_AbpUsers_UserGuid",
                table: "AppBookMarkStory");

            migrationBuilder.DropForeignKey(
                name: "FK_AppFollowingAuthor_AbpUsers_UserGuid",
                table: "AppFollowingAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_AppStoryNotifications_AbpUsers_UserGuid",
                table: "AppStoryNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_AppStoryPublish_AbpUsers_UserGuid",
                table: "AppStoryPublish");

            migrationBuilder.DropForeignKey(
                name: "FK_AppStoryReview_AbpUsers_UserGuid",
                table: "AppStoryReview");

            migrationBuilder.DropIndex(
                name: "IX_AbpUsers_GroupId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ActiveToken",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ForgotToken",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LockReason",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "LoginAttemp",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Profile",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpiredTime",
                table: "AbpUsers");

            migrationBuilder.CreateTable(
                name: "AppUserMember",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    ActiveToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ForgotToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LockReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginAttemp = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TokenExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserMember_AppGroupUserMember_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AppGroupUserMember",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserMember_GroupId",
                table: "AppUserMember",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppBookMarkStory_AppUserMember_UserGuid",
                table: "AppBookMarkStory",
                column: "UserGuid",
                principalTable: "AppUserMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppFollowingAuthor_AppUserMember_UserGuid",
                table: "AppFollowingAuthor",
                column: "UserGuid",
                principalTable: "AppUserMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStoryNotifications_AppUserMember_UserGuid",
                table: "AppStoryNotifications",
                column: "UserGuid",
                principalTable: "AppUserMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStoryPublish_AppUserMember_UserGuid",
                table: "AppStoryPublish",
                column: "UserGuid",
                principalTable: "AppUserMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppStoryReview_AppUserMember_UserGuid",
                table: "AppStoryReview",
                column: "UserGuid",
                principalTable: "AppUserMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
