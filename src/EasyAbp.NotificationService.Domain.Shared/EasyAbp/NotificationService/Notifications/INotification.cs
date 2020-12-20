using System;

namespace EasyAbp.NotificationService.Notifications
{
    public interface INotification
    {
        Guid UserId { get; }
    }
}