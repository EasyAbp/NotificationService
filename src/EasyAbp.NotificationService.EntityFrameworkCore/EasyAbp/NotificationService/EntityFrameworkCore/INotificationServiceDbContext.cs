using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.NotificationInfos;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    [ConnectionStringName(NotificationServiceDbProperties.ConnectionStringName)]
    public interface INotificationServiceDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<Notification> Notifications { get; set; }
        DbSet<NotificationInfo> NotificationInfos { get; set; }
    }
}
