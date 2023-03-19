using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.PrivateMessaging.PrivateMessages;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public class PrivateMessageNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod => NotificationProviderPrivateMessagingConsts.NotificationMethod;

    protected IDistributedEventBus DistributedEventBus =>
        LazyServiceProvider.LazyGetRequiredService<IDistributedEventBus>();

    [UnitOfWork(true)]
    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(
        CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        notificationInfo.SetPrivateMessagingData(model.GetTitle(), model.GetContent());

        var notifications = await CreateNotificationsAsync(notificationInfo, model.UserIds);

        return (notifications, notificationInfo);
    }

    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var eto = new SendPrivateMessageEto(
            notification.TenantId,
            notification.CreatorId,
            notification.UserId,
            notificationInfo.GetPrivateMessagingTitle(),
            notificationInfo.GetPrivateMessagingContent());

        eto.SetProperty(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName, notification.Id);

        await DistributedEventBus.PublishAsync(eto);
    }
}