using EasyAbp.NotificationService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.NotificationService
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(NotificationServiceEntityFrameworkCoreTestModule)
        )]
    public class NotificationServiceDomainTestModule : AbpModule
    {
        
    }
}
