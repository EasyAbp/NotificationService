using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using EasyAbp.NotificationService.Provider.PrivateMessaging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.PrivateMessaging.UserWelcomeNotifications
{
    public class UserWelcomeNotificationFactory :
        NotificationFactory<UserWelcomeNotificationDataModel, CreatePrivateMessageNotificationEto>, ITransientDependency
    {
        public override async Task<CreatePrivateMessageNotificationEto> CreateAsync(
            UserWelcomeNotificationDataModel model, IEnumerable<Guid> userIds)
        {
            var text = $"Hello, here is a gift card code for you: {model.GiftCardCode}";

            return new CreatePrivateMessageNotificationEto(CurrentTenant.Id, userIds, "Gift Card Code", text, true);
        }
    }
}