using System;

using System.ComponentModel.DataAnnotations;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification.ViewModels
{
    public class CreateEditNotificationViewModel
    {
        [Display(Name = "NotificationUserId")]
        public Guid UserId { get; set; }

        [Display(Name = "NotificationNotificationInfoId")]
        public Guid NotificationInfoId { get; set; }

        [Display(Name = "NotificationNotificationMethod")]
        public string NotificationMethod { get; set; }

        [Display(Name = "NotificationSuccess")]
        public bool? Success { get; set; }

        [Display(Name = "NotificationCompletionTime")]
        public DateTime? CompletionTime { get; set; }

        [Display(Name = "NotificationFailureReason")]
        public string FailureReason { get; set; }

        [Display(Name = "NotificationRetryNotificationId")]
        public Guid? RetryNotificationId { get; set; }
    }
}