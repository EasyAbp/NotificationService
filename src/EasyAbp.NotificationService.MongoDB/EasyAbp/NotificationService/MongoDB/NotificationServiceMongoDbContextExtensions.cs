using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.NotificationService.MongoDB
{
    public static class NotificationServiceMongoDbContextExtensions
    {
        public static void ConfigureNotificationService(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new NotificationServiceMongoModelBuilderConfigurationOptions(
                NotificationServiceDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}