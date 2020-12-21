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

        public virtual async Task PublishAsync(CreateNotificationEto createNotificationEto)
        {
            await _distributedEventBus.PublishAsync(createNotificationEto);
        }
    }
}