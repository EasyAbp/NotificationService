using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    public class NotificationServiceHttpApiHostMigrationsDbContext : AbpDbContext<NotificationServiceHttpApiHostMigrationsDbContext>
    {
        public NotificationServiceHttpApiHostMigrationsDbContext(DbContextOptions<NotificationServiceHttpApiHostMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureNotificationService();
            modelBuilder.ConfigureAuditLogging();
            modelBuilder.ConfigurePermissionManagement();
            modelBuilder.ConfigureSettingManagement();
        }
    }
}
