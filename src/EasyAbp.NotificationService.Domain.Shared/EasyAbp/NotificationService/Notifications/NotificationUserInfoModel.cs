using System;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Notifications;

[Serializable]
public class NotificationUserInfoModel
{
    public Guid Id { get; set; }

    [NotNull]
    public string UserName { get; set; } = null!;

    public NotificationUserInfoModel()
    {
    }

    public NotificationUserInfoModel(Guid id, [NotNull] string userName)
    {
        Id = id;
        UserName = userName;
    }
}