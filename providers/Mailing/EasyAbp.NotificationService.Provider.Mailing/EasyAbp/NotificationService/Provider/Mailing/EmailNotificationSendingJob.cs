using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Timing;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class EmailNotificationSendingJob : IAsyncBackgroundJob<EmailNotificationSendingJobArgs>, ITransientDependency
    {
        private readonly IExternalUserLookupServiceProvider _userLookupServiceProvider;
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmailSender _emailSender;
        private readonly IClock _clock;

        public EmailNotificationSendingJob(
            IExternalUserLookupServiceProvider userLookupServiceProvider,
            INotificationInfoRepository notificationInfoRepository,
            INotificationRepository notificationRepository,
            IEmailSender emailSender,
            IClock clock)
        {
            _userLookupServiceProvider = userLookupServiceProvider;
            _notificationInfoRepository = notificationInfoRepository;
            _notificationRepository = notificationRepository;
            _emailSender = emailSender;
            _clock = clock;
        }
        
        [UnitOfWork]
        public virtual async Task ExecuteAsync(EmailNotificationSendingJobArgs args)
        {
            var notification = await _notificationRepository.GetAsync(args.NotificationId);
            
            var userData = await _userLookupServiceProvider.FindByIdAsync(notification.UserId);

            if (userData == null)
            {
                await SaveNotificationResultAsync(notification, false, NotificationConsts.FailureReasons.UserNotFound);
                return;
            }
            
            var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

            await _emailSender.SendAsync(userData.Email,
                notificationInfo.GetDataValue(NotificationProviderMailingConsts.NotificationInfoSubjectPropertyName),
                notificationInfo.GetDataValue(NotificationProviderMailingConsts.NotificationInfoBodyPropertyName));
            
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