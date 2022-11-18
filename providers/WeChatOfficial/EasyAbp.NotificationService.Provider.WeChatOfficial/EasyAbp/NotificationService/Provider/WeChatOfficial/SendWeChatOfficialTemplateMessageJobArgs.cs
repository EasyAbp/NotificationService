using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class SendWeChatOfficialTemplateMessageJobArgs : IMultiTenant
{
    public Guid? TenantId { get; set; }

    public Guid NotificationId { get; set; }

    protected SendWeChatOfficialTemplateMessageJobArgs()
    {
    }

    public SendWeChatOfficialTemplateMessageJobArgs(Guid? tenantId, Guid notificationId)
    {
        TenantId = tenantId;
        NotificationId = notificationId;
    }
}