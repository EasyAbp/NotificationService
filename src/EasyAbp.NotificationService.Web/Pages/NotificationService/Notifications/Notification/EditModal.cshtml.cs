using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Notifications.Dtos;
using EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification.ViewModels;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification
{
    public class EditModalModel : NotificationServicePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditNotificationViewModel ViewModel { get; set; }

        private readonly INotificationAppService _service;

        public EditModalModel(INotificationAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<NotificationDto, CreateEditNotificationViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditNotificationViewModel, CreateUpdateNotificationDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}