using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace EasyAbp.NotificationService.Pages
{
    public class IndexModel : NotificationServicePageModel
    {
        public void OnGet()
        {
            
        }

        public async Task OnPostLoginAsync()
        {
            await HttpContext.ChallengeAsync("oidc");
        }
    }
}