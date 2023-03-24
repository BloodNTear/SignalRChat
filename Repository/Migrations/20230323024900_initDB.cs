using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "chatGroups",
                columns: table => new
                {
                    ChatGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatGroups", x => x.ChatGroupID);
                });

            migrationBuilder.CreateTable(
                name: "AccountChatGroup",
                columns: table => new
                {
                    AccountsUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatGroupsChatGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountChatGroup", x => new { x.AccountsUserID, x.ChatGroupsChatGroupID });
                    table.ForeignKey(
                        name: "FK_AccountChatGroup_accounts_AccountsUserID",
                        column: x => x.AccountsUserID,
                        principalTable: "accounts",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountChatGroup_chatGroups_ChatGroupsChatGroupID",
                        column: x => x.ChatGroupsChatGroupID,
                        principalTable: "chatGroups",
                        principalColumn: "ChatGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    MessageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromAccountUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToChatGroupChatGroupID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.MessageID);
                    table.ForeignKey(
                        name: "FK_messages_accounts_FromAccountUserID",
                        column: x => x.FromAccountUserID,
                        principalTable: "accounts",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_chatGroups_ToChatGroupChatGroupID",
                        column: x => x.ToChatGroupChatGroupID,
                        principalTable: "chatGroups",
                        principalColumn: "ChatGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountChatGroup_ChatGroupsChatGroupID",
                table: "AccountChatGroup",
                column: "ChatGroupsChatGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_messages_FromAccountUserID",
                table: "messages",
                column: "FromAccountUserID");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ToChatGroupChatGroupID",
                table: "messages",
                column: "ToChatGroupChatGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountChatGroup");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "chatGroups");
        }
    }
}
