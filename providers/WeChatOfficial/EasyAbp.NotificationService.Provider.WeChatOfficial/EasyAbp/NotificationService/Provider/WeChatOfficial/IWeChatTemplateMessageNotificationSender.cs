using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Response;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial;

public interface IWeChatTemplateMessageNotificationSender
{
    Task<SendMessageResponse> SendAsync(string openId, WeChatOfficialTemplateMessageDataModel dataModel);
}