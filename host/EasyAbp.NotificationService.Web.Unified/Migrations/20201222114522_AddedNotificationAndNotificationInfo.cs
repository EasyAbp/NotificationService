using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyAbp.NotificationService.Migrations
{
    public partial class AddedNotificationAndNotificationInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationServiceNotificationInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationServiceNotificationInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationServiceNotifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    NotificationInfoId = table.Column<Guid>(nullable: false),
                    NotificationMethod = table.Column<string>(nullable: true),
                    Success = table.Column<bool>(nullable: true),
                    CompletionTime = table.Column<DateTime>(nullable: true),
                    FailureReason = table.Column<string>(nullable: true),
                    RetryNotificationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationServiceNotifications", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationServiceNotificationInfos");

            migrationBuilder.DropTable(
                name: "NotificationServiceNotifications");
        }
    }
}
