using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification
{
    public class IndexModel : NotificationServicePageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
