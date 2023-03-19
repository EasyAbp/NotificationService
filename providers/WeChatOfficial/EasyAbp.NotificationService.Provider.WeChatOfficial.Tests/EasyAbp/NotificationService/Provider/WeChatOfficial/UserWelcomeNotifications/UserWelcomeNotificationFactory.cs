using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial.UserWelcomeNotifications
{
    public class UserWelcomeNotificationFactory :
        NotificationFactory<UserWelcomeNotificationDataModel, CreateWeChatOfficialTemplateMessageNotificationEto>,
        ITransientDependency
    {
        protected ITemplateMessageDataModelJsonSerializer TemplateMessageDataModelJsonSerializer =>
            LazyGetRequiredService(ref _templateMessageDataModelJsonSerializer);

        private ITemplateMessageDataModelJsonSerializer _templateMessageDataModelJsonSerializer;

        public override Task<CreateWeChatOfficialTemplateMessageNotificationEto> CreateAsync(
            UserWelcomeNotificationDataModel model, IEnumerable<Guid> userIds)
        {
            var templateData = new TemplateMessage($"Hello, {model.UserName}", "Thank you");

            templateData.AddKeywords("gift-card-code", model.GiftCardCode);

            var dataModel = new WeChatOfficialTemplateMessageDataModel("my-template-id", new MiniProgramRequest
            {
                AppId = "my-mini-program-appid",
                PagePath = "my-mini-program-page-path"
            }, "https://github.com", templateData, "my-official-appid");

            return Task.FromResult(new CreateWeChatOfficialTemplateMessageNotificationEto(CurrentTenant.Id, userIds,
                dataModel, TemplateMessageDataModelJsonSerializer));
        }
    }
}