using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification
{
    public class IndexModel : NotificationServicePageModel
    {
        public NotificationFilterInput NotificationFilter { get; set; } = null!;

        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }

    public class NotificationFilterInput
    {
        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationUserId")]
        public Guid? UserId { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationUserName")]
        [CanBeNull]
        public string UserName { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationNotificationInfoId")]
        public Guid? NotificationInfoId { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationNotificationMethod")]
        [CanBeNull]
        public string NotificationMethod { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationSuccess")]
        public bool? Success { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationCreationTime")]
        public DateTime? CreationTime { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationCreatorId")]
        public Guid? CreatorId { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationCompletionTime")]
        public DateTime? CompletionTime { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationFailureReason")]
        [CanBeNull]
        public string FailureReason { get; set; }

        [FormControlSize(AbpFormControlSize.Small)]
        [Display(Name = "NotificationRetryForNotificationId")]
        public Guid? RetryForNotificationId { get; set; }
    }
}