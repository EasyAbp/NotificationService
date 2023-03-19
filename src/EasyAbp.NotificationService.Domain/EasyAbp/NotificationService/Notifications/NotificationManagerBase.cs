using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Notifications;

public abstract class NotificationManagerBase : DomainService, INotificationManager
{
    protected abstract string NotificationMethod { get; }

    protected INotificationRepository NotificationRepository =>
        LazyServiceProvider.LazyGetRequiredService<INotificationRepository>();

    protected INotificationInfoRepository NotificationInfoRepository =>
        LazyServiceProvider.LazyGetRequiredService<INotificationInfoRepository>();

    public abstract Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model);

    protected virtual Task<List<Notification>> CreateNotificationsAsync(NotificationInfo notificationInfo,
        IEnumerable<Guid> userIds)
    {
        return Task.FromResult(userIds.Select(userId => new Notification(GuidGenerator.Create(), CurrentTenant.Id,
            userId,
            notificationInfo.Id, NotificationMethod)).ToList());
    }

    [UnitOfWork]
    protected virtual Task SetNotificationResultAsync(Notification notification, bool success,
        [CanBeNull] string failureReason = null)
    {
        notification.SetResult(Clock, success, failureReason);

        return Task.CompletedTask;
    }

    [UnitOfWork(true)]
    public virtual async Task SendNotificationsAsync(List<Notification> notifications,
        NotificationInfo notificationInfo, bool autoUpdateWithRepository = true)
    {
        foreach (var notification in notifications)
        {
            await SendNotificationAsync(notification, notificationInfo);

            if (autoUpdateWithRepository)
            {
                await NotificationRepository.UpdateAsync(notification, true);
            }
        }
    }

    protected abstract Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo);
}