using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.NotificationService.Notifications.Dtos
{
    [Serializable]
    public class NotificationDto : CreationAuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public Guid NotificationInfoId { get; set; }

        public string NotificationMethod { get; set; }

        public bool? Success { get; set; }

        public DateTime? CompletionTime { get; set; }

        public string FailureReason { get; set; }

        public Guid? RetryForNotificationId { get; set; }
    }
}