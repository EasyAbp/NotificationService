using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

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
        }
    }
}
