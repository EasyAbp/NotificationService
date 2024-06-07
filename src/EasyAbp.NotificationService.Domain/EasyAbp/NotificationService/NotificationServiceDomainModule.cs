using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpBackgroundJobsAbstractionsModule),
        typeof(AbpUsersAbstractionModule),
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceDomainModule : AbpModule
    {
    }
}
