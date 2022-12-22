using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    [Serializable]
    public class EmailNotificationSendingJobArgs : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid NotificationId { get; set; }

        public EmailNotificationSendingJobArgs()
        {
        }

        public EmailNotificationSendingJobArgs(Guid? tenantId, Guid notificationId)
        {
            TenantId = tenantId;
            NotificationId = notificationId;
        }
    }
}