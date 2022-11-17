using System.Threading.Tasks;
using EasyAbp.NotificationService.Provider.Mailing.UserWelcomeNotifications;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class NotificationFactoryTests : NotificationServiceTestBase<NotificationServiceProviderMailingTestsModule>
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
                await ExternalUserLookupServiceProvider.FindByIdAsync(NotificationServiceProviderMailingTestConsts
                    .FakeUser1Id);

            const string giftCardCode = "123456";    // a random gift card code

            var eto = await userWelcomeNotificationFactory.CreateAsync(
                model: new UserWelcomeNotificationDataModel(userData.UserName, giftCardCode),
                userId: userData.Id
            );

            eto.TenantId.ShouldBeNull();
            eto.Subject.ShouldBe($"Welcome, {userData.UserName}");
            eto.Body.ShouldBe($"Here is a gift card code for you: {giftCardCode}");
        }
    }
}
