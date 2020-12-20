using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.NotificationService.MongoDB
{
    [ConnectionStringName(NotificationServiceDbProperties.ConnectionStringName)]
    public class NotificationServiceMongoDbContext : AbpMongoDbContext, INotificationServiceMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureNotificationService();
        }
    }
}