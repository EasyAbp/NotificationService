using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.NotificationService.MongoDB
{
    public class NotificationServiceMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public NotificationServiceMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}