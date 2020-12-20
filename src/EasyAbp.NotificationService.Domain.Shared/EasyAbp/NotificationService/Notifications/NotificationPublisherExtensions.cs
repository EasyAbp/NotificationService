using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Notifications
{
    public static class NotificationPublisherExtensions
    {
        public static async Task PublishAsync<TCreateNotificationEto>(
            this INotificationPublisher notificationPublisher,
            NotificationDefinition<TCreateNotificationEto> notificationDefinition, Guid userId)
            where TCreateNotificationEto : CreateNotificationEto
        {
            await notificationPublisher.PublishAsync(notificationDefinition, new List<Guid> {userId});
        }
    }
}