using EasyAbp.NotificationService.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService
{
    public abstract class NotificationServiceAppService : ApplicationService
    {
        protected NotificationServiceAppService()
        {
            LocalizationResource = typeof(NotificationServiceResource);
            ObjectMapperContext = typeof(NotificationServiceApplicationModule);
        }
    }
}
