using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAbp.NotificationService.Migrations
{
    public partial class ChangedDbTablePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationServiceNotifications",
                table: "NotificationServiceNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationServiceNotificationInfos",
                table: "NotificationServiceNotificationInfos");

            migrationBuilder.RenameTable(
                name: "NotificationServiceNotifications",
                newName: "EasyAbpNotificationServiceNotifications");

            migrationBuilder.RenameTable(
                name: "NotificationServiceNotificationInfos",
                newName: "EasyAbpNotificationServiceNotificationInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpNotificationServiceNotifications",
                table: "EasyAbpNotificationServiceNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpNotificationServiceNotificationInfos",
                table: "EasyAbpNotificationServiceNotificationInfos",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpNotificationServiceNotifications",
                table: "EasyAbpNotificationServiceNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpNotificationServiceNotificationInfos",
                table: "EasyAbpNotificationServiceNotificationInfos");

            migrationBuilder.RenameTable(
                name: "EasyAbpNotificationServiceNotifications",
                newName: "NotificationServiceNotifications");

            migrationBuilder.RenameTable(
                name: "EasyAbpNotificationServiceNotificationInfos",
                newName: "NotificationServiceNotificationInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationServiceNotifications",
                table: "NotificationServiceNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationServiceNotificationInfos",
                table: "NotificationServiceNotificationInfos",
                column: "Id");
        }
    }
}
