using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceApplicationModule),
        typeof(NotificationServiceDomainTestModule)
        )]
    public class NotificationServiceApplicationTestModule : AbpModule
    {
    }
}
