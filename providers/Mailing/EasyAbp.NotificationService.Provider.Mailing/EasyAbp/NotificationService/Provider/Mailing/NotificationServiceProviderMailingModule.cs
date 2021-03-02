using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Emailing;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(NotificationServiceProviderMailingAbstractionsModule),
        typeof(AbpUsersAbstractionModule),
        typeof(AbpEmailingModule)
    )]
    public class NotificationServiceProviderMailingModule : AbpModule
    {
    }
}
