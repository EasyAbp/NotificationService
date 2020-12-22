using System;

namespace EasyAbp.NotificationService.Provider.Sms
{
    public class SmsNotificationSendingJobArgs
    {
        public Guid NotificationId { get; set; }

        public SmsNotificationSendingJobArgs(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}