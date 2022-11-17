using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    [DependsOn(
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceProviderPrivateMessagingAbstractionsModule : AbpModule
    {
    }
}
