using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class
    WeChatOfficialTemplateMessageNotificationManager : NotificationManagerBase
{
    protected override string NotificationMethod =>
        NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod;

    protected IWeChatTemplateMessageNotificationSender WeChatTemplateMessageNotificationSender =>
        LazyServiceProvider.LazyGetRequiredService<IWeChatTemplateMessageNotificationSender>();

    protected IWeChatOfficialUserOpenIdProvider WeChatOfficialUserOpenIdProvider =>
        LazyServiceProvider.LazyGetRequiredService<IWeChatOfficialUserOpenIdProvider>();

    protected ITemplateMessageDataModelJsonSerializer TemplateMessageDataModelJsonSerializer =>
        LazyServiceProvider.LazyGetRequiredService<ITemplateMessageDataModelJsonSerializer>();


    [UnitOfWork(true)]
    public override async Task<(List<Notification>, NotificationInfo)> CreateAsync(CreateNotificationInfoModel model)
    {
        var notificationInfo = new NotificationInfo(GuidGenerator.Create(), CurrentTenant.Id);

        notificationInfo.SetWeChatOfficialTemplateMessageData(model.GetDataModel(TemplateMessageDataModelJsonSerializer), TemplateMessageDataModelJsonSerializer);

        var notifications = await CreateNotificationsAsync(notificationInfo, model.UserIds);

        return (notifications, notificationInfo);
    }

    [UnitOfWork]
    protected override async Task SendNotificationAsync(Notification notification, NotificationInfo notificationInfo)
    {
        var dataModel = notificationInfo.GetWeChatOfficialTemplateMessageData(TemplateMessageDataModelJsonSerializer);

        await SendTemplateMessageAsync(dataModel, notification);
    }

    [UnitOfWork]
    protected virtual async Task SendTemplateMessageAsync(WeChatOfficialTemplateMessageDataModel dataModel,
        Notification notification)
    {
        var openId = await ResolveOpenIdAsync(dataModel.AppId, notification.UserId);

        if (openId.IsNullOrWhiteSpace())
        {
            await SetNotificationResultAsync(notification, false,
                NotificationProviderWeChatOfficialConsts.UserOpenIdNotFoundFailureReason);

            return;
        }

        try
        {
            var response = await WeChatTemplateMessageNotificationSender.SendAsync(openId, dataModel);

            if (response.ErrorCode == 0)
            {
                await SetNotificationResultAsync(notification, true);
            }
            else
            {
                await SetNotificationResultAsync(notification, false,
                    $"[{response.ErrorCode}] {response.ErrorMessage}");
            }
        }
        catch (Exception e)
        {
            Logger.LogException(e);
            var message = e is IHasErrorCode b ? b.Code ?? e.Message : e.ToString();
            await SetNotificationResultAsync(notification, false, message);
        }
    }

    protected virtual async Task<string> ResolveOpenIdAsync([CanBeNull] string appId, Guid userId)
    {
        if (appId.IsNullOrWhiteSpace())
        {
            return await WeChatOfficialUserOpenIdProvider.GetOrNullAsync(userId);
        }

        return await WeChatOfficialUserOpenIdProvider.GetOrNullAsync(appId!, userId);
    }
}