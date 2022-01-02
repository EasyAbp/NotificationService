using EasyAbp.NotificationService.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.NotificationService
{
    [Area(NotificationServiceRemoteServiceConsts.ModuleName)]
    public abstract class NotificationServiceController : AbpControllerBase
    {
        protected NotificationServiceController()
        {
            LocalizationResource = typeof(NotificationServiceResource);
        }
    }
}
