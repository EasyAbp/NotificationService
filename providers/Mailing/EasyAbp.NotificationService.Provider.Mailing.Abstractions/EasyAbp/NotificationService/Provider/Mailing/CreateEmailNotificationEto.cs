using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Mailing;

[Serializable]
public class CreateEmailNotificationEto : CreateNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    [NotNull]
    public string Subject
    {
        get => this.GetSubject();
        set => this.SetSubject(value);
    }

    [CanBeNull]
    public string Body
    {
        get => this.GetBody();
        set => this.SetBody(value);
    }

    public CreateEmailNotificationEto()
    {
    }

    public CreateEmailNotificationEto(
        Guid? tenantId,
        IEnumerable<Guid> userIds,
        [NotNull] string subject,
        [CanBeNull] string body) : base(NotificationProviderMailingConsts.NotificationMethod, userIds)
    {
        TenantId = tenantId;
        Subject = subject;
        Body = body;
    }

    public CreateEmailNotificationEto(
        Guid? tenantId,
        Guid userId,
        [NotNull] string subject,
        [CanBeNull] string body) : base(NotificationProviderMailingConsts.NotificationMethod, userId)
    {
        TenantId = tenantId;
        Subject = subject;
        Body = body;
    }
}