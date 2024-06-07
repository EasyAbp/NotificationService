using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification.ViewModels;

public class NotificationDetailsViewModel
{
    [ReadOnlyInput]
    [Display(Name = "NotificationId")]
    public Guid Id { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationUserId")]
    public Guid UserId { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationUserName")]
    public string UserName { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationNotificationInfoId")]
    public Guid NotificationInfoId { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationNotificationMethod")]
    public string NotificationMethod { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationSuccess")]
    public bool? Success { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationCreationTime")]
    public DateTime CreationTime { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationCreatorId")]
    public Guid? CreatorId { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationCompletionTime")]
    public DateTime? CompletionTime { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationFailureReason")]
    public string FailureReason { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationRetryForNotificationId")]
    public Guid? RetryForNotificationId { get; set; }

    [ReadOnlyInput]
    [Display(Name = "NotificationProperties")]
    [TextArea(Rows = 8)]
    public string Properties { get; set; }

    public NotificationDetailsViewModel()
    {
    }

    public NotificationDetailsViewModel(Guid id, Guid userId, string userName, Guid infoId, string method,
        bool? success, DateTime? completionTime, string failureReason, Guid? retryForNotificationId, string properties,
        DateTime creationTime, Guid? creatorId)
    {
        Id = id;
        UserId = userId;
        UserName = userName;
        NotificationInfoId = infoId;
        NotificationMethod = method;
        Success = success;
        CompletionTime = completionTime;
        FailureReason = failureReason;
        RetryForNotificationId = retryForNotificationId;
        Properties = properties;
        CreationTime = creationTime;
        CreatorId = creatorId;
    }
}