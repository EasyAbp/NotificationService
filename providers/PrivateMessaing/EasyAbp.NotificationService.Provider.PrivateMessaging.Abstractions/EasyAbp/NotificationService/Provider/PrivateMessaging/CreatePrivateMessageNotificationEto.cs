using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

[Serializable]
public class CreatePrivateMessageNotificationEto : CreateNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    [NotNull]
    public string Title
    {
        get => this.GetTitle();
        set => this.SetTitle(value);
    }

    [CanBeNull]
    public string Content
    {
        get => this.GetContent();
        set => this.SetContent(value);
    }

    public CreatePrivateMessageNotificationEto()
    {
    }

    public CreatePrivateMessageNotificationEto(
        Guid? tenantId,
        IEnumerable<Guid> userIds,
        [NotNull] string title,
        [CanBeNull] string content) : base(NotificationProviderPrivateMessagingConsts.NotificationMethod, userIds)
    {
        TenantId = tenantId;
        Title = title;
        Content = content;
    }

    public CreatePrivateMessageNotificationEto(
        Guid? tenantId,
        Guid userId,
        [NotNull] string title,
        [CanBeNull] string content) : base(NotificationProviderPrivateMessagingConsts.NotificationMethod, userId)
    {
        TenantId = tenantId;
        Title = title;
        Content = content;
    }
}