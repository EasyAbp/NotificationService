using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.NotificationInfos;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    [ConnectionStringName(NotificationServiceDbProperties.ConnectionStringName)]
    public class NotificationServiceDbContext : AbpDbContext<NotificationServiceDbContext>, INotificationServiceDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationInfo> NotificationInfos { get; set; }

        public NotificationServiceDbContext(DbContextOptions<NotificationServiceDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureNotificationService();
        }
    }
}
