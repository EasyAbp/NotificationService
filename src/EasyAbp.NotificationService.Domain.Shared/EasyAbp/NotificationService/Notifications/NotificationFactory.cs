using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.NotificationService.Notifications
{
    public abstract class NotificationFactory<TNotificationDataModel, TCreateNotificationEto>
        where TNotificationDataModel : class
        where TCreateNotificationEto : CreateNotificationEto
    {
        public abstract Task<TCreateNotificationEto> CreateAsync(TNotificationDataModel model,
            IEnumerable<Guid> userIds);
    }
}