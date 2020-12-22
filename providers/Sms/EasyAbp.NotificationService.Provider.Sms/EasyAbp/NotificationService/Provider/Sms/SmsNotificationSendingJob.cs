using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.NotificationInfos;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.Sms;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace EasyAbp.NotificationService.Provider.Sms
{
    public class SmsNotificationSendingJob : IAsyncBackgroundJob<SmsNotificationSendingJobArgs>, ITransientDependency
    {
        private readonly INotificationInfoRepository _notificationInfoRepository;
        private readonly IUserPhoneNumberProvider _userPhoneNumberProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ISmsSender _smsSender;
        private readonly IClock _clock;

        public SmsNotificationSendingJob(
            INotificationInfoRepository notificationInfoRepository,
            IUserPhoneNumberProvider userPhoneNumberProvider,
            INotificationRepository notificationRepository,
            IJsonSerializer jsonSerializer,
            ISmsSender smsSender,
            IClock clock)
        {
            _notificationInfoRepository = notificationInfoRepository;
            _userPhoneNumberProvider = userPhoneNumberProvider;
            _notificationRepository = notificationRepository;
            _jsonSerializer = jsonSerializer;
            _smsSender = smsSender;
            _clock = clock;
        }
        
        [UnitOfWork]
        public virtual async Task ExecuteAsync(SmsNotificationSendingJobArgs args)
        {
            var notification = await _notificationRepository.GetAsync(args.NotificationId);

            var userPhoneNumber = await _userPhoneNumberProvider.GetAsync(notification.UserId);

            if (userPhoneNumber.IsNullOrWhiteSpace())
            {
                await SaveNotificationResultAsync(notification, false,
                    NotificationConsts.FailureReasons.ReceiverInfoNotFound);
                return;
            }
            
            var notificationInfo = await _notificationInfoRepository.GetAsync(notification.NotificationInfoId);

            var properties = _jsonSerializer.Deserialize<IDictionary<string, object>>(notificationInfo
                .GetDataValue(NotificationProviderSmsConsts.NotificationInfoPropertiesPropertyName).ToString());

            var smsMessage = new SmsMessage(userPhoneNumber,
                notificationInfo.GetDataValue(NotificationProviderSmsConsts.NotificationInfoTextPropertyName)
                    .ToString());

            foreach (var property in properties)
            {
                smsMessage.Properties.AddIfNotContains(property);
            }
            
            await _smsSender.SendAsync(smsMessage);
            
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