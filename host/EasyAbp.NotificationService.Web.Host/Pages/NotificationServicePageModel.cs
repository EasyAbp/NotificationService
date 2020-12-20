using EasyAbp.NotificationService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.NotificationService.Pages
{
    public abstract class NotificationServicePageModel : AbpPageModel
    {
        protected NotificationServicePageModel()
        {
            LocalizationResourceType = typeof(NotificationServiceResource);
        }
    }
}