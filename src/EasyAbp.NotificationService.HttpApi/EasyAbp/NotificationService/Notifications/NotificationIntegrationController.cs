using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using EasyAbp.NotificationService.IntegrationServices;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.NotificationService.Notifications;

[RemoteService(Name = NotificationServiceRemoteServiceConsts.RemoteServiceName)]
[Route("/integration-api/notification-service/notification")]
public class NotificationIntegrationController : NotificationServiceController, INotificationIntegrationService
{
    private readonly INotificationIntegrationService _service;

    public NotificationIntegrationController(INotificationIntegrationService service)
    {
        _service = service;
    }

    [HttpPost]
    public virtual Task<ListResultDto<NotificationDto>> CreateAsync(CreateNotificationInfoModel input)
    {
        return _service.CreateAsync(input);
    }

    [HttpPost]
    [Route("quick-send")]
    public virtual Task<ListResultDto<NotificationDto>> QuickSendAsync(CreateNotificationInfoModel input)
    {
        return _service.QuickSendAsync(input);
    }
}