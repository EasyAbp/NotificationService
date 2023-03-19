using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Serializable]
public class WeChatOfficialTemplateMessageNotificationSendingJobArgs : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid NotificationId { get; set; }

    public WeChatOfficialTemplateMessageNotificationSendingJobArgs()
    {
    }

    public WeChatOfficialTemplateMessageNotificationSendingJobArgs(Guid? tenantId, Guid notificationId)
    {
        TenantId = tenantId;
        NotificationId = notificationId;
    }
}