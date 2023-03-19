using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.IntegrationServices;

[IntegrationService]
public interface INotificationIntegrationService : IApplicationService
{
    /// <summary>
    /// Create notification entities and start sending in the background.
    /// </summary>
    Task<ListResultDto<NotificationDto>> CreateAsync(CreateNotificationInfoModel input);

    /// <summary>
    /// Create notification entities and send them immediately.
    /// </summary>
    Task<ListResultDto<NotificationDto>> QuickSendAsync(CreateNotificationInfoModel input);
}