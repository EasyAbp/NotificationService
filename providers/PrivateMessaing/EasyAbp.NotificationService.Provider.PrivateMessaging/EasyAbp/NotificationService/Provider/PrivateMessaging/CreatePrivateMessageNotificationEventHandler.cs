using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.PrivateMessaging.PrivateMessages;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class
        CreatePrivateMessageNotificationEventHandler : IDistributedEventHandler<CreatePrivateMessageNotificationEto>,
            ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly IDistributedEventBus _distributedEventBus;

        public CreatePrivateMessageNotificationEventHandler(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IUnitOfWorkManager unitOfWorkManager,
            IServiceScopeFactory serviceScopeFactory,
            INotificationRepository notificationRepository,
            IDistributedEventBus distributedEventBus,
            INotificationInfoRepository notificationInfoRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _unitOfWorkManager = unitOfWorkManager;
            _serviceScopeFactory = serviceScopeFactory;
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

        protected virtual Task SendNotificationsAsync(List<Notification> notifications, string title, string content)
        {
            // todo: should use Stepping.NET or distributed event bus to ensure done?
            _unitOfWorkManager.Current.OnCompleted(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var backgroundJobManager = scope.ServiceProvider.GetRequiredService<IBackgroundJobManager>();

                foreach (var notification in notifications)
                {
                    var eto = new SendPrivateMessageEto(notification.TenantId, notification.CreatorId,
                        notification.UserId, title, content);

                    eto.SetProperty(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName,
                        notification.Id);

                    await _distributedEventBus.PublishAsync(eto);
                }
            });

            return Task.CompletedTask;
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