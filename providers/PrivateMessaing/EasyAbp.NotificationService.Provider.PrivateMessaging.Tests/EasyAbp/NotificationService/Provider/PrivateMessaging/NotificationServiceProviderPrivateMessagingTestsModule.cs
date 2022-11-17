using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpBackgroundJobsModule),
        typeof(NotificationServiceProviderPrivateMessagingModule),
        typeof(NotificationServiceDomainTestModule)
    )]
    public class NotificationServiceProviderPrivateMessagingTestsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddSingleton<IBackgroundJobManager, NullBackgroundJobManager>();

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NotificationServiceProviderPrivateMessagingTestsModule>();
            });
        }
    }
}