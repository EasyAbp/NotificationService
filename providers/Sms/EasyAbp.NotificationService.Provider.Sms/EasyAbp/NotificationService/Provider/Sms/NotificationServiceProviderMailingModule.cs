using EasyAbp.NotificationService.Options;
using Volo.Abp.Modularity;
using Volo.Abp.Sms;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Sms;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceProviderSmsAbstractionsModule),
    typeof(AbpUsersAbstractionModule),
    typeof(AbpSmsModule)
)]
public class NotificationServiceProviderSmsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<NotificationServiceOptions>(options =>
        {
            options.Providers.AddProvider(new NotificationServiceProviderConfiguration(
                NotificationProviderSmsConsts.NotificationMethod, typeof(SmsNotificationManager)));
        });
    }
}