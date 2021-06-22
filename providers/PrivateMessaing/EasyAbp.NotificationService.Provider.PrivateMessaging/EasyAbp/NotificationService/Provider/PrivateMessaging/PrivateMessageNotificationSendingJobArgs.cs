using System;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class PrivateMessageNotificationSendingJobArgs
    {
        public Guid NotificationId { get; set; }

        public PrivateMessageNotificationSendingJobArgs(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}