using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.NotificationService.MongoDB
{
    [ConnectionStringName(NotificationServiceDbProperties.ConnectionStringName)]
    public interface INotificationServiceMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
