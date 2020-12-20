using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.NotificationService.Notifications
{
    public class NotificationPublisher : INotificationPublisher, ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public NotificationPublisher(
            IDistributedEventBus distributedEventBus)
        {
            _distributedEventBus = distributedEventBus;
        }
        
        public virtual async Task PublishAsync<TCreateNotificationEto>(
            NotificationDefinition<TCreateNotificationEto> notificationDefinition, IEnumerable<Guid> userIds)
            where TCreateNotificationEto : CreateNotificationEto
        {
            await _distributedEventBus.PublishAsync(notificationDefinition.CreateAsync(userIds));
        }
    }
}