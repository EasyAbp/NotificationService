using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class CreateEmailNotificationEventHandler : IDistributedEventHandler<CreateEmailNotificationEto>,
        ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationInfoRepository _notificationInfoRepository;

        public CreateEmailNotificationEventHandler(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IUnitOfWorkManager unitOfWorkManager,
            IServiceScopeFactory serviceScopeFactory,
            INotificationRepository notificationRepository,
            INotificationInfoRepository notificationInfoRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _unitOfWorkManager = unitOfWorkManager;
            _serviceScopeFactory = serviceScopeFactory;
            _notificationRepository = notificationRepository;
            _notificationInfoRepository = notificationInfoRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(CreateEmailNotificationEto eventData)
        {
            var notificationInfo = new NotificationInfo(_guidGenerator.Create(), _currentTenant.Id);

            notificationInfo.SetMailingData(eventData.Subject, eventData.Body);

            await _notificationInfoRepository.InsertAsync(notificationInfo, true);

            var notifications = await CreateNotificationsAsync(notificationInfo, eventData.UserIds);

            await SendNotificationsAsync(notifications);
        }

        protected virtual Task SendNotificationsAsync(List<Notification> notifications)
        {
            // todo: should use Stepping.NET or distributed event bus to ensure done?
            _unitOfWorkManager.Current.OnCompleted(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();

                var backgroundJobManager = scope.ServiceProvider.GetRequiredService<IBackgroundJobManager>();

                foreach (var notification in notifications)
                {
                    await backgroundJobManager.EnqueueAsync(
                        new EmailNotificationSendingJobArgs(notification.TenantId, notification.Id));
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
                    NotificationProviderMailingConsts.NotificationMethod
                );

                await _notificationRepository.InsertAsync(notification, true);

                notifications.Add(notification);
            }

            return notifications;
        }
    }
}