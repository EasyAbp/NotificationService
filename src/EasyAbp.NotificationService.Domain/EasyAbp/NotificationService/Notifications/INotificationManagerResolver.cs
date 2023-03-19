using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Notifications;

public interface INotificationManagerResolver
{
    INotificationManager Resolve([NotNull] string notificationMethod);
}