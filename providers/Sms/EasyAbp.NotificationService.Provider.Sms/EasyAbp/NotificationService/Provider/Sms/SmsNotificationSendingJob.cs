using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Sms;

public class SmsNotificationSendingJob : IAsyncBackgroundJob<SmsNotificationSendingJobArgs>, ITransientDependency
{
    private readonly SmsNotificationManager _smsNotificationManager;
    private readonly ICurrentTenant _currentTenant;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly INotificationRepository _notificationRepository;

    public SmsNotificationSendingJob(
        SmsNotificationManager smsNotificationManager,
        ICurrentTenant currentTenant,
        INotificationInfoRepository notificationInfoRepository,
        INotificationRepository notificationRepository)
    {
        _smsNotificationManager = smsNotificationManager;
        _currentTenant = currentTenant;
        _notificationInfoRepository = notificationInfoRepository;
        _notificationRepository = notificationRepository;
    }

    [UnitOfWork]
    public virtual async Task ExecuteAsync(SmsNotificationSendingJobArgs args)
    {
        using var changeTenant = _currentTenant.Change(args.TenantId);

        var notification = await _notificationRepository.GetAsync(args.NotificationId);
        var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

        await _smsNotificationManager.SendNotificationsAsync(new List<Notification> { notification }, notificationInfo);
    }
}