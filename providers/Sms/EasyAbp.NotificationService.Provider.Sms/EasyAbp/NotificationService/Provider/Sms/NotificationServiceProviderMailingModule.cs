using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Sms
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(NotificationServiceProviderSmsAbstractionsModule),
        typeof(AbpUsersAbstractionModule),
        typeof(AbpSmsModule)
    )]
    public class NotificationServiceProviderSmsModule : AbpModule
    {
    }
}
