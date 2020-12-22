using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpBackgroundJobsAbstractionsModule),
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceDomainModule : AbpModule
    {
    }
}
