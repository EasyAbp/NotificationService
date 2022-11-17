using System.Threading.Tasks;
using EasyAbp.NotificationService.Provider.PrivateMessaging;
using EasyAbp.NotificationService.Provider.PrivateMessaging.UserWelcomeNotifications;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class NotificationFactoryTests : NotificationServiceTestBase<NotificationServiceProviderPrivateMessagingTestsModule>
    {
        
        public NotificationFactoryTests()
        {
        }
        
        [Fact]
        public async Task Should_Create_User_Welcome_Notification()
        {
            var userWelcomeNotificationFactory = ServiceProvider.GetRequiredService<UserWelcomeNotificationFactory>();
            

            const string giftCardCode = "123456";    // a random gift card code

            var eto = await userWelcomeNotificationFactory.CreateAsync(
                model: new UserWelcomeNotificationDataModel(NotificationServiceProviderPrivateMessagingTestConsts
                    .FakeUser1Id, giftCardCode),
                userId: NotificationServiceProviderPrivateMessagingTestConsts
                    .FakeUser1Id
            );

            eto.Content.ShouldBe($"Hello, here is a gift card code for you: {giftCardCode}");
        }
    }
}
