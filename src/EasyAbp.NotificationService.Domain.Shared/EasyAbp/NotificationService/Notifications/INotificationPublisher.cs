using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Notifications
{
    public interface INotificationPublisher
    {
        Task PublishAsync<TCreateNotificationEto>(NotificationDefinition<TCreateNotificationEto> notificationDefinition,
            IEnumerable<Guid> userIds) where TCreateNotificationEto : CreateNotificationEto;
    }
}