using System;
using System.Collections.Generic;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Sms;

[Serializable]
public class CreateSmsNotificationEto : CreateNotificationInfoModel, IMultiTenant
{
    public Guid? TenantId { get; set; }

    [NotNull]
    public string Text
    {
        get => this.GetText();
        set => this.SetText(value);
    }

    [NotNull]
    public string JsonProperties
    {
        get => this.GetJsonProperties();
        set => this.SetJsonProperties(value);
    }

    public CreateSmsNotificationEto()
    {
    }

    public CreateSmsNotificationEto(
        Guid? tenantId,
        IEnumerable<NotificationUserInfoModel> users,
        [NotNull] string text,
        [NotNull] IDictionary<string, object> properties,
        IJsonSerializer jsonSerializer) :
        base(NotificationProviderSmsConsts.NotificationMethod, users)
    {
        TenantId = tenantId;
        Text = text;
        JsonProperties = jsonSerializer.Serialize(properties);
    }

    public CreateSmsNotificationEto(
        Guid? tenantId,
        IEnumerable<Guid> userIds,
        [NotNull] string text,
        [NotNull] IDictionary<string, object> properties,
        IJsonSerializer jsonSerializer) :
        base(NotificationProviderSmsConsts.NotificationMethod, userIds)
    {
        TenantId = tenantId;
        Text = text;
        JsonProperties = jsonSerializer.Serialize(properties);
    }

    public CreateSmsNotificationEto(
        Guid? tenantId,
        NotificationUserInfoModel user,
        [NotNull] string text,
        [NotNull] IDictionary<string, object> properties,
        IJsonSerializer jsonSerializer) :
        base(NotificationProviderSmsConsts.NotificationMethod, user)
    {
        TenantId = tenantId;
        Text = text;
        JsonProperties = jsonSerializer.Serialize(properties);
    }

    public CreateSmsNotificationEto(
        Guid? tenantId,
        Guid userId,
        [NotNull] string text,
        [NotNull] IDictionary<string, object> properties,
        IJsonSerializer jsonSerializer) :
        base(NotificationProviderSmsConsts.NotificationMethod, userId)
    {
        TenantId = tenantId;
        Text = text;
        JsonProperties = jsonSerializer.Serialize(properties);
    }
}