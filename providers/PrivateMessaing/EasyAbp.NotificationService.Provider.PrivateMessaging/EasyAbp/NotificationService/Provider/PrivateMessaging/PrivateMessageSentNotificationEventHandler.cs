using EasyAbp.NotificationService.Notifications;
using EasyAbp.PrivateMessaging.PrivateMessages;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class PrivateMessageSentNotificationEventHandler : IDistributedEventHandler<PrivateMessageSentEto>, ITransientDependency
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IClock _clock;

        public PrivateMessageSentNotificationEventHandler(
            INotificationRepository notificationRepository,
            IClock clock)
        {
            _notificationRepository = notificationRepository;
            _clock = clock;
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(PrivateMessageSentEto eventData)
        {
            if (!eventData.HasProperty(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName))
            {
                return;
            }

            var notificationId = eventData.GetProperty<Guid>(NotificationProviderPrivateMessagingConsts.NotificationIdPropertyName);

            var notification = await _notificationRepository.FindAsync(x => x.Id == notificationId);

            if (notification == null)
            {
                return;
            }

            await SaveNotificationResultAsync(notification, true);
        }

        protected virtual async Task SaveNotificationResultAsync(Notification notification, bool success,
            [CanBeNull] string failureReason = null)
        {
            notification.SetResult(_clock, success, failureReason);

            await _notificationRepository.UpdateAsync(notification, true);
        }
    }
}