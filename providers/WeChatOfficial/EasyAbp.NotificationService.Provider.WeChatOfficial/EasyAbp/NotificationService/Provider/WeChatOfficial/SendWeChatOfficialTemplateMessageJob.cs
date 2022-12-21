using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class SendWeChatOfficialTemplateMessageJob : AsyncBackgroundJob<SendWeChatOfficialTemplateMessageJobArgs>,
    ITransientDependency
{
    private readonly IClock _clock;
    private readonly IAbpWeChatServiceFactory _abpWeChatServiceFactory;
    private readonly ITemplateMessageDataModelJsonSerializer _jsonSerializer;
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly IWeChatOfficialUserOpenIdProvider _userOpenIdProvider;

    public SendWeChatOfficialTemplateMessageJob(
        IClock clock,
        IAbpWeChatServiceFactory abpWeChatServiceFactory,
        ITemplateMessageDataModelJsonSerializer jsonSerializer,
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        IWeChatOfficialUserOpenIdProvider userOpenIdProvider)
    {
        _clock = clock;
        _abpWeChatServiceFactory = abpWeChatServiceFactory;
        _jsonSerializer = jsonSerializer;
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _userOpenIdProvider = userOpenIdProvider;
    }

    [UnitOfWork]
    public override async Task ExecuteAsync(SendWeChatOfficialTemplateMessageJobArgs args)
    {
        var notification = await _notificationRepository.GetAsync(args.NotificationId);

        var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

        var dataModel = notificationInfo.GetWeChatOfficialTemplateMessageData(_jsonSerializer);

        await SendTemplateMessageAsync(dataModel, notification);
    }

    protected virtual async Task SendTemplateMessageAsync(WeChatOfficialTemplateMessageDataModel dataModel,
        Notification notification)
    {
        var openId = await ResolveOpenIdAsync(dataModel.AppId, notification.UserId);

        if (openId.IsNullOrWhiteSpace())
        {
            await SaveNotificationResultAsync(notification, false,
                NotificationProviderWeChatOfficialConsts.UserOpenIdNotFoundFailureReason);

            return;
        }

        TemplateMessageWeService templateMessageWeService;

        try
        {
            templateMessageWeService =
                await _abpWeChatServiceFactory.CreateAsync<TemplateMessageWeService>(dataModel.AppId);
        }
        catch (Exception e)
        {
            await SaveNotificationResultAsync(notification, false,
                NotificationProviderWeChatOfficialConsts.FailedToCreateTemplateMessageWeServiceFailureReason);

            Logger.LogException(e);

            return;
        }

        var response = await templateMessageWeService.SendMessageAsync(
            openId, dataModel.TemplateId, dataModel.Url, dataModel.Data, dataModel.MiniProgram);

        if (response.ErrorCode == 0)
        {
            await SaveNotificationResultAsync(notification, true);
        }
        else
        {
            await SaveNotificationResultAsync(notification, false, response.ErrorMessage);
        }
    }

    protected virtual async Task<string> ResolveOpenIdAsync([CanBeNull] string appId, Guid userId)
    {
        if (appId.IsNullOrWhiteSpace())
        {
            return await _userOpenIdProvider.GetOrNullAsync(userId);
        }

        return await _userOpenIdProvider.GetOrNullAsync(appId!, userId);
    }

    protected virtual async Task SaveNotificationResultAsync(Notification notification, bool success,
        [CanBeNull] string failureReason = null)
    {
        notification.SetResult(_clock, success, failureReason);

        await _notificationRepository.UpdateAsync(notification, true);
    }
}