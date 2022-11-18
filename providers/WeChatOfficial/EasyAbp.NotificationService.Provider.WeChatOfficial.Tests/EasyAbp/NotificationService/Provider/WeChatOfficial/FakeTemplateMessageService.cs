using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(FakeTemplateMessageService), typeof(TemplateMessageService))]
public class FakeTemplateMessageService : TemplateMessageService
{
    public override async Task<SendMessageResponse> SendMessageAsync(string openId, string templateId, string targetUrl,
        TemplateMessage templateMessage, MiniProgramRequest miniProgramRequest = null)
    {
        return new SendMessageResponse();
    }
}