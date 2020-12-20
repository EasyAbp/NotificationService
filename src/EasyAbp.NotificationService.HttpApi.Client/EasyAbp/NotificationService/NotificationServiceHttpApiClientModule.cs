using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NotificationServiceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EasyAbpNotificationService";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NotificationServiceApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
