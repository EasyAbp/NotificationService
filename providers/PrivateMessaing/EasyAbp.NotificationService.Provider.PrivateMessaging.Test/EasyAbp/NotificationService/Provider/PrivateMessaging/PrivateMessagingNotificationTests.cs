using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Provider.PrivateMessaging;
using EasyAbp.PrivateMessaging.PrivateMessages;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class PrivateMessagingNotificationTests : NotificationServiceTestBase<NotificationServiceProviderPrivateMessagingTestModule>
    {
        private const string Content = "MyContent";
        private const string Title = "MyTitle";
        
        protected ICurrentTenant CurrentTenant { get; set; }
        protected IJsonSerializer JsonSerializer { get; set; }
        protected INotificationRepository NotificationRepository { get; set; }
        protected INotificationInfoRepository NotificationInfoRepository { get; set; }
        
        public PrivateMessagingNotificationTests()
        {
            CurrentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
            JsonSerializer = ServiceProvider.GetRequiredService<IJsonSerializer>();
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

            await CreatePrivateMessagingNotificationAsync(userIds, Title, Content);
            
            var notifications = await NotificationRepository.GetListAsync();
            
            notifications.Count.ShouldBe(2);

            foreach (var notification in userIds.Select(userId => notifications.Find(x => x.UserId == userId)))
            {
                notification.ShouldNotBeNull();
            }
            
            var notificationInfo = await NotificationInfoRepository.GetAsync(notifications.First().NotificationInfoId);

            var title = notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoTitlePropertyName)
                .ToString();

            var content = notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoContentPropertyName)
                .ToString();

            title.ShouldBe(Title);
            content.ShouldBe(Content);
        }

        private async Task CreatePrivateMessagingNotificationAsync(List<Guid> userIds, string title, string content)
        {
            var handler = ServiceProvider.GetRequiredService<CreatePrivateMessageNotificationEventHandler>();
            
            var eto = new CreatePrivateMessageNotificationEto(CurrentTenant.Id, userIds, title, content);

            await handler.HandleEventAsync(eto);
        }

        [Fact]
        public async Task Should_Set_Notification_Result_To_Success()
        {
            var userIds = new List<Guid>
            {
                NotificationServiceProviderPrivateMessagingTestConsts.FakeUser1Id
            };

            await CreatePrivateMessagingNotificationAsync(userIds, Title, Content);
            
            var notification = (await NotificationRepository.GetListAsync()).First();

            var handler = ServiceProvider.GetRequiredService<PrivateMessageSentNotificationEventHandler>();

            var eto = new PrivateMessageSentEto(null, Guid.NewGuid(), null, null, NotificationServiceProviderPrivateMessagingTestConsts.FakeUser1Id, null, DateTime.Now, Title);

            eto.SetProperty(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName, notification.Id);

            await handler.HandleEventAsync(eto);
            
            notification = await NotificationRepository.GetAsync(notification.Id);

            notification.Success.ShouldBe(true);
            notification.CompletionTime.ShouldNotBeNull();
            notification.FailureReason.ShouldBeNull();
        }
        
    }
}
