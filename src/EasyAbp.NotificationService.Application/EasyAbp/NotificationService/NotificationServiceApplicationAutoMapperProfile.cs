using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Notifications.Dtos;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using AutoMapper;

namespace EasyAbp.NotificationService
{
    public class NotificationServiceApplicationAutoMapperProfile : Profile
    {
        public NotificationServiceApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationInfo, NotificationInfoDto>();
        }
    }
}
