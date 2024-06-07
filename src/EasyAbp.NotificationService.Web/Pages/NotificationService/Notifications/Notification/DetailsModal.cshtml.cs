using System;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp.Json;

namespace EasyAbp.NotificationService.Web.Pages.NotificationService.Notifications.Notification;

public class DetailsModal : NotificationServicePageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public NotificationDetailsViewModel ViewModel { get; set; }

    private readonly IJsonSerializer _jsonSerializer;
    private readonly INotificationAppService _notificationAppService;
    private readonly INotificationInfoAppService _notificationInfoAppService;

    public DetailsModal(
        IJsonSerializer jsonSerializer,
        INotificationAppService notificationAppService,
        INotificationInfoAppService notificationInfoAppService)
    {
        _jsonSerializer = jsonSerializer;
        _notificationAppService = notificationAppService;
        _notificationInfoAppService = notificationInfoAppService;
    }

    public virtual async Task OnGetAsync()
    {
        var notification = await _notificationAppService.GetAsync(Id);
        var notificationInfo = await _notificationInfoAppService.GetAsync(notification.NotificationInfoId);

        var properties = _jsonSerializer.Serialize(notificationInfo.ExtraProperties);
        var beautifiedProperties = JToken.Parse(properties).ToString(Formatting.Indented);

        ViewModel = new NotificationDetailsViewModel(notification.Id, notification.UserId, notification.UserName,
            notification.NotificationInfoId, notification.NotificationMethod, notification.Success,
            notification.CompletionTime, notification.FailureReason, notification.RetryForNotificationId,
            beautifiedProperties, notification.CreationTime, notification.CreatorId);
    }
}