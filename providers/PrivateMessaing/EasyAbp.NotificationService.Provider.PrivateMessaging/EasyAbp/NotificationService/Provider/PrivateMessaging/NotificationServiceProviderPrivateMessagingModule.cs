using EasyAbp.PrivateMessaging;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(NotificationServiceProviderPrivateMessagingAbstractionsModule),
        typeof(PrivateMessagingDomainSharedModule)
    )]
    public class NotificationServiceProviderPrivateMessagingModule : AbpModule
    {
    }
}