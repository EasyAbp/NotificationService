using EasyAbp.NotificationService.Options;
using EasyAbp.PrivateMessaging;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceProviderPrivateMessagingAbstractionsModule),
    typeof(PrivateMessagingApplicationContractsModule)
)]
public class NotificationServiceProviderPrivateMessagingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<NotificationServiceOptions>(options =>
        {
            options.Providers.AddProvider(new NotificationServiceProviderConfiguration(
                NotificationProviderPrivateMessagingConsts.NotificationMethod,
                typeof(PrivateMessageNotificationManager)));
        });
    }
}