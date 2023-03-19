using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.NotificationService.Notifications;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.NotificationService.Provider.Sms.UserWelcomeNotifications
{
    public class UserWelcomeNotificationFactory :
        NotificationFactory<UserWelcomeNotificationDataModel, CreateSmsNotificationEto>, ITransientDependency
    {
        public override async Task<CreateSmsNotificationEto> CreateAsync(UserWelcomeNotificationDataModel model,
            IEnumerable<Guid> userIds)
        {
            var text = $"Hello, {model.UserName}, here is a gift card code for you: {model.GiftCardCode}";

            return new CreateSmsNotificationEto(
                CurrentTenant.Id, userIds, text, new Dictionary<string, object>(), JsonSerializer);
        }
    }
}