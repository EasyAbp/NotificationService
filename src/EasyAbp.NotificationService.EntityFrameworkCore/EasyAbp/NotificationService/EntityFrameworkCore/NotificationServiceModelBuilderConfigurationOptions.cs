using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    public class NotificationServiceModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public NotificationServiceModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}