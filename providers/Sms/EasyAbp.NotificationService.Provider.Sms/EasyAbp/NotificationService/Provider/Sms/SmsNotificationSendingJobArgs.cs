using System;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.NotificationService.Provider.Sms
{
    [Serializable]
    public class SmsNotificationSendingJobArgs : IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public Guid NotificationId { get; set; }

        public SmsNotificationSendingJobArgs()
        {
        }

        public SmsNotificationSendingJobArgs(Guid? tenantId, Guid notificationId)
        {
            TenantId = tenantId;
            NotificationId = notificationId;
        }
    }
}