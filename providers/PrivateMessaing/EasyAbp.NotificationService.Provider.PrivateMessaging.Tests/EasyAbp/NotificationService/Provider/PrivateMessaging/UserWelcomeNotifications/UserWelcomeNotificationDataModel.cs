using JetBrains.Annotations;
using System;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging.UserWelcomeNotifications
{
    public class UserWelcomeNotificationDataModel
    {
        public Guid UserId { get; protected set; }
        
        public string GiftCardCode { get; protected set; }

        public UserWelcomeNotificationDataModel(
            [NotNull] Guid userId,
            [NotNull] string giftCardCode)
        {
            UserId = userId;
            GiftCardCode = giftCardCode;
        }
    }
}