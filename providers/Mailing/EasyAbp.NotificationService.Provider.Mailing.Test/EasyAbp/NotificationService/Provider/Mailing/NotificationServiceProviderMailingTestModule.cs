using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(AbpAuthorizationModule),
        typeof(AbpBackgroundJobsModule),
        typeof(NotificationServiceProviderMailingModule),
        typeof(NotificationServiceDomainTestModule)
    )]
    public class NotificationServiceProviderMailingTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddSingleton<IBackgroundJobManager, NullBackgroundJobManager>();
            context.Services.TryAddTransient<IAsyncBackgroundJob<EmailNotificationSendingJobArgs>, EmailNotificationSendingJob>();
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NotificationServiceProviderMailingTestModule>();
            });
        }
        
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
        }
    }
}
