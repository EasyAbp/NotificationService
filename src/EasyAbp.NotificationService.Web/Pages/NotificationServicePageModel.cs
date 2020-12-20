using EasyAbp.NotificationService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.NotificationService.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class NotificationServicePageModel : AbpPageModel
    {
        protected NotificationServicePageModel()
        {
            LocalizationResourceType = typeof(NotificationServiceResource);
            ObjectMapperContext = typeof(NotificationServiceWebModule);
        }
    }
}