using EasyAbp.Abp.WeChat.Official;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    [DependsOn(
        typeof(AbpWeChatOfficialModule),
        typeof(NotificationServiceDomainSharedModule)
    )]
    public class NotificationServiceProviderWeChatOfficialAbstractionsModule : AbpModule
    {
    }
}
