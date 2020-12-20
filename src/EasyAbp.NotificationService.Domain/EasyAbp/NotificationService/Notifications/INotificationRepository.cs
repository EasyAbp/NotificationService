using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.NotificationService.Notifications
{
    public interface INotificationRepository : IRepository<Notification, Guid>
    {
    }
}