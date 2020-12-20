using EasyAbp.NotificationService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.NotificationService
{
    public abstract class NotificationServiceController : AbpController
    {
        protected NotificationServiceController()
        {
            LocalizationResource = typeof(NotificationServiceResource);
        }
    }
}
