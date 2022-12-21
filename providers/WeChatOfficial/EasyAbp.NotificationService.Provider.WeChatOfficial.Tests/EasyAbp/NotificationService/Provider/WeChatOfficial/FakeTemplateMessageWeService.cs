using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(FakeTemplateMessageWeService), typeof(TemplateMessageWeService))]
public class FakeTemplateMessageWeService : TemplateMessageWeService
{
    public FakeTemplateMessageWeService(AbpWeChatOfficialOptions options, IAbpLazyServiceProvider lazyServiceProvider) :
        base(options, lazyServiceProvider)
    {
    }

    public override Task<SendMessageResponse> SendMessageAsync(string openId, string templateId, string targetUrl,
        TemplateMessage templateMessage, MiniProgramRequest miniProgramRequest = null)
    {
        return Task.FromResult(new SendMessageResponse());
    }
}