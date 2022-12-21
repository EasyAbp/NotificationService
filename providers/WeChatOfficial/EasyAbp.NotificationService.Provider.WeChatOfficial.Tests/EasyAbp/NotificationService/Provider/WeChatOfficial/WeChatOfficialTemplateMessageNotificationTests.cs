using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage.Request;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    public class WeChatOfficialTemplateMessageNotificationTests :
        NotificationServiceTestBase<NotificationServiceProviderWeChatOfficialTestsModule>
    {
        protected ICurrentTenant CurrentTenant { get; set; }
        protected ITemplateMessageDataModelJsonSerializer JsonSerializer { get; set; }
        protected INotificationRepository NotificationRepository { get; set; }
        protected INotificationInfoRepository NotificationInfoRepository { get; set; }
        protected SendWeChatOfficialTemplateMessageJob NotificationSendingJob { get; set; }

        public WeChatOfficialTemplateMessageNotificationTests()
        {
            CurrentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
            JsonSerializer = ServiceProvider.GetRequiredService<ITemplateMessageDataModelJsonSerializer>();
            NotificationRepository = ServiceProvider.GetRequiredService<INotificationRepository>();
            NotificationInfoRepository = ServiceProvider.GetRequiredService<INotificationInfoRepository>();
            NotificationSendingJob = ServiceProvider.GetRequiredService<SendWeChatOfficialTemplateMessageJob>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            base.AfterAddApplication(services);

            var abpWeChatServiceFactory = Substitute.For<IAbpWeChatServiceFactory>();
            services.Replace(ServiceDescriptor.Transient(s => abpWeChatServiceFactory));

            abpWeChatServiceFactory.CreateAsync<TemplateMessageWeService>(Arg.Any<string>())
                .Returns(new FakeTemplateMessageWeService(null, null));
        }

        [Fact]
        public async Task Should_Create_Notifications()
        {
            var userIds = new List<Guid>
            {
                NotificationServiceProviderWeChatOfficialTestConsts.FakeUser1Id,
                NotificationServiceProviderWeChatOfficialTestConsts.FakeUser2Id
            };

            await CreateWeChatOfficialTemplateMessageNotificationAsync(userIds);

            var notifications = await NotificationRepository.GetListAsync();

            notifications.Count.ShouldBe(2);

            foreach (var notification in userIds.Select(userId => notifications.Find(x => x.UserId == userId)))
            {
                notification.ShouldNotBeNull();
            }

            var notificationInfo = await NotificationInfoRepository.GetAsync(notifications.First().NotificationInfoId);

            var dataModel = notificationInfo.GetWeChatOfficialTemplateMessageData(JsonSerializer);

            dataModel.Url.ShouldBe("https://github.com");
            dataModel.AppId.ShouldBeNull();
            dataModel.MiniProgram.ShouldNotBeNull();
            dataModel.MiniProgram.AppId.ShouldBe("my-mini-program-appid");
            dataModel.MiniProgram.PagePath.ShouldBe("my-mini-program-page-path");
            dataModel.TemplateId.ShouldBe("my-template-id");
            dataModel.Data.ShouldNotBeNull();
            dataModel.Data.ShouldContain(x => x.Key == "first" && x.Value.Value == "Hello, world");
            dataModel.Data.ShouldContain(x => x.Key == "remark" && x.Value.Value == "Thank you");
            dataModel.Data.ShouldContain(x => x.Key == "gift-card-code" && x.Value.Value == "1234");
        }

        private async Task CreateWeChatOfficialTemplateMessageNotificationAsync(IEnumerable<Guid> userIds,
            [CanBeNull] string appId = null)
        {
            var templateData = new TemplateMessage($"Hello, world", "Thank you");

            templateData.AddKeywords("gift-card-code", "1234");

            var dataModel = new WeChatOfficialTemplateMessageDataModel("my-template-id", new MiniProgramRequest
            {
                AppId = "my-mini-program-appid",
                PagePath = "my-mini-program-page-path"
            }, "https://github.com", templateData, appId);

            var handler = ServiceProvider
                .GetRequiredService<CreateWeChatOfficialTemplateMessageNotificationEventHandler>();

            var eto = new CreateWeChatOfficialTemplateMessageNotificationEto(CurrentTenant.Id, userIds, dataModel);

            await handler.HandleEventAsync(eto);
        }

        [Fact]
        public async Task Should_Set_Notification_Result_To_Success()
        {
            var userIds = new List<Guid>
            {
                NotificationServiceProviderWeChatOfficialTestConsts.FakeUser1Id
            };

            await CreateWeChatOfficialTemplateMessageNotificationAsync(userIds);

            var notification = (await NotificationRepository.GetListAsync()).First();

            await NotificationSendingJob.ExecuteAsync(
                new SendWeChatOfficialTemplateMessageJobArgs(notification.TenantId, notification.Id));

            notification = await NotificationRepository.GetAsync(notification.Id);

            notification.Success.ShouldBe(true);
            notification.CompletionTime.ShouldNotBeNull();
            notification.FailureReason.ShouldBeNull();
        }

        [Fact]
        public async Task Should_Set_Notification_Result_To_Failure_If_User_OpenId_Not_Found()
        {
            var userIds = new List<Guid>
            {
                Guid.NewGuid()
            };

            await CreateWeChatOfficialTemplateMessageNotificationAsync(userIds);

            var notification = (await NotificationRepository.GetListAsync()).First();

            await NotificationSendingJob.ExecuteAsync(
                new SendWeChatOfficialTemplateMessageJobArgs(notification.TenantId, notification.Id));

            notification = await NotificationRepository.GetAsync(notification.Id);

            notification.Success.ShouldBe(false);
            notification.CompletionTime.ShouldNotBeNull();
            notification.FailureReason.ShouldBe(
                NotificationProviderWeChatOfficialConsts.UserOpenIdNotFoundFailureReason);
        }
    }
}