using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Sms;

public class CreateSmsNotificationEventHandler : IDistributedEventHandler<CreateSmsNotificationEto>,
    ITransientDependency
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly SmsNotificationManager _smsNotificationManager;

    public CreateSmsNotificationEventHandler(
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        SmsNotificationManager smsNotificationManager)
    {
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _smsNotificationManager = smsNotificationManager;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreateSmsNotificationEto eventData)
    {
        var result = await _smsNotificationManager.CreateAsync(eventData);

        await _notificationInfoRepository.InsertAsync(result.Item2, true);

        foreach (var notification in result.Item1)
        {
            await _notificationRepository.InsertAsync(notification, true);
        }
    }
}