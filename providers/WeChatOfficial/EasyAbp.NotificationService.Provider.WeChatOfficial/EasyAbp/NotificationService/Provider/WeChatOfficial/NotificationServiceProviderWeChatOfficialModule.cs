using EasyAbp.Abp.WeChat.Official;
using EasyAbp.NotificationService.Options;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[DependsOn(
    typeof(NotificationServiceDomainModule),
    typeof(NotificationServiceProviderWeChatOfficialAbstractionsModule),
    typeof(AbpWeChatOfficialModule)
)]
public class NotificationServiceProviderWeChatOfficialModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<NotificationServiceOptions>(options =>
        {
            options.Providers.AddProvider(new NotificationServiceProviderConfiguration(
                NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod,
                typeof(WeChatOfficialTemplateMessageNotificationManager)));
        });
    }
}