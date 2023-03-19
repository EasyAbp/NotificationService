using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public class PrivateMessageNotificationCreationEventHandler : NotificationCreationEventHandlerBase
{
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PrivateMessageNotificationCreationEventHandler(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceScopeFactory serviceScopeFactory)
    {
        _unitOfWorkManager = unitOfWorkManager;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override string NotificationMethod => NotificationProviderPrivateMessagingConsts.NotificationMethod;

    protected override Task InternalHandleEventAsync(EntityCreatedEventData<Notification> eventData)
    {
        // todo: should use Stepping.NET or distributed event bus to ensure done?
        _unitOfWorkManager.Current.OnCompleted(async () =>
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var notificationInfoRepository = scope.ServiceProvider.GetRequiredService<INotificationInfoRepository>();

            var privateMessageNotificationManager =
                scope.ServiceProvider.GetRequiredService<PrivateMessageNotificationManager>();

            var notificationInfo = await notificationInfoRepository.GetAsync(eventData.Entity.NotificationInfoId);

            await privateMessageNotificationManager.SendNotificationsAsync(new List<Notification> { eventData.Entity },
                notificationInfo);
        });

        return Task.CompletedTask;
    }
}