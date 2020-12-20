using EasyAbp.NotificationService.Notifications.Dtos;
using EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification.ViewModels;
using AutoMapper;

namespace EasyAbp.NotificationService.Web
{
    public class NotificationServiceWebAutoMapperProfile : Profile
    {
        public NotificationServiceWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<NotificationDto, CreateEditNotificationViewModel>();
            CreateMap<CreateEditNotificationViewModel, CreateUpdateNotificationDto>();
        }
    }
}
