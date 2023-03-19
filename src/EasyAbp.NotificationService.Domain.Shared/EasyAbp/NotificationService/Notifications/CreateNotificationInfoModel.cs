using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.NotificationService.Notifications;

[Serializable]
public class CreateNotificationInfoModel : ExtensibleObject
{
    public string NotificationMethod { get; set; }

    public IEnumerable<Guid> UserIds { get; set; }

    public CreateNotificationInfoModel()
    {
    }

    public CreateNotificationInfoModel([NotNull] string notificationMethod, IEnumerable<Guid> userIds)
    {
        NotificationMethod = notificationMethod;
        UserIds = userIds;
    }

    public CreateNotificationInfoModel([NotNull] string notificationMethod, Guid userId)
    {
        NotificationMethod = notificationMethod;
        UserIds = new List<Guid> { userId };
    }
}