using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class CreateWeChatOfficialTemplateMessageNotificationEventHandler : IDistributedEventHandler<CreateWeChatOfficialTemplateMessageNotificationEto>,
    ITransientDependency
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly WeChatOfficialTemplateMessageNotificationManager _weChatOfficialTemplateMessageNotificationManager;

    public CreateWeChatOfficialTemplateMessageNotificationEventHandler(
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        WeChatOfficialTemplateMessageNotificationManager weChatOfficialTemplateMessageNotificationManager)
    {
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _weChatOfficialTemplateMessageNotificationManager = weChatOfficialTemplateMessageNotificationManager;
    }

    [UnitOfWork(true)]
    public virtual async Task HandleEventAsync(CreateWeChatOfficialTemplateMessageNotificationEto eventData)
    {
        var result = await _weChatOfficialTemplateMessageNotificationManager.CreateAsync(eventData);

        await _notificationInfoRepository.InsertAsync(result.Item2, true);

        foreach (var notification in result.Item1)
        {
            await _notificationRepository.InsertAsync(notification, true);
        }
    }
}