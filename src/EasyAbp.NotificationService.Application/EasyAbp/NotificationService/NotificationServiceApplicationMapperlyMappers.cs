using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.NotificationInfos.Dtos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Notifications.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.NotificationService
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class NotificationToNotificationDtoMapper : MapperBase<Notification, NotificationDto>
    {
        public override partial NotificationDto Map(Notification source);

        public override partial void Map(Notification source, NotificationDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class NotificationInfoToNotificationInfoDtoMapper : MapperBase<NotificationInfo, NotificationInfoDto>
    {
        public override partial NotificationInfoDto Map(NotificationInfo source);

        public override partial void Map(NotificationInfo source, NotificationInfoDto destination);
    }
}
