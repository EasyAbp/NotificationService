using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyAbp.NotificationService.Migrations
{
    /// <inheritdoc />
    public partial class Added_UserName_To_Notification_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "EasyAbpNotificationServiceNotifications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "EasyAbpNotificationServiceNotifications");
        }
    }
}
