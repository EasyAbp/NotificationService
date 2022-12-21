using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public class WeChatTemplateMessageNotificationSender : IWeChatTemplateMessageNotificationSender, ITransientDependency
{
    protected IAbpWeChatServiceFactory AbpWeChatServiceFactory { get; }
    protected Logger<WeChatTemplateMessageNotificationSender> Logger { get; }

    public WeChatTemplateMessageNotificationSender(
        IAbpWeChatServiceFactory abpWeChatServiceFactory,
        Logger<WeChatTemplateMessageNotificationSender> logger)
    {
        AbpWeChatServiceFactory = abpWeChatServiceFactory;
        Logger = logger;
    }

    public virtual async Task<SendMessageResponse> SendAsync(
        string openId, WeChatOfficialTemplateMessageDataModel dataModel)
    {
        try
        {
            var templateMessageWeService =
                await AbpWeChatServiceFactory.CreateAsync<TemplateMessageWeService>(dataModel.AppId);

            return await templateMessageWeService.SendMessageAsync(
                openId, dataModel.TemplateId, dataModel.Url, dataModel.Data, dataModel.MiniProgram);
        }
        catch (Exception e)
        {
            Logger.LogException(e);

            return null;
        }
    }
}