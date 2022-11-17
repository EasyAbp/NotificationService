using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class MailingNotificationTests : NotificationServiceTestBase<NotificationServiceProviderMailingTestsModule>
    {
        private const string Subject = "test";
        private const string Body = "test123";
        
        protected ICurrentTenant CurrentTenant { get; set; }
        protected INotificationRepository NotificationRepository { get; set; }
        protected INotificationInfoRepository NotificationInfoRepository { get; set; }
        protected IAsyncBackgroundJob<EmailNotificationSendingJobArgs> EmailNotificationSendingJob { get; set; }
        
        public MailingNotificationTests()
        {
            CurrentTenant = ServiceProvider.GetRequiredService<ICurrentTenant>();
            NotificationRepository = ServiceProvider.GetRequiredService<INotificationRepository>();
            NotificationInfoRepository = ServiceProvider.GetRequiredService<INotificationInfoRepository>();
            EmailNotificationSendingJob = ServiceProvider.GetRequiredService<IAsyncBackgroundJob<EmailNotificationSendingJobArgs>>();
        }
        
        [Fact]
        public async Task Should_Create_Notifications()
        {
            var userIds = new List<Guid>
            {
                NotificationServiceProviderMailingTestConsts.FakeUser1Id,
                NotificationServiceProviderMailingTestConsts.FakeUser2Id
            };

            await CreateEmailNotificationAsync(userIds, Subject, Body);
            
            var notifications = await NotificationRepository.GetListAsync();
            
            notifications.Count.ShouldBe(2);

            foreach (var notification in userIds.Select(userId => notifications.Find(x => x.UserId == userId)))
            {
                notification.ShouldNotBeNull();
            }
            
            var notificationInfo = await NotificationInfoRepository.GetAsync(notifications.First().NotificationInfoId);

            var subject = notificationInfo
                .GetDataValue(NotificationProviderMailingConsts.NotificationInfoSubjectPropertyName).ToString();

            var body = notificationInfo.GetDataValue(NotificationProviderMailingConsts.NotificationInfoBodyPropertyName)
                .ToString();
            
            subject.ShouldBe(Subject);
            body.ShouldBe(Body);
        }

        private async Task CreateEmailNotificationAsync(List<Guid> userIds, string subject, string body)
        {
            var handler = ServiceProvider.GetRequiredService<CreateEmailNotificationEventHandler>();
            
            var eto = new CreateEmailNotificationEto(CurrentTenant.Id, userIds, subject, body);

            await handler.HandleEventAsync(eto);
        }

        [Fact]
        public async Task Should_Set_Notification_Result_To_Success()
        {
            var userIds = new List<Guid>
            {
                NotificationServiceProviderMailingTestConsts.FakeUser1Id
            };

            await CreateEmailNotificationAsync(userIds, Subject, Body);
            
            var notification = (await NotificationRepository.GetListAsync()).First();

            await EmailNotificationSendingJob.ExecuteAsync(new EmailNotificationSendingJobArgs(notification.Id));
            
            notification = await NotificationRepository.GetAsync(notification.Id);

            notification.Success.ShouldBe(true);
            notification.CompletionTime.ShouldNotBeNull();
            notification.FailureReason.ShouldBeNull();
        }
        
        [Fact]
        public async Task Should_Set_Notification_Result_To_Failure_If_User_Not_Found()
        {
            var userIds = new List<Guid>
            {
                Guid.NewGuid()
            };

            await CreateEmailNotificationAsync(userIds, Subject, Body);
            
            var notification = (await NotificationRepository.GetListAsync()).First();

            await EmailNotificationSendingJob.ExecuteAsync(new EmailNotificationSendingJobArgs(notification.Id));

            notification = await NotificationRepository.GetAsync(notification.Id);
            
            notification.Success.ShouldBe(false);
            notification.CompletionTime.ShouldNotBeNull();
            notification.FailureReason.ShouldBe(NotificationConsts.FailureReasons.ReceiverInfoNotFound);
        }
    }
}
