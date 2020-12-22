using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    [DependsOn(
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceProviderMailingAbstractionsModule : AbpModule
    {
    }
}
