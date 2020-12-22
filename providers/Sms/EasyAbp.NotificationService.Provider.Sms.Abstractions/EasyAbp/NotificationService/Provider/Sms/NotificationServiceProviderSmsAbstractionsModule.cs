using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.Sms
{
    [DependsOn(
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceProviderSmsAbstractionsModule : AbpModule
    {
    }
}
