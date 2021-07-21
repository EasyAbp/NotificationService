using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.PrivateMessaging.PrivateMessages;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class CreatePrivateMessageNotificationEventHandler : IDistributedEventHandler<CreatePrivateMessageNotificationEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IBackgroundJobManager _backgroundJobManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        public CreatePrivateMessageNotificationEventHandler(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IBackgroundJobManager backgroundJobManager,
            INotificationRepository notificationRepository,
            IDistributedEventBus distributedEventBus,
            INotificationInfoRepository notificationInfoRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _backgroundJobManager = backgroundJobManager;
            _notificationRepository = notificationRepository;
            _notificationInfoRepository = notificationInfoRepository;
            _distributedEventBus = distributedEventBus;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(CreatePrivateMessageNotificationEto eventData)
        {
            var notificationInfo = new NotificationInfo(_guidGenerator.Create(), _currentTenant.Id);
            
            notificationInfo.SetPrivateMessagingData(eventData.Title, eventData.Content);

            await _notificationInfoRepository.InsertAsync(notificationInfo, true);

            var notifications = await CreateNotificationsAsync(notificationInfo, eventData.UserIds);

            await SendNotificationsAsync(notifications, eventData.Title, eventData.Content);
        }

        protected virtual async Task SendNotificationsAsync(List<Notification> notifications, string title, string content)
        {
            foreach (var notification in notifications)
            {
                var eto = new SendPrivateMessageEto(notification.TenantId, notification.CreatorId, notification.UserId, title, content);

                eto.SetProperty(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName, notification.Id);

                await _distributedEventBus.PublishAsync(eto);
            }
        }

        protected virtual async Task<List<Notification>> CreateNotificationsAsync(NotificationInfo notificationInfo,
            IEnumerable<Guid> userIds)
        {
            var notifications = new List<Notification>();
            
            foreach (var userId in userIds)
            {
                var notification = new Notification(
                    _guidGenerator.Create(),
                    _currentTenant.Id,
                    userId,
                    notificationInfo.Id,
                    NotificationProviderPrivateMessagingConsts.NotificationMethod
                );

                await _notificationRepository.InsertAsync(notification, true);
                
                notifications.Add(notification);
            }

            return notifications;
        }

    }
}