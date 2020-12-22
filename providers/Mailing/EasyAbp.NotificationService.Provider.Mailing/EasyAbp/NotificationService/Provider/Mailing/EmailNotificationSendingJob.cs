using System;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class EmailNotificationSendingJob : IAsyncBackgroundJob<EmailNotificationSendingJobArgs>, ITransientDependency
    {
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly IUserEmailAddressProvider _userEmailAddressProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmailSender _emailSender;
        private readonly IClock _clock;

        public EmailNotificationSendingJob(
            INotificationInfoRepository notificationInfoRepository,
            IUserEmailAddressProvider userEmailAddressProvider,
            INotificationRepository notificationRepository,
            IEmailSender emailSender,
            IClock clock)
        {
            _notificationInfoRepository = notificationInfoRepository;
            _userEmailAddressProvider = userEmailAddressProvider;
            _notificationRepository = notificationRepository;
            _emailSender = emailSender;
            _clock = clock;
        }
        
        [UnitOfWork]
        public virtual async Task ExecuteAsync(EmailNotificationSendingJobArgs args)
        {
            var notification = await _notificationRepository.GetAsync(args.NotificationId);

            var userEmailAddress = await _userEmailAddressProvider.GetAsync(notification.UserId);

            if (userEmailAddress.IsNullOrWhiteSpace())
            {
                await SaveNotificationResultAsync(notification, false, NotificationConsts.FailureReasons.ReceiverInfoNotFound);
                return;
            }
            
            var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

            await _emailSender.SendAsync(userEmailAddress,
                notificationInfo.GetDataValue(NotificationProviderMailingConsts.NotificationInfoSubjectPropertyName).ToString(),
                notificationInfo.GetDataValue(NotificationProviderMailingConsts.NotificationInfoBodyPropertyName).ToString());
            
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