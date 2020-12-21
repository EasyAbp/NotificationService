using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Notifications
{
    public interface INotificationPublisher
    {
        Task PublishAsync(CreateNotificationEto createNotificationEto);
    }
}