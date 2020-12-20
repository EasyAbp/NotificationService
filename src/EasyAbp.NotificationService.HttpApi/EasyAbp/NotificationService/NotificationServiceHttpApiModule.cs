using Localization.Resources.AbpUi;
using EasyAbp.NotificationService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class NotificationServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(NotificationServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<NotificationServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
