using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.NotificationService.MongoDB
{
    [DependsOn(
        typeof(NotificationServiceDomainModule),
        typeof(AbpMongoDbModule)
        )]
    public class NotificationServiceMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<NotificationServiceMongoDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
            });
        }
    }
}
