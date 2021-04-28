using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class DatingDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    birthday = table.Column<DateTime>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    orientation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    visibleSearch = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    imagePath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "friendRequest",
                columns: table => new
                {
                    userSent = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userPending = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__friendRe__CA4D816B5E0CA4A5", x => new { x.userSent, x.userPending });
                    table.ForeignKey(
                        name: "FK_postWall_userPending",
                        column: x => x.userPending,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postWall_userSent",
                        column: x => x.userSent,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "friends",
                columns: table => new
                {
                    user1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user2 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__friends__E934B3F5D121C264", x => new { x.user1, x.user2 });
                    table.ForeignKey(
                        name: "FK_postWall_user1",
                        column: x => x.user1,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_postWall_user2",
                        column: x => x.user2,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    postID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    publishDate = table.Column<DateTime>(type: "date", nullable: true),
                    author = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    profile = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.postID);
                    table.ForeignKey(
                        name: "FK_posts_users_UserId",
                        column: x => x.author,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_users_userId",
                        column: x => x.profile,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_friendRequest_userPending",
                table: "friendRequest",
                column: "userPending");

            migrationBuilder.CreateIndex(
                name: "IX_friends_user2",
                table: "friends",
                column: "user2");

            migrationBuilder.CreateIndex(
                name: "IX_posts_author",
                table: "posts",
                column: "author");

            migrationBuilder.CreateIndex(
                name: "IX_posts_profile",
                table: "posts",
                column: "profile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "friendRequest");

            migrationBuilder.DropTable(
                name: "friends");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
