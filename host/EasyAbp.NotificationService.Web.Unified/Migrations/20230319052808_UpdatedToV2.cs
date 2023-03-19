using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.NotificationService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedToV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RetryNotificationId",
                table: "EasyAbpNotificationServiceNotifications",
                newName: "RetryForNotificationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RetryForNotificationId",
                table: "EasyAbpNotificationServiceNotifications",
                newName: "RetryNotificationId");
        }
    }
}
