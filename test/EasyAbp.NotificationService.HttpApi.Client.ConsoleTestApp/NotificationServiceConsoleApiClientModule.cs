using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService
{
    [DependsOn(
        typeof(NotificationServiceHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class NotificationServiceConsoleApiClientModule : AbpModule
    {
        
    }
}
