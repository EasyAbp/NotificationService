using EasyAbp.NotificationService.Provider.Sms.UserWelcomeNotifications;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.NotificationService.Provider.Sms
{
    public class NotificationFactoryTests : NotificationServiceTestBase<NotificationServiceProviderSmsTestsModule>
    {
        protected IExternalUserLookupServiceProvider ExternalUserLookupServiceProvider { get; set; }

        public NotificationFactoryTests()
        {
            ExternalUserLookupServiceProvider = ServiceProvider.GetRequiredService<IExternalUserLookupServiceProvider>();
        }

        [Fact]
        public async Task Should_Create_User_Welcome_Notification()
        {
            var userWelcomeNotificationFactory = ServiceProvider.GetRequiredService<UserWelcomeNotificationFactory>();

            var userData =
                await ExternalUserLookupServiceProvider.FindByIdAsync(NotificationServiceProviderSmsTestConsts
                    .FakeUser1Id);

            const string giftCardCode = "123456";    // a random gift card code

            var eto = await userWelcomeNotificationFactory.CreateAsync(
                model: new UserWelcomeNotificationDataModel(userData.UserName, giftCardCode),
                userId: userData.Id
            );

            eto.Text.ShouldBe($"Hello, {userData.UserName}, here is a gift card code for you: {giftCardCode}");
            eto.GetProperties(JsonSerializer).Count.ShouldBe(0);
        }
    }
}