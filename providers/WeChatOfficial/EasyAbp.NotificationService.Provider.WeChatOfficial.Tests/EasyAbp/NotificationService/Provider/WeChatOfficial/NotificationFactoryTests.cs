using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;
using EasyAbp.NotificationService.Provider.WeChatOfficial.UserWelcomeNotifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Xunit;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    public class NotificationFactoryTests :
        NotificationServiceTestBase<NotificationServiceProviderWeChatOfficialTestsModule>
    {
        protected ITemplateMessageDataModelJsonSerializer TemplateMessageDataModelJsonSerializer { get; }

        public NotificationFactoryTests()
        {
            TemplateMessageDataModelJsonSerializer = GetRequiredService<ITemplateMessageDataModelJsonSerializer>();
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
        public async Task Should_Create_User_Welcome_Notification()
        {
            var userWelcomeNotificationFactory = ServiceProvider.GetRequiredService<UserWelcomeNotificationFactory>();

            const string giftCardCode = "123456"; // a random gift card code

            var eto = await userWelcomeNotificationFactory.CreateAsync(
                model: new UserWelcomeNotificationDataModel("my-username", giftCardCode),
                userIds: new[]
                {
                    NotificationServiceTestConsts.FakeUser1Id,
                    NotificationServiceTestConsts.FakeUser2Id
                }
            );

            eto.UserIds.ShouldContain(NotificationServiceTestConsts.FakeUser1Id);
            eto.UserIds.ShouldContain(NotificationServiceTestConsts.FakeUser2Id);
            var dataModel = eto.GetDataModel(TemplateMessageDataModelJsonSerializer);
            dataModel.Url.ShouldBe("https://github.com");
            dataModel.AppId.ShouldBe("my-official-appid");
            dataModel.MiniProgram.ShouldNotBeNull();
            dataModel.MiniProgram.AppId.ShouldBe("my-mini-program-appid");
            dataModel.MiniProgram.PagePath.ShouldBe("my-mini-program-page-path");
            dataModel.TemplateId.ShouldBe("my-template-id");
            dataModel.Data.ShouldNotBeNull();
            dataModel.Data.ShouldContain(x => x.Key == "first" && x.Value.Value == "Hello, my-username");
            dataModel.Data.ShouldContain(x => x.Key == "remark" && x.Value.Value == "Thank you");
            dataModel.Data.ShouldContain(x => x.Key == "gift-card-code" && x.Value.Value == giftCardCode);
        }
    }
}