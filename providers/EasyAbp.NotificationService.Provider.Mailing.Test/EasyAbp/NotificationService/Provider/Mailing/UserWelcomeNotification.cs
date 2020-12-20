using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using JetBrains.Annotations;

namespace EasyAbp.NotificationService.Provider.Mailing
{
    public class UserWelcomeNotification : NotificationDefinition<CreateEmailNotificationEto>
    {
        public string UserName { get; protected set; }
        
        public string GiftCardCode { get; protected set; }

        public UserWelcomeNotification(
            [NotNull] string userName,
            [NotNull] string giftCardCode)
        {
            UserName = userName;
            GiftCardCode = giftCardCode;
        }

        public override Task<CreateEmailNotificationEto> CreateAsync(IEnumerable<Guid> userIds)
        {
            return Task.FromResult(new CreateEmailNotificationEto(
                userIds,
                $"Welcome, {UserName}",
                $"Here a gift card code for you: {GiftCardCode}"
            ));
        }
    }
}