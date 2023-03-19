using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.PrivateMessaging.PrivateMessages;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging;

public class PrivateMessageNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod => NotificationProviderPrivateMessagingConsts.NotificationMethod;

    protected IPrivateMessageIntegrationService PrivateMessageIntegrationService =>
        LazyServiceProvider.LazyGetRequiredService<IPrivateMessageIntegrationService>();

    [UnitOfWork(true)]
    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(
        CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        notificationInfo.SetPrivateMessagingData(model.GetTitle(), model.GetContent(), model.GetSendFromCreator());

        var notifications = await CreateNotificationsAsync(notificationInfo, model.UserIds);

        return (notifications, notificationInfo);
    }

    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var model = new CreatePrivateMessageInfoModel(
            notificationInfo.GetPrivateMessagingSendFromCreator() ? notification.CreatorId : null,
            notification.UserId,
            notificationInfo.GetPrivateMessagingTitle(),
            notificationInfo.GetPrivateMessagingContent());

        model.SetProperty(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName, notification.Id);

        try
        {
            await PrivateMessageIntegrationService.CreateAsync(model);
            await SetNotificationResultAsync(notification, true);
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            var message = e is IHasErrorCode b ? b.Code ?? e.Message : e.ToString();
            await SetNotificationResultAsync(notification, false, message);
        }
    }
}