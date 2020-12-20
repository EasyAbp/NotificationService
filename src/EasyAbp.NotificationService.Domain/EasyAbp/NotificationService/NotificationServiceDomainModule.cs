using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceDomainModule : AbpModule
    {
    }
}
