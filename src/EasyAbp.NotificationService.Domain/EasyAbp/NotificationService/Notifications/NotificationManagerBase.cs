using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Notifications;

public abstract class NotificationManagerBase : DomainService, INotificationManager
{
    public static string UnknownUserName = "__UNKNOWN";

    protected abstract string NotificationMethod { get; }

    protected INotificationRepository NotificationRepository =>
        LazyServiceProvider.LazyGetRequiredService<INotificationRepository>();

    protected INotificationInfoRepository NotificationInfoRepository =>
        LazyServiceProvider.LazyGetRequiredService<INotificationInfoRepository>();

    protected IExternalUserLookupServiceProvider ExternalUserLookupServiceProvider =>
        LazyServiceProvider.LazyGetRequiredService<IExternalUserLookupServiceProvider>();

    public abstract Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model);

    protected virtual async Task<List<Notification>> CreateNotificationsAsync(NotificationInfo notificationInfo,
        CreateNotificationInfoModel model)
    {
        if (model.Users is not null)
        {
            return model.Users.Select(user => new Notification(GuidGenerator.Create(), CurrentTenant.Id, user.Id,
                user.UserName, notificationInfo.Id, NotificationMethod)).ToList();
        }

        var notifications = new List<Notification>();

        foreach (var userId in model.UserIds)
        {
            var user = await ExternalUserLookupServiceProvider.FindByIdAsync(userId);

            var userName = user?.UserName ?? UnknownUserName;

            notifications.Add(new Notification(GuidGenerator.Create(), CurrentTenant.Id, userId, userName,
                notificationInfo.Id, NotificationMethod));
        }

        return notifications;
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