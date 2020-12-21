using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Mailing.UserWelcomeNotifications
{
    public class UserWelcomeNotificationDataModel
    {
        public string UserName { get; protected set; }
        
        public string GiftCardCode { get; protected set; }

        public UserWelcomeNotificationDataModel(
            [NotNull] string userName,
            [NotNull] string giftCardCode)
        {
            UserName = userName;
            GiftCardCode = giftCardCode;
        }
    }
}