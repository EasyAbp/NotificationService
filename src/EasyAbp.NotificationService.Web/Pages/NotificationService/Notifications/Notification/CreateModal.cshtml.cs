using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Notifications.Dtos;
using EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification.ViewModels;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification
{
    public class CreateModalModel : NotificationServicePageModel
    {
        [BindProperty]
        public CreateEditNotificationViewModel ViewModel { get; set; }

        private readonly INotificationAppService _service;

        public CreateModalModel(INotificationAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditNotificationViewModel, CreateUpdateNotificationDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}