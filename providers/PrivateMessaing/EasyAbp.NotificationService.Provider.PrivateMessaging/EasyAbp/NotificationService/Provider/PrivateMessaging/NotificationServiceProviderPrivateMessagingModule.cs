using EasyAbp.PrivateMessaging;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(NotificationServiceProviderPrivateMessagingAbstractionsModule),
        typeof(AbpUsersAbstractionModule),
        typeof(PrivateMessagingApplicationContractsModule)
    )]
    public class NotificationServiceProviderPrivateMessagingModule : AbpModule
    {
    }
}