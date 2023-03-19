using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;

namespace EasyAbp.NotificationService.Notifications;

public interface INotificationManager
{
    Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model);

    Task SendNotificationsAsync(List<Notification> notifications, NotificationInfo notificationInfo,
        bool autoUpdateWithRepository = true);
}