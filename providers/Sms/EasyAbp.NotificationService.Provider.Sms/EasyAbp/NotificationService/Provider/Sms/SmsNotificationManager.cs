using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.Logging;
using Volo.Abp.Json;
using Volo.Abp.Sms;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Sms;

public class SmsNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod => NotificationProviderSmsConsts.NotificationMethod;

    protected ISmsSender SmsSender => LazyServiceProvider.LazyGetRequiredService<ISmsSender>();

    protected IJsonSerializer JsonSerializer => LazyServiceProvider.LazyGetRequiredService<IJsonSerializer>();

    protected IUserPhoneNumberProvider UserPhoneNumberProvider =>
        LazyServiceProvider.LazyGetRequiredService<IUserPhoneNumberProvider>();


    [UnitOfWork(true)]
    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        notificationInfo.SetSmsData(model.GetText(), JsonSerializer.Serialize(model.GetProperties(JsonSerializer)));

        var notifications = await CreateNotificationsAsync(notificationInfo, model.UserIds);

        return (notifications, notificationInfo);
    }

    [UnitOfWork]
    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var userPhoneNumber = await UserPhoneNumberProvider.GetAsync(notification.UserId);

        if (userPhoneNumber.IsNullOrWhiteSpace())
        {
            await SetNotificationResultAsync(notification, false,
                NotificationConsts.FailureReasons.ReceiverInfoNotFound);

            return;
        }

        var properties =
            JsonSerializer.Deserialize<IDictionary<string, object>>(notificationInfo.GetSmsJsonProperties());

        var smsMessage = new SmsMessage(userPhoneNumber, notificationInfo.GetSmsText());

        foreach (var property in properties)
        {
            smsMessage.Properties.AddIfNotContains(property);
        }

        try
        {
            await SmsSender.SendAsync(smsMessage);
            await SetNotificationResultAsync(notification, true);
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            await SetNotificationResultAsync(notification, false, e.ToString());
        }
    }
}