using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Emailing;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(NotificationServiceApplicationContractsModule),
        typeof(AbpUsersAbstractionModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpEmailingModule)
    )]
    public class NotificationServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<NotificationServiceApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<NotificationServiceApplicationModule>(validate: true);
            });
        }
    }
}
