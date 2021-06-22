using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.PrivateMessaging.PrivateMessages;
using JetBrains.Annotations;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging
{
    public class PrivateMessageNotificationSendingJob : IAsyncBackgroundJob<PrivateMessageNotificationSendingJobArgs>, ITransientDependency
    {
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly IUserUserNameProvider _userUserNameProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IPrivateMessageAppService _privateMessageAppService;
        private readonly IClock _clock;

        public PrivateMessageNotificationSendingJob(
            INotificationInfoRepository notificationInfoRepository,
            IUserUserNameProvider userUserNameProvider,
            INotificationRepository notificationRepository,
            IPrivateMessageAppService privateMessageAppService,
        IClock clock)
        {
            _notificationInfoRepository = notificationInfoRepository;
            _userUserNameProvider = userUserNameProvider;
            _notificationRepository = notificationRepository;
            _privateMessageAppService = privateMessageAppService;
            _clock = clock;
        }

        [UnitOfWork]
        public virtual async Task ExecuteAsync(PrivateMessageNotificationSendingJobArgs args)
        {
            var notification = await _notificationRepository.GetAsync(args.NotificationId);

            var userName = await _userUserNameProvider.GetAsync(notification.UserId);

            if (userName.IsNullOrWhiteSpace())
            {
                await SaveNotificationResultAsync(notification, false, NotificationConsts.FailureReasons.ReceiverInfoNotFound);
                return;
            }

            var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

            await _privateMessageAppService.CreateAsync(new EasyAbp.PrivateMessaging.PrivateMessages.Dtos.CreateUpdatePrivateMessageDto
            {
                Title = notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoTitlePropertyName).ToString(),
                Content = notificationInfo.GetDataValue(NotificationProviderPrivateMessagingConsts.NotificationInfoContentPropertyName).ToString(),
                ToUserName = userName
            });

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