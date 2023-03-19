using EasyAbp.NotificationService.Options;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Mailing;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceProviderMailingAbstractionsModule),
    typeof(AbpUsersAbstractionModule),
    typeof(AbpEmailingModule)
)]
public class NotificationServiceProviderMailingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<NotificationServiceOptions>(options =>
        {
            options.Providers.AddProvider(new NotificationServiceProviderConfiguration(
                NotificationProviderMailingConsts.NotificationMethod, typeof(EmailNotificationManager)));
        });
    }
}