using EasyAbp.Abp.WeChat.Official;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(NotificationServiceProviderWeChatOfficialAbstractionsModule),
        typeof(AbpWeChatOfficialModule)
    )]
    public class NotificationServiceProviderWeChatOfficialModule : AbpModule
    {
    }
}