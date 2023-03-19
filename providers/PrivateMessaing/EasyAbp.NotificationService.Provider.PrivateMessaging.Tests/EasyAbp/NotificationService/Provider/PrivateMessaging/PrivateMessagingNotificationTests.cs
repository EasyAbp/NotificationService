using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class
        PrivateMessagingNotificationTests : NotificationServiceTestBase<
            NotificationServiceProviderPrivateMessagingTestsModule>
    {
        private const string Content = "MyContent";
        private const string Title = "MyTitle";

        protected ICurrentTenant CurrentTenant { get; set; }
        protected INotificationRepository NotificationRepository { get; set; }
        protected INotificationInfoRepository NotificationInfoRepository { get; set; }

        public PrivateMessagingNotificationTests()
        {
            CurrentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
            NotificationRepository = ServiceProvider.GetRequiredService<INotificationRepository>();
            NotificationInfoRepository = ServiceProvider.GetRequiredService<INotificationInfoRepository>();
        }

        [Fact]
        public async Task Should_Create_Notifications()
        {
            var userIds = new List<Guid>
            {
                NotificationServiceProviderPrivateMessagingTestConsts.FakeUser1Id,
                NotificationServiceProviderPrivateMessagingTestConsts.FakeUser2Id
            };

            await CreatePrivateMessagingNotificationAsync(userIds, Title, Content, true);

            var notifications = await NotificationRepository.GetListAsync();

            notifications.Count.ShouldBe(2);

            foreach (var notification in userIds.Select(userId => notifications.Find(x => x.UserId == userId)))
            {
                notification.ShouldNotBeNull();
            }

            var notificationInfo = await NotificationInfoRepository.GetAsync(notifications.First().NotificationInfoId);

            notificationInfo.GetPrivateMessagingTitle().ShouldBe(Title);
            notificationInfo.GetPrivateMessagingContent().ShouldBe(Content);
            notificationInfo.GetPrivateMessagingSendFromCreator().ShouldBe(true);
        }

        private async Task CreatePrivateMessagingNotificationAsync(List<Guid> userIds, string title, string content,
            bool sendFromCreator)
        {
            var handler = ServiceProvider.GetRequiredService<CreatePrivateMessageNotificationEventHandler>();

            var eto = new CreatePrivateMessageNotificationEto(CurrentTenant.Id, userIds, title, content,
                sendFromCreator);

            await handler.HandleEventAsync(eto);
        }
    }
}