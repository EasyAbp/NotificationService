using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.Users;
using Xunit;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class NotificationPublisherTests : NotificationServiceTestBase<NotificationServiceProviderMailingTestModule>
    {
        protected IExternalUserLookupServiceProvider ExternalUserLookupServiceProvider { get; set; }
        
        public NotificationPublisherTests()
        {
            ExternalUserLookupServiceProvider = ServiceProvider.GetRequiredService<IExternalUserLookupServiceProvider>();
        }
        
        [Fact]
        public async Task Should_Create_User_Welcome_Notification()
        {
            var userData =
                await ExternalUserLookupServiceProvider.FindByIdAsync(NotificationServiceProviderMailingTestConsts
                    .FakeUser1Id);

            const string giftCardCode = "123456";    // a random gift card code

            var notificationDefinition = new UserWelcomeNotification(userData.UserName, giftCardCode);

            var eto = await notificationDefinition.CreateAsync(new List<Guid> {userData.Id});
            
            eto.Subject.ShouldBe($"Welcome, {userData.UserName}");
            eto.Body.ShouldBe($"Here a gift card code for you: {giftCardCode}");
        }
    }
}
