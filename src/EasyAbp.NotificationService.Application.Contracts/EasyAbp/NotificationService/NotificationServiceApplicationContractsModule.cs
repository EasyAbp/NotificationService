using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class NotificationServiceApplicationContractsModule : AbpModule
    {

    }
}
