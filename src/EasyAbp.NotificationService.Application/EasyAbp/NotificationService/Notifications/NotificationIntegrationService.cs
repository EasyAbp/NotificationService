using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.IntegrationServices;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.NotificationService.Notifications;

public class NotificationIntegrationService : ApplicationService, INotificationIntegrationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly INotificationManagerResolver _notificationManagerResolver;

    public NotificationIntegrationService(
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        INotificationManagerResolver notificationManagerResolver)
    {
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _notificationManagerResolver = notificationManagerResolver;
    }

    public virtual async Task<ListResultDto<NotificationDto>> CreateAsync(CreateNotificationInfoModel input)
    {
        var manager = _notificationManagerResolver.Resolve(input.NotificationMethod);

        var result = await manager.CreateAsync(input);

        await _notificationInfoRepository.InsertAsync(result.Item2, true);

        foreach (var notification in result.Item1)
        {
            await _notificationRepository.InsertAsync(notification, true);
        }

        return new ListResultDto<NotificationDto>(
            ObjectMapper.Map<List<Notification>, List<NotificationDto>>(result.Item1));
    }

    public virtual async Task<ListResultDto<NotificationDto>> QuickSendAsync(CreateNotificationInfoModel input)
    {
        var manager = _notificationManagerResolver.Resolve(input.NotificationMethod);

        var result = await manager.CreateAsync(input);

        await _notificationInfoRepository.InsertAsync(result.Item2, true);

        foreach (var notification in result.Item1)
        {
            await _notificationRepository.InsertAsync(notification, true);
        }

        await manager.SendNotificationsAsync(result.Item1, result.Item2);

        return new ListResultDto<NotificationDto>(
            ObjectMapper.Map<List<Notification>, List<NotificationDto>>(result.Item1));
    }
}