using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public class CreatePrivateMessageNotificationEventHandler :
    IDistributedEventHandler<CreatePrivateMessageNotificationEto>, ITransientDependency
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly PrivateMessageNotificationManager _privateMessageNotificationManager;

    public CreatePrivateMessageNotificationEventHandler(
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        PrivateMessageNotificationManager privateMessageNotificationManager)
    {
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _privateMessageNotificationManager = privateMessageNotificationManager;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreatePrivateMessageNotificationEto eventData)
    {
        var result = await _privateMessageNotificationManager.CreateAsync(eventData);

        await _notificationInfoRepository.InsertAsync(result.Item2, true);

        foreach (var notification in result.Item1)
        {
            await _notificationRepository.InsertAsync(notification, true);
        }
    }
}