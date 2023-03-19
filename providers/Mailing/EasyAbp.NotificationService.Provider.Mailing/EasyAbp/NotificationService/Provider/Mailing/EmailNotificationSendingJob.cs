using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Mailing;

public class EmailNotificationSendingJob : IAsyncBackgroundJob<EmailNotificationSendingJobArgs>, ITransientDependency
{
    private readonly EmailNotificationManager _emailNotificationManager;
    private readonly ICurrentTenant _currentTenant;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly INotificationRepository _notificationRepository;

    public EmailNotificationSendingJob(
        EmailNotificationManager emailNotificationManager,
        ICurrentTenant currentTenant,
        INotificationInfoRepository notificationInfoRepository,
        INotificationRepository notificationRepository)
    {
        _emailNotificationManager = emailNotificationManager;
        _currentTenant = currentTenant;
        _notificationInfoRepository = notificationInfoRepository;
        _notificationRepository = notificationRepository;
    }

    [UnitOfWork]
    public virtual async Task ExecuteAsync(EmailNotificationSendingJobArgs args)
    {
        using var changeTenant = _currentTenant.Change(args.TenantId);

        var notification = await _notificationRepository.GetAsync(args.NotificationId);
        var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

        await _emailNotificationManager.SendNotificationsAsync(new List<Notification> { notification },
            notificationInfo);
    }
}