using System;
using System.ComponentModel;
namespace EasyAbp.NotificationService.Notifications.Dtos
{
    [Serializable]
    public class CreateUpdateNotificationDto
    {
        public Guid UserId { get; set; }

        public Guid NotificationInfoId { get; set; }

        public string NotificationMethod { get; set; }

        public bool? Success { get; set; }

        public DateTime? CompletionTime { get; set; }

        public string FailureReason { get; set; }

        public Guid? RetryForNotificationId { get; set; }
    }
}