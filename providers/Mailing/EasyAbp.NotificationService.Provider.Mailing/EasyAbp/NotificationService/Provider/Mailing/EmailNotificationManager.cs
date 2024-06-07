using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Emailing;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Mailing;

public class EmailNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod => NotificationProviderMailingConsts.NotificationMethod;

    protected IEmailSender EmailSender => LazyServiceProvider.LazyGetRequiredService<IEmailSender>();

    protected IUserEmailAddressProvider UserEmailAddressProvider =>
        LazyServiceProvider.LazyGetRequiredService<IUserEmailAddressProvider>();

    [UnitOfWork(true)]
    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        notificationInfo.SetMailingData(model.GetSubject(), model.GetBody());

        var notifications = await CreateNotificationsAsync(notificationInfo, model);

        return (notifications, notificationInfo);
    }

    [UnitOfWork]
    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var userEmailAddress = await UserEmailAddressProvider.GetAsync(notification.UserId);

        if (userEmailAddress.IsNullOrWhiteSpace())
        {
            await SetNotificationResultAsync(
                notification, false, NotificationConsts.FailureReasons.ReceiverInfoNotFound);

            return;
        }

        try
        {
            await EmailSender.SendAsync(userEmailAddress,
                notificationInfo.GetMailingSubject(),
                notificationInfo.GetMailingBody());

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