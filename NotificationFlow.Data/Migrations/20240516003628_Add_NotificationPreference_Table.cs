using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotificationFlow.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_NotificationPreference_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Read",
                table: "NotificationUser",
                newName: "IsRead");

            migrationBuilder.RenameColumn(
                name: "SendToAll",
                table: "Notification",
                newName: "IsGeneral");

            migrationBuilder.CreateTable(
                name: "NotificationPreference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReceiveGeneralNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ReceiveSpecificNotifications = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPreference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationPreference_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreference_UserId",
                table: "NotificationPreference",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPreference");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "NotificationUser",
                newName: "Read");

            migrationBuilder.RenameColumn(
                name: "IsGeneral",
                table: "Notification",
                newName: "SendToAll");
        }
    }
}
