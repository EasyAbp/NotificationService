using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class WeChatOfficialTemplateMessageNotificationSendingJob :
    IAsyncBackgroundJob<WeChatOfficialTemplateMessageNotificationSendingJobArgs>, ITransientDependency
{
    private readonly WeChatOfficialTemplateMessageNotificationManager _weChatOfficialTemplateMessageNotificationManager;
    private readonly ICurrentTenant _currentTenant;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly INotificationRepository _notificationRepository;

    public WeChatOfficialTemplateMessageNotificationSendingJob(
        WeChatOfficialTemplateMessageNotificationManager weChatOfficialTemplateMessageNotificationManager,
        ICurrentTenant currentTenant,
        INotificationInfoRepository notificationInfoRepository,
        INotificationRepository notificationRepository)
    {
        _weChatOfficialTemplateMessageNotificationManager = weChatOfficialTemplateMessageNotificationManager;
        _currentTenant = currentTenant;
        _notificationInfoRepository = notificationInfoRepository;
        _notificationRepository = notificationRepository;
    }

    [UnitOfWork]
    public virtual async Task ExecuteAsync(WeChatOfficialTemplateMessageNotificationSendingJobArgs args)
    {
        using var changeTenant = _currentTenant.Change(args.TenantId);

        var notification = await _notificationRepository.GetAsync(args.NotificationId);
        var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

        await _weChatOfficialTemplateMessageNotificationManager.SendNotificationsAsync(
            new List<Notification> { notification }, notificationInfo);
    }
}