using System;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class SendWeChatOfficialTemplateMessageJob : AsyncBackgroundJob<SendWeChatOfficialTemplateMessageJobArgs>,
    ITransientDependency
{
    private readonly IClock _clock;
    private readonly ICurrentTenant _currentTenant;
    private readonly ITemplateMessageDataModelJsonSerializer _jsonSerializer;
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationInfoRepository _notificationInfoRepository;
    private readonly IWeChatOfficialUserOpenIdProvider _userOpenIdProvider;
    private readonly IWeChatTemplateMessageNotificationSender _notificationSender;

    public SendWeChatOfficialTemplateMessageJob(
        IClock clock,
        ICurrentTenant currentTenant,
        ITemplateMessageDataModelJsonSerializer jsonSerializer,
        INotificationRepository notificationRepository,
        INotificationInfoRepository notificationInfoRepository,
        IWeChatOfficialUserOpenIdProvider userOpenIdProvider,
        IWeChatTemplateMessageNotificationSender notificationSender)
    {
        _clock = clock;
        _currentTenant = currentTenant;
        _jsonSerializer = jsonSerializer;
        _notificationRepository = notificationRepository;
        _notificationInfoRepository = notificationInfoRepository;
        _userOpenIdProvider = userOpenIdProvider;
        _notificationSender = notificationSender;
    }

    [UnitOfWork]
    public override async Task ExecuteAsync(SendWeChatOfficialTemplateMessageJobArgs args)
    {
        using var changeTenant = _currentTenant.Change(args.TenantId);

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

        var response = await _notificationSender.SendAsync(openId, dataModel);

        if (response?.ErrorCode == 0)
        {
            await SaveNotificationResultAsync(notification, true);
        }
        else
        {
            await SaveNotificationResultAsync(notification, false, response?.ErrorMessage ?? "See exception logs");
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