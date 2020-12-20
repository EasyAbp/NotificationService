using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class NotificationServiceEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<NotificationServiceDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Notification, NotificationRepository>();
                options.AddRepository<NotificationInfo, NotificationInfoRepository>();
            });
        }
    }
}
