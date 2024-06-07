using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.NotificationService.Notifications;

[Serializable]
public abstract class CreateNotificationInfoModel : ExtensibleObject
{
    public string NotificationMethod { get; set; }

    public IEnumerable<NotificationUserInfoModel> Users { get; set; }

    public IEnumerable<Guid> UserIds { get; set; }

    public CreateNotificationInfoModel()
    {
    }

    public CreateNotificationInfoModel([NotNull] string notificationMethod, IEnumerable<Guid> userIds)
    {
        NotificationMethod = notificationMethod;
        UserIds = userIds;
        Users = null;
    }

    public CreateNotificationInfoModel([NotNull] string notificationMethod,
        IEnumerable<NotificationUserInfoModel> users)
    {
        NotificationMethod = notificationMethod;
        UserIds = null;
        Users = users;
    }

    public CreateNotificationInfoModel([NotNull] string notificationMethod, Guid userId)
    {
        NotificationMethod = notificationMethod;
        UserIds = new[] { userId };
        Users = null;
    }

    public CreateNotificationInfoModel([NotNull] string notificationMethod, NotificationUserInfoModel user)
    {
        NotificationMethod = notificationMethod;
        UserIds = null;
        Users = new[] { user };
    }
}