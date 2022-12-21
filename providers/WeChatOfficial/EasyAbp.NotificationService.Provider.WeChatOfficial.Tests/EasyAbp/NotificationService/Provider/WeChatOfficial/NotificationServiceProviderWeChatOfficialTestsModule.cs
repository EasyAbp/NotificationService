using EasyAbp.Abp.WeChat.Official.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpBackgroundJobsModule),
        typeof(NotificationServiceProviderWeChatOfficialModule),
        typeof(NotificationServiceDomainTestModule)
    )]
    public class NotificationServiceProviderWeChatOfficialTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddSingleton<IBackgroundJobManager, NullBackgroundJobManager>();

            Configure<AbpWeChatOfficialOptions>(options =>
            {
                options.AppId = "my-appid";
                options.AppSecret = "my-appsecret";
            });
        }
    }
}