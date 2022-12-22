using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Serializable]
public class SendWeChatOfficialTemplateMessageJobArgs : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid NotificationId { get; set; }

    public SendWeChatOfficialTemplateMessageJobArgs()
    {
    }

    public SendWeChatOfficialTemplateMessageJobArgs(Guid? tenantId, Guid notificationId)
    {
        TenantId = tenantId;
        NotificationId = notificationId;
    }
}