using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.BackgroundJobs;
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

        public CreatePrivateMessageNotificationEventHandler(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IBackgroundJobManager backgroundJobManager,
            INotificationRepository notificationRepository,
            INotificationInfoRepository notificationInfoRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _backgroundJobManager = backgroundJobManager;
            _notificationRepository = notificationRepository;
            _notificationInfoRepository = notificationInfoRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(CreatePrivateMessageNotificationEto eventData)
        {
            var notificationInfo = new NotificationInfo(_guidGenerator.Create(), _currentTenant.Id);
            
            notificationInfo.SetPrivateMessagingData(eventData.Title, eventData.Content);

            await _notificationInfoRepository.InsertAsync(notificationInfo, true);

            var notifications = await CreateNotificationsAsync(notificationInfo, eventData.UserIds);

            await SendNotificationsAsync(notifications);
        }

        protected virtual async Task SendNotificationsAsync(List<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                await _backgroundJobManager.EnqueueAsync(new PrivateMessageNotificationSendingJobArgs(notification.Id));
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