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
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.WeChatOfficial
{
    public class CreateWeChatOfficialTemplateMessageNotificationEventHandler :
        IDistributedEventHandler<CreateWeChatOfficialTemplateMessageNotificationEto>, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITemplateMessageDataModelJsonSerializer _jsonSerializer;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationInfoRepository _notificationInfoRepository;

        public CreateWeChatOfficialTemplateMessageNotificationEventHandler(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IServiceScopeFactory serviceScopeFactory,
            ITemplateMessageDataModelJsonSerializer jsonSerializer,
            IUnitOfWorkManager unitOfWorkManager,
            INotificationRepository notificationRepository,
            INotificationInfoRepository notificationInfoRepository)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _serviceScopeFactory = serviceScopeFactory;
            _jsonSerializer = jsonSerializer;
            _unitOfWorkManager = unitOfWorkManager;
            _notificationRepository = notificationRepository;
            _notificationInfoRepository = notificationInfoRepository;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(CreateWeChatOfficialTemplateMessageNotificationEto eventData)
        {
            var notificationInfo = new NotificationInfo(_guidGenerator.Create(), _currentTenant.Id);

            notificationInfo.SetWeChatOfficialTemplateMessageData(eventData.DataModel, _jsonSerializer);

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
                        new SendWeChatOfficialTemplateMessageJobArgs(notification.TenantId, notification.Id));
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
                    NotificationProviderWeChatOfficialConsts.TemplateMessageNotificationMethod
                );

                notifications.Add(notification);
            }

            foreach (var notification in notifications)
            {
                await _notificationRepository.InsertAsync(notification, true);
            }

            return notifications;
        }
    }
}