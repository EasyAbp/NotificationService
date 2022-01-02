using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NotificationServiceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = NotificationServiceRemoteServiceConsts.RemoteServiceName;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NotificationServiceApplicationContractsModule).Assembly,
                RemoteServiceName
            );
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NotificationServiceApplicationContractsModule>();
            });
        }
    }
}
