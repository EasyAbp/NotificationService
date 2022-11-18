using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve.Contributors;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class SendWeChatOfficialTemplateMessageJob : AsyncBackgroundJob<SendWeChatOfficialTemplateMessageJobArgs>,
    ITransientDependency
{
    private readonly IClock _clock;
    private readonly ITemplateMessageDataModelJsonSerializer _jsonSerializer;
    private readonly TemplateMessageService _templateMessageService;
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly IWeChatOfficialAsyncLocal _weChatOfficialAsyncLocal;
    private readonly IWeChatOfficialUserOpenIdProvider _userOpenIdProvider;
    private readonly IAbpWeChatOfficialOptionsProvider _abpWeChatOfficialOptionsProvider;

    public SendWeChatOfficialTemplateMessageJob(
        IClock clock,
        ITemplateMessageDataModelJsonSerializer jsonSerializer,
        TemplateMessageService templateMessageService,
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        IWeChatOfficialAsyncLocal weChatOfficialAsyncLocal,
        IWeChatOfficialUserOpenIdProvider userOpenIdProvider,
        IAbpWeChatOfficialOptionsProvider abpWeChatOfficialOptionsProvider)
    {
        _clock = clock;
        _jsonSerializer = jsonSerializer;
        _templateMessageService = templateMessageService;
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _weChatOfficialAsyncLocal = weChatOfficialAsyncLocal;
        _userOpenIdProvider = userOpenIdProvider;
        _abpWeChatOfficialOptionsProvider = abpWeChatOfficialOptionsProvider;
    }

    [UnitOfWork]
    public override async Task ExecuteAsync(SendWeChatOfficialTemplateMessageJobArgs args)
    {
        var notification = await _notificationRepository.GetAsync(args.NotificationId);

        var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

        var dataModel = notificationInfo.GetWeChatOfficialTemplateMessageData(_jsonSerializer);

        if (dataModel.AppId.IsNullOrWhiteSpace())
        {
            await SendTemplateMessageAsync(dataModel, notification);
        }
        else
        {
            var options = await _abpWeChatOfficialOptionsProvider.GetOrNullAsync(dataModel.AppId!);

            if (options is null)
            {
                await SaveNotificationResultAsync(notification, false,
                    NotificationProviderWeChatOfficialConsts.AbpWeChatOfficialOptionsNotFoundFailureReason);

                return;
            }

            using var change = _weChatOfficialAsyncLocal.Change(options);

            await SendTemplateMessageAsync(dataModel, notification);
        }
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

        var response = await _templateMessageService.SendMessageAsync(
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