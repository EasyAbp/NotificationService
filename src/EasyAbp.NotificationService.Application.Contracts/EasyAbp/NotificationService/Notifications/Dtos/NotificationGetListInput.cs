using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.NotificationService.Notifications.Dtos;

[Serializable]
public class NotificationGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? UserId { get; set; }

    [CanBeNull]
    public string UserName { get; set; }

    public Guid? NotificationInfoId { get; set; }

    [CanBeNull]
    public string NotificationMethod { get; set; }

    public bool? Success { get; set; }

    public DateTime? CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? CompletionTime { get; set; }

    [CanBeNull]
    public string FailureReason { get; set; }

    public Guid? RetryForNotificationId { get; set; }
}