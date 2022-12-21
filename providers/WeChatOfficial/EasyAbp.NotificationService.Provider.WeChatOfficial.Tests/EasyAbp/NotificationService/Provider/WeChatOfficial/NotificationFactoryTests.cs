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
                    NotificationServiceProviderWeChatOfficialTestConsts.FakeUser1Id,
                    NotificationServiceProviderWeChatOfficialTestConsts.FakeUser2Id
                }
            );

            eto.UserIds.ShouldContain(NotificationServiceProviderWeChatOfficialTestConsts.FakeUser1Id);
            eto.UserIds.ShouldContain(NotificationServiceProviderWeChatOfficialTestConsts.FakeUser2Id);
            eto.DataModel.Url.ShouldBe("https://github.com");
            eto.DataModel.AppId.ShouldBe("my-official-appid");
            eto.DataModel.MiniProgram.ShouldNotBeNull();
            eto.DataModel.MiniProgram.AppId.ShouldBe("my-mini-program-appid");
            eto.DataModel.MiniProgram.PagePath.ShouldBe("my-mini-program-page-path");
            eto.DataModel.TemplateId.ShouldBe("my-template-id");
            eto.DataModel.Data.ShouldNotBeNull();
            eto.DataModel.Data.ShouldContain(x => x.Key == "first" && x.Value.Value == "Hello, my-username");
            eto.DataModel.Data.ShouldContain(x => x.Key == "remark" && x.Value.Value == "Thank you");
            eto.DataModel.Data.ShouldContain(x => x.Key == "gift-card-code" && x.Value.Value == giftCardCode);
        }
    }
}