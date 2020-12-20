using System;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class EmailNotificationSendingJobArgs
    {
        public Guid NotificationId { get; set; }

        public EmailNotificationSendingJobArgs(Guid notificationId)
        {
            NotificationId = notificationId;
        }
    }
}